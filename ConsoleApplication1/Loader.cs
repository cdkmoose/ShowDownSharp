using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

using DS.Showdown.DbLibrary;
using DS.Showdown.ObjectLibrary;
using DS.Showdown.Engines;

namespace DS.Showdown.CardLoader
{
    class Loader
    {
        string name;
        Dictionary<string, int> setCodes;

        public Loader()
        {
            DbUtils.SetPath(@"Data Source=C:\Projects\showdownsharp\db\showdown.db");

            LoadSetCodes();
        }

        private void LoadSetCodes()
        {
            DataTable tableCodes;

            tableCodes = DbUtils.GetDataTable(string.Format("select * from card_sets"));

            if (tableCodes.Rows.Count == 0)
            {
                return;
            }
            else
            {
                setCodes = new Dictionary<string, int>();
                foreach (DataRow row in tableCodes.Rows)
                {
                    setCodes[row["set_code"].ToString()] = Int32.Parse(row["set_id"].ToString());
                }
            }

        }
        public int ProcessPUErrors(string fileName)
        {
            name = fileName;
            int lineCount = 0;
            StreamReader data = new StreamReader(fileName);
            string line;

            // read header line
            line = data.ReadLine();
            DbUtils.StartTransaction();

            //while ((line = data.ReadLine()) != null && lineCount < 5)
            while ((line = data.ReadLine()) != null)
            {
                string[] pieces = line.Split(',');

                UpdatePlayer(pieces);
            }
            DbUtils.Commit();
            data.Close();

            return lineCount;
        }

        private void UpdatePlayer(string[] attributes)
        {
            // find card info record to get player id
            int playerId = GetPlayerId(attributes);

            // update results for player_id
            if (playerId != 0)
            {
                string resultString = GetResultString(attributes);
                DbUtils.ExecuteScalar(String.Format("update players set results = '{0}' where player_id = {1};", resultString, playerId));
            }
        }

        private int GetPlayerId(string[] attribs)
        {
            int setId = setCodes[attribs[2]];
            int year = 2000 + Convert.ToInt16(attribs[6].Substring(1));
            int card = Convert.ToInt16(attribs[0]);

            string stmt = String.Format("select player_id from card_info where season_year = {0} and card_set = {1} and card_number = {2};",
                year, setId, card);
            string result = DbUtils.ExecuteScalar(stmt);

            if (String.IsNullOrEmpty(result) == true)
                return 0;
            else
                return Convert.ToInt16(result);
        }

        public int ProcessFile(string fileName)
        {
            name = fileName;
            int lineCount = 0;
            StreamReader data = new StreamReader(fileName);
            string line;

            // read header line
            line = data.ReadLine();
            DbUtils.StartTransaction();

            //while ((line = data.ReadLine()) != null && lineCount < 5)
            while ((line = data.ReadLine()) != null)
            {
                string[] pieces = line.Split(',');

                lineCount++;

                if (lineCount % 100 == 0)
                {
                    Console.WriteLine("{0} players", lineCount);
                }

                CreatePlayer(lineCount, pieces);
            }
            
            DbUtils.Commit();
            data.Close();

            return lineCount;
        }

        private void CreatePlayer(int id, string[] attributes)
        {
            bool isBatter;
            Position pos = GetPosition(attributes[10]);
            DBEngine engine = new DBEngine();

            // identify batter/pitcher
            if (pos == Position.Starter || pos == Position.Reliever || pos == Position.Closer)
            {
                isBatter = false;
            }
            else
            {
                isBatter = true;
            }
           
            //create  Player record
            CreatePlayerRecord(id, isBatter, attributes);

            // create card record
            CreatePlayerCard(id, attributes);

            // batting skills or pitching skills
            if (isBatter)
            {
                FillBatterSkills(id, attributes);
                engine.InitializeBatterStats(id);
            }
            else
            {
                FillPitcherSkills(id, attributes);
                engine.InitializePitcherStats(id);
            }
            // parse results
        }

        private void CreatePlayerCard(int id, string[] attrib)
        {
            string sql;

            // year, set, number, team

            sql = string.Format("insert into card_info values ({0}, '{1}', '{2}', '{3}', '{4}');",
                id, attrib[6], setCodes[attrib[2]], attrib[0], attrib[4]);

            DbUtils.ExecuteNonQuery(sql);

            return;
        }


        private void CreatePlayerRecord(int id, bool batter, string[] attrib)
        {
            string sql;

            sql = string.Format("insert into players values ({0}, '{1}', '{2}', '{3}', {4});",
                id, attrib[3].Replace("'", "''"), GetResultString(attrib), attrib[5], batter ? 1 : 0);

            DbUtils.ExecuteNonQuery(sql);
        }

        private void FillPitcherSkills(int id, string[] attributes)
        {
            Position pos;
            pos = GetPosition(attributes[10]);

            string sql = string.Format("insert into pitcher_skill values ({0}, {1}, {2}, {3}, {4});",
                id, attributes[7], (int) pos, attributes[8], SideToInt(attributes[12][0]));

            DbUtils.ExecuteNonQuery(sql);
        }

        private void FillBatterSkills(int id, string[] attributes)
        {
            Position pos;
            Position pos2;
            int rating1;
            int rating2;
            int battingSide;

            pos = GetPosition(attributes[10]);
            rating1 = GetRating(attributes[10]);
            pos2 = GetPosition(attributes[11]);
            if (pos2 != Position.None)
            {
                rating2 = GetRating(attributes[11]);
            }
            else
            {
                rating2 = 0;
            }

            battingSide = SideToInt(attributes[12][0]);


            string sql = string.Format("insert into batting_skill values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});",
                id, (int)pos, rating1, (int) pos2, rating2, attributes[8],  attributes[7], 
                battingSide);
            
            DbUtils.ExecuteNonQuery(sql);

        }

        private int SideToInt(char c)
        {
            int side = -1;

            switch (c)
            {
                case 'R':
                case 'r':
                    side = 0;
                    break;

                case 'L':
                case 'l':
                    side = 1;
                    break;

                case 'S':
                case 's':
                    side = 2;
                    break;

                default:
                    side = -1;
                    break;
            }

            return side;

        }

        private string GetResultString(string[] attribs)
        {
            string results;
            char[] rawResults = {'0','0','0','0','0','0','0','0','0','0',
                                 '0','0','0','0','0','0','0','0','0','0',
                                 '0','0','0','0','0','0','0','0','0','0'};
            Range range;

            // attribute 14 is first
            //PU, SO,GB,FB,BB,1B,1B+,2B,3B,HR
            range = ParseResultString(attribs[14]); // PU
            SetResultValues(rawResults, range, '1');

            range = ParseResultString(attribs[15]); // SO
            SetResultValues(rawResults, range, '0');

            range = ParseResultString(attribs[16]); // GB
            SetResultValues(rawResults, range, '2');

            range = ParseResultString(attribs[17]); // FB
            SetResultValues(rawResults, range, '3');

            range = ParseResultString(attribs[18]); // BB
            SetResultValues(rawResults, range, '4');

            range = ParseResultString(attribs[19]); // 1B
            SetResultValues(rawResults, range, '5');

            range = ParseResultString(attribs[20]); // 1B+
            SetResultValues(rawResults, range, '6');

            range = ParseResultString(attribs[21]); // 2B
            SetResultValues(rawResults, range, '7');

            range = ParseResultString(attribs[22]); // 3B
            SetResultValues(rawResults, range, '8');

            range = ParseResultString(attribs[23]); // HR
            SetResultValues(rawResults, range, '9');

            // need to extend to 30

            int i = 29;

            while (rawResults[i] == '0' && i >= 0)
            {
                i--;
            }

            for (int j = i + 1; j < 30; j++)
            {
                rawResults[j] = rawResults[i];
            }

            results = new string(rawResults);

            return results;
        }

        private void SetResultValues(char[] s, Range r, char val)
        {
            if (r.low != 0)
            {
                for (int i = r.low - 1; i < r.high; i++)
                {
                    s[i] = val;
                }
            }
        }

        private Range ParseResultString(string rangeString)
        {
            string[] vals = rangeString.Split('-');
            Range result;

            if (rangeString.EndsWith("+") == true)
            {
                result.low = Int32.Parse(rangeString.Substring(0, rangeString.Length - 1));
                result.high = 30;
            }
            else if (vals.Length == 0 || rangeString == "-" || vals[0] == string.Empty)
            {
                result = new Range(0, 0);
            }
            else if (vals.Length == 1 || vals[1] == string.Empty)
            {
                result = new Range();
                result.high = result.low = int.Parse(vals[0]);
            }
            else
            {
                result = new Range(int.Parse(vals[0]), int.Parse(vals[1]));
            }

            return result;
        }

        private Position GetPosition(string posString)
        {
            Position pos = Position.None;

            if (posString == "Closer")
            {
                pos = Position.Closer;
            }
            else if (posString == "Reliever")
            {
                pos = Position.Reliever;
            }
            else if (posString == "Starter")
            {
                pos = Position.Starter;
            }
            else if (posString == "---" || posString == string.Empty)
            {
                pos = Position.None;
            }
            else if (posString == "DH")
            {
                pos = Position.DesignatedHitter;
            }
            else
            {
                int plusPos = posString.IndexOf('+');
                string posSubstring;
                if (plusPos != -1)
                {
                    posSubstring = posString.Substring(0, plusPos);
                }
                else
                {
                    posSubstring = posString;
                }

                switch (posSubstring)
                {
                    case "C":
                        pos = Position.Catcher;
                        break;

                    case "1B":
                        pos = Position.FirstBase;
                        break;

                    case "2B":
                        pos = Position.SecondBase;
                        break;

                    case "SS":
                        pos = Position.Shortstop;
                        break;

                    case "2B-SS":
                        pos = Position.SecondShort;
                        break;

                    case "3B":
                        pos = Position.ThirdBase;
                        break;

                    case "1B-3B":
                        pos = Position.FirstThird;
                        break;

                    case "IF":
                        pos = Position.Infielder;
                        break;

                    case "LF":
                        pos = Position.LeftField;
                        break;

                    case "CF":
                        pos = Position.CenterField;
                        break;

                    case "RF":
                        pos = Position.RightField;
                        break;

                    case "LF-RF":
                        pos = Position.LeftRight;
                        break;

                    case "OF":
                        pos = Position.Outfielder;
                        break;

                }
            }

            return pos;
        }

        private int GetRating(string posString)
        {
            int rating;
            int index = posString.IndexOf('+');

            if ( index == -1)
            {
                rating = 0;
            }
            else
            {
                rating = int.Parse(posString.Substring(index + 1));
            }

            return rating;
        }
    }
}

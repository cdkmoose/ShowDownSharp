using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using DS.Showdown.DbLibrary;
using DS.Showdown.ObjectLibrary;
using DS.Showdown.Engines;

namespace DS.Showdown.ShowdownGameSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class GameService : IGameService
    {
        GameEngine engine;
        DBEngine dbEngine;
        League league;

        public GameService()
        {
            engine = new GameEngine();
            dbEngine = new DBEngine();
            league = dbEngine.LoadLeague3(1);

        }

        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string[] GetStandings()
        {
            string sql = "select team_name, wins, losses from teams join team_record on teams.team_id =team_record.team_id order by wins desc;";
            List<string> res = new List<string>();

            DbUtils.SetPath(@"Data Source=D:\Projects\showdownsharp\db\showdown.db");

            DataTable tbl = DbLibrary.DbUtils.GetDataTable(sql);

            foreach (DataRow team in tbl.Rows)
            {
                string foo = String.Format("{0:-20} {1:3} - {2:3}", team["team_name"], team["wins"], team["losses"]);
                res.Add(foo);
            }

            return res.ToArray();
        }

        public TeamRecord[] GetLeagueStandings()
        {
            string sql = "select team_name, wins, losses from teams join team_record on teams.team_id =team_record.team_id order by wins desc;";
            List<TeamRecord> res = new List<TeamRecord>();

            DbUtils.SetPath(@"Data Source=D:\Projects\showdownsharp\db\showdown.db");

            DataTable tbl = DbLibrary.DbUtils.GetDataTable(sql);

            bool parseStatus;
            int parsedInt = 0;

            foreach (DataRow team in tbl.Rows)
            {
                TeamRecord rec = new TeamRecord();
                rec.TeamName = team["team_name"].ToString();

                parseStatus = Int32.TryParse(team["wins"].ToString(), out parsedInt);
                rec.Wins = parsedInt;
                parseStatus = Int32.TryParse(team["losses"].ToString(), out parsedInt);
                rec.Losses = parsedInt;

                res.Add(rec);
            }

            return res.ToArray();
        }

        public string[] PlayGame(string homeTeam, string awayTeam)
        {
            engine.HomeTeam = league.GetTeamByAbbrev(homeTeam);
            engine.VisitingTeam = league.GetTeamByAbbrev(awayTeam);

            engine.PlayBall();

            while (engine.GameIsComplete == false)
            {
                engine.NextAtBat();
            }

            return engine.BoxScore;

        }
    }
}

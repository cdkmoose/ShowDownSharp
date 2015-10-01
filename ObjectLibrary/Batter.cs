using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using DS.Showdown.DbLibrary;

namespace DS.Showdown.ObjectLibrary
{
	public class Batter : Player
	{
		BatterStats	statSeason;
		BatterStats	statGame;

		private int secondaryRating;
		Position secondaryPosition;
		Position primaryPosition;
		int primaryRating;
		int onBase;
		int speed;
		BattingSide battingSide;

		public Batter()
		{
			statGame = new BatterStats();
			statGame.Reset();

			statSeason = new BatterStats();
			statSeason.Reset();
		}

		public Batter(BattingSide side, int onBase, Position primary, int priRating, Position secondary, 
						 int secRating, int speed, int points, string results, string name) 
						 : base(points, results, name)
		{
			secondaryRating = secRating;
			secondaryPosition = secondary;
			primaryPosition = primary;
			primaryRating = priRating;
			this.onBase = onBase;
			this.speed = speed;
			battingSide = side;

			statGame = new BatterStats();
			statGame.Reset();

			statSeason = new BatterStats();
			statSeason.Reset();
		}

        public Batter(DataRow row, DataRow statRow, DataRow skillRow)
        {
            ID = Int32.Parse(row["player_id"].ToString());

            SetResults(row["results"].ToString());

            LoadCardInfo(ID);

            Name = row["name"].ToString();
            Points = Int32.Parse(row["points"].ToString());

            primaryPosition = (Position)Int32.Parse(skillRow["primary_position"].ToString());
            primaryRating = Int32.Parse(skillRow["primary_rating"].ToString());

            secondaryPosition = (Position)Int32.Parse(skillRow["secondary_position"].ToString());
            secondaryRating = Int32.Parse(skillRow["secondary_rating"].ToString());

            speed = Int32.Parse(skillRow["speed"].ToString());
            onBase = Int32.Parse(skillRow["on_base"].ToString());

            battingSide = (BattingSide)Int32.Parse(skillRow["batting_side"].ToString());

            //get stats
            statGame = new BatterStats();
            statGame.Reset();

            statSeason = new BatterStats();
            statSeason.Reset();

            statSeason.AtBats = short.Parse(statRow["atbats"].ToString());
            statSeason.Hits = short.Parse(statRow["hits"].ToString());
            statSeason.Doubles = short.Parse(statRow["doubles"].ToString());
            statSeason.Triples = short.Parse(statRow["triples"].ToString());
            statSeason.HomeRuns = short.Parse(statRow["homeruns"].ToString());
            statSeason.Walks = short.Parse(statRow["walks"].ToString());
            statSeason.Steals = short.Parse(statRow["steals"].ToString());
            statSeason.SacFlies = short.Parse(statRow["sacflies"].ToString());
            statSeason.SacBunts = short.Parse(statRow["sacbunts"].ToString());
            statSeason.HomeRuns = short.Parse(statRow["homeruns"].ToString());
            statSeason.StrikeOuts = short.Parse(statRow["strikeouts"].ToString());
            statSeason.RunsScored = short.Parse(statRow["runs"].ToString());
            statSeason.RBIs = short.Parse(statRow["rbis"].ToString());
            statSeason.Games = short.Parse(statRow["games"].ToString());

        }

		public Batter(DataRow row)
		{
			DataTable table;
			DataRow statRow;

			ID = Int32.Parse(row["player_id"].ToString());

			SetResults(row["results"].ToString());

            LoadCardInfo(ID);

			Name = row["name"].ToString();
			Points = Int32.Parse(row["points"].ToString());

			// get skills
			table = DbUtils.GetDataTable(string.Format("select * from batting_skill where player_id = {0}", ID));

			if (table.Rows.Count > 0) // just use the first row, should only be one
			{
				statRow = table.Rows[0];
				primaryPosition = (Position)Int32.Parse(statRow["primary_position"].ToString());
				primaryRating = Int32.Parse(statRow["primary_rating"].ToString());

				secondaryPosition = (Position)Int32.Parse(statRow["secondary_position"].ToString());
				secondaryRating = Int32.Parse(statRow["secondary_rating"].ToString());

				speed = Int32.Parse(statRow["speed"].ToString());
				onBase = Int32.Parse(statRow["on_base"].ToString());

				battingSide = (BattingSide)Int32.Parse(statRow["batting_side"].ToString());
			}

			//get stats
			statGame = new BatterStats();
			statGame.Reset(); 

			table = DbUtils.GetDataTable(string.Format("select * from batter_stats where player_id = {0}", ID));
			statSeason = new BatterStats();
			statSeason.Reset();

			if (table.Rows.Count > 0) // just use the first row, should only be one
			{
				statRow = table.Rows[0];

				statSeason.AtBats = short.Parse(statRow["atbats"].ToString());
				statSeason.Hits = short.Parse(statRow["hits"].ToString());
				statSeason.Doubles = short.Parse(statRow["doubles"].ToString());
				statSeason.Triples = short.Parse(statRow["triples"].ToString());
				statSeason.HomeRuns = short.Parse(statRow["homeruns"].ToString());
				statSeason.Walks = short.Parse(statRow["walks"].ToString());
				statSeason.Steals = short.Parse(statRow["steals"].ToString());
				statSeason.SacFlies = short.Parse(statRow["sacflies"].ToString());
				statSeason.SacBunts = short.Parse(statRow["sacbunts"].ToString());
				statSeason.HomeRuns = short.Parse(statRow["homeruns"].ToString());
				statSeason.StrikeOuts = short.Parse(statRow["strikeouts"].ToString());
				statSeason.RunsScored = short.Parse(statRow["runs"].ToString());
				statSeason.RBIs = short.Parse(statRow["rbis"].ToString());
				statSeason.Games = short.Parse(statRow["games"].ToString());
			}

		}

		public int OnBase
		{
			get 
			{
				return onBase;
			}

			set
			{
				onBase = value;
			}
		}

		public string LineUpText
		{
			get
			{
				return Name + " (" + EnumHelpers.PositionToString(primaryPosition) +")";
			}
		}

		public override void UpdateSeasonStats(bool updateDb)
		{
			// update season stats from game results

			statSeason.AtBats += statGame.AtBats;
			statSeason.Hits += statGame.Hits;
			statSeason.Doubles += statGame.Doubles;
			statSeason.Triples += statGame.Triples;
			statSeason.HomeRuns += statGame.HomeRuns;
			statSeason.StrikeOuts += statGame.StrikeOuts;
			statSeason.Steals += statGame.Steals;
			statSeason.SacBunts += statGame.SacBunts;
			statSeason.SacFlies += statGame.SacFlies;
			statSeason.RBIs += statGame.RBIs;
			statSeason.RunsScored += statGame.RunsScored;
			statSeason.Walks += statGame.Walks;
			statSeason.Games++;

			if (updateDb == true)
			{
				SaveSeasonStats();
			}

		}

		public override void SaveSeasonStats()
		{
			StringBuilder s = new StringBuilder();

			s.Append("update batter_stats set ");

			s.AppendFormat("atbats = {0}, ", statSeason.AtBats);
			s.AppendFormat("hits = {0}, ", statSeason.Hits);
			s.AppendFormat("doubles = {0}, ", statSeason.Doubles);
			s.AppendFormat("triples = {0}, ", statSeason.Triples);
			s.AppendFormat("homeruns = {0}, ", statSeason.HomeRuns);
			s.AppendFormat("strikeouts = {0}, ", statSeason.StrikeOuts);
			s.AppendFormat("steals = {0}, ", statSeason.Steals);
			s.AppendFormat("sacbunts = {0}, ", statSeason.SacBunts);
			s.AppendFormat("sacflies = {0}, ", statSeason.SacFlies);
			s.AppendFormat("rbis = {0}, ", statSeason.RBIs);
			s.AppendFormat("runs = {0}, ", statSeason.RunsScored);
			s.AppendFormat("walks = {0}, ", statSeason.Walks);
			s.AppendFormat("games = {0} ", statSeason.Games);

			s.AppendFormat(" where player_id = {0}", ID);

			DbUtils.ExecuteNonQuery(s.ToString());
		}

        private int FirstSafe(int[] res)
        {
            int i = 0;
            while (res[i] < 4 && i < 30)
            {
                i++;
            }

            return i + 1;

        }

        private int MinHR(int[] res)
        {
            int i = 0;

            while (i < 30 && res[i] < 9)
            {
                i++;
            }

            return i < 30 ? i : 0;
        }
        
        public override string KeyStatLine
        {
            get
            {
                return string.Format("OB: {0}, Out: {1}, HR: {2}", OnBase,
                    FirstSafe(results) - 1, MinHR(results));
            }
        }

        public override string  PositionText
        {
	        get 
	        {
                string txt;
                txt = EnumHelpers.PositionToString(primaryPosition);
                if (secondaryPosition != Position.None)
                {
                    txt += ", " + EnumHelpers.PositionToString(secondaryPosition);
                }

                return txt;
	        }
        }

        public override void ResetGameStats()
		{
			Reset();
		}

		public override void ResetSeasonStats()
		{
			// set season stats to 0

			statSeason.Reset();

			SaveSeasonStats();
		}

		public int Speed
		{
			get
			{
				return speed;
			}
			set
			{
				speed = value;
			}
		}

		public Position PrimaryPosition
		{
			get
			{
				return primaryPosition;
			}
		}

		public Position SecondaryPosition
		{
			get
			{
				return secondaryPosition;
			}
		}

		public int PrimaryRating
		{
			get 
			{
				return primaryRating;
			}
		}

		public int SecondaryRating
		{
			get 
			{
				return secondaryRating;
			}
		}

        public List<Position> PlayablePositions
        {
            get
            {
                List<Position> list = new List<Position>();

                if (primaryPosition != Position.None)
                {
                    list.AddRange(EnumHelpers.PositionToList(primaryPosition));
                }

                if (secondaryPosition != Position.None)
                {
                    list.AddRange(EnumHelpers.PositionToList(secondaryPosition));
                }

                return list;
            }

        }

		public int GetFieldingRating(Position pos)
		{
			int result = 0;

			if (pos == primaryPosition)
			{
				result = primaryRating;
			}
			else if (pos == SecondaryPosition)
			{
				result = secondaryRating;
			}
			else
			{
				result = 0;
			}

			return result;
		}

		public void Reset()
		{
			statGame.Reset();

			PlayedThisGame = false;
		}

		public BattingSide BattingSide
		{
			get
			{
				return battingSide;
			}
		}

		public string BoxScoreLine
		{
			get
			{
				string line;

				line = String.Format("{0,-20} {1,4} {2,4} {3,4} {4,4} {5,4} {6,4}\r\n", Name,
					statGame.AtBats, statGame.RunsScored, statGame.Hits,
					statGame.RBIs, statGame.Walks, statGame.StrikeOuts);

				return line;
			}
		}

		public string StatsLine(bool gameOnly)
		{
			string line;
			BatterStats	stats;

			if (gameOnly == true)
			{
				stats = statGame;
			}
			else
			{
				stats = statSeason;
			}

			line = String.Format(
				"{0,-20}  {1,3} {2,3}  {3,3} {4,3} {5,3} {6,3} {7,3} {8,3} {9,3} {10,3} {11,3} {12,3}   {13,4:F3}  {14,4:F3}  {15,4:F3}\r\n",
				Name, stats.Games, stats.AtBats, stats.RunsScored,
				stats.Hits, stats.Doubles, stats.Triples, stats.HomeRuns,
				stats.RBIs, stats.Walks, stats.StrikeOuts,
				stats.SacBunts + stats.SacFlies, stats.Steals,
				stats.BattingAvg, stats.OnBasePct, stats.SluggingPct);

			return line;
		}

		public BatterStats GameStats
		{
			get
			{
				return statGame;
			}
		}

		public BatterStats SeasonStats
		{
			get
			{
				return statSeason;
			}
		}

	}
}

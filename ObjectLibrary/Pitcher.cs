using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using DS.Showdown.DbLibrary;

namespace DS.Showdown.ObjectLibrary
{
	public class Pitcher : Player
	{
		PitcherStats	statSeason;
		PitcherStats	statGame;

		private PitcherType pitcherRole;
		private int	inningsPitched;
		private ThrowingSide pitchingArm;
		private int	control;

		public Pitcher()
		{
			statGame = new PitcherStats();
			statGame.Reset();

			statSeason = new PitcherStats();
			statSeason.Reset();

		}

		public int Control
		{
			get
			{
				return control;
			}
		}

		public Pitcher(int Control, int IP, ThrowingSide Arm, int Points, PitcherType Position, 
						   string Results, string Name)
						   : base(Points, Results, Name)
		{
			control = Control;
			inningsPitched = IP;
			pitchingArm = Arm;
			pitcherRole = Position;

			statGame = new PitcherStats();
			statGame.Reset();

			statSeason = new PitcherStats();
			statSeason.Reset();
		}

        public Pitcher(DataRow row, DataRow statRow, DataRow skillRow)
        {
            ID = Int32.Parse(row["player_id"].ToString());

            LoadCardInfo(ID);

            SetResults(row["results"].ToString());

            Name = row["name"].ToString();
            Points = Int32.Parse(row["points"].ToString());

            // get skills
            control = Int32.Parse(skillRow["control"].ToString());
            pitcherRole = (PitcherType)Int32.Parse(skillRow["role"].ToString());
            inningsPitched = Int32.Parse(skillRow["innings"].ToString());
            pitchingArm = (ThrowingSide)Int32.Parse(skillRow["throwing_side"].ToString());

            // get stats
            statGame = new PitcherStats();
            statGame.Reset();

            statSeason = new PitcherStats();
            statSeason.Reset();

            statSeason.Runs = short.Parse(statRow["runs"].ToString());
            statSeason.Walks = short.Parse(statRow["walks"].ToString());
            statSeason.StrikeOuts = short.Parse(statRow["strikeouts"].ToString());
            statSeason.EarnedRuns = short.Parse(statRow["earned_runs"].ToString());
            statSeason.Outs = short.Parse(statRow["outs"].ToString());
            statSeason.Games = short.Parse(statRow["games"].ToString());
            statSeason.Wins = short.Parse(statRow["wins"].ToString());
            statSeason.Losses = short.Parse(statRow["losses"].ToString());
            statSeason.Saves = short.Parse(statRow["saves"].ToString());
        }

		public Pitcher(DataRow row)
		{
			DataTable table;
			DataRow statRow;
			DataRow skillRow;

			ID = Int32.Parse(row["player_id"].ToString());

            LoadCardInfo(ID);

            SetResults(row["results"].ToString());

			Name = row["name"].ToString();
			Points = Int32.Parse(row["points"].ToString());

			// get skills
			table = DbUtils.GetDataTable(string.Format("select * from pitcher_skill where player_id = {0}", ID));

			if (table.Rows.Count > 0) // just use the first row, should only be one
			{
				skillRow = table.Rows[0];

				control = Int32.Parse(skillRow["control"].ToString());
				pitcherRole = (PitcherType)Int32.Parse(skillRow["role"].ToString());
				inningsPitched = Int32.Parse(skillRow["innings"].ToString());
				pitchingArm = (ThrowingSide)Int32.Parse(skillRow["throwing_side"].ToString());
			}

			// get stats
			//get stats
			statGame = new PitcherStats();
			statGame.Reset();

			table = DbUtils.GetDataTable(string.Format("select * from pitcher_stats where player_id = {0}", ID));
			statSeason = new PitcherStats();
			statSeason.Reset();

			if (table.Rows.Count > 0) // just use the first row, should only be one
			{
				statRow = table.Rows[0];

				statSeason.Runs = short.Parse(statRow["runs"].ToString());
				statSeason.Walks = short.Parse(statRow["walks"].ToString());
				statSeason.StrikeOuts = short.Parse(statRow["strikeouts"].ToString());
				statSeason.EarnedRuns = short.Parse(statRow["earned_runs"].ToString());
				statSeason.Outs = short.Parse(statRow["outs"].ToString());
				statSeason.Games = short.Parse(statRow["games"].ToString());
				statSeason.Wins = short.Parse(statRow["wins"].ToString());
				statSeason.Losses = short.Parse(statRow["losses"].ToString());
				statSeason.Saves = short.Parse(statRow["saves"].ToString());
			}
		}

		public override void UpdateSeasonStats(bool updateDb)
		{
			// update season stats with game results

			statSeason.Outs += statGame.Outs;
			statSeason.StrikeOuts += statGame.StrikeOuts;
			statSeason.Walks += statGame.Walks;
			statSeason.Wins += statGame.Wins;
			statSeason.Losses += statGame.Losses;
			statSeason.Saves += statGame.Saves;
			statSeason.EarnedRuns += statGame.EarnedRuns;
			statSeason.Runs += statGame.Runs;
			statSeason.Games++;
            statSeason.Hits += statGame.Hits;

			if (updateDb == true)
			{
				SaveSeasonStats();
			}
		}

        public override string KeyStatLine
        {
            get
            {
                return string.Format("Cont: {0}, IP: {1}, Out: {2}", Control,
                   IP, MaxOut(results));
            }

        }

        public override string PositionText
        {
            get
            {
                return EnumHelpers.PitcherTypeToString(pitcherRole);
            }
        }

        private int MaxOut(int[] res)
        {
            int i = 0;

            while (res[i] < 4 && i < 30)
            {
                i++;
            }

            return i;
        }

        public string StatsLine(bool gameOnly)
        {
            string line;
            PitcherStats stats;

            if (gameOnly == true)
            {
                stats = statGame;
            }
            else
            {
                stats = statSeason;
            }

            line = String.Format(
                "{0,-20}  {1,3} {2,3} {3,3} {4,3} {5,3}  {6,3}  {7,3}   {8,4:F2}\r\n",
                Name, stats.Games, stats.Wins, stats.Losses, stats.Saves, stats.StrikeOuts, stats.Walks, 
                stats.EarnedRuns, stats.EarnedRunAvg);

            return line;
        }

        public override void SaveSeasonStats()
		{
			StringBuilder s = new StringBuilder();

			s.Append("update pitcher_stats set ");

			s.AppendFormat("games = {0}, ", statSeason.Games);
			s.AppendFormat("outs = {0}, ", statSeason.Outs);
			s.AppendFormat("earned_runs = {0}, ", statSeason.EarnedRuns);
			s.AppendFormat("runs = {0}, ", statSeason.Runs);
			s.AppendFormat("strikeouts = {0}, ", statSeason.StrikeOuts);
			s.AppendFormat("walks = {0}, ", statSeason.Walks);
			s.AppendFormat("wins = {0}, ", statSeason.Wins);
			s.AppendFormat("losses = {0}, ", statSeason.Losses);
			s.AppendFormat("saves = {0} ", statSeason.Saves);

			s.AppendFormat(" where player_id = {0}", ID);

			DbUtils.ExecuteNonQuery(s.ToString());
		}

		public int AdjustedControl
		{
			get
			{
				int	ipPenalty;
				int runsPenalty;

				// get control value after adjusted for IP and runs allowed control value
				// adjusted control = control - (actaul IP - rated IP) - runs/3

				runsPenalty = statGame.Runs / 3;

				ipPenalty = statGame.Outs / 3 - inningsPitched + 1;

				ipPenalty = (ipPenalty < 0) ? 0 : ipPenalty;

				return control - ipPenalty - runsPenalty;
			}
			
		}

		public void Reset()
		{
			statGame.Reset();

			PlayedThisGame = false;
		}

		public override void ResetGameStats()
		{
			// set game stats to 0

			Reset();

		}

		public override void ResetSeasonStats()
		{
			// set season stats to 0

			statSeason.Reset();

			SaveSeasonStats();
		}

		public int IP
		{
			get
			{
				return inningsPitched;
			}

		}

		public PitcherType PitcherRole
		{
			get
			{
				return pitcherRole;
			}
		}

		public ThrowingSide PitchingArm
		{
			get
			{
				return pitchingArm;
			}
		}

		public PitcherStats GameStats
		{
			get
			{
				return statGame;
			}
		}

		public PitcherStats SeasonStats
		{
			get
			{
				return statSeason;
			}
		}
	}
}

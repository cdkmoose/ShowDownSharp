using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using DS.Showdown.DbLibrary;

namespace DS.Showdown.ObjectLibrary
{
	public enum TeamLocation
	{
		Home,
		Visitor,

	}
	public class Team
	{
		string abbrev;
		int	gamesPlayed;
		int	wins;
		int	losses;
		List<Batter> batterList;
		List<Pitcher> pitcherList;
        List<Pitcher> rotation;
        List<Pitcher> bullpen;
		string name;
		int id;
        bool lineupModified;
        bool battersModified;
        bool pitchersModified;
        bool gameMode;
		
		BattingOrderSlot[]	battingOrder;
		Pitcher	currentPitcher;

		private void Initialize()
		{
			battingOrder = new BattingOrderSlot[9];
			batterList = new List<Batter>();
            pitcherList = new List<Pitcher>();
            bullpen = new List<Pitcher>();
            rotation = new List<Pitcher>();

			name = "";
			gamesPlayed = 0;
			wins = 0;
			losses = 0;

            lineupModified = false;
            pitchersModified = false;
            battersModified = false;
            gameMode = false;
		}

		public Team()
		{
			Initialize();

		}

        public Team(string Name)
        {
            Initialize();

            name = Name;
        }

        public Team(string teamName, int leagueID, string teamAbbr)
        {
            Initialize();

            name = teamName;
            abbrev = teamAbbr;

            InitializeDB();
            
        }

        public Team(DataRow row)
		{
			DataTable table;
			Initialize();
			Batter btr;

			name = row["team_name"].ToString();
			abbrev = row["abbrev"].ToString();
			id = Int32.Parse(row["team_id"].ToString());

			table = DbUtils.GetDataTable(string.Format("select * from team_record where team_id = {0}", id));

			if (table.Rows.Count == 0)
			{
				gamesPlayed = wins = losses = 0;
			}
			else
			{
				wins = Int32.Parse(table.Rows[0]["wins"].ToString());
				losses = Int32.Parse(table.Rows[0]["losses"].ToString());
				gamesPlayed = wins + losses;
			}

			table = DbUtils.GetDataTable(string.Format("select * from team_players where team_id = {0}", id));

			if (table.Rows.Count != 0)
			{
				DataTable tablePlayer;
				foreach (DataRow playerRow in table.Rows)
				{

					tablePlayer = DbUtils.GetDataTable(string.Format("select * from players where player_id = {0}",
						Int32.Parse(playerRow["player_id"].ToString())));

					if (tablePlayer.Rows.Count == 0)
					{
						continue;
					}
					else
					{
						bool isBatter = Boolean.Parse(tablePlayer.Rows[0]["batter"].ToString());

                        if (isBatter)
                        {
                            Batter batter = new Batter(tablePlayer.Rows[0]);
                            batterList.Add(batter);
                        }
                        else
                        {
                            Pitcher pitcher = new Pitcher(tablePlayer.Rows[0]);

                            pitcherList.Add(pitcher);
                            if (pitcher.PitcherRole == PitcherType.Starter)
                            {
                                StartingPitchers.Add(pitcher);
                            }
                            else
                            {
                                bullpen.Add(pitcher);
                            }
                        }
					}
				}
			}

			// now build lineup
			table = DbUtils.GetDataTable(string.Format("select * from team_lineup where team_id = {0} order by lineup_order", id));
			foreach (DataRow lineupRow in table.Rows)
			{
				btr = GetBatterByID(Int32.Parse(lineupRow["player_id"].ToString()));
				if (btr != null)
				{
					int lineupSlot = Int32.Parse(lineupRow["lineup_order"].ToString()) - 1;
					battingOrder[lineupSlot] = new BattingOrderSlot();
					battingOrder[lineupSlot].Hitter = btr;
					battingOrder[lineupSlot].FieldingPosition = (Position) Int32.Parse(lineupRow["position"].ToString());
				}
			}
		}

        public Team(DataRow row, League lge)
        {
            DataTable table;
            Initialize();

            name = row["team_name"].ToString();
            abbrev = row["abbrev"].ToString();
            id = Int32.Parse(row["team_id"].ToString());

            table = DbUtils.GetDataTable(string.Format("select * from team_record where team_id = {0}", id));

            if (table.Rows.Count == 0)
            {
                gamesPlayed = wins = losses = 0;
            }
            else
            {
                wins = Int32.Parse(table.Rows[0]["wins"].ToString());
                losses = Int32.Parse(table.Rows[0]["losses"].ToString());
                gamesPlayed = wins + losses;
            }

            table = DbUtils.GetDataTable(string.Format("select * from team_players where team_id = {0}", id));

            if (table.Rows.Count != 0)
            {
                foreach (DataRow playerRow in table.Rows)
                {
                    Player player;
                    player = lge.GetPlayer(Int32.Parse(playerRow["player_id"].ToString()));
                    if (player == (Player)null)
                    {
                        continue;
                    }
                    else
                    {
                        player.IsOnATeam = true;
                        if (player is Batter)
                        {
                            batterList.Add((Batter)player);
                        }
                        else
                        {
                            Pitcher pitcher = (Pitcher) player;

                            pitcherList.Add(pitcher);
                            if (pitcher.PitcherRole == PitcherType.Starter)
                            {
                                StartingPitchers.Add(pitcher);
                            }
                            else
                            {
                                bullpen.Add(pitcher);
                            }
                        }
                    }
                }
            }

            // now build lineup
            Batter btr;
            table = DbUtils.GetDataTable(string.Format("select * from team_lineup where team_id = {0} order by lineup_order", id));
            foreach (DataRow lineupRow in table.Rows)
            {
                btr = GetBatterByID(Int32.Parse(lineupRow["player_id"].ToString()));
                if (btr != null)
                {
                    int lineupSlot = Int32.Parse(lineupRow["lineup_order"].ToString()) - 1;
                    battingOrder[lineupSlot] = new BattingOrderSlot();
                    battingOrder[lineupSlot].Hitter = btr;
                    battingOrder[lineupSlot].FieldingPosition = (Position)Int32.Parse(lineupRow["position"].ToString());
                }
            }
        }

        public void InitializeDB()
        {
            string sql;

            id = Convert.ToInt32(DbUtils.ExecuteScalar("select max(team_id) + 1 from teams"));
            sql = string.Format("insert into teams values ({0}, '{1}', 1, '{2}');", id, name, abbrev);
            DbUtils.ExecuteNonQuery(sql);

            sql = string.Format("insert into team_record values ({0}, 0, 0);", id);
            DbUtils.ExecuteNonQuery(sql);

        }

        public void RemoveFromDB()
        {
            string sql;

            sql = string.Format("delete from team_record where team_id = {0};", id);
            DbUtils.ExecuteNonQuery(sql);

            sql = string.Format("delete from team_players where team_id = {0};", id);
            DbUtils.ExecuteNonQuery(sql);

            sql = string.Format("delete from team_lineup where team_id = {0};", id);
            DbUtils.ExecuteNonQuery(sql);

            sql = string.Format("delete from teams where team_id = {0};", id);
            DbUtils.ExecuteNonQuery(sql);


        }

        public BattingOrderSlot[] BattingOrder
        {
            get
            {
                return battingOrder;
            }
        }
        public override string ToString()
        {
            return Name;
        }

        public bool IsModified
        {
            get
            {
                return lineupModified || battersModified || pitchersModified;
            }

        }

        public bool GameMode
        {
            get
            {
                return gameMode;
            }

            set
            {
                gameMode = value;
            }

        }

        public double WinningPercentage
        {
            get
            {
                if (gamesPlayed == 0)
                {
                    return 0.0F;
                }
                else
                {
                    return (double)wins / (double)gamesPlayed;
                }
            }
        }

        public bool LineupIsComplete
        {
            get
            {
                foreach (BattingOrderSlot slot in battingOrder)
                {
                    if (slot == null)
                    {
                        return false;
                    }
                }
                    
                return true;
            }

        }

        private Batter GetBatterByID(int ID)
		{
			foreach (Batter btr in batterList)
			{
				if (btr.ID == ID)
				{
					return btr;
				}
			}

			return null;
		}

		public int GamesPlayed
		{
			get 
			{
				return gamesPlayed;
			}
			set
			{
				gamesPlayed = value;
			}
		}

		public int Wins
		{
			get
			{
				return wins;
			}
			set
			{
				wins = value;
			}
		}

		public int Losses
		{
			get
			{
				return losses;
			}
			set
			{
				losses = value;
			}
		}
		
		public void AddPitcher(Pitcher pitcher)
		{
			pitcherList.Add(pitcher);
            pitchersModified = true;
            if (pitcher.PitcherRole == PitcherType.Starter)
            {
                rotation.Add(pitcher);
            }
            else
            {
                bullpen.Add(pitcher);
            }
		}

        public void RemovePitcher(Pitcher pitcher)
        {
            pitcherList.Remove(pitcher);

            if (pitcher.PitcherRole == PitcherType.Starter)
            {
                rotation.Remove(pitcher);
            }
            else
            {
                bullpen.Remove(pitcher);
            }
            pitchersModified = true;
        }

		public int PitcherCount
		{
			get 
			{
				return pitcherList.Count;
			}
		}

		public Pitcher CurrentPitcher
		{
			get
			{
				return currentPitcher;
			}
			set
			{
				currentPitcher = value;
				currentPitcher.PlayedThisGame = true;
			}
		}

		public void AddBatter(Batter batter)
		{
			batterList.Add(batter);
            battersModified = true;
		}

        public void RemoveBatter(Batter batter)
        {
            batterList.Remove(batter);
            RemoveFromLineup(batter);
            battersModified = true;
        }

        public void RemoveFromLineup(Batter btr)
        {
            for (int i = 0; i < 9; i++)
            {
                if (battingOrder[i].Hitter == btr)
                {
                    battingOrder[i] = null;
                    lineupModified = true;
                }
            }

        }

        public int BatterCount
		{
			get 
			{
				return batterList.Count;
			}
		}

		public void SetLineUp()
		{
			int i = 0;
			foreach(Batter btr in batterList)
			{
				battingOrder[i] = new BattingOrderSlot();
				battingOrder[i].Hitter = btr;
				battingOrder[i].FieldingPosition = btr.PrimaryPosition;
                i++;
			}
		}
		
		public void SetPitcher()
		{
		    int index = gamesPlayed % (rotation.Count);
		    
			currentPitcher = rotation.ToArray()[index];
			currentPitcher.ResetGameStats();
			currentPitcher.PlayedThisGame = true;
		}

		public Batter GetBatterByLineup(int nBatter)
		{
			return battingOrder[nBatter].Hitter;
		}

		public BattingOrderSlot GetLineupSlot(int nBatter)
		{
			return battingOrder[nBatter];
		}

        public void SetLineupSlot(BattingOrderSlot slot, int idx)
        {
            battingOrder[idx] = slot;
            lineupModified = true;
        }

        public void SaveTeam()
        {
            string sql;

            if (IsModified == false)
            {
                // nothing to save, shouldn't be here
                return;
            }

            if (battersModified == true)
            {
                
                // save batter list
                sql = String.Format("delete from team_players where team_id = {0} and player_id in (select player_id from players where player_id = team_players.player_id and batter = 1)", id);
                DbUtils.ExecuteNonQuery(sql);
                
                foreach (Batter batter in Hitters)
                {
                    sql = string.Format("insert into team_players values ({0}, {1});",
                        id, batter.ID);
                    DbUtils.ExecuteNonQuery(sql);
                }

            }

            if (pitchersModified == true)
            {
                // save pitchers list

                sql = String.Format("delete from team_players where team_id = {0} and player_id in (select player_id from players where player_id = team_players.player_id and batter = 0)", id);
                DbUtils.ExecuteNonQuery(sql);
                
                foreach (Pitcher pitcher in StartingPitchers)
                {
                    sql = string.Format("insert into team_players values ({0}, {1});",
                        id, pitcher.ID);
                    DbUtils.ExecuteNonQuery(sql);
                }

                foreach (Pitcher pitcher in BullPen)
                {
                    sql = string.Format("insert into team_players values ({0}, {1});",
                        id, pitcher.ID);
                    DbUtils.ExecuteNonQuery(sql);
                }

            }

            if (lineupModified == true)
            {

                // save lineup
                sql = String.Format("delete from team_lineup where team_id = {0}", id);
                DbUtils.ExecuteNonQuery(sql);
                
                int i = 1;
                foreach (BattingOrderSlot slot in battingOrder)
                {
                    if (slot == null)
                    {
                        continue;
                    }

                    sql = string.Format("insert into team_lineup values ({0}, {1}, {2}, {3});",
                        id, slot.Hitter.ID, i, (int) slot.FieldingPosition);
                    DbUtils.ExecuteNonQuery(sql);
                    i++;
                }
            }
        }



		public void UpdateSeasonStats()
		{
			// update all batter statistics

			foreach (Batter btr in batterList)
			{
				if (btr.PlayedThisGame)
				{
					btr.UpdateSeasonStats(true);
				}
			}

			// update all pitcher statistics

			foreach (Pitcher ptr in pitcherList)
			{
				if (ptr.PlayedThisGame)
				{
					ptr.UpdateSeasonStats(true);
				}
			}

			SaveSeasonStats();
		}

		private void SaveSeasonStats()
		{
			StringBuilder s = new StringBuilder();

			s.Append("update team_record set ");

			s.AppendFormat("wins = {0}, ", wins);
			s.AppendFormat("losses = {0}", losses);

			s.AppendFormat(" where team_id = {0}", id);

			DbUtils.ExecuteNonQuery(s.ToString());
		}


		public void ResetGameStats()
		{
			// reset all batter statistics

			foreach (Batter btr in batterList)
			{
				btr.ResetGameStats();
			}

			// reset all pitcher statistics

			foreach (Pitcher ptr in pitcherList)
			{
				ptr.ResetGameStats();
			}
		}

		public void ResetSeasonStats()
		{
			// reset all batter statistics

			foreach (Batter btr in batterList)
			{
				btr.ResetSeasonStats();
			}

			// reset all pitcher statistics

			foreach (Pitcher ptr in pitcherList)
			{
				ptr.ResetSeasonStats();
			}

			gamesPlayed = wins = losses = 0;

			SaveSeasonStats();
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string Abbrev
		{
			get
			{
				return abbrev;
			}

			set
			{
				abbrev = value;
			}
		}

		public Player GetPlayerAtPosition(Position pos)
		{
			if (pos == Position.Starter)
				return currentPitcher;

			for (int i = 0; i < 9; i++)
			{
				if (battingOrder[i].FieldingPosition == pos)
					return (Player) battingOrder[i].Hitter;
			}

			return null;
		}

		public string GenerateBoxScore()
		{
			int		i;
			StringBuilder s = new StringBuilder();

			s.AppendFormat("{0,-20}   AB    R    H   RBI  BB   SO \r\n", name);

			s.Append("==============================================================\r\n");

			for (i = 0; i < 9; i++)
				s.Append(battingOrder[i].Hitter.BoxScoreLine);

			return s.ToString();
			
		}

        public int InfieldRating
        {
            get
            {
                int rating = 0;
                int i;

                for (i = 0; i < 9; i++)
                {
                    if (IsInfieldPosition(battingOrder[i].FieldingPosition))
                    {
                         rating += battingOrder[i].Hitter.GetFieldingRating(battingOrder[i].FieldingPosition);
                    }
                }

                return rating;
            }

        }

        private bool IsInfieldPosition(Position pos)
        {
	        bool	result = false;

            if (pos == Position.FirstBase || pos == Position.SecondBase || pos == Position.ThirdBase ||
                pos == Position.Shortstop)
	        {
		        result = true;
	        }

	        return result;
        }
 

		public string GenerateStatsReport(bool bGameOnly)
		{
			int		i; 
			StringBuilder s = new StringBuilder();
            s.AppendFormat("{0,-20}\r\n\r\n", Name);

            // hitter stats
			s.AppendFormat("{0,-20}    G   AB   R   H  2B  3B  HR RBI  BB  SO  Sac Stl   Avg    OBP    Slg\r\n", "Hitting");
			s.Append("============================================================================================\r\n");

            if (bGameOnly == true)
            {
                for (i = 0; i < 9; i++)
                {
                    s.Append(battingOrder[i].Hitter.StatsLine(bGameOnly));
                }
            }
            else
            {
                foreach (Batter b in batterList)
                {
                    s.Append(b.StatsLine(bGameOnly));
                }
            }

            s.Append("\r\n\r\n");

            // pitcher stats
			s.AppendFormat("{0,-20}    G   W   L  Sv   K   BB   ER    ERA\r\n", "Pitching");
			s.Append("================================================================\r\n");

            foreach (Pitcher p in pitcherList)
            {
                if (p.PlayedThisGame == true || bGameOnly == false)
                {
                    s.Append(p.StatsLine(bGameOnly));
                }
            }
            
            return s.ToString();
			
		}

		public List<Batter> Hitters
		{
			get
			{
				return batterList;
			}
		}

		public List<Pitcher> StartingPitchers
		{
			get
			{
                return rotation;
            }
		}

		public List<Pitcher> BullPen
		{
			get
			{
				return bullpen;
			}
		}

	}
}

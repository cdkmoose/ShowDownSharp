using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using DS.Showdown.ObjectLibrary;
using DS.Showdown.DbLibrary;

namespace DS.Showdown.Engines
{
	public class DBEngine
	{
		public DBEngine()
		{
		}

		public League LoadLeague(int leagueID)
		{
			League league = new League();

			foreach (Team team in GetTeams(leagueID))
			{
				league.AddTeam(team);
			}

			return league;
		}

        public League LoadLeague2(int leagueID)
        {
            DbUtils.SetPath(@"Data Source=D:\github-repos\ShowDownSharp\db\showdown.db");
            League league = new League();
            AddAllPlayers(league);
            foreach (Team team in GetTeams2(leagueID, league))
            {
                league.AddTeam(team);
            }

            return league;
        }

        public League LoadLeague3(int leagueID)
        {
            DbUtils.SetPath(@"Data Source=D:\github-repos\ShowDownSharp\db\showdown.db");
            League league = new League();

            AddAllPlayers2(league);
            
            foreach (Team team in GetTeams2(leagueID, league))
            {
                league.AddTeam(team);
            }

            return league;
        }

        private Dictionary<long, DataRow> LoadDictionary(string sql, string idColumn)
        {
            Dictionary<long, DataRow> dict = new Dictionary<long, DataRow>();
            long id;

            DataTable table = DbLibrary.DbUtils.GetDataTable(sql);
            foreach (DataRow row in table.Rows)
            {
                id = long.Parse(row[idColumn].ToString());
                dict[id] = row;
            }

            return dict;
        }

        private void AddAllPlayers2(League lge)
        {
            Dictionary<long, DataRow> batterStats = LoadDictionary("select * from batter_stats", "player_id");
            Dictionary<long, DataRow> batterSkill = LoadDictionary("select * from batting_skill", "player_id");
            Dictionary<long, DataRow> pitcherStats = LoadDictionary("select * from pitcher_stats", "player_id");
            Dictionary<long, DataRow> pitcherSkill = LoadDictionary("select * from pitcher_skill", "player_id");
            long id;
            DataTable tablePlayer;

            tablePlayer = DbUtils.GetDataTable(string.Format("select * from players"));

            if (tablePlayer.Rows.Count == 0)
            {
                return;
            }
            else
            {
                foreach (DataRow row in tablePlayer.Rows)
                {
                    id = long.Parse(row["player_id"].ToString());

                    bool isBatter = Boolean.Parse(row["batter"].ToString());

                    if (isBatter)
                    {
                        Batter batter = new Batter(row, batterStats[id], batterSkill[id]);
                        lge.AddPlayer(batter);
                    }
                    else
                    {
                        Pitcher pitcher = new Pitcher(row, pitcherStats[id], pitcherSkill[id]);
                        lge.AddPlayer(pitcher);
                    }
                }
            }

            return;
        }

        private void AddAllPlayers(League lge)
        {
            DataTable tablePlayer;

            tablePlayer = DbUtils.GetDataTable(string.Format("select * from players"));

            if (tablePlayer.Rows.Count == 0)
            {
                return;
            }
            else
            {
                foreach (DataRow row in tablePlayer.Rows)
                {
                    bool isBatter = Boolean.Parse(row["batter"].ToString());

                    if (isBatter)
                    {
                        Batter batter = new Batter(row);
                        lge.AddPlayer(batter);
                    }
                    else
                    {
                        Pitcher pitcher = new Pitcher(row);
                        lge.AddPlayer(pitcher);
                    }
                }
            }

            return;
        }

		private List<Player> GetRoster(int teamID)
		{
			return new List<Player>();
		}

		private List<int> GetLineup(int teamID)
		{
			return new List<int>();
		}

        private List<Team> GetTeams(int leagueId)
        {
            DataTable table;
            List<Team> teamList = new List<Team>();

            table = DbUtils.GetDataTable(string.Format("select * from teams where league_id = {0}", leagueId));
            foreach (DataRow row in table.Rows)
            {
                Team team = new Team(row);
                teamList.Add(team);
            }

            return teamList;
        }

        private List<Team> GetTeams2(int leagueId, League lge)
        {
            DataTable table;
            List<Team> teamList = new List<Team>();

            table = DbUtils.GetDataTable(string.Format("select * from teams where league_id = {0}", leagueId));
            foreach (DataRow row in table.Rows)
            {
                Team team = new Team(row, lge);
                teamList.Add(team);
            }

            return teamList;
        }

        public void InitializeBatterStats(int id)
        {
            string sql = String.Format("insert into batter_stats values ({0},0,0,0,0,0,0,0,0,0,0,0,0, 0);",
                id);

            DbUtils.ExecuteNonQuery(sql);

        }

        public void InitializePitcherStats(int id)
        {
            string sql = String.Format("insert into pitcher_stats values ({0}, 0, 0.0, 0,0,0,0,0,0, 0);",
                id);

            DbUtils.ExecuteNonQuery(sql);
        }

    }
}

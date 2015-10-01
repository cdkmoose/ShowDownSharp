using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
	public class League
	{
		List<Team> teams;
		string name;
        Dictionary<int, Player> playerList;

		public League()
		{
			teams = new List<Team>();
			name = "New League";
            playerList = new Dictionary<int, Player>();
		}

		public League(string leagueName)
		{
			teams = new List<Team>();
			name = leagueName;
            playerList = new Dictionary<int, Player>();
		}

        public Dictionary<int, Player> PlayerList
        {
            get
            {
                return playerList;
            }
        }

		public string Name
		{
			get
			{
				return name;
			}

		}

		public List<Team> Teams
		{
			get
			{
				return teams;
			}
		}

		public int AddTeam(Team team)
		{
			teams.Add(team);

			return teams.Count;
		}

        public Player GetPlayer(int id)
        {
            if (playerList.ContainsKey(id) == false)
            {
                return (Player)null;
            }
            else
            {
                return playerList[id];
            }
        }

        public int AddPlayer(Player ply)
        {
            if (playerList.ContainsKey(ply.ID) == true)
            {
                return -1;
            }
            else
            {
                playerList.Add(ply.ID, ply);
                return ply.ID;
            }
        }

        public Team GetTeamByAbbrev(string abbrev)
        {
            foreach (Team team in Teams)
            {
                if (team.Abbrev == abbrev)
                {
                    return team;
                }
            }

            return null;
        }

        public string SeasonStats
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Season Statistics\r\n\r\n");
                foreach (Team team in Teams)
                {
                    sb.Append(team.GenerateStatsReport(false));

                    sb.Append("\r\n");
                }

                return sb.ToString();

            }

        }


        public Team GetTeamByName(string name)
        {
            foreach (Team team in Teams)
            {
                if (team.Name == name)
                {
                    return team;
                }
            }

            return null;
        }

        public string 
            GetStandingsText()
        {
            List<Team> standings = new List<Team>();
            StringBuilder sb = new StringBuilder();

            foreach (Team team in teams)
            {
                if (standings.Count == 0)
                {
                    standings.Add(team);
                }
                else
                {
                    bool teamAdded = false;
                    foreach (Team standingTeam in standings)
                    {
                        if (team.WinningPercentage > standingTeam.WinningPercentage)
                        {
                            standings.Insert(standings.IndexOf(standingTeam), team);
                            teamAdded = true;
                            
                            break;
                        }
                    }
                    if (teamAdded == false)
                    {
                        standings.Add(team);
                    }
                }
            }

            sb.Append("Team                    W    L    Pct\r\n");
            sb.Append("=======================================\r\n");

            foreach (Team team in standings)
            {
                sb.AppendFormat("{0,-20}  {1,3}  {2,3}   {3,4:F3}\r\n", team.Name, team.Wins, team.Losses,
                    team.WinningPercentage);
            }

            return sb.ToString();
        }
	}
}

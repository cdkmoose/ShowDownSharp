using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
    public class TopTenItem<T>
    {
        string playerName;
        string teamName;
        T itemScore;

        public TopTenItem(string player, string team, T score)
        {
            playerName = player;
            teamName = team;
            itemScore = score;
        }

        public string PlayerName
        {
            get
            {
                return playerName;
            }
        }

        public string TeamName
        {
            get
            {
                return teamName;
            }
        }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,-15}  {2}", playerName, teamName, itemScore);
        }

        public T Score
        {
            get
            {
                return itemScore;
            }
        }

    }
}

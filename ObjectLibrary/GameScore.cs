using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
    public class GameScore
    {
        public int HomeScore
        {
            get;
            set;
        }

        public int VisitingScore
        {
            get;
            set;
        }

        public GameScore()
        {
            Reset();
        }

        public GameWinner WinningTeam
        {
            get
            {
                if (VisitingScore > HomeScore)
                {
                    return GameWinner.VisitingTeam;
                }
                else if (HomeScore > VisitingScore)
                {
                    return GameWinner.HomeTeam;
                }
                else
                {
                    return GameWinner.TieScore;
                }
            }
        }

        public void Reset()
        {
            HomeScore = 0;
            VisitingScore = 0;
        }
    }
}

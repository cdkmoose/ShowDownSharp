using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using DS.Showdown.DbLibrary;

namespace DS.Showdown.ObjectLibrary
{
	public class Player
	{
		private const int NUM_RESULTS = 30;
		string name;
		int points;
		protected int[] results;
		private int id;
		protected bool playedThisGame;
        private CardInfo card;
        private bool isOnATeam;

		public Player()
		{
			results = new int[NUM_RESULTS];
			playedThisGame = false;
            card = null;
            isOnATeam = false;
		}

        public void LoadCardInfo(int playerId)
        {
            DataTable table;

			// get card info
			table = DbUtils.GetDataTable(string.Format("select * from card_info where player_id = {0}", playerId));

			if (table.Rows.Count > 0) // just use the first row, should only be one
			{
                card = new CardInfo(table.Rows[0]);
            }
        }

        public bool IsOnATeam
        {
            get
            {
                return isOnATeam;
            }
            set
            {
                isOnATeam = value;
            }
        }

		public int ID
		{
			get 
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

        public CardInfo Card
        {
            set
            {
                card = value;
            }
        }

        public string CardTeam
        {
            get
            {
                if (card == null)
                {
                    return "";
                }
                else
                {
                    return card.CardTeam;
                }
            }
        }

        public int CardNumber
        {
            get
            {
                if (card == null)
                {
                    return 0;
                }
                else
                {
                    return card.CardNumber;
                }
            }
        }

        public int CardSeason
        {
            get
            {
                if (card == null)
                {
                    return 0;
                }
                else
                {
                    return card.SeasonYear;
                }
            }
        }
    

		public Player(int Points, string Results, string Name)
		{
			points = Points;
			name = Name;
			results = new int[NUM_RESULTS];

			for (int i = 0; i < NUM_RESULTS; i++)
				results[i] = Int32.Parse(Results.Substring(i, 1));

			playedThisGame = false;
		}

		public int GetResult(int roll)
		{
			return results[roll - 1];
		}

		public void SetResults(string resString)
		{
			if (results == null)
				results = new int[NUM_RESULTS];

			for(int i = 0; i < resString.Length && i < NUM_RESULTS; i++)
				results[i] = Int32.Parse(resString.Substring(i, 1));
		}

		public virtual void UpdateSeasonStats(bool updateDb)
		{

		}

		public virtual void SaveSeasonStats()
		{

		}

		public virtual void ResetSeasonStats()
		{

		}

		public virtual void ResetGameStats()
		{

		}

        public virtual string KeyStatLine
        {
            get
            {
                return "";
            }
        }

        public virtual string PositionText
        {
            get
            {
                return "";
            }
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

		public bool PlayedThisGame
		{
			get
			{
				return playedThisGame;
			}
			set
			{
				playedThisGame = value;
			}
		}

		public string GetResultRange(int result)
		{
			int rangeLow = -1;
			int rangeHigh = -1;
			int i;
			string range;

			for (i = 0; i < NUM_RESULTS; i++)
			{
				if (results[i] == result)
					break;
			}

			if (i != NUM_RESULTS)
			{
				rangeLow = i + 1;

				for (i = rangeLow; i < NUM_RESULTS; i++)
				{
					if (results[i] != result)
						break;
				}

				rangeHigh = (i == NUM_RESULTS) ? NUM_RESULTS : i;
			}

			if (rangeLow == -1)
				range = "--";
			else if (rangeLow == rangeHigh)
				range = rangeLow.ToString();
			else
				range = String.Format("{0} - {1}", rangeLow, rangeHigh);

			return range;
		}

		public int Points
		{
			get
			{
				return points;
			}

			set
			{
				points = value;
			}


		}
	}
}

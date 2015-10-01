using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
    public class CardInfo
    {
        private int seasonYear;   
        private int cardSet;    
        private int cardNumber;
        private string cardTeam;

        public CardInfo(int year, int set, int number, string team)
        { 
            seasonYear = year;;   
            cardSet = set;
            cardNumber = number;
            cardTeam = team;
        }

        public CardInfo()
        {
        }

        public CardInfo(DataRow row)
        { 
            seasonYear = int.Parse(row["season_year"].ToString());
            cardSet = int.Parse(row["card_set"].ToString());
            cardNumber = int.Parse(row["card_number"].ToString());
            cardTeam = row["card_team"].ToString();
        }

        public int SeasonYear
        {
            get
            {
                return seasonYear;
            }
            set
            {
                seasonYear = value;
            }
        }

        public int CardSet
        {
            get
            {
                return cardSet;
            }
            set
            {
                cardSet = value;
            }
        }

        public int CardNumber
        {
            get
            {
                return cardNumber;
            }
            set
            {
                cardNumber = value;
            }
        }

        public string CardTeam
        {
            get
            {
                return cardTeam;
            }
            set
            {
                cardTeam = value;
            }
        }
    }
}

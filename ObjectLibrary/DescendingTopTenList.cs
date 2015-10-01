using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
    public class DescendingTopTenList<T> : TopTenList<T>
    {
        public override int AddPlayer(string team, string player, T score)
        {
            int i = 0;

            if (base.topTen.Count == 0)
            {
                base.topTen.Add(new TopTenItem<T>(player, team, score));
            }
            else
            {
                bool playerAdded = false;

                foreach (TopTenItem<T> tti in base.topTen)
                {

                    if (Comparer<T>.Default.Compare(score, tti.Score) == 1)
                    {
                        base.topTen.Insert(i, new TopTenItem<T>(player, team, score));
                        playerAdded = true;
                        break;
                    }
                    i++;

                    if (i >= 10)
                    {
                        break;
                    }
                }
                if (i < 10 && playerAdded == false)
                {
                    base.topTen.Add(new TopTenItem<T>(player, team, score));
                }
            }

            return i;
        } 

    }
}

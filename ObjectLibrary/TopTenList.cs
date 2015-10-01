using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
    public abstract class TopTenList<T>
    {
        public List<TopTenItem<T>> topTen;

        public TopTenList()
        {
            topTen = new List<TopTenItem<T>>();
        }

        public List<TopTenItem<T>> TopTen
        {
            get
            {
                return topTen.GetRange(0, topTen.Count < 10 ? topTen.Count : 10);
            }
        }

        public abstract int AddPlayer(string team, string player, T score);

        public string Summary
        {
            get
            {
                int i = 0;

                StringBuilder sb = new StringBuilder();
                foreach (TopTenItem<T> tti in topTen)
                {
                    sb.AppendFormat("{0}\r\n", tti.ToString());
                    i++;
                    if (i > 9)
                        break;
                }

                return sb.ToString();
            }
        }
    }
}

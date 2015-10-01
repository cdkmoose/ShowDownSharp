using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
	public class PitcherStats
	{
		private short games;
		private short outs;
		private short earnedRuns;
		private short runs;
		private short strikeOuts;
		private short walks;
		private short wins;
		private short losses;
		private short saves;
        private short hits;

		#region Constructor
		public PitcherStats()
		{
			Reset();
		}
		#endregion

		#region Public Properties
		public short Games
		{
			get
			{
				return games;
			}
			set
			{
				games = value;
			}
		}

		public short Outs
		{
			get
			{
				return outs;
			}
			set
			{
				outs = value;
			}
		}

		public short EarnedRuns
		{
			get
			{
				return earnedRuns;
			}
			set
			{
				earnedRuns = value;
			}
		}

		public short Runs
		{
			get
			{
				return runs;
			}
			set
			{
				runs = value;
			}
		}

		public short StrikeOuts
		{
			get
			{
				return strikeOuts;
			}
			set
			{
				strikeOuts = value;
			}
		}

		public short Walks
		{
			get
			{
				return walks;
			}
			set
			{
				walks = value;
			}
		}

		public short Wins
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

		public short Losses
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

		public short Saves
		{
			get
			{
				return saves;
			}
			set
			{
				saves = value;
			}

		}

        public short Hits
        {
            get
            {
                return hits;
            }

            set
            {
                hits = value;
            }
        }

		#endregion

		public void Reset()
		{
			outs = 0;
			earnedRuns = 0;
			runs = 0;
			strikeOuts = 0;
			walks = 0;
			wins = 0;
			losses = 0;
			saves = 0;
            games = 0;
            hits = 0;
		}

		public float EarnedRunAvg
		{
			get
			{
				float innings = (float) (outs / (3.0F));

				if (outs == 0)
				{
					if (earnedRuns == 0)
						return (float) 0.0F;
					else
						return (float) 99.99F;
				}
				else
					return (float) runs * 9.0F / innings; 

			}

		}

		public float InningsPitched
		{
			get
			{
				float fIP;

				fIP = (float) (outs / 3);
				fIP += (float)(outs % 3) / (float) 10;

				return fIP;
			}
		}


	}
}

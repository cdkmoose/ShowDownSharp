using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
	public class BatterStats
	{
		#region member variables
			short	games;
			short	atBats;
			short	hits;
			short	doubles;
			short	triples;
			short	homeRuns;
			short	strikeOuts;
			short	steals;
			short	sacBunts;
			short	sacFlies;
			short	runsScored;
			short	rbis;
			short	walks;

		#endregion

		#region Properties

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

			public short AtBats
			{
				get 
				{
					return atBats;
				}
				set
				{
					atBats = value;
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

			public short Doubles
			{
				get 
				{
					return doubles;
				}
				set
				{
					doubles = value;
				}

			}

			public short Triples
			{
				get 
				{
					return triples;
				}
				set
				{
					triples = value;
				}

			}

			public short HomeRuns
			{
				get 
				{
					return homeRuns;
				}
				set
				{
					homeRuns = value;
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

			public short Steals
			{
				get 
				{
					return steals;
				}
				set
				{
					steals = value;
				}

			}

			public short SacBunts
			{
				get 
				{
					return sacBunts;
				}
				set
				{
					sacBunts = value;
				}

			}

			public short SacFlies
			{
				get 
				{
					return sacFlies;
				}
				set
				{
					sacFlies = value;
				}

			}

			public short RunsScored
			{
				get 
				{
					return runsScored;
				}
				set
				{
					runsScored = value;
				}

			}

			public short RBIs
			{
				get 
				{
					return rbis;
				}
				set
				{
					rbis = value;
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

		#endregion


		public BatterStats()
		{
			Reset();
		}


		public void Reset()
		{
			games = 0;
			atBats = 0;
			hits = 0;
			doubles = 0;
			triples = 0;
			homeRuns = 0;
			strikeOuts = 0;
			steals = 0;
			sacBunts = 0;
			sacFlies = 0;
			runsScored = 0;
			rbis = 0;
			walks = 0;
		}

		public float BattingAvg
		{
			get
			{
				float	numerator;
				float	denominator;

				numerator = (float)hits;
				denominator = (float)atBats;

				if (denominator == 0.0)
					return 0.0F;
				else
					return numerator / denominator;
			}
		}

		public float OnBasePct
		{
			get 
			{
				float	numerator;
				float	denominator;

				numerator = (float) (hits + walks);
				denominator = (float) (atBats + walks - sacBunts);

				if (denominator == 0.0)
					return 0.0F;
				else
					return numerator / denominator;
			}
		}

		public float SluggingPct
		{
			get
			{
				float	numerator;
				float	denominator;

				numerator = (float) (hits + doubles + (2 * triples) + (3 * homeRuns));
				denominator = (float) atBats;

				if (denominator == 0.0)
					return 0.0F;
				else
					return numerator / denominator;
			}
		}


	}
}

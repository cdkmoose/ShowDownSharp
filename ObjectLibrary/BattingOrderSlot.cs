using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
	public class BattingOrderSlot
	{
		Batter batter;
		Position position;

		public Batter Hitter
		{
			get
			{
				return batter;
			}

			set
			{
				batter = value;
			}
		}

		public Position FieldingPosition
		{
			get
			{
				return position;
			}

			set
			{
				position = value;
			}
		}
	}

}

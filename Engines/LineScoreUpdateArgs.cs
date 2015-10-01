using System;
using System.Collections.Generic;
using System.Text;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.Engines
{
	public enum LineScoreUpdateType
	{
		Name,
		InningScore,
		CurrentScore,
		CurrentHits,
		CurrentErrors,
		Outs,
		Inning
	}

	public class LineScoreUpdateArgs : EventArgs
	{
		private LineScoreUpdateType updateType;
		private TeamLocation loc;
		private int inning;
		private int iValue;
		private string sValue;

		public LineScoreUpdateType UpdateType
		{
			get 
			{
				return updateType;
			}
			set
			{
				updateType = value;
			}
		}

		public TeamLocation Location
		{
			get 
			{
				return loc;
			}
			set
			{
				loc = value;
			}
		}


		public int Inning
		{
			get 
			{
				return inning;
			}
			set
			{
				inning = value;
			}
		}

		public int IValue
		{
			get 
			{
				return iValue;
			}
			set
			{
				iValue = value;
			}
		}

		public string SValue
		{
			get 
			{
				return sValue;
			}
			set
			{
				sValue = value;
			}
		}

	}
}

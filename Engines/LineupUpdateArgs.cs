using System;
using System.Collections.Generic;
using System.Text;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.Engines
{
	public enum LineupRoleType
	{
		Batter,
		Pitcher,
        Team,
	}

	public class LineupUpdateArgs : EventArgs
	{
		private LineupRoleType roleType;
		private TeamLocation location;
		private int slotNumber;
		private string name;

		public LineupRoleType RoleType
		{
			get 
			{
				return roleType;
			}
			set
			{
				roleType = value;
			}
		}

		public TeamLocation Location
		{
			get 
			{
				return location;
			}
			set
			{
				location = value;
			}
		}


		public int SlotNumber
		{
			get 
			{
				return slotNumber;
			}
			set
			{
				slotNumber = value;
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

	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DS.Showdown.ObjectLibrary
{
    public enum GameWinner
    {
        VisitingTeam = 0,
        HomeTeam = 1,
        TieScore = 2,
    }

	public enum Result 
	{
		StrikeOut,	
		Popup,		
		GroundOut,
		Flyout, 	
		Walk,			
		Single,		
		SinglePlus,	
		Double,		
		Triple,		
		HomeRun,		
	}

	public enum Position 
	{
		None,
		Catcher,
		FirstBase,
		SecondBase,
		ThirdBase,
		Shortstop,
		SecondShort,
        FirstThird,
        Infielder,
		LeftField,
		CenterField,
		RightField,
		LeftRight,
		Outfielder,
		DesignatedHitter,
		Starter,
		Reliever,
		Closer,
	}

	public enum PitcherType
	{
		Starter,
		Reliever,
		Closer,
	}

	public enum BattingSide 
	{
		RightHanded,
		LeftHanded,
		SwitchHitter,
	}

	public enum ThrowingSide
	{
		Right,
		Left,
	}

	public class EnumHelpers
	{
		public static string PitcherTypeToString(PitcherType ptype)
		{
			string result;

			switch (ptype)
			{
				case PitcherType.Closer:
					result = "CL";
					break;

				case PitcherType.Reliever:
					result = "RP";
					break;

				case PitcherType.Starter:
					result = "SP";
					break;

				default:
					result = "None";
					break;
			}

			return result;
		}

        public static List<Position> PositionToList(Position pos)
        {
            List<Position> list = new List<Position>();

            switch (pos)
            {
                case Position.None:
                    break;

                case Position.Catcher:
                    list.Add(Position.Catcher);
                    break;

                case Position.FirstBase:
                    list.Add(Position.FirstBase);
                    break;

                case Position.SecondBase:
                    list.Add(Position.SecondBase);
                    break;

                case Position.ThirdBase:
                    list.Add(Position.ThirdBase);
                    break;

                case Position.Shortstop:
                    list.Add(Position.Shortstop);
                    break;

                case Position.SecondShort:
                    list.Add(Position.SecondBase);
                    list.Add(Position.Shortstop);
                    break;

                case Position.FirstThird:
                    list.Add(Position.FirstBase);
                    list.Add(Position.ThirdBase);
                    break;

                case Position.Infielder:
                    list.Add(Position.FirstBase);
                    list.Add(Position.SecondBase);
                    list.Add(Position.Shortstop);
                    list.Add(Position.ThirdBase);
                    break;

                case Position.LeftField:
                    list.Add(Position.LeftField);
                    break;

                case Position.CenterField:
                    list.Add(Position.CenterField);
                    break;

                case Position.RightField:
                    list.Add(Position.RightField);
                    break;

                case Position.LeftRight:
                    list.Add(Position.LeftField);
                    list.Add(Position.RightField);
                    break;

                case Position.Outfielder:
                    list.Add(Position.LeftField);
                    list.Add(Position.CenterField);
                    list.Add(Position.RightField);
                    break;

                case Position.DesignatedHitter:
                    list.Add(Position.DesignatedHitter);
                    break;

                default:
                    break;

            }

            return list;
        }

        public static Position StringToPosition(string posString)
        {
            Position pos;

            if ("C".Equals(posString))
            {
                pos = Position.Catcher;
            }
            else if ("1B".Equals(posString))
            {
                pos = Position.FirstBase;
            }
            else if ("2B".Equals(posString))
            {
                pos = Position.SecondBase;
            }
            else if ("SS".Equals(posString))
            {
                pos = Position.Shortstop;
            }
            else if ("3B".Equals(posString))
            {
                pos = Position.ThirdBase;
            }
            else if ("RF".Equals(posString))
            {
                pos = Position.RightField;
            }
            else if ("LF".Equals(posString))
            {
                pos = Position.LeftField;
            }
            else if ("CF".Equals(posString))
            {
                pos = Position.CenterField;
            }
            else if ("DH".Equals(posString))
            {
                pos = Position.DesignatedHitter;
            }
            else
            {
                pos = Position.None;
            }

            return pos;
        }

		public static string PositionToString(Position pos)
		{
			string result;
			switch (pos)
			{
				case Position.None:
					result = "--";
					break;
				case Position.Catcher:
					result = "C";
					break;
				case Position.FirstBase:
					result = "1B";
					break;
				case Position.SecondBase:
					result = "2B";
					break;
				case Position.ThirdBase:
					result = "3B";
					break;
				case Position.Shortstop:
					result = "SS";
					break;
				case Position.SecondShort:
					result = "2B/SS";
					break;
				case Position.LeftField:
					result = "LF";
					break;
				case Position.CenterField:
					result = "CF";
					break;
				case Position.RightField:
					result = "RF";
					break;
				case Position.LeftRight:
					result = "LF/RF";
					break;
				case Position.Outfielder:
					result = "OF";
					break;
				case Position.DesignatedHitter:
					result = "DH";
					break;
				case Position.Starter:
					result = "SP";
					break;
				case Position.Reliever:
					result = "RP";
					break;
				case Position.Closer:
					result = "CL";
					break;
				default:
					result = "??";
					break;

			}

			return result;
		}

		public static string ThrowingSideToString(ThrowingSide throws)
		{
			string result;

			switch (throws)
			{
				case ThrowingSide.Left:
					result = "Left";
					break;

				case ThrowingSide.Right:
					result = "Right";
					break;

				default:
					result = "--";
					break;

			}

			return result;
		}

		public static string BattingSideToString(BattingSide side)
		{
			string result;

			switch (side)
			{
				case BattingSide.LeftHanded:
					result = "Left";
					break;

				case BattingSide.RightHanded:
					result = "Right";
					break;

				case BattingSide.SwitchHitter:
					result = "Switch";
					break;

				default:
					result = "--";
					break;

			}

			return result;
		}
	}
}

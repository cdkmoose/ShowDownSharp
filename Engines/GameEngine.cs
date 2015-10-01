using System;
using System.Collections.Generic;
using System.Text;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.Engines
{
	public class GameEngine
	{
		#region private members
		Team visitingTeam;
		Team homeTeam;
		Team battingTeam;
		Team fieldingTeam;
		bool gameInProgress = false;
		bool visitingTeamSelected = false;
		bool visitingLineupSelected = false;
		bool homeTeamSelected = false;
		bool homeLineupSelected = false;
		BaseRunner runnerOnFirst;
        BaseRunner runnerOnSecond;
        BaseRunner runnerOnThird;
		Batter currentHitter;
		Pitcher currentPitcher;
		Random rand;
		StringBuilder gameLog;
        TeamLocation battingLocation = TeamLocation.Visitor;

        Pitcher winningPitcher = null;
        Pitcher losingPitcher = null;
        Pitcher savePitcher = null;

		int outs;
		int	inning;
		int teamAtBat;
		int teamInField;
		int[] atBatIndex;
		int[] runs;
		int[] errors;
		int[] hits;
		int[,] inningScores = new int[2,30];
        int[] inningHomeRuns;

        GameScore score;

		const int VISITING_TEAM = 0;
		const int HOME_TEAM = 1;

		const int RESULT_STRIKEOUT = 0;
		const int RESULT_POPUP = 1;
		const int RESULT_GROUNDOUT = 2;
		const int RESULT_FLYOUT = 3;
		const int RESULT_WALK = 4;
		const int RESULT_SINGLE = 5;
		const int RESULT_SINGLEPLUS = 6;
		const int RESULT_DOUBLE = 7;
		const int RESULT_TRIPLE = 8;
		const int RESULT_HOMERUN = 9;

		const int SPEED_A = 20;
		const int SPEED_B = 15;
		const int SPEED_C = 10;
		const int SPEED_D = 5;

		#endregion

		public GameEngine()
		{
			atBatIndex = new int[2];
			runs = new int[2];
			errors = new int[2];
			hits = new int[2];
            score = new GameScore();
			rand = new Random();

            inningHomeRuns = new int[15];
            for (int i = 0; i < 15; i++)
            {
                inningHomeRuns[i] = 0;
            }
		}

        #region public Properties
        public Team VisitingTeam
		{
			get
			{
				return visitingTeam;
			}
			set
			{
				visitingTeam = value;
				visitingTeamSelected = true;
				visitingLineupSelected = true;

				RaiseLineScoreUpdateEvent(LineScoreUpdateType.Name, -1, -1, TeamLocation.Visitor, visitingTeam.Abbrev);
			}
		}

		public Team HomeTeam
		{
			get
			{
				return homeTeam;
			}

			set
			{
				homeTeam = value;
				homeTeamSelected = true;
				homeLineupSelected = true;

				RaiseLineScoreUpdateEvent(LineScoreUpdateType.Name, -1, -1, TeamLocation.Home, homeTeam.Abbrev);
			}
		}
		
		public bool GameInProgress
		{
		    get
		    {
		        return gameInProgress;
		    }
		    
		}
		
		public string GameLog
		{
		    get
		    {
		        return gameLog.ToString();
		    }
		    
		}

        public string[] BoxScore
        {
            get
            {
                string[] res = new string[3];

                res[0] = "Team     R   H   E";
                res[1] = string.Format("{0}  {1,2} {2,2} 0", visitingTeam.Abbrev, runs[0], hits[0]);
                res[2] = string.Format("{0}  {1,2} {2,2} 0", homeTeam.Abbrev, runs[1], hits[1]);

                return res;
            }
        }

		public string GameStats
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				sb.Append("Game Statistics\r\n\r\n");

				sb.Append(visitingTeam.GenerateStatsReport(true));

				sb.Append("\r\n");

				sb.Append(homeTeam.GenerateStatsReport(true));

				return sb.ToString();

			}

		}

		public string SeasonStats
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				sb.Append("Game Statistics\r\n\r\n");

				sb.Append(visitingTeam.GenerateStatsReport(false));

				sb.Append("\r\n");

				sb.Append(homeTeam.GenerateStatsReport(false));

				return sb.ToString();

			}

		}

		public bool GameIsComplete
        {
            get 
            {
            	bool	result = false;
            
            	if (inning < 9 ) 
            	{
            		// not enough innings played
            		
            		result = false;
            	}
            	else if (runs[HOME_TEAM] > runs[VISITING_TEAM]
            		&& teamAtBat == VISITING_TEAM && outs == 3)
            	{
            		// home team has the lead, 9th or later, middle of inning
            		
            		result = true;
            	}
            	else if (teamAtBat == HOME_TEAM && outs == 3
            		&& (runs[HOME_TEAM] != runs[VISITING_TEAM]))
            	{
            		//visitors winning end of inning
            		
            		result = true;
            	}
            	else if (teamAtBat == HOME_TEAM && (runs[HOME_TEAM] > runs[VISITING_TEAM]))
            	{
            		// home team has taken the lead 9th or later
            		result = true;
            	}
            	else
            	{
            		result = false;
            	}
            
            	return result;
            }
            
        
        }
		
		#endregion

		#region events/delegates
		public delegate void LineScoreUpdateHandler(object sender, LineScoreUpdateArgs args);
		public delegate void LineupUpdateHandler(object sender, LineupUpdateArgs args);
		public delegate void BaseRunnerUpdateHandler(object sender, BaseRunnerUpdateArgs args);
		public delegate void AtBatResultUpdateHandler(object sender, string result);
        public delegate void InningChangeHandler(object sender, int newInning);
		public event LineScoreUpdateHandler LineScoreUpdate;
		public event LineupUpdateHandler LineupUpdate;
		public event BaseRunnerUpdateHandler BaseRunnerUpdate;
        public event AtBatResultUpdateHandler AtBatResultUpdate;
        public event InningChangeHandler InningChange;
        #endregion

        public void CreateTeams()
        {
            SelectVisitingTeam();
            SelectHomeTeam();
        }

        private void SelectVisitingTeam()
        {
            Pitcher pitcher;
            Batter batter;

            visitingTeam = new Team();

            // for now create players in code and add to roster
            pitcher = new Pitcher(5, 7, ThrowingSide.Right, 600, PitcherType.Starter,
                "00000111222223334557", "Kevin Brown");
            visitingTeam.AddPitcher(pitcher);

            pitcher = new Pitcher(4, 7, ThrowingSide.Right, 430, PitcherType.Starter,
                "11000002222223333455", "Jose Lima");
            visitingTeam.AddPitcher(pitcher);

            pitcher = new Pitcher(4, 7, ThrowingSide.Left, 330, PitcherType.Starter,
                "11100000222223334555", "Odalis Perez");
            visitingTeam.AddPitcher(pitcher);

            pitcher = new Pitcher(3, 6, ThrowingSide.Right, 270, PitcherType.Starter,
                "11000002222222334459", "Darren Dreifort");
            visitingTeam.AddPitcher(pitcher);

            batter = new Batter(BattingSide.SwitchHitter, 9, Position.SecondBase, 2, Position.None, 0, 15,
                320, "11224444445555567789", "Jose Offerman");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.LeftHanded, 9, Position.LeftRight, 1, Position.FirstBase, 0, 15,
                280, "00022444444555557779", "John Vander Wal");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.LeftHanded, 10, Position.FirstBase, 1, Position.None, 0, 10,
                360, "22234444444455557799", "John Olerud");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 10, Position.LeftRight, 2, Position.None, 0, 10,
                490, "00444444555555777999", "Kevin Pulsifer");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 10, Position.CenterField, 4, Position.None, 0, 20,
                490, "00444444555555777999", "David Pulsifer");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.LeftHanded, 9, Position.LeftRight, 1, Position.None, 0, 20,
                340, "00044444445555556799", "J.D. Drew");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 9, Position.ThirdBase, 2, Position.None, 0, 15,
                320, "02223445555555557799", "Chris Stynes");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.LeftHanded, 9, Position.Catcher, 0, Position.None, 0, 10,
                180, "02234444444555555779", "Scott Hatteberg");
            visitingTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 7, Position.Shortstop, 3, Position.None, 0, 15,
                230, "00244445555555567999", "Jose Hernandez");
            visitingTeam.AddBatter(batter);

            visitingTeamSelected = true;
            visitingTeam.Name = "Brooklyn Dodgers";
            visitingTeam.Abbrev = "BRK";

            visitingTeam.SetLineUp();

            visitingLineupSelected = true;

            RaiseLineScoreUpdateEvent(LineScoreUpdateType.Name, -1, -1, TeamLocation.Visitor, visitingTeam.Abbrev);

        }

        private void SelectHomeTeam()
        {
            Pitcher pitcher;
            Batter batter;

            homeTeam = new Team();

            // for now create players in code and add to roster
            pitcher = new Pitcher(5, 7, ThrowingSide.Right, 550, PitcherType.Starter,
                "10000002222233333457", "Curt Schilling");
            homeTeam.AddPitcher(pitcher);

            pitcher = new Pitcher(4, 7, ThrowingSide.Right, 550, PitcherType.Starter,
                "11110000002222233335", "Derek Lowe");
            homeTeam.AddPitcher(pitcher);

            pitcher = new Pitcher(5, 6, ThrowingSide.Right, 440, PitcherType.Starter,
                "11100000222223333555", "Pedro Martinez");
            homeTeam.AddPitcher(pitcher);

            pitcher = new Pitcher(3, 6, ThrowingSide.Right, 250, PitcherType.Starter,
                "11110000222222334557", "Tim Wakefield");
            homeTeam.AddPitcher(pitcher);

            batter = new Batter(BattingSide.LeftHanded, 12, Position.CenterField, 0, Position.None, 0, 22,
                390, "00222344555555666679", "Jacoby Ellsbury");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 12, Position.Shortstop, 2, Position.None, 0, 17,
                590, "02223344555555677899", "Nomar Garciaparra");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.LeftHanded, 14, Position.DesignatedHitter, 0, Position.None, 0, 10,
                540, "00223344444455557799", "David Ortiz");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 14, Position.LeftRight, 0, Position.None, 0, 10,
                610, "00233344444455557799", "Manny Ramirez");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 12, Position.FirstBase, 1, Position.None, 0, 15,
                270, "00223344444455555579", "Kevin Youkilis");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 11, Position.ThirdBase, 3, Position.None, 0, 12,
                230, "02233344455555555799", "Mike Lowell");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.LeftHanded, 12, Position.LeftRight, 0, Position.None, 0, 12,
                340, "00023334444455557799", "Trot Nixon");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.SwitchHitter, 12, Position.Catcher, 5, Position.None, 0, 15,
                360, "00223344445555566779", "Jason Varitek");
            homeTeam.AddBatter(batter);

            batter = new Batter(BattingSide.RightHanded, 11, Position.SecondBase, 3, Position.None, 0, 17,
                250, "02223344455555556779", "Dustin Pedroia");
            homeTeam.AddBatter(batter);

            homeTeamSelected = true;
            homeTeam.Name = "Boston Red Sox";
            homeTeam.Abbrev = "BOS";

            homeTeam.SetLineUp();

            homeLineupSelected = true;

            RaiseLineScoreUpdateEvent(LineScoreUpdateType.Name, -1, -1, TeamLocation.Home, homeTeam.Abbrev);
        }

		private void RaiseBaseRunnerUpdateEvent(Base runnerBase, string name)
		{
			if (BaseRunnerUpdate != null)
			{
				BaseRunnerUpdateArgs args = new BaseRunnerUpdateArgs();
				args.UpdateBase = runnerBase;
				args.RunnerName = name;

				BaseRunnerUpdate(this, args);
			}
		}

		private void RaiseLineScoreUpdateEvent(LineScoreUpdateType updateType, int inning, int iValue, TeamLocation loc, string sValue)
		{
			if (LineScoreUpdate != null)
			{
				LineScoreUpdateArgs args = new LineScoreUpdateArgs();

				args.UpdateType = updateType;
				args.SValue = sValue;
				args.Location = loc;
				args.Inning = inning;
				args.IValue = iValue;

				LineScoreUpdate(this, args);
			}

		}

        private void RaiseInningChangeEvent(int inning)
        {
            if (InningChange != null)
            {
                InningChange(this, inning);
            }
        }

		private void RaiseLineupUpdateEvent(LineupRoleType roleType, int slotNumber, TeamLocation loc, string name)
		{
			if (LineupUpdate != null)
			{
				LineupUpdateArgs args = new LineupUpdateArgs();

				args.RoleType = roleType;
				args.SlotNumber = slotNumber;
				args.Location = loc;
				args.Name = name;

				LineupUpdate(this, args);
			}

		}
		
		private void RaiseAtBatResultUpdateEvent(string result)
		{
		    if (AtBatResultUpdate != null)
		    {
		        AtBatResultUpdate(this, result);    
		    }
		}
		

		private void InitializeGameStats()
		{
			string result;
			battingTeam = visitingTeam;
			fieldingTeam = homeTeam;
			battingLocation = TeamLocation.Visitor;
            score.Reset();

            winningPitcher = null;
            losingPitcher = null;
            savePitcher = null;

            runnerOnFirst = null;
			runnerOnSecond = null;
			runnerOnThird = null;
			currentHitter = null;
			currentPitcher = null;

			outs = 0;
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");
			inning = 1;
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Inning, inning, -1, battingLocation, "");
            RaiseInningChangeEvent(inning);
			teamAtBat = VISITING_TEAM;
			teamInField = HOME_TEAM;

			atBatIndex[VISITING_TEAM] = 0;
			atBatIndex[HOME_TEAM] = 0;
			runs[VISITING_TEAM] = 0;
			hits[VISITING_TEAM] = 0;
			errors[VISITING_TEAM] = 0;
			runs[HOME_TEAM] = 0;
			hits[HOME_TEAM] = 0;
			errors[HOME_TEAM] = 0;

			gameLog = new StringBuilder();
			result = string.Format("{0} {1}:\r\n\r\n", (teamAtBat == VISITING_TEAM) ? "TOP" : "BOTTOM",
				inning);
			gameLog.Append(result);

			for (int i = 0; i < 30; i++)
			{
				inningScores[HOME_TEAM, i] = inningScores[VISITING_TEAM, i] = -1;
			}

		}

		public void PlayBall()
		{
			gameInProgress = true;

			InitializeGameStats();

			homeTeam.ResetGameStats();
			visitingTeam.ResetGameStats();

            RaiseLineupUpdateEvent(LineupRoleType.Team, -1, TeamLocation.Home, homeTeam.Name);
            RaiseLineupUpdateEvent(LineupRoleType.Team, -1, TeamLocation.Visitor, visitingTeam.Name);

            for (int i = 0; i < 9; i++)
			{

				RaiseLineupUpdateEvent(LineupRoleType.Batter, i, TeamLocation.Home, homeTeam.GetBatterByLineup(i).LineUpText);
				RaiseLineupUpdateEvent(LineupRoleType.Batter, i, TeamLocation.Visitor, visitingTeam.GetBatterByLineup(i).LineUpText);

				homeTeam.GetBatterByLineup(i).PlayedThisGame = true;
				visitingTeam.GetBatterByLineup(i).PlayedThisGame = true;
			}

			visitingTeam.SetPitcher();
            RaiseLineupUpdateEvent(LineupRoleType.Pitcher, -1, TeamLocation.Visitor, visitingTeam.CurrentPitcher.Name);

            homeTeam.SetPitcher();
            RaiseLineupUpdateEvent(LineupRoleType.Pitcher, -1, TeamLocation.Home, homeTeam.CurrentPitcher.Name);

			// need to clear display here
            RaiseBaseRunnerUpdateEvent(Base.Home, "");
            RaiseBaseRunnerUpdateEvent(Base.First, "");
            RaiseBaseRunnerUpdateEvent(Base.Second, "");
            RaiseBaseRunnerUpdateEvent(Base.Third, "");
            
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Name, -1, -1, TeamLocation.Visitor, visitingTeam.Abbrev);
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentScore, -1, 0, TeamLocation.Visitor, "");
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentHits, -1, 0, TeamLocation.Visitor, "");
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentErrors, -1, 0, TeamLocation.Visitor, "");
            RaiseLineScoreUpdateEvent(LineScoreUpdateType.InningScore, 1, 0, TeamLocation.Visitor, "");

			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Name, -1, -1, TeamLocation.Home, homeTeam.Abbrev);
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentScore, -1, 0, TeamLocation.Home, "");
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentHits, -1, 0, TeamLocation.Home, "");
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentErrors, -1, 0, TeamLocation.Home, "");

            currentPitcher = fieldingTeam.CurrentPitcher;
            currentHitter = battingTeam.GetBatterByLineup(atBatIndex[teamAtBat]);
            RaiseBaseRunnerUpdateEvent(Base.Home, currentHitter.Name);

            inningScores[VISITING_TEAM, 0] = 0;
        }

		private int DoAtBat(Pitcher pitcher, Batter batter)
		{
			int pitch = RollDie(20);
			int	swing = RollDie(20);
			int	onBase = batter.OnBase;
			int	control = pitcher.AdjustedControl;
			int	result;

			if (control + pitch > onBase)
				result = pitcher.GetResult(swing);
			else
				result = batter.GetResult(swing);


			return result;
			
		}

		private int RollDie(int sides)
		{
			int result;

			result = rand.Next(sides) + 1;

			return result;

		}

		public void NextAtBat() 
		{
			int			atBatResult;
			string		result;
            GameWinner  previousWinner;
            GameWinner  currentWinner;

            previousWinner = score.WinningTeam;

			atBatResult = DoAtBat(currentPitcher, currentHitter);

			switch (atBatResult)
			{
				case RESULT_GROUNDOUT:
					result = HandleGroundOut();
					break;

				case RESULT_STRIKEOUT:
					result = HandleStrikeOut();
					break;

				case RESULT_POPUP:
					result = HandlePopUp();
					break;

				case RESULT_FLYOUT:
					result = HandleFlyOut();
					break;

				case RESULT_WALK:
					result = HandleWalk();
					break;

				case RESULT_SINGLE:
					result = HandleSingle();
					break;

				case RESULT_SINGLEPLUS:
					result = HandleSinglePlus();
					break;

				case RESULT_DOUBLE:
					result = HandleDouble();
					break;

				case RESULT_TRIPLE:
					result = HandleTriple();
					break;

				case RESULT_HOMERUN:
					result = HandleHomeRun();
					break;

				default:
                    result = "";
					break;

			}

            RaiseAtBatResultUpdateEvent(result);

			gameLog.Append(result);

			//  new batter and other values to setup for next atbat

            currentWinner = score.WinningTeam;

            if (currentWinner != previousWinner)
            {
                HandleLeadChange(previousWinner, currentWinner);
            }

			if (GameIsComplete == true)
			{
                HandleEndGame();
			}
			else
			{
				atBatIndex[teamAtBat] = IncrementAtBatIndex(atBatIndex[teamAtBat]);
			
				if (outs > 2)
				{
                    HandleHalfInningEnd();
				}

				currentHitter = battingTeam.GetBatterByLineup(atBatIndex[teamAtBat]);
                RaiseBaseRunnerUpdateEvent(Base.Home, currentHitter.Name);
            }

		}

        private void HandleHalfInningEnd()
        {
            string result;

            if (teamAtBat == HOME_TEAM)
            {
                inning++;
                RaiseLineScoreUpdateEvent(LineScoreUpdateType.Inning, inning, -1, battingLocation, "");
                RaiseInningChangeEvent(inning);
            }

            SwapTeams();

            runnerOnFirst = null;
            runnerOnSecond = null;
            runnerOnThird = null;
            RaiseBaseRunnerUpdateEvent(Base.First, "");
            RaiseBaseRunnerUpdateEvent(Base.Second, "");
            RaiseBaseRunnerUpdateEvent(Base.Third, "");

            outs = 0;
            RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");
            inningScores[teamAtBat, inning - 1] = 0;
            RaiseLineScoreUpdateEvent(LineScoreUpdateType.InningScore, inning, 0, battingLocation, "");

            currentPitcher = fieldingTeam.CurrentPitcher;
            currentHitter = battingTeam.GetBatterByLineup(atBatIndex[teamAtBat]);

            result = string.Format("{0} {1}:\r\n\r\n", (teamAtBat == VISITING_TEAM) ? "TOP" : "BOTTOM",
                inning);
            gameLog.Append("\r\n=================================================\r\n" + result);
        }

        private void HandleEndGame()
        {
            string result;

            gameInProgress = false;

            homeTeam.GamesPlayed++;
            visitingTeam.GamesPlayed++;

            winningPitcher.GameStats.Wins++;
            losingPitcher.GameStats.Losses++;
            if (savePitcher != null)
            {
                savePitcher.GameStats.Saves++;
            }

            if (score.WinningTeam == GameWinner.HomeTeam)
            {
                result = string.Format("The {2} have won by a score of {0} - {1}",
                    runs[HOME_TEAM], runs[VISITING_TEAM], homeTeam.Name);

                homeTeam.Wins++;

                visitingTeam.Losses++;
            }
            else
            {
                result = string.Format("The {2} have won by a score of {0} - {1}",
                    runs[VISITING_TEAM], runs[HOME_TEAM], visitingTeam.Name);

                visitingTeam.Wins++;

                homeTeam.Losses++;
            }

            homeTeam.UpdateSeasonStats();
            visitingTeam.UpdateSeasonStats();

            gameLog.Append("=================================================\r\n\r\n");
            gameLog.Append(result);


        }

        private void HandleLeadChange(GameWinner prev, GameWinner curr)
        {
            // if the  status has changed and there was a pitcher in line for a save, he can't be anymore
            savePitcher = null;

            if (curr == GameWinner.TieScore)
            {
                winningPitcher = null;
                losingPitcher = null;
            }
            else if (curr == GameWinner.HomeTeam)
            {
                winningPitcher = homeTeam.CurrentPitcher;
                losingPitcher = visitingTeam.CurrentPitcher;
            }
            else
            {
                winningPitcher = visitingTeam.CurrentPitcher;
                losingPitcher = homeTeam.CurrentPitcher;
            }
        }

		void SwapTeams()
		{
			int tmp;
			Team tmpTeam;

			tmp = teamAtBat;
			teamAtBat = teamInField;
			teamInField = tmp;

			tmpTeam = fieldingTeam;
			fieldingTeam = battingTeam;
			battingTeam = tmpTeam;
			
            battingLocation = battingLocation == TeamLocation.Home ? TeamLocation.Visitor : TeamLocation.Home;
		}

        public void SwapCurrentPitcher(bool swapHomeTeam)
        {
            // update current pitcher for next at bat
            if (currentPitcher != fieldingTeam.CurrentPitcher)
            {
                currentPitcher = fieldingTeam.CurrentPitcher;
            }

            // evalaute save opportunity for new pitcher
            int scoreDiff = Math.Abs(score.VisitingScore - score.HomeScore);
            int runnersOnBase = CountRunnersOnBase();

            if (scoreDiff < 4 || scoreDiff < runnersOnBase + 3)
            {
                // could be a save now check for change matches winning team
                if (swapHomeTeam == true && score.WinningTeam == GameWinner.HomeTeam)
                {
                    savePitcher = homeTeam.CurrentPitcher;
                }
                else if (swapHomeTeam == false && score.WinningTeam == GameWinner.VisitingTeam)
                {
                    savePitcher = visitingTeam.CurrentPitcher;
                }
            }
        }

        private int CountRunnersOnBase()
        {
            int runners = 0;

            if (runnerOnFirst != null)
            {
                runners++;
            }

            if (runnerOnSecond != null)
            {
                runners++;
            }

            if (runnerOnThird != null)
            {
                runners++;
            }

            return runners;
        }

	    private string HandleStrikeOut()
	    {
		    outs++;
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");

		    currentPitcher.GameStats.Outs++;
		    currentPitcher.GameStats.StrikeOuts++;

		    currentHitter.GameStats.AtBats++;
		    currentHitter.GameStats.StrikeOuts++;

		    return (currentHitter.Name + " strikes out.\r\n");

	    }

	    private string  HandlePopUp()
	    {
		    outs++;
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");

		    currentPitcher.GameStats.Outs++;

		    currentHitter.GameStats.AtBats++;

		    return (currentHitter.Name + " pops out.\r\n");
	    }

	    private string  HandleGroundOut()
	    {
		    bool	doublePlay = false;
		    int		rating = fieldingTeam.InfieldRating;
		    
		    StringBuilder sb = new StringBuilder();

		    outs++;
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");
			currentHitter.GameStats.AtBats++;
		    currentPitcher.GameStats.Outs++;

		    sb.Append(currentHitter.Name + " grounds out.\r\n");

		    // check for runner on first double play
		    if (outs < 3)
		    {
			    if (runnerOnFirst != null)
			    {
                    if (rating + RollDie(20) > currentHitter.Speed)
                    {
                        outs++;
						RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");
						doublePlay = true;
                        currentPitcher.GameStats.Outs++;

                        runnerOnFirst = null;
                        RaiseBaseRunnerUpdateEvent(Base.First, "");
                    }
                    else
                    {
                        BaseRunner runner = new BaseRunner();
                        runner.Hitter = currentHitter;
                        runner.PitcherFaced = currentPitcher;
                        runnerOnFirst = runner;
                        RaiseBaseRunnerUpdateEvent(Base.First, runnerOnFirst.Hitter.Name);
                    }
			    }
		    }

		    if (outs < 3) // need to check again incase of double play
		    {
			    // if runner on third and < 3 outs then runner scores
			    if (runnerOnThird != null)
			    {
                    ScoreRunner((BaseRunner)runnerOnThird, !doublePlay);
				    runnerOnThird = null;
                    RaiseBaseRunnerUpdateEvent(Base.Third, "");
			    }

			    // if runner on second and < 3 outs runner advances
			    if (runnerOnSecond != null)
			    {
				    runnerOnThird = runnerOnSecond;
                    RaiseBaseRunnerUpdateEvent(Base.Third, runnerOnThird.Hitter.Name);
				    runnerOnSecond = null;
                    RaiseBaseRunnerUpdateEvent(Base.Second, "");
			    }
		    }

		    if (doublePlay)
		    {
		        sb.Append("Double play.\r\n");
		    }
		    
			return sb.ToString();

	    }

        private string HandleFlyOut()
        {
            StringBuilder sb = new StringBuilder();
            
	        outs++;
			RaiseLineScoreUpdateEvent(LineScoreUpdateType.Outs, -1, outs, battingLocation, "");

	        currentPitcher.GameStats.Outs++;

	        currentHitter.GameStats.AtBats++;

	        sb.Append(currentHitter.Name + " flies out.\r\n");

	        if (runnerOnThird != null && outs < 3)
	        {
		        if (((Batter)runnerOnThird.Hitter).Speed >= SPEED_B)
		        {
			        sb.Append(runnerOnThird.Hitter.Name + " scores from third.\r\n");
			        sb.Append(currentHitter.Name + " gets the RBI.\r\n");

                    ScoreRunner((BaseRunner)runnerOnThird, true);
			        runnerOnThird = null;
                    RaiseBaseRunnerUpdateEvent(Base.Third, "");

			        currentHitter.GameStats.AtBats--;
			        currentHitter.GameStats.SacFlies++;

		        }
	        }

	        if (runnerOnThird == null && runnerOnSecond != null && outs < 3)
	        {
		        if (((Batter)runnerOnSecond.Hitter).Speed >= SPEED_A)
		        {
			        sb.Append(runnerOnSecond.Hitter.Name + " advances to third.\r\n");
			        runnerOnThird = runnerOnSecond;
                    RaiseBaseRunnerUpdateEvent(Base.Third, runnerOnThird.Hitter.Name);
			        runnerOnSecond = null;
                    RaiseBaseRunnerUpdateEvent(Base.Second, "");
		        }
	        }

            return sb.ToString();
        }

        private string HandleWalk()
        {
	        currentPitcher.GameStats.Walks++;
	        currentHitter.GameStats.Walks++;

	        if (runnerOnFirst != null)
	        {
		        if (runnerOnSecond != null)
		        {
			        if (runnerOnThird != null)
			        {
				        ScoreRunner((BaseRunner) runnerOnThird, true);
				        runnerOnThird = null;
                        RaiseBaseRunnerUpdateEvent(Base.Third, "");
			        }

			        runnerOnThird = runnerOnSecond;
                    RaiseBaseRunnerUpdateEvent(Base.Third, runnerOnThird.Hitter.Name);
			        runnerOnSecond = null;;
                    RaiseBaseRunnerUpdateEvent(Base.Second, "");
		        }

		        runnerOnSecond = runnerOnFirst;
                RaiseBaseRunnerUpdateEvent(Base.Second, runnerOnSecond.Hitter.Name);
		        runnerOnFirst = null;
                RaiseBaseRunnerUpdateEvent(Base.First, "");
	        }

            runnerOnFirst = new BaseRunner();
            runnerOnFirst.Hitter = currentHitter;
            runnerOnFirst.PitcherFaced = currentPitcher;
            RaiseBaseRunnerUpdateEvent(Base.First, runnerOnFirst.Hitter.Name);

	        return (currentHitter.Name + " walks.\r\n");
        }

        private string HandleSingle()
        {
	        currentHitter.GameStats.Hits++;;
	        currentHitter.GameStats.AtBats++;
	        hits[teamAtBat]++;

            RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentHits, inning, hits[teamAtBat], battingLocation, "");

	        if (runnerOnThird != null)
	        {
                ScoreRunner((BaseRunner)runnerOnThird, true);
		        runnerOnThird = null;
                RaiseBaseRunnerUpdateEvent(Base.Third, "");
	        }

	        if (runnerOnSecond != null)
	        {
                ScoreRunner((BaseRunner)runnerOnSecond, true);
		        runnerOnSecond = null;
                RaiseBaseRunnerUpdateEvent(Base.Second, "");
	        }

	        if (runnerOnFirst != null)
	        {
		        runnerOnSecond = runnerOnFirst;
                RaiseBaseRunnerUpdateEvent(Base.Second, runnerOnSecond.Hitter.Name);
		        runnerOnFirst = null;
                RaiseBaseRunnerUpdateEvent(Base.First, "");
	        }

            runnerOnFirst = new BaseRunner();
            runnerOnFirst.Hitter = currentHitter;
            runnerOnFirst.PitcherFaced = currentPitcher;
            RaiseBaseRunnerUpdateEvent(Base.First, runnerOnFirst.Hitter.Name);

	        return (currentHitter.Name + " singles.\r\n");

        }

        private string HandleSinglePlus()
        {
            string result;
	        result = HandleSingle();

	        if (runnerOnSecond == null)
	        {
		        runnerOnSecond = runnerOnFirst;
		        ((Batter)runnerOnSecond.Hitter).GameStats.Steals++;
                RaiseBaseRunnerUpdateEvent(Base.Second, runnerOnSecond.Hitter.Name);

		        runnerOnFirst = null;
                RaiseBaseRunnerUpdateEvent(Base.First, "");

				result += currentHitter.Name + " steals second.\r\n";
	        }
	        
	        return result;
        }

        private string HandleDouble()
        {
	        currentHitter.GameStats.Hits++;
	        currentHitter.GameStats.Doubles++;
	        currentHitter.GameStats.AtBats++;
	        hits[teamAtBat]++;
            RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentHits, inning, hits[teamAtBat], battingLocation, "");

	        if (runnerOnThird != null)
	        {
                ScoreRunner((BaseRunner)runnerOnThird, true);
                RaiseBaseRunnerUpdateEvent(Base.Third, "");
	        }

	        if (runnerOnSecond != null)
	        {
                ScoreRunner((BaseRunner)runnerOnSecond, true);
		        runnerOnSecond = null;
                RaiseBaseRunnerUpdateEvent(Base.Second, "");
	        }

	        if (runnerOnFirst != null)
	        {
		        if (outs == 2 && ((Batter)runnerOnFirst.Hitter).Speed == SPEED_A)
		        {
                    ScoreRunner((BaseRunner)runnerOnFirst, true);
			        runnerOnFirst = null;
                    RaiseBaseRunnerUpdateEvent(Base.First, "");
		        }
		        else
		        {
			        runnerOnThird = runnerOnFirst;
                    RaiseBaseRunnerUpdateEvent(Base.Third, runnerOnThird.Hitter.Name);
                    runnerOnFirst = null;
                    RaiseBaseRunnerUpdateEvent(Base.First, "");
		        }

	        }

            runnerOnSecond = new BaseRunner();
            runnerOnSecond.Hitter = currentHitter;
            runnerOnSecond.PitcherFaced = currentPitcher;

            RaiseBaseRunnerUpdateEvent(Base.Second, currentHitter.Name);

	        return (currentHitter.Name + " doubles.\r\n");

        }

        private string HandleTriple()
        {
        	currentHitter.GameStats.Hits++;
        	currentHitter.GameStats.Triples++;
        	currentHitter.GameStats.AtBats++;
        	hits[teamAtBat]++;
        	
        	RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentHits, inning, hits[teamAtBat], battingLocation, "");
        
        	if (runnerOnThird != null)
        	{
                ScoreRunner((BaseRunner)runnerOnThird, true);
        		runnerOnThird = null;
                RaiseBaseRunnerUpdateEvent(Base.Third, "");
        	}
        
        	if (runnerOnSecond != null)
        	{
                ScoreRunner((BaseRunner)runnerOnSecond, true);
        		runnerOnSecond = null;
                RaiseBaseRunnerUpdateEvent(Base.Second, "");
        	}
        
        	if (runnerOnFirst != null)
        	{
                ScoreRunner((BaseRunner)runnerOnFirst, true);
        		runnerOnFirst = null;
                RaiseBaseRunnerUpdateEvent(Base.First, "");
        	}

            runnerOnThird = new BaseRunner();
            runnerOnThird.Hitter = currentHitter;
            runnerOnThird.PitcherFaced = currentPitcher;
            RaiseBaseRunnerUpdateEvent(Base.Third, currentHitter.Name);
        
        	return (currentHitter.Name + " triples.\r\n");
        }

        private string HandleHomeRun()
        {
        	currentHitter.GameStats.Hits++;
        	currentHitter.GameStats.HomeRuns++;
        	currentHitter.GameStats.AtBats++;
        	hits[teamAtBat]++;
            RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentHits, inning, hits[teamAtBat], battingLocation, "");
        
        	if (runnerOnThird != null)
        	{
                ScoreRunner((BaseRunner)runnerOnThird, true);
        		runnerOnThird = null;
                RaiseBaseRunnerUpdateEvent(Base.Third, "");
        	}
        
        	if (runnerOnSecond != null)
        	{
                ScoreRunner((BaseRunner)runnerOnSecond, true);
        		runnerOnSecond = null;
                RaiseBaseRunnerUpdateEvent(Base.Second, "");
        	}
        
        	if (runnerOnFirst != null)
        	{
                ScoreRunner((BaseRunner)runnerOnFirst, true);
        		runnerOnFirst = null;
                RaiseBaseRunnerUpdateEvent(Base.First, "");
        	}

            BaseRunner runner = new BaseRunner();
            runner.Hitter = currentHitter;
            runner.PitcherFaced = currentPitcher;

            ScoreRunner(runner, true);

            if (inning < 15)
            {
                inningHomeRuns[inning - 1]++;
            }
        
        	return (currentHitter.Name + " homers.\r\n");
        
        }

        private void ScoreRunner(BaseRunner runner, bool rbi)
        {
	        runner.PitcherFaced.GameStats.Runs++;
            runner.PitcherFaced.GameStats.EarnedRuns++;
        	
	        runner.Hitter.GameStats.RunsScored++;

	        runs[teamAtBat]++;

            if (battingLocation == TeamLocation.Home)
            {
                score.HomeScore++;
            }
            else
            {
                score.VisitingScore++;
            }

	        inningScores[teamAtBat, inning - 1]++;

            RaiseLineScoreUpdateEvent(LineScoreUpdateType.CurrentScore, inning, runs[teamAtBat], battingLocation, "");
            RaiseLineScoreUpdateEvent(LineScoreUpdateType.InningScore, inning, inningScores[teamAtBat, inning - 1], battingLocation, "");

			gameLog.AppendFormat("{0} scores.\r\n", runner.Hitter.Name);

	        if (rbi)
		        currentHitter.GameStats.RBIs++;
        }

        private int IncrementAtBatIndex(int currentIndex)
        {
	        currentIndex++;

	        if (currentIndex > 8)
		        currentIndex = 0;

	        return currentIndex;
        }

        
        
	}
}

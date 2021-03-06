
//This code has been automatically generated by DudeLabs LLC RCConverter
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.Game
{
	/// <summary>
	/// Summary description for ShowPitcherDlgForm.
	/// </summary>
	public partial class ShowPitcherForm : System.Windows.Forms.Form
	{
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
		Pitcher pitcher;

		public ShowPitcherForm(Pitcher ptr)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			pitcher = ptr;

			FillPlayerValues();
		}
		private void FillPlayerValues()
		{

			// player info
			lblName.Text = pitcher.Name;
			lblThrows.Text = EnumHelpers.ThrowingSideToString(pitcher.PitchingArm);
			lblControl.Text = pitcher.Control.ToString();
			lblIp.Text = pitcher.IP.ToString();
			lblPoints.Text = pitcher.Points.ToString();

			lblType.Text = EnumHelpers.PitcherTypeToString(pitcher.PitcherRole);

			// outcomes table
			lblRangeso.Text = pitcher.GetResultRange(RESULT_STRIKEOUT);
			lblRangefo.Text = pitcher.GetResultRange(RESULT_FLYOUT);
			lblRangepu.Text = pitcher.GetResultRange(RESULT_POPUP);
			lblRangegb.Text = pitcher.GetResultRange(RESULT_GROUNDOUT);
			lblRangew.Text = pitcher.GetResultRange(RESULT_WALK);
			lblRange1b.Text = pitcher.GetResultRange(RESULT_SINGLE);
			lblRange1bplus.Text = pitcher.GetResultRange(RESULT_SINGLEPLUS);
			lblRange2b.Text = pitcher.GetResultRange(RESULT_DOUBLE);
			lblRange3b.Text = pitcher.GetResultRange(RESULT_TRIPLE);
			lblRangehr.Text = pitcher.GetResultRange(RESULT_HOMERUN);

			// season stats
			lblWseason.Text = pitcher.SeasonStats.Wins.ToString();
			lblLseason.Text = pitcher.SeasonStats.Losses.ToString();
			lblSseason.Text = pitcher.SeasonStats.Saves.ToString();
			lblErseason.Text = pitcher.SeasonStats.EarnedRuns.ToString();
			lblRseason.Text = pitcher.SeasonStats.Runs.ToString();
			lblKseason.Text = pitcher.SeasonStats.StrikeOuts.ToString();
			lblBbseason.Text = pitcher.SeasonStats.Walks.ToString();
			lblIpseason.Text = pitcher.SeasonStats.InningsPitched.ToString();
			lblEraseason.Text = String.Format("{0,4:F2}", pitcher.SeasonStats.EarnedRunAvg);

			// game stats
			lblWgame.Text = pitcher.GameStats.Wins.ToString();
			lblLgame.Text = pitcher.GameStats.Losses.ToString();
			lblSgame.Text = pitcher.GameStats.Saves.ToString();
			lblErgame.Text = pitcher.GameStats.EarnedRuns.ToString();
			lblRgame.Text = pitcher.GameStats.Runs.ToString();
			lblKgame.Text = pitcher.GameStats.StrikeOuts.ToString();
			lblBbgame.Text = pitcher.GameStats.Walks.ToString();
			lblIpgame.Text = pitcher.GameStats.InningsPitched.ToString();
			lblEragame.Text = String.Format("{0,4:F2}", pitcher.GameStats.EarnedRunAvg);

		}
	}
}

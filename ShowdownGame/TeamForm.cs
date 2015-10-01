using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.Game
{
	public partial class TeamForm : Form
	{
		public TeamForm()
		{
			InitializeComponent();
		}

		public TeamForm(Team team)
		{
			ListViewItem item;
			InitializeComponent();

			// should use ListViewItems

			foreach (Batter btr in team.Hitters)
			{
				item = new ListViewItem();
				FillItem(item, btr);
				batterListView.Items.Add(item);
			}

			foreach (Pitcher ptr in team.StartingPitchers)
			{
				item = new ListViewItem();
				FillItem(item, ptr);
				starterListView.Items.Add(item);
			}

			foreach (Pitcher ptr in team.BullPen)
			{
				item = new ListViewItem();
				FillItem(item, ptr);
				bullpenListView.Items.Add(item);
			}
		}

		private void FillItem(ListViewItem itm, Batter btr)
		{
			itm.Text = btr.Name;
			itm.SubItems.Add(btr.PrimaryPosition.ToString());
			itm.SubItems.Add(btr.SeasonStats.BattingAvg.ToString("0.000"));
			itm.SubItems.Add(btr.SeasonStats.SluggingPct.ToString("0.000"));
		}

		private void FillItem(ListViewItem itm, Pitcher ptr)
		{
			itm.Text = ptr.Name;
			itm.SubItems.Add(ptr.SeasonStats.Wins.ToString());
			itm.SubItems.Add(ptr.SeasonStats.Losses.ToString());
			itm.SubItems.Add(ptr.SeasonStats.Saves.ToString());
			itm.SubItems.Add(ptr.SeasonStats.EarnedRunAvg.ToString("#0.00"));
		}
	}
}
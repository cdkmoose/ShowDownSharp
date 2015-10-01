using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.SharedForms
{
	public partial class TeamForm : Form
	{
        private Team team;

		public TeamForm()
		{
			InitializeComponent();
		}

		public TeamForm(Team team)
		{
            this.team = team;

			ListViewItem item;
			InitializeComponent();

			// should use ListViewItems

			foreach (Batter btr in team.Hitters)
			{
				item = new ListViewItem();
				FillItem(item, btr);
                item.Tag = btr;
				batterListView.Items.Add(item);
			}

			foreach (Pitcher ptr in team.StartingPitchers)
			{
				item = new ListViewItem();
				FillItem(item, ptr);
                item.Tag = ptr;
				starterListView.Items.Add(item);
			}

			foreach (Pitcher ptr in team.BullPen)
			{
				item = new ListViewItem();
				FillItem(item, ptr);
                item.Tag = ptr;
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

        void batterListView_DoubleClick(object sender, System.EventArgs e)
        {
            if (batterListView.SelectedItems.Count == 1)
            {
                Batter btr = (Batter) batterListView.SelectedItems[0].Tag;
                ShowBatterForm frm = new ShowBatterForm(btr);

                frm.ShowDialog();
            }
        }

        void starterListView_DoubleClick(object sender, System.EventArgs e)
        {
            if (starterListView.SelectedItems.Count == 1)
            {
                Pitcher ptr = (Pitcher)starterListView.SelectedItems[0].Tag;
                ShowPitcherForm frm = new ShowPitcherForm(ptr);

                frm.ShowDialog();
            }
        }

        void bullpenListView_DoubleClick(object sender, System.EventArgs e)
        {
            if (bullpenListView.SelectedItems.Count == 1)
            {
                Pitcher ptr = (Pitcher)bullpenListView.SelectedItems[0].Tag;
                ShowPitcherForm frm = new ShowPitcherForm(ptr);

                frm.ShowDialog();
            }
        }

        private void batterListView_KeyDown(object sender, KeyEventArgs e)
        {
            ListViewItem itm = batterListView.SelectedItems[0];
            Batter btr = itm.Tag as Batter;
            team.RemoveBatter(btr);
            batterListView.Items.Remove(itm);
        }

        private void starterListView_KeyDown(object sender, KeyEventArgs e)
        {
            ListViewItem itm = starterListView.SelectedItems[0];
            Pitcher ptr = itm.Tag as Pitcher;
            team.RemovePitcher(ptr);
            starterListView.Items.Remove(itm);

        }

        private void bullpenListView_KeyDown(object sender, KeyEventArgs e)
        {
            ListViewItem itm = bullpenListView.SelectedItems[0];
            Pitcher ptr = itm.Tag as Pitcher;
            team.RemovePitcher(ptr);
            bullpenListView.Items.Remove(itm);

        }

    }
}
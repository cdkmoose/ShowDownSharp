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
    public partial class SelectTeamsForm : Form
    {
        private League league;
        public SelectTeamsForm()
        {
            InitializeComponent();
        }

        public Team HomeTeam
        {
            get
            {
                return (Team)homeTextBox.Tag;
            }
            set
            {
                homeTextBox.Tag = value;
                homeTextBox.Text = value.ToString();

                teamListBox.Items.Remove(value);

                EnableOK();
            }
        }

        public Team VisitingTeam
        {
            get
            {
                return (Team)visitorTextBox.Tag;
            }
            set
            {
                visitorTextBox.Tag = value;
                visitorTextBox.Text = value.ToString();

                teamListBox.Items.Remove(value);

                EnableOK();
            }
        }

        public SelectTeamsForm(League activeLeague)
        {
            InitializeComponent();

            league = activeLeague;

            FillTeamList(league);
        }

        private void FillTeamList(League league)
        {
            foreach (Team team in league.Teams)
            {
                teamListBox.Items.Add(team);
            }
        }

        private void visitorTextBox_DragDrop(object sender, DragEventArgs e)
        {
            Team team = (Team)e.Data.GetData(typeof(Team));

            visitorTextBox.Text = team.ToString();
            if (visitorTextBox.Tag != null)
            {
                teamListBox.Items.Add(visitorTextBox.Tag);
            }

            visitorTextBox.Tag = team;

            teamListBox.Items.Remove(team);

            EnableOK();

        }

        private void visitorTextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void homeTextBox_DragDrop(object sender, DragEventArgs e)
        {
            Team team = (Team)e.Data.GetData(typeof(Team));

            homeTextBox.Text = team.ToString();
            if (homeTextBox.Tag != null)
            {
                teamListBox.Items.Add(homeTextBox.Tag);
            }

            homeTextBox.Tag = team;

            teamListBox.Items.Remove(team);

            EnableOK();
        }

        private void homeTextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

        }

        private void EnableOK()
        {
            if (homeTextBox.Tag != null && visitorTextBox.Tag != null)
            {
                okButton.Enabled = true;
            }
            else
            {
                okButton.Enabled = false;
            }
        }

        private void teamListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int indexOfItem = teamListBox.IndexFromPoint(e.X, e.Y);
            if (indexOfItem >= 0 && indexOfItem
              < teamListBox.Items.Count)  // check we clicked down on a string
            {
                // Set allowed DragDropEffect to Copy selected 
                // from DragDropEffects enumberation of None, Move, All etc.
                teamListBox.DoDragDrop((Team) teamListBox.Items[indexOfItem], DragDropEffects.Move);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void teamListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teamListBox.SelectedIndex == -1)
            {
                moveToHomeButton.Enabled = moveToVisitorButton.Enabled = false;
            }
            else
            {
                moveToHomeButton.Enabled = moveToVisitorButton.Enabled = true;
            }
        }

        private void moveToVisitorButton_Click(object sender, EventArgs e)
        {
            if (visitorTextBox.Tag != null)
            {
                teamListBox.Items.Add(visitorTextBox.Tag);
            }

            visitorTextBox.Tag = teamListBox.SelectedItem;
            visitorTextBox.Text = visitorTextBox.Tag.ToString();
            teamListBox.Items.Remove(teamListBox.SelectedItem);

            EnableOK();
        }

        private void moveToHomeButton_Click(object sender, EventArgs e)
        {
            if (homeTextBox.Tag != null)
            {
                teamListBox.Items.Add(homeTextBox.Tag);
            }

            homeTextBox.Tag = teamListBox.SelectedItem;
            homeTextBox.Text = homeTextBox.Tag.ToString();

            teamListBox.Items.Remove(teamListBox.SelectedItem);

            EnableOK();
        }
    }
}

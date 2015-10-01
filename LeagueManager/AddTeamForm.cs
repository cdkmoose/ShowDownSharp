using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DS.Showdown.LeagueManager
{
    public partial class AddTeamForm : Form
    {
        private string teamName;
        private string teamAbbr;

        public AddTeamForm()
        {
            teamName = "";
            teamAbbr = "";
            InitializeComponent();
        }

        public string TeamName
        {
            get
            {
                return teamName;
            }

            set
            {
                teamName = value;
                teamNameTextBox.Text = teamName;
            }
        }

        public string TeamAbbrev
        {
            get
            {
                return teamAbbr;
            }

            set
            {
                teamAbbr = value;
                teamAbbrTextBox.Text = teamAbbr;

            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            teamName = teamNameTextBox.Text;
            teamAbbr = teamAbbrTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}

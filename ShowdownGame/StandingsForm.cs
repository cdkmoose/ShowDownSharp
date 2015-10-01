using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DS.Showdown.Game
{
    public partial class StandingsForm : Form
    {
        public StandingsForm()
        {
            InitializeComponent();
        }

        public string StandingsText
        {
            set
            {
                standingsTextBox.Text = value;
                standingsTextBox.Select(0, 0);
            }
        }
    }
}

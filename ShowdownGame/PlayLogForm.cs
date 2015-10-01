using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DS.Showdown.Game
{
	public partial class PlayLogForm : Form
	{
		public PlayLogForm()
		{
			InitializeComponent();
		}

        public string WindowTitle
        {
            set
            {
                Text = value;
            }
        }

		public string LogText
		{
			set
			{
				txtPlayLog.Text = value;
				txtPlayLog.Select(0, 0);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
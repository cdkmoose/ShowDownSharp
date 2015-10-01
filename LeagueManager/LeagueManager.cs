using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DS.Showdown.ObjectLibrary;
using DS.Showdown.DbLibrary;
using DS.Showdown.Engines;
using DS.Showdown.SharedForms;
using WeifenLuo.WinFormsUI.Docking;

namespace DS.Showdown.LeagueManager
{
	public partial class LeagueManager : Form
	{
        private League league;
        private DBEngine dbEngine;
        private PlayerListForm playerForm;
        private TeamListForm teamForm;
        private LeagueListForm leagueForm;

		public LeagueManager()
		{
            dbEngine = new DBEngine();
            Form frm = new SplashForm();

            Cursor = Cursors.WaitCursor;

            frm.Show();
            frm.Update();

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch() ;

            watch.Start();
            league = dbEngine.LoadLeague3(1);
            watch.Stop();

            MessageBox.Show(watch.Elapsed.ToString());

            InitializeComponent();

            frm.Hide();
            Cursor = Cursors.Default;

            //InitializeComponent();

            DockPanel dockPanel = new DockPanel();
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.BackColor = Color.Beige;
            Controls.Add(dockPanel);
            dockPanel.BringToFront();

            leagueForm = new LeagueListForm();
            leagueForm.ShowHint = DockState.Document;
            leagueForm.Show(dockPanel);

            playerForm = new PlayerListForm();
            playerForm.ShowHint = DockState.Document;
            playerForm.Show(dockPanel);

            teamForm = new TeamListForm();
            teamForm.ShowHint = DockState.Document;
            teamForm.Show(dockPanel);

        }

        private void LeagueManager_Load(object sender, EventArgs e)
        {

            playerForm.BindPlayers(league);
            teamForm.BindTeams(league);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            teamForm.SaveChanges();
            leagueForm.SaveChanges();
        }

	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using DS.Showdown.ObjectLibrary;
using DS.Showdown.SharedForms;

namespace DS.Showdown.LeagueManager
{
    public partial class TeamListForm : DockContent
    {
        private bool isModified;
        private List<Team> teams;
        SortableBindingList<Team> bindingList;


        public TeamListForm()
        {
            isModified = false;

            InitializeComponent();
            teamGrid.AutoGenerateColumns = false;
        }

        public void BindTeams(League lge)
        {
            teamGrid.Columns["abbrevColumn"].DataPropertyName = "Abbrev";
            teamGrid.Columns["teamNameColumn"].DataPropertyName = "Name";
            teamGrid.Columns["winsColumn"].DataPropertyName = "Wins";
            teamGrid.Columns["lossesColumn"].DataPropertyName = "Losses";

            bindingList = new SortableBindingList<Team>();

            teams = lge.Teams;

            foreach (Team tm in teams)
            {
                bindingList.Add(tm);
            }

            teamGrid.DataSource = bindingList;
        }

        private void teamGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Team team = (Team)teamGrid.Rows[e.RowIndex].DataBoundItem;

            ShowTeam(team);
        }

        private void ShowTeam(Team team)
        {
            TeamForm frm = new TeamForm(team);
            frm.ShowDialog();
        }

        private void teamGrid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        public bool IsModified
        {
            get
            {
                return isModified;
            }
            set
            {
                isModified = value;

                if (isModified == true)
                {
                    
                }
            }
        }

        private void teamGrid_DragOver(object sender, DragEventArgs e)
        {
            DataGridView.HitTestInfo info = GetHitTestInfo(e.X, e.Y);

            if (info.RowIndex >= 0  && info.RowIndex < teamGrid.RowCount)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void teamGrid_DragDrop(object sender, DragEventArgs e)
        {
            Pitcher pitcher;
            Batter batter;
            DataGridView.HitTestInfo info = GetHitTestInfo(e.X, e.Y);

            Team team = (Team)teamGrid.Rows[info.RowIndex].DataBoundItem;
            
            batter = (Batter)e.Data.GetData(typeof(Batter));
            pitcher = (Pitcher)e.Data.GetData(typeof(Pitcher));

            if (batter != null)
            {
                batter.IsOnATeam = true;
                team.AddBatter(batter);
                IsModified = true;
            }

            if (pitcher is Pitcher)
            {
                pitcher.IsOnATeam = true;
                team.AddPitcher(pitcher);
                IsModified = true;
            }
        }

        private DataGridView.HitTestInfo GetHitTestInfo(int x, int y)
        {
            Point p = teamGrid.PointToClient(new Point(x, y));
            DataGridView.HitTestInfo info = teamGrid.HitTest(p.X, p.Y);

            return info;
        }

        public void SaveChanges()
        {
            if (teams == null)
            {
                return;
            }

            foreach (Team team in teams)
            {
                if (team.IsModified)
                {
                    team.SaveTeam();
                }
            }

        }

        private void editLineupMenuItem_Click(object sender, EventArgs e)
        {
            if (teamGrid.SelectedRows.Count == 1)
            {
                Team tm = (Team)teamGrid.SelectedRows[0].DataBoundItem;

                TeamLineupForm frm = new TeamLineupForm(tm);
                frm.GameMode = false;

                frm.ShowDialog();
            }

        }

        private void viewTeamMenuItem_Click(object sender, EventArgs e)
        {
            if (teamGrid.SelectedRows.Count == 1)
            {
                Team tm = (Team)teamGrid.SelectedRows[0].DataBoundItem;
                ShowTeam(tm);
            }

        }

        private void newTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTeamForm frm = new AddTeamForm();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Team tm = new Team(frm.TeamName, 1, frm.TeamAbbrev);
                teams.Add(tm);
                bindingList.Add(tm);

            }
        }

    }
}

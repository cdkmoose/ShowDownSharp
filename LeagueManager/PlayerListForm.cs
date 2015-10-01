using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using DS.Showdown.ObjectLibrary;
using DS.Showdown.SharedForms;
using DS.Showdown.Widgets;

namespace DS.Showdown.LeagueManager
{
    public partial class PlayerListForm : DockContent
    {

        public PlayerListForm()
        {
            InitializeComponent();
            playerGrid.AutoGenerateColumns = false;

        }

        public void BindPlayers(League lge)
        {
            playerGrid.Columns[0].DataPropertyName = "Name";
            playerGrid.Columns[1].DataPropertyName = "CardSeason";
            playerGrid.Columns[2].DataPropertyName = "CardTeam";
            playerGrid.Columns[3].DataPropertyName = "Points";
            playerGrid.Columns[4].DataPropertyName = "PositionText";
            playerGrid.Columns[5].DataPropertyName = "KeyStatLine";

            SortableBindingList<Player> players = new SortableBindingList<Player>();

            foreach (Player plyr in lge.PlayerList.Values)
            {
                players.Add(plyr);
            }

            playerGrid.DataSource = players;
        }

        private void playerGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = playerGrid.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0 && info.RowIndex < playerGrid.RowCount)
                {
                    Player plyr = (Player)playerGrid.Rows[info.RowIndex].DataBoundItem;
                    if (plyr.IsOnATeam == false)
                    {
                        playerGrid.DoDragDrop(plyr, DragDropEffects.Copy);
                    }
                }
            }
        }

        private void toolStripViewPlayer_Click(object sender, EventArgs e)
        {
            if (playerGrid.SelectedRows.Count == 1)
            {
                Player player = (Player)playerGrid.SelectedRows[0].DataBoundItem;

                if (player is Batter)
                {
                    ShowBatterForm frm = new ShowBatterForm((Batter)player);
                    frm.ShowDialog();
                }
                else if (player is Pitcher)
                {
                    ShowPitcherForm frm = new ShowPitcherForm((Pitcher)player);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid player type.");
                }
            }
        }

    }
}

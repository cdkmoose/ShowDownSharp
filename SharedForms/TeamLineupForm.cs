using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DS.Showdown.ObjectLibrary;

namespace DS.Showdown.SharedForms
{
    public partial class TeamLineupForm : Form
    {
        private Team team;
        private bool lineupModified = false;
        private bool pitcherModified = false;
        private ListViewItem selectedItem = null;
        public bool GameMode { get; set; }

        public TeamLineupForm()
        {
            InitializeComponent();
        }

        public TeamLineupForm(Team tm)
        {
            InitializeComponent();

            team = tm;
            GameMode = false;

            LoadLists(tm);
        }

        public TeamLineupForm(Team tm, bool useGameMode)
        {
            InitializeComponent();

            team = tm;
            GameMode = useGameMode;

            LoadLists(tm);
        }

        public bool LineupModified
        {
            get
            {
                return lineupModified;
            }
        }

        public bool PitcherModified
        {
            get
            {
                return pitcherModified;
            }
        }

        private void RemoveFromBatterList(Batter btr)
        {
            foreach (ListViewItem lvi in battersListView.Items)
            {
                if (lvi.Tag == btr)
                {
                    battersListView.Items.Remove(lvi);
                    break;
                }
            }
        }

        private void AddToBatterList(Batter btr)
        {
            ListViewItem lvi = new ListViewItem(btr.Name);
            string posString = EnumHelpers.PositionToString(btr.PrimaryPosition);
            if (btr.SecondaryPosition != Position.None)
            {
                posString += ", " + EnumHelpers.PositionToString(btr.SecondaryPosition);
            }
            lvi.SubItems.Add(posString);
            lvi.Tag = btr;
            battersListView.Items.Add(lvi);
        }


        private void LoadLists(Team tm)
        {
            foreach (Batter btr in tm.Hitters)
            {
                AddToBatterList(btr);
            }

            int slotNum = 1;

            foreach (BattingOrderSlot slot in tm.BattingOrder)
            {
                ListViewItem lvi;

                lvi = new ListViewItem(slotNum.ToString());

                if (slot == null)
                {
                    lvi.SubItems.Add("[empty]");
                    lvi.SubItems.Add("");

                    lineupListView.Items.Add(lvi);
                }
                else
                {
                    lvi.SubItems.Add(slot.Hitter.Name);
                    lvi.SubItems.Add(EnumHelpers.PositionToString(slot.FieldingPosition));
                    lvi.Tag = slot.Hitter;

                    lineupListView.Items.Add(lvi);
                    RemoveFromBatterList(slot.Hitter);
                }
                slotNum++;

            }

            foreach (Pitcher ptr in tm.StartingPitchers)
            {
                if (GameMode == true && team.CurrentPitcher.ID == ptr.ID)
                {
                    pitcherTextbox.Tag = ptr;
                    pitcherTextbox.Text = ptr.Name;
                }
                else
                {
                    ListViewItem lvi = new ListViewItem(ptr.Name);
                    lvi.Tag = ptr;
                    startersListView.Items.Add(lvi);
                }
            }

            foreach (Pitcher ptr in tm.BullPen)
            {
                if (GameMode == true && team.CurrentPitcher.ID == ptr.ID)
                {
                    pitcherTextbox.Tag = ptr;
                    pitcherTextbox.Text = ptr.Name;
                }
                else
                {
                    ListViewItem lvi = new ListViewItem(ptr.Name);
                    lvi.Tag = ptr;
                    lvi.SubItems.Add(ptr.PitcherRole.ToString());
                    bullpenListView.Items.Add(lvi);
                }
            }

            return;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            BattingOrderSlot slot;
            int idx;

            if (lineupModified == true)
            {
                foreach (ListViewItem itm in lineupListView.Items)
                {
                    idx = int.Parse(itm.Text);

                    if (itm.SubItems[1].ToString() == "[empty]")
                    {
                        team.SetLineupSlot(null, idx - 1);
                    }
                    else
                    {
                        slot = new BattingOrderSlot(); 
                        slot.Hitter = (Batter) itm.Tag;
                        slot.FieldingPosition = EnumHelpers.StringToPosition(itm.SubItems[2].Text);
                        team.SetLineupSlot(slot, idx - 1);
                    }
                }
            }

            if (pitcherModified == true)
            {
                team.CurrentPitcher.PlayedThisGame = true;
                
            }

            DialogResult = DialogResult.OK;
            Close();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EnableButtons()
        {
            // move to lineup
            if (battersListView.SelectedItems.Count == 0)
            {
                addToOrderButton.Enabled = false;
            }
            else
            {
                if (team.LineupIsComplete == false || lineupListView.SelectedItems.Count == 1)
                {
                    addToOrderButton.Enabled = true;
                }
                else
                {
                    addToOrderButton.Enabled = false;
                }
            }

            // remove from lineup
            if (lineupListView.SelectedItems.Count == 0)
            {
                removeFromOrderButton.Enabled = false;
            }
            else
            {
                ListViewItem lvi = lineupListView.SelectedItems[0];

                if (lvi.Tag != null)
                {
                    removeFromOrderButton.Enabled = true;
                }
                else
                {
                    removeFromOrderButton.Enabled = false;
                }
            }

        }

        private void lineupListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
            
        }

        private void battersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void addToOrderButton_Click(object sender, EventArgs e)
        {
            Batter btr = (Batter) battersListView.SelectedItems[0].Tag;

            if (btr.PlayedThisGame == true && GameMode == true)
            {
                // in game mode, players cannot return to the lineup
                return;
            }

            if (lineupListView.SelectedItems.Count == 1)
            {
                ListViewItem lvi = lineupListView.SelectedItems[0];

                if (lvi.Tag != null)
                {
                    AddToBatterList((Batter)lvi.Tag);
                }

                lvi.Tag = btr;
                lvi.SubItems[1] = new ListViewItem.ListViewSubItem(lvi, btr.Name);
                lvi.SubItems[2] = new ListViewItem.ListViewSubItem(lvi, EnumHelpers.PositionToString(btr.PrimaryPosition));
            }
            else  // find first empy slot and position there
            {
                foreach (ListViewItem lviSlot in lineupListView.Items)
                {
                    if (lviSlot.SubItems[1].Text == "[empty]")
                    {
                        lviSlot.Tag = btr;
                        lviSlot.SubItems[1] = new ListViewItem.ListViewSubItem(lviSlot, btr.Name);
                        lviSlot.SubItems[2] = new ListViewItem.ListViewSubItem(lviSlot, EnumHelpers.PositionToString(btr.PrimaryPosition));

                        break;
                    }
                }
            }

            RemoveFromBatterList(btr);
            lineupModified = true;

        }

        private void removeFromOrderButton_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lineupListView.SelectedItems[0];
            int index = lineupListView.SelectedItems[0].Index;

            AddToBatterList((Batter)lvi.Tag);

            ListViewItem newItem = new ListViewItem((index + 1).ToString());
            newItem.SubItems.Add("[empty]");
            newItem.SubItems.Add("");

            lineupListView.Items[index] = newItem;

        }

        private void positionMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Position pos = (Position)e.ClickedItem.Tag;
            selectedItem.SubItems[2] = new ListViewItem.ListViewSubItem(selectedItem, EnumHelpers.PositionToString(pos));
            lineupModified = true;
        }

        private void lineupListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = new Point(e.X, e.Y);
                ListViewHitTestInfo hit = lineupListView.HitTest(pt);
                selectedItem = hit.Item;

                if (selectedItem == null)
                {
                    return;
                }
                List<Position> posList = ((Batter)selectedItem.Tag).PlayablePositions;
                positionMenuStrip.Items.Clear();

                ToolStripMenuItem itm = new ToolStripMenuItem(EnumHelpers.PositionToString(Position.DesignatedHitter));
                itm.Tag = Position.DesignatedHitter;
                positionMenuStrip.Items.Add(itm);

                foreach (Position pos in posList)
                {
                    itm = new ToolStripMenuItem(EnumHelpers.PositionToString(pos));
                    itm.Tag = pos;

                    positionMenuStrip.Items.Add(itm);
                }

                positionMenuStrip.Show(lineupListView, pt);
            }

        }

        private void bullpenListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Pitcher ptr = (Pitcher)bullpenListView.SelectedItems[0].Tag;
            bullpenListView.DoDragDrop(ptr, DragDropEffects.Move);
        }

        private void pitcherTextbox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void pitcherTextbox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void pitcherTextbox_DragDrop(object sender, DragEventArgs e)
        {
            Pitcher curPitcher = (Pitcher)pitcherTextbox.Tag;
            Pitcher newPitcher = (Pitcher)e.Data.GetData(typeof(Pitcher));

            pitcherTextbox.Tag = newPitcher;
            pitcherTextbox.Text = newPitcher.Name;
            team.CurrentPitcher = newPitcher;

            ListViewItem lvi = new ListViewItem(curPitcher.Name);
            lvi.Tag = curPitcher;

            if (curPitcher.PitcherRole == PitcherType.Starter)
            {
                startersListView.Items.Add(lvi);
            }
            else
            {
                lvi.SubItems.Add(curPitcher.PitcherRole.ToString());
                bullpenListView.Items.Add(lvi);
            }
            pitcherModified = true;
        }
    }
}

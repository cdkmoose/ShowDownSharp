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
    public partial class TopTenForm : Form
    {
        private League statLeague;
        public TopTenForm()
        {
            InitializeComponent();
        }

        public League StatLeague
        {
            set
            {
                statLeague = value;
            }
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            statsList.Items.Clear();

            ProcessStats(statCombo.SelectedItem.ToString(), statsList);


        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProcessStats(string statName, ListView statsListView)
        {
            statsList.Columns[2].Text = statName;

            switch (statName)
            {
                case "Doubles":
                    ProcessDoubles(statsListView);
                    break;

                case "Triples":
                    ProcessTriples(statsListView);
                    break;

                case "HomeRuns":
                    ProcessHomeRuns(statsListView);
                    break;

                case "Hits":
                    ProcessHits(statsListView);
                    break;

                case "RBIs":
                    ProcessRBIs(statsListView);
                    break;

                case "Average":
                    ProcessAverage(statsListView);
                    break;

                case "Slg Pct":
                    ProcessSlugging(statsListView);
                    break;

                case "Wins":
                    ProcessWins(statsListView);
                    break;

                case "Losses":
                    ProcessLosses(statsListView);
                    break;

                case "Saves":
                    ProcessSaves(statsListView);
                    break;

                case "ERA":
                    ProcessERA(statsListView);
                    break;

                case "Strikeouts":
                    ProcessStrikeouts(statsListView);
                    break;

                case "Walks":
                    ProcessWalks(statsListView);
                    break;

                default:
                    break;
            }

        }

        private void ProcessDoubles(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.Doubles > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.Doubles);
                }
            }

            FillListView(leaders);
        }

        private void ProcessTriples(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.Triples > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.Triples);
                }
            }

            FillListView(leaders);
        }

        private void ProcessHomeRuns(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.HomeRuns > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.HomeRuns);
                }
            }

            FillListView(leaders);
        }

        private void ProcessHits(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.Hits > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.Hits);
                }
            }

            FillListView(leaders);
        }

        private void ProcessRBIs(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.RBIs > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.RBIs);
                }
            }

            FillListView(leaders);
        }

        private void ProcessAverage(ListView statsView)
        {
            DescendingTopTenList<double> leaders = new DescendingTopTenList<double>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.BattingAvg > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.BattingAvg);
                }
            }

            FillListView(leaders, "0.000");
        }

        private void ProcessERA(ListView statsView)
        {
            AscendingTopTenList<double> leaders = new AscendingTopTenList<double>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Pitcher ptr in team.StartingPitchers)
                {
                    if (ptr.SeasonStats.InningsPitched > 0.0F)
                        leaders.AddPlayer(team.Abbrev, ptr.Name, ptr.SeasonStats.EarnedRunAvg);
                }
            }

            FillListView(leaders, "0.00");
        }

        private void ProcessWins(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Pitcher ptr in team.StartingPitchers)
                {
                    if (ptr.SeasonStats.Wins > 0)
                        leaders.AddPlayer(team.Abbrev, ptr.Name, ptr.SeasonStats.Wins);
                }
            }

            FillListView(leaders);
        }

        private void ProcessSaves(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Pitcher ptr in team.BullPen)
                {
                    if (ptr.SeasonStats.Saves > 0)
                        leaders.AddPlayer(team.Abbrev, ptr.Name, ptr.SeasonStats.Saves);
                }
            }

            FillListView(leaders);
        }

        private void ProcessLosses(ListView statsView)
        {
            AscendingTopTenList<int> leaders = new AscendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Pitcher ptr in team.StartingPitchers)
                {
                    if (ptr.SeasonStats.Losses > 0)
                        leaders.AddPlayer(team.Abbrev, ptr.Name, ptr.SeasonStats.Losses);
                }
            }

            FillListView(leaders);
        }

        private void ProcessStrikeouts(ListView statsView)
        {
            DescendingTopTenList<int> leaders = new DescendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Pitcher ptr in team.StartingPitchers)
                {
                    if (ptr.SeasonStats.StrikeOuts > 0)
                        leaders.AddPlayer(team.Abbrev, ptr.Name, ptr.SeasonStats.StrikeOuts);
                }
            }

            FillListView(leaders);
        }

        private void ProcessWalks(ListView statsView)
        {
            AscendingTopTenList<int> leaders = new AscendingTopTenList<int>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Pitcher ptr in team.StartingPitchers)
                {
                    if (ptr.SeasonStats.Walks > 0)
                        leaders.AddPlayer(team.Abbrev, ptr.Name, ptr.SeasonStats.Walks);
                }
            }

            FillListView(leaders);

        }

        private void ProcessSlugging(ListView statsView)
        {
            DescendingTopTenList<double> leaders = new DescendingTopTenList<double>();

            foreach (Team team in statLeague.Teams)
            {
                foreach (Batter btr in team.Hitters)
                {
                    if (btr.SeasonStats.SluggingPct > 0)
                        leaders.AddPlayer(team.Abbrev, btr.Name, btr.SeasonStats.SluggingPct);
                }
            }

            FillListView(leaders, "0.000");
        }

        private void FillListView(TopTenList<Double> topten, string format)
        {
            foreach (TopTenItem<double> player in topten.TopTen)
            {
                if (player.Score == 0 && topten is DescendingTopTenList<Double>)
                {
                    break;
                }
                ListViewItem lvi = new ListViewItem(player.PlayerName);
                lvi.SubItems.Add(player.TeamName);
                lvi.SubItems.Add(player.Score.ToString(format));

                statsList.Items.Add(lvi);
            }

        }

        private void FillListView(TopTenList<int> topten)
        {
            foreach (TopTenItem<int> player in topten.TopTen)
            {
                if (player.Score == 0 && topten is DescendingTopTenList<int>)
                {
                    break;
                }
                ListViewItem lvi = new ListViewItem(player.PlayerName);
                lvi.SubItems.Add(player.TeamName);
                lvi.SubItems.Add(player.Score.ToString());

                statsList.Items.Add(lvi);
            }

        }
    }
}

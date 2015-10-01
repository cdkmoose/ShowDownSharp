namespace DS.Showdown.SharedForms
{
	partial class TeamForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bullpenPanel = new DS.Showdown.Widgets.CollapsiblePanel();
            this.bullpenListView = new System.Windows.Forms.ListView();
            this.pitcher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rwins = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rlosses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rsaves = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rera = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.starterPanel = new DS.Showdown.Widgets.CollapsiblePanel();
            this.starterListView = new System.Windows.Forms.ListView();
            this.starters = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wins = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.losses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saves = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.era = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.batterPanel = new DS.Showdown.Widgets.CollapsiblePanel();
            this.batterListView = new System.Windows.Forms.ListView();
            this.player = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.average = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.slug = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.bullpenPanel.SuspendLayout();
            this.starterPanel.SuspendLayout();
            this.batterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(504, 551);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.bullpenPanel);
            this.panel2.Controls.Add(this.starterPanel);
            this.panel2.Controls.Add(this.batterPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(8, 8);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(488, 535);
            this.panel2.TabIndex = 0;
            // 
            // bullpenPanel
            // 
            this.bullpenPanel.Collapsed = false;
            this.bullpenPanel.Controls.Add(this.bullpenListView);
            this.bullpenPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bullpenPanel.Location = new System.Drawing.Point(3, 378);
            this.bullpenPanel.Name = "bullpenPanel";
            this.bullpenPanel.Size = new System.Drawing.Size(482, 155);
            this.bullpenPanel.TabIndex = 2;
            this.bullpenPanel.Text = "Bullpen";
            // 
            // bullpenListView
            // 
            this.bullpenListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pitcher,
            this.rwins,
            this.rlosses,
            this.rsaves,
            this.rera});
            this.bullpenListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bullpenListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.bullpenListView.Location = new System.Drawing.Point(0, 26);
            this.bullpenListView.MultiSelect = false;
            this.bullpenListView.Name = "bullpenListView";
            this.bullpenListView.Size = new System.Drawing.Size(482, 129);
            this.bullpenListView.TabIndex = 1;
            this.bullpenListView.UseCompatibleStateImageBehavior = false;
            this.bullpenListView.View = System.Windows.Forms.View.Details;
            this.bullpenListView.DoubleClick += new System.EventHandler(this.bullpenListView_DoubleClick);
            this.bullpenListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bullpenListView_KeyDown);
            // 
            // pitcher
            // 
            this.pitcher.Text = "Relievers";
            this.pitcher.Width = 250;
            // 
            // rwins
            // 
            this.rwins.Text = "W";
            this.rwins.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rwins.Width = 30;
            // 
            // rlosses
            // 
            this.rlosses.Text = "L";
            this.rlosses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rlosses.Width = 30;
            // 
            // rsaves
            // 
            this.rsaves.Text = "S";
            this.rsaves.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rsaves.Width = 30;
            // 
            // rera
            // 
            this.rera.Text = "ERA";
            this.rera.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // starterPanel
            // 
            this.starterPanel.Collapsed = false;
            this.starterPanel.Controls.Add(this.starterListView);
            this.starterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.starterPanel.Location = new System.Drawing.Point(3, 243);
            this.starterPanel.Name = "starterPanel";
            this.starterPanel.Size = new System.Drawing.Size(482, 135);
            this.starterPanel.TabIndex = 1;
            this.starterPanel.Text = "Starters";
            // 
            // starterListView
            // 
            this.starterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.starters,
            this.wins,
            this.losses,
            this.saves,
            this.era});
            this.starterListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.starterListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.starterListView.Location = new System.Drawing.Point(0, 25);
            this.starterListView.MultiSelect = false;
            this.starterListView.Name = "starterListView";
            this.starterListView.Size = new System.Drawing.Size(482, 110);
            this.starterListView.TabIndex = 1;
            this.starterListView.UseCompatibleStateImageBehavior = false;
            this.starterListView.View = System.Windows.Forms.View.Details;
            this.starterListView.DoubleClick += new System.EventHandler(this.starterListView_DoubleClick);
            this.starterListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.starterListView_KeyDown);
            // 
            // starters
            // 
            this.starters.Text = "Pitcher";
            this.starters.Width = 250;
            // 
            // wins
            // 
            this.wins.Text = "W";
            this.wins.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.wins.Width = 30;
            // 
            // losses
            // 
            this.losses.Text = "L";
            this.losses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.losses.Width = 30;
            // 
            // saves
            // 
            this.saves.Text = "S";
            this.saves.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.saves.Width = 30;
            // 
            // era
            // 
            this.era.Text = "ERA";
            this.era.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // batterPanel
            // 
            this.batterPanel.Collapsed = false;
            this.batterPanel.Controls.Add(this.batterListView);
            this.batterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.batterPanel.Location = new System.Drawing.Point(3, 3);
            this.batterPanel.Name = "batterPanel";
            this.batterPanel.Size = new System.Drawing.Size(482, 240);
            this.batterPanel.TabIndex = 0;
            this.batterPanel.Text = "Batters";
            // 
            // batterListView
            // 
            this.batterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.player,
            this.position,
            this.average,
            this.slug});
            this.batterListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.batterListView.FullRowSelect = true;
            this.batterListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.batterListView.Location = new System.Drawing.Point(0, 25);
            this.batterListView.MultiSelect = false;
            this.batterListView.Name = "batterListView";
            this.batterListView.Size = new System.Drawing.Size(482, 215);
            this.batterListView.TabIndex = 1;
            this.batterListView.UseCompatibleStateImageBehavior = false;
            this.batterListView.View = System.Windows.Forms.View.Details;
            this.batterListView.DoubleClick += new System.EventHandler(this.batterListView_DoubleClick);
            this.batterListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.batterListView_KeyDown);
            // 
            // player
            // 
            this.player.Text = "Batter";
            this.player.Width = 250;
            // 
            // position
            // 
            this.position.Text = "Position";
            this.position.Width = 100;
            // 
            // average
            // 
            this.average.Text = "Avg.";
            // 
            // slug
            // 
            this.slug.Text = "Slug Pct";
            // 
            // TeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(504, 551);
            this.Controls.Add(this.panel1);
            this.Name = "TeamForm";
            this.Text = "Team Roster";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.bullpenPanel.ResumeLayout(false);
            this.starterPanel.ResumeLayout(false);
            this.batterPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private DS.Showdown.Widgets.CollapsiblePanel bullpenPanel;
		private DS.Showdown.Widgets.CollapsiblePanel starterPanel;
		private DS.Showdown.Widgets.CollapsiblePanel batterPanel;
		private System.Windows.Forms.ListView batterListView;
		private System.Windows.Forms.ColumnHeader player;
		private System.Windows.Forms.ListView bullpenListView;
		private System.Windows.Forms.ColumnHeader pitcher;
		private System.Windows.Forms.ListView starterListView;
		private System.Windows.Forms.ColumnHeader starters;
		private System.Windows.Forms.ColumnHeader position;
		private System.Windows.Forms.ColumnHeader average;
		private System.Windows.Forms.ColumnHeader slug;
		private System.Windows.Forms.ColumnHeader wins;
		private System.Windows.Forms.ColumnHeader losses;
		private System.Windows.Forms.ColumnHeader saves;
		private System.Windows.Forms.ColumnHeader era;
        private System.Windows.Forms.ColumnHeader rwins;
        private System.Windows.Forms.ColumnHeader rlosses;
        private System.Windows.Forms.ColumnHeader rsaves;
        private System.Windows.Forms.ColumnHeader rera;

	}
}
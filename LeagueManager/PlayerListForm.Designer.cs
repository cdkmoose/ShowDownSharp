namespace DS.Showdown.LeagueManager
{
    partial class PlayerListForm
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripViewPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.playerStatusBar = new System.Windows.Forms.StatusStrip();
            this.filterStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.playerGrid = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyStatLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.playerStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripViewPlayer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 26);
            // 
            // toolStripViewPlayer
            // 
            this.toolStripViewPlayer.Name = "toolStripViewPlayer";
            this.toolStripViewPlayer.Size = new System.Drawing.Size(134, 22);
            this.toolStripViewPlayer.Text = "&View Player";
            this.toolStripViewPlayer.Click += new System.EventHandler(this.toolStripViewPlayer_Click);
            // 
            // playerStatusBar
            // 
            this.playerStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterStatusText,
            this.showAllLink});
            this.playerStatusBar.Location = new System.Drawing.Point(0, 537);
            this.playerStatusBar.Name = "playerStatusBar";
            this.playerStatusBar.Size = new System.Drawing.Size(826, 22);
            this.playerStatusBar.TabIndex = 2;
            this.playerStatusBar.Text = "statusStrip1";
            // 
            // filterStatusText
            // 
            this.filterStatusText.Name = "filterStatusText";
            this.filterStatusText.Size = new System.Drawing.Size(44, 17);
            this.filterStatusText.Text = "Players";
            // 
            // showAllLink
            // 
            this.showAllLink.Name = "showAllLink";
            this.showAllLink.Size = new System.Drawing.Size(0, 17);
            // 
            // playerGrid
            // 
            this.playerGrid.AllowDrop = true;
            this.playerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.Year,
            this.Team,
            this.pointsColumn,
            this.Position,
            this.KeyStatLine});
            this.playerGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.playerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerGrid.Location = new System.Drawing.Point(0, 0);
            this.playerGrid.MultiSelect = false;
            this.playerGrid.Name = "playerGrid";
            this.playerGrid.RowHeadersVisible = false;
            this.playerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.playerGrid.Size = new System.Drawing.Size(826, 537);
            this.playerGrid.TabIndex = 3;
            this.playerGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playerGrid_MouseDown);
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Year
            // 
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Team
            // 
            this.Team.HeaderText = "Team";
            this.Team.Name = "Team";
            this.Team.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pointsColumn
            // 
            this.pointsColumn.HeaderText = "Points";
            this.pointsColumn.Name = "pointsColumn";
            this.pointsColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Position
            // 
            this.Position.HeaderText = "Position";
            this.Position.Name = "Position";
            this.Position.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // KeyStatLine
            // 
            this.KeyStatLine.HeaderText = "Key Stats";
            this.KeyStatLine.Name = "KeyStatLine";
            this.KeyStatLine.ReadOnly = true;
            this.KeyStatLine.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KeyStatLine.Width = 130;
            // 
            // PlayerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 559);
            this.Controls.Add(this.playerGrid);
            this.Controls.Add(this.playerStatusBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PlayerListForm";
            this.Text = "Players";
            this.contextMenuStrip1.ResumeLayout(false);
            this.playerStatusBar.ResumeLayout(false);
            this.playerStatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripViewPlayer;
        private System.Windows.Forms.StatusStrip playerStatusBar;
        private System.Windows.Forms.DataGridView playerGrid;
        private System.Windows.Forms.ToolStripStatusLabel filterStatusText;
        private System.Windows.Forms.ToolStripStatusLabel showAllLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyStatLine;
    }
}
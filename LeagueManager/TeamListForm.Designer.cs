namespace DS.Showdown.LeagueManager
{
    partial class TeamListForm
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
            this.teamGrid = new System.Windows.Forms.DataGridView();
            this.abbrevColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teamNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.winsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lossesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teamsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewTeamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLineupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.teamGrid)).BeginInit();
            this.teamsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // teamGrid
            // 
            this.teamGrid.AllowDrop = true;
            this.teamGrid.AllowUserToAddRows = false;
            this.teamGrid.AllowUserToDeleteRows = false;
            this.teamGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teamGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.abbrevColumn,
            this.teamNameColumn,
            this.winsColumn,
            this.lossesColumn});
            this.teamGrid.ContextMenuStrip = this.teamsContextMenu;
            this.teamGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teamGrid.Location = new System.Drawing.Point(0, 0);
            this.teamGrid.Name = "teamGrid";
            this.teamGrid.ReadOnly = true;
            this.teamGrid.RowHeadersVisible = false;
            this.teamGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.teamGrid.Size = new System.Drawing.Size(733, 508);
            this.teamGrid.TabIndex = 1;
            this.teamGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.teamGrid_CellDoubleClick);
            this.teamGrid.DragOver += new System.Windows.Forms.DragEventHandler(this.teamGrid_DragOver);
            this.teamGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.teamGrid_DragEnter);
            this.teamGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.teamGrid_DragDrop);
            // 
            // abbrevColumn
            // 
            this.abbrevColumn.HeaderText = "Abbrev";
            this.abbrevColumn.Name = "abbrevColumn";
            this.abbrevColumn.ReadOnly = true;
            // 
            // teamNameColumn
            // 
            this.teamNameColumn.HeaderText = "Name";
            this.teamNameColumn.Name = "teamNameColumn";
            this.teamNameColumn.ReadOnly = true;
            // 
            // winsColumn
            // 
            this.winsColumn.HeaderText = "Wins";
            this.winsColumn.Name = "winsColumn";
            this.winsColumn.ReadOnly = true;
            // 
            // lossesColumn
            // 
            this.lossesColumn.HeaderText = "Losses";
            this.lossesColumn.Name = "lossesColumn";
            this.lossesColumn.ReadOnly = true;
            // 
            // teamsContextMenu
            // 
            this.teamsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTeamMenuItem,
            this.editLineupMenuItem,
            this.newTeamToolStripMenuItem});
            this.teamsContextMenu.Name = "teamsContextMenu";
            this.teamsContextMenu.Size = new System.Drawing.Size(134, 70);
            // 
            // viewTeamMenuItem
            // 
            this.viewTeamMenuItem.Name = "viewTeamMenuItem";
            this.viewTeamMenuItem.Size = new System.Drawing.Size(133, 22);
            this.viewTeamMenuItem.Text = "&View Team";
            this.viewTeamMenuItem.Click += new System.EventHandler(this.viewTeamMenuItem_Click);
            // 
            // editLineupMenuItem
            // 
            this.editLineupMenuItem.Name = "editLineupMenuItem";
            this.editLineupMenuItem.Size = new System.Drawing.Size(133, 22);
            this.editLineupMenuItem.Text = "&Edit Lineup";
            this.editLineupMenuItem.Click += new System.EventHandler(this.editLineupMenuItem_Click);
            // 
            // newTeamToolStripMenuItem
            // 
            this.newTeamToolStripMenuItem.Name = "newTeamToolStripMenuItem";
            this.newTeamToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.newTeamToolStripMenuItem.Text = "New Team";
            this.newTeamToolStripMenuItem.Click += new System.EventHandler(this.newTeamToolStripMenuItem_Click);
            // 
            // TeamListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 508);
            this.Controls.Add(this.teamGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TeamListForm";
            this.Text = "Teams";
            ((System.ComponentModel.ISupportInitialize)(this.teamGrid)).EndInit();
            this.teamsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView teamGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn abbrevColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn teamNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn winsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lossesColumn;
        private System.Windows.Forms.ContextMenuStrip teamsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem viewTeamMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLineupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTeamToolStripMenuItem;
    }
}
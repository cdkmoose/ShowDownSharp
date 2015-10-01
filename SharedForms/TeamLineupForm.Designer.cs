namespace DS.Showdown.SharedForms
{
    partial class TeamLineupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.battersListView = new System.Windows.Forms.ListView();
            this.player = new System.Windows.Forms.ColumnHeader();
            this.position = new System.Windows.Forms.ColumnHeader();
            this.startersListView = new System.Windows.Forms.ListView();
            this.starter = new System.Windows.Forms.ColumnHeader();
            this.bullpenListView = new System.Windows.Forms.ListView();
            this.reliever = new System.Windows.Forms.ColumnHeader();
            this.relievertype = new System.Windows.Forms.ColumnHeader();
            this.pitcherTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.lineupListView = new System.Windows.Forms.ListView();
            this.slotNum = new System.Windows.Forms.ColumnHeader();
            this.hitterColumn = new System.Windows.Forms.ColumnHeader();
            this.positionColumn = new System.Windows.Forms.ColumnHeader();
            this.label5 = new System.Windows.Forms.Label();
            this.addToOrderButton = new System.Windows.Forms.Button();
            this.removeFromOrderButton = new System.Windows.Forms.Button();
            this.positionMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hitters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Starters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 348);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Bullpen";
            // 
            // battersListView
            // 
            this.battersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.player,
            this.position});
            this.battersListView.FullRowSelect = true;
            this.battersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.battersListView.HideSelection = false;
            this.battersListView.Location = new System.Drawing.Point(34, 30);
            this.battersListView.MultiSelect = false;
            this.battersListView.Name = "battersListView";
            this.battersListView.Size = new System.Drawing.Size(227, 195);
            this.battersListView.TabIndex = 4;
            this.battersListView.UseCompatibleStateImageBehavior = false;
            this.battersListView.View = System.Windows.Forms.View.Details;
            this.battersListView.SelectedIndexChanged += new System.EventHandler(this.battersListView_SelectedIndexChanged);
            // 
            // player
            // 
            this.player.Text = "Player";
            this.player.Width = 166;
            // 
            // position
            // 
            this.position.Text = "Position";
            this.position.Width = 56;
            // 
            // startersListView
            // 
            this.startersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.starter});
            this.startersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.startersListView.Location = new System.Drawing.Point(34, 253);
            this.startersListView.MultiSelect = false;
            this.startersListView.Name = "startersListView";
            this.startersListView.Size = new System.Drawing.Size(227, 82);
            this.startersListView.TabIndex = 5;
            this.startersListView.UseCompatibleStateImageBehavior = false;
            this.startersListView.View = System.Windows.Forms.View.Details;
            // 
            // starter
            // 
            this.starter.Text = "starter";
            this.starter.Width = 200;
            // 
            // bullpenListView
            // 
            this.bullpenListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.reliever,
            this.relievertype});
            this.bullpenListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.bullpenListView.Location = new System.Drawing.Point(34, 365);
            this.bullpenListView.MultiSelect = false;
            this.bullpenListView.Name = "bullpenListView";
            this.bullpenListView.Size = new System.Drawing.Size(227, 134);
            this.bullpenListView.TabIndex = 6;
            this.bullpenListView.UseCompatibleStateImageBehavior = false;
            this.bullpenListView.View = System.Windows.Forms.View.Details;
            this.bullpenListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.bullpenListView_ItemDrag);
            // 
            // reliever
            // 
            this.reliever.Text = "";
            this.reliever.Width = 145;
            // 
            // relievertype
            // 
            this.relievertype.Width = 55;
            // 
            // pitcherTextbox
            // 
            this.pitcherTextbox.AllowDrop = true;
            this.pitcherTextbox.Location = new System.Drawing.Point(393, 300);
            this.pitcherTextbox.Name = "pitcherTextbox";
            this.pitcherTextbox.Size = new System.Drawing.Size(159, 20);
            this.pitcherTextbox.TabIndex = 7;
            this.pitcherTextbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pitcherTextbox_DragDrop);
            this.pitcherTextbox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pitcherTextbox_DragEnter);
            this.pitcherTextbox.DragOver += new System.Windows.Forms.DragEventHandler(this.pitcherTextbox_DragOver);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Current Pitcher";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(423, 402);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(514, 401);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lineupListView
            // 
            this.lineupListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.slotNum,
            this.hitterColumn,
            this.positionColumn});
            this.lineupListView.FullRowSelect = true;
            this.lineupListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lineupListView.HideSelection = false;
            this.lineupListView.Location = new System.Drawing.Point(365, 30);
            this.lineupListView.MultiSelect = false;
            this.lineupListView.Name = "lineupListView";
            this.lineupListView.Size = new System.Drawing.Size(235, 195);
            this.lineupListView.TabIndex = 11;
            this.lineupListView.UseCompatibleStateImageBehavior = false;
            this.lineupListView.View = System.Windows.Forms.View.Details;
            this.lineupListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lineupListView_MouseClick);
            this.lineupListView.SelectedIndexChanged += new System.EventHandler(this.lineupListView_SelectedIndexChanged);
            // 
            // slotNum
            // 
            this.slotNum.Text = "#";
            this.slotNum.Width = 15;
            // 
            // hitterColumn
            // 
            this.hitterColumn.Width = 150;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Batting Order";
            // 
            // addToOrderButton
            // 
            this.addToOrderButton.Enabled = false;
            this.addToOrderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addToOrderButton.Location = new System.Drawing.Point(297, 68);
            this.addToOrderButton.Name = "addToOrderButton";
            this.addToOrderButton.Size = new System.Drawing.Size(36, 23);
            this.addToOrderButton.TabIndex = 13;
            this.addToOrderButton.Text = "->";
            this.addToOrderButton.UseVisualStyleBackColor = true;
            this.addToOrderButton.Click += new System.EventHandler(this.addToOrderButton_Click);
            // 
            // removeFromOrderButton
            // 
            this.removeFromOrderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeFromOrderButton.Location = new System.Drawing.Point(297, 110);
            this.removeFromOrderButton.Name = "removeFromOrderButton";
            this.removeFromOrderButton.Size = new System.Drawing.Size(36, 23);
            this.removeFromOrderButton.TabIndex = 14;
            this.removeFromOrderButton.Text = "<-";
            this.removeFromOrderButton.UseVisualStyleBackColor = true;
            this.removeFromOrderButton.Click += new System.EventHandler(this.removeFromOrderButton_Click);
            // 
            // positionMenuStrip
            // 
            this.positionMenuStrip.Name = "positionMenuStrip";
            this.positionMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.positionMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.positionMenuStrip_ItemClicked);
            // 
            // TeamLineupForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(645, 511);
            this.Controls.Add(this.removeFromOrderButton);
            this.Controls.Add(this.addToOrderButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lineupListView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pitcherTextbox);
            this.Controls.Add(this.bullpenListView);
            this.Controls.Add(this.startersListView);
            this.Controls.Add(this.battersListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TeamLineupForm";
            this.Text = "TeamLineup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView battersListView;
        private System.Windows.Forms.ListView startersListView;
        private System.Windows.Forms.ListView bullpenListView;
        private System.Windows.Forms.TextBox pitcherTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader player;
        private System.Windows.Forms.ColumnHeader position;
        private System.Windows.Forms.ColumnHeader starter;
        private System.Windows.Forms.ColumnHeader reliever;
        private System.Windows.Forms.ColumnHeader relievertype;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListView lineupListView;
        private System.Windows.Forms.ColumnHeader hitterColumn;
        private System.Windows.Forms.ColumnHeader positionColumn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader slotNum;
        private System.Windows.Forms.Button addToOrderButton;
        private System.Windows.Forms.Button removeFromOrderButton;
        private System.Windows.Forms.ContextMenuStrip positionMenuStrip;
    }
}
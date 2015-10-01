namespace DS.Showdown.Game
{
    partial class TopTenForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.statsList = new System.Windows.Forms.ListView();
            this.nameColumn = new System.Windows.Forms.ColumnHeader();
            this.teamColumn = new System.Windows.Forms.ColumnHeader();
            this.statColumn = new System.Windows.Forms.ColumnHeader();
            this.btnLookup = new System.Windows.Forms.Button();
            this.statCombo = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.statsList);
            this.panel1.Controls.Add(this.btnLookup);
            this.panel1.Controls.Add(this.statCombo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 448);
            this.panel1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(227, 398);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // statsList
            // 
            this.statsList.BackColor = System.Drawing.Color.CornflowerBlue;
            this.statsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.teamColumn,
            this.statColumn});
            this.statsList.Location = new System.Drawing.Point(56, 88);
            this.statsList.Name = "statsList";
            this.statsList.Size = new System.Drawing.Size(381, 272);
            this.statsList.TabIndex = 2;
            this.statsList.UseCompatibleStateImageBehavior = false;
            this.statsList.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 220;
            // 
            // teamColumn
            // 
            this.teamColumn.Text = "Team";
            // 
            // statColumn
            // 
            this.statColumn.Text = "stat";
            this.statColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.statColumn.Width = 100;
            // 
            // btnLookup
            // 
            this.btnLookup.Location = new System.Drawing.Point(327, 28);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(75, 23);
            this.btnLookup.TabIndex = 1;
            this.btnLookup.Text = "Lookup";
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // statCombo
            // 
            this.statCombo.FormattingEnabled = true;
            this.statCombo.Items.AddRange(new object[] {
            "Hits",
            "Average",
            "Doubles",
            "Triples",
            "HomeRuns",
            "RBIs",
            "Slg Pct",
            "Wins",
            "Losses",
            "Saves",
            "ERA",
            "Strikeouts",
            "Walks"});
            this.statCombo.Location = new System.Drawing.Point(119, 30);
            this.statCombo.Name = "statCombo";
            this.statCombo.Size = new System.Drawing.Size(183, 21);
            this.statCombo.TabIndex = 0;
            // 
            // TopTenForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 448);
            this.Controls.Add(this.panel1);
            this.Name = "TopTenForm";
            this.Text = "TopTenForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLookup;
        private System.Windows.Forms.ComboBox statCombo;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ListView statsList;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader teamColumn;
        private System.Windows.Forms.ColumnHeader statColumn;
    }
}
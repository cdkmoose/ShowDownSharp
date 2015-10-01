namespace DS.Showdown.Game
{
    partial class SelectTeamsForm
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
            this.teamListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.visitorTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.homeTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.moveToVisitorButton = new System.Windows.Forms.Button();
            this.moveToHomeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // teamListBox
            // 
            this.teamListBox.FormattingEnabled = true;
            this.teamListBox.Location = new System.Drawing.Point(36, 62);
            this.teamListBox.Name = "teamListBox";
            this.teamListBox.Size = new System.Drawing.Size(173, 186);
            this.teamListBox.TabIndex = 0;
            this.teamListBox.SelectedIndexChanged += new System.EventHandler(this.teamListBox_SelectedIndexChanged);
            this.teamListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.teamListBox_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Teams";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Visiting Team";
            // 
            // visitorTextBox
            // 
            this.visitorTextBox.AllowDrop = true;
            this.visitorTextBox.Location = new System.Drawing.Point(295, 96);
            this.visitorTextBox.Name = "visitorTextBox";
            this.visitorTextBox.ReadOnly = true;
            this.visitorTextBox.Size = new System.Drawing.Size(155, 20);
            this.visitorTextBox.TabIndex = 3;
            this.visitorTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.visitorTextBox_DragDrop);
            this.visitorTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.visitorTextBox_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Home Team";
            // 
            // homeTextBox
            // 
            this.homeTextBox.AllowDrop = true;
            this.homeTextBox.Location = new System.Drawing.Point(295, 190);
            this.homeTextBox.Name = "homeTextBox";
            this.homeTextBox.ReadOnly = true;
            this.homeTextBox.Size = new System.Drawing.Size(152, 20);
            this.homeTextBox.TabIndex = 5;
            this.homeTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.homeTextBox_DragDrop);
            this.homeTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.homeTextBox_DragEnter);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(158, 295);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(298, 294);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // moveToVisitorButton
            // 
            this.moveToVisitorButton.Enabled = false;
            this.moveToVisitorButton.Location = new System.Drawing.Point(234, 96);
            this.moveToVisitorButton.Name = "moveToVisitorButton";
            this.moveToVisitorButton.Size = new System.Drawing.Size(42, 23);
            this.moveToVisitorButton.TabIndex = 8;
            this.moveToVisitorButton.Text = ">>";
            this.moveToVisitorButton.UseVisualStyleBackColor = true;
            this.moveToVisitorButton.Click += new System.EventHandler(this.moveToVisitorButton_Click);
            // 
            // moveToHomeButton
            // 
            this.moveToHomeButton.Enabled = false;
            this.moveToHomeButton.Location = new System.Drawing.Point(234, 187);
            this.moveToHomeButton.Name = "moveToHomeButton";
            this.moveToHomeButton.Size = new System.Drawing.Size(42, 23);
            this.moveToHomeButton.TabIndex = 9;
            this.moveToHomeButton.Text = ">>";
            this.moveToHomeButton.UseVisualStyleBackColor = true;
            this.moveToHomeButton.Click += new System.EventHandler(this.moveToHomeButton_Click);
            // 
            // SelectTeamsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(490, 345);
            this.Controls.Add(this.moveToHomeButton);
            this.Controls.Add(this.moveToVisitorButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.homeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.visitorTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.teamListBox);
            this.Name = "SelectTeamsForm";
            this.Text = "Select Teams for Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox teamListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox visitorTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox homeTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button moveToVisitorButton;
        private System.Windows.Forms.Button moveToHomeButton;
    }
}
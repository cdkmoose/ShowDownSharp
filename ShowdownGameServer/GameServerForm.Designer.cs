namespace ShowdownGameServer
{
    partial class GameServerForm
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
            this.statusList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // statusList
            // 
            this.statusList.FormattingEnabled = true;
            this.statusList.Location = new System.Drawing.Point(27, 24);
            this.statusList.Name = "statusList";
            this.statusList.Size = new System.Drawing.Size(767, 524);
            this.statusList.TabIndex = 0;
            // 
            // GameServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 583);
            this.Controls.Add(this.statusList);
            this.Name = "GameServerForm";
            this.Text = "Showdown Game Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameServerForm_FormClosing);
            this.Load += new System.EventHandler(this.GameServerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox statusList;
    }
}


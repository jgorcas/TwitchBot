namespace TwitchBot.PcClient.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bConnect = new System.Windows.Forms.Button();
            this.bDisconnect = new System.Windows.Forms.Button();
            this.lbLogs = new System.Windows.Forms.ListBox();
            this.bClearLogs = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.lbMessages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(12, 12);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 0;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bDisconnect
            // 
            this.bDisconnect.Location = new System.Drawing.Point(12, 55);
            this.bDisconnect.Name = "bDisconnect";
            this.bDisconnect.Size = new System.Drawing.Size(75, 23);
            this.bDisconnect.TabIndex = 1;
            this.bDisconnect.Text = "Disconnect";
            this.bDisconnect.UseVisualStyleBackColor = true;
            this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
            // 
            // lbLogs
            // 
            this.lbLogs.FormattingEnabled = true;
            this.lbLogs.ItemHeight = 15;
            this.lbLogs.Location = new System.Drawing.Point(99, 12);
            this.lbLogs.Name = "lbLogs";
            this.lbLogs.Size = new System.Drawing.Size(689, 274);
            this.lbLogs.TabIndex = 2;
            // 
            // bClearLogs
            // 
            this.bClearLogs.Location = new System.Drawing.Point(12, 713);
            this.bClearLogs.Name = "bClearLogs";
            this.bClearLogs.Size = new System.Drawing.Size(75, 23);
            this.bClearLogs.TabIndex = 3;
            this.bClearLogs.Text = "Clear Logs";
            this.bClearLogs.UseVisualStyleBackColor = true;
            this.bClearLogs.Click += new System.EventHandler(this.bClearLogs_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.ItemHeight = 15;
            this.lbUsers.Location = new System.Drawing.Point(99, 298);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(316, 409);
            this.lbUsers.TabIndex = 5;
            this.lbUsers.SelectedValueChanged += new System.EventHandler(this.lbUsers_SelectedValueChanged);
            // 
            // lbMessages
            // 
            this.lbMessages.FormattingEnabled = true;
            this.lbMessages.ItemHeight = 15;
            this.lbMessages.Location = new System.Drawing.Point(421, 298);
            this.lbMessages.Name = "lbMessages";
            this.lbMessages.Size = new System.Drawing.Size(367, 409);
            this.lbMessages.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 751);
            this.Controls.Add(this.lbMessages);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.bClearLogs);
            this.Controls.Add(this.lbLogs);
            this.Controls.Add(this.bDisconnect);
            this.Controls.Add(this.bConnect);
            this.Name = "MainForm";
            this.Text = "TwitchBot";
            this.ResumeLayout(false);

        }

        #endregion

        private Button bConnect;
        private Button bDisconnect;
        private ListBox lbLogs;
        private Button bClearLogs;
        private ListBox lbUsers;
        private ListBox lbMessages;
    }
}
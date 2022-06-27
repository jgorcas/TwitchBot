namespace TwitchBot.PcV2
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
            this.SuspendLayout();
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(18, 21);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 0;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bDisconnect
            // 
            this.bDisconnect.Location = new System.Drawing.Point(18, 59);
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
            this.lbLogs.Location = new System.Drawing.Point(101, 24);
            this.lbLogs.Name = "lbLogs";
            this.lbLogs.Size = new System.Drawing.Size(687, 409);
            this.lbLogs.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbLogs);
            this.Controls.Add(this.bDisconnect);
            this.Controls.Add(this.bConnect);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button bConnect;
        private Button bDisconnect;
        private ListBox lbLogs;
    }
}
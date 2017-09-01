namespace JobHunt
{
	partial class JobHunt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobHunt));
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnJobHuntLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.Location = new System.Drawing.Point(12, 222);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(108, 41);
            this.btnQuit.TabIndex = 9;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnJobHuntLog
            // 
            this.btnJobHuntLog.Location = new System.Drawing.Point(12, 23);
            this.btnJobHuntLog.Name = "btnJobHuntLog";
            this.btnJobHuntLog.Size = new System.Drawing.Size(108, 33);
            this.btnJobHuntLog.TabIndex = 10;
            this.btnJobHuntLog.Text = "View Log";
            this.btnJobHuntLog.UseVisualStyleBackColor = true;
            this.btnJobHuntLog.Click += new System.EventHandler(this.btnJobHuntLog_Click);
            // 
            // JobHunt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnJobHuntLog);
            this.Controls.Add(this.btnQuit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JobHunt";
            this.Text = "Job Hunt";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.JobHunt_HelpButtonClicked);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnJobHuntLog;
	}
}


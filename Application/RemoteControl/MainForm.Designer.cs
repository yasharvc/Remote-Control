
namespace RemoteControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.TakeScreenShootButton = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.RebuildButton = new System.Windows.Forms.Button();
			this.ScheduleTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// TakeScreenShootButton
			// 
			this.TakeScreenShootButton.Location = new System.Drawing.Point(12, 12);
			this.TakeScreenShootButton.Name = "TakeScreenShootButton";
			this.TakeScreenShootButton.Size = new System.Drawing.Size(141, 23);
			this.TakeScreenShootButton.TabIndex = 0;
			this.TakeScreenShootButton.Text = "Take screen shoot";
			this.TakeScreenShootButton.UseVisualStyleBackColor = true;
			this.TakeScreenShootButton.Click += new System.EventHandler(this.TakeScreenShootButton_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.textBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBox1.ForeColor = System.Drawing.Color.Yellow;
			this.textBox1.Location = new System.Drawing.Point(12, 41);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(562, 364);
			this.textBox1.TabIndex = 3;
			// 
			// RebuildButton
			// 
			this.RebuildButton.Location = new System.Drawing.Point(159, 12);
			this.RebuildButton.Name = "RebuildButton";
			this.RebuildButton.Size = new System.Drawing.Size(75, 23);
			this.RebuildButton.TabIndex = 4;
			this.RebuildButton.Text = "Rebuild !!!";
			this.RebuildButton.UseVisualStyleBackColor = true;
			this.RebuildButton.Click += new System.EventHandler(this.RebuildButton_Click);
			// 
			// ScheduleTimer
			// 
			this.ScheduleTimer.Enabled = true;
			this.ScheduleTimer.Interval = 5000;
			this.ScheduleTimer.Tick += new System.EventHandler(this.ScheduleTimer_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(586, 417);
			this.Controls.Add(this.RebuildButton);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.TakeScreenShootButton);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Main Form";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button TakeScreenShootButton;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button RebuildButton;
		private System.Windows.Forms.Timer ScheduleTimer;
	}
}


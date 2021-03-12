
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
			this.TitleChaserTimer = new System.Windows.Forms.Timer(this.components);
			this.TakeScreenShootButton = new System.Windows.Forms.Button();
			this.WindowTitleLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ScreenPicture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.ScreenPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// TitleChaserTimer
			// 
			this.TitleChaserTimer.Enabled = true;
			this.TitleChaserTimer.Interval = 500;
			this.TitleChaserTimer.Tick += new System.EventHandler(this.TitleChaserTimer_Tick);
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
			// WindowTitleLabel
			// 
			this.WindowTitleLabel.AutoSize = true;
			this.WindowTitleLabel.Location = new System.Drawing.Point(13, 42);
			this.WindowTitleLabel.Name = "WindowTitleLabel";
			this.WindowTitleLabel.Size = new System.Drawing.Size(35, 13);
			this.WindowTitleLabel.TabIndex = 1;
			this.WindowTitleLabel.Text = "label1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			// 
			// ScreenPicture
			// 
			this.ScreenPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ScreenPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ScreenPicture.Location = new System.Drawing.Point(160, 12);
			this.ScreenPicture.Name = "ScreenPicture";
			this.ScreenPicture.Size = new System.Drawing.Size(628, 426);
			this.ScreenPicture.TabIndex = 3;
			this.ScreenPicture.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ScreenPicture);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.WindowTitleLabel);
			this.Controls.Add(this.TakeScreenShootButton);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.ScreenPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer TitleChaserTimer;
		private System.Windows.Forms.Button TakeScreenShootButton;
		private System.Windows.Forms.Label WindowTitleLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox ScreenPicture;
	}
}


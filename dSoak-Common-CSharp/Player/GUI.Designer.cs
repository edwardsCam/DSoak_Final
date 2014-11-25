namespace Player
{
	partial class GUI
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
			this.BUT_start_brain = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.LAB_game_status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BUT_start_brain
			// 
			this.BUT_start_brain.Location = new System.Drawing.Point(13, 13);
			this.BUT_start_brain.Name = "BUT_start_brain";
			this.BUT_start_brain.Size = new System.Drawing.Size(611, 23);
			this.BUT_start_brain.TabIndex = 0;
			this.BUT_start_brain.Text = "Start Brain";
			this.BUT_start_brain.UseVisualStyleBackColor = true;
			this.BUT_start_brain.Click += new System.EventHandler(this.BUT_start_brain_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Status: ";
			// 
			// LAB_game_status
			// 
			this.LAB_game_status.AutoSize = true;
			this.LAB_game_status.Location = new System.Drawing.Point(63, 43);
			this.LAB_game_status.Name = "LAB_game_status";
			this.LAB_game_status.Size = new System.Drawing.Size(0, 13);
			this.LAB_game_status.TabIndex = 2;
			// 
			// GUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 262);
			this.Controls.Add(this.LAB_game_status);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BUT_start_brain);
			this.Name = "GUI";
			this.Text = "DSoak";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BUT_start_brain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label LAB_game_status;
	}
}


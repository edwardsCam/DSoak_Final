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
			this.LAB_id1 = new System.Windows.Forms.Label();
			this.LAB_id2 = new System.Windows.Forms.Label();
			this.LAB_gameid1 = new System.Windows.Forms.Label();
			this.LAB_gameID2 = new System.Windows.Forms.Label();
			this.LAB_lp1 = new System.Windows.Forms.Label();
			this.LAB_lp2 = new System.Windows.Forms.Label();
			this.BUT_stop = new System.Windows.Forms.Button();
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
			// LAB_id1
			// 
			this.LAB_id1.AutoSize = true;
			this.LAB_id1.Location = new System.Drawing.Point(10, 92);
			this.LAB_id1.Name = "LAB_id1";
			this.LAB_id1.Size = new System.Drawing.Size(10, 13);
			this.LAB_id1.TabIndex = 3;
			this.LAB_id1.Text = " ";
			// 
			// LAB_id2
			// 
			this.LAB_id2.AutoSize = true;
			this.LAB_id2.Location = new System.Drawing.Point(82, 92);
			this.LAB_id2.Name = "LAB_id2";
			this.LAB_id2.Size = new System.Drawing.Size(10, 13);
			this.LAB_id2.TabIndex = 4;
			this.LAB_id2.Text = " ";
			// 
			// LAB_gameid1
			// 
			this.LAB_gameid1.AutoSize = true;
			this.LAB_gameid1.Location = new System.Drawing.Point(13, 131);
			this.LAB_gameid1.Name = "LAB_gameid1";
			this.LAB_gameid1.Size = new System.Drawing.Size(10, 13);
			this.LAB_gameid1.TabIndex = 5;
			this.LAB_gameid1.Text = " ";
			// 
			// LAB_gameID2
			// 
			this.LAB_gameID2.AutoSize = true;
			this.LAB_gameID2.Location = new System.Drawing.Point(74, 131);
			this.LAB_gameID2.Name = "LAB_gameID2";
			this.LAB_gameID2.Size = new System.Drawing.Size(10, 13);
			this.LAB_gameID2.TabIndex = 6;
			this.LAB_gameID2.Text = " ";
			// 
			// LAB_lp1
			// 
			this.LAB_lp1.AutoSize = true;
			this.LAB_lp1.Location = new System.Drawing.Point(13, 163);
			this.LAB_lp1.Name = "LAB_lp1";
			this.LAB_lp1.Size = new System.Drawing.Size(10, 13);
			this.LAB_lp1.TabIndex = 7;
			this.LAB_lp1.Text = " ";
			// 
			// LAB_lp2
			// 
			this.LAB_lp2.AutoSize = true;
			this.LAB_lp2.Location = new System.Drawing.Point(77, 163);
			this.LAB_lp2.Name = "LAB_lp2";
			this.LAB_lp2.Size = new System.Drawing.Size(10, 13);
			this.LAB_lp2.TabIndex = 8;
			this.LAB_lp2.Text = " ";
			// 
			// BUT_stop
			// 
			this.BUT_stop.Enabled = false;
			this.BUT_stop.Location = new System.Drawing.Point(515, 38);
			this.BUT_stop.Name = "BUT_stop";
			this.BUT_stop.Size = new System.Drawing.Size(109, 23);
			this.BUT_stop.TabIndex = 9;
			this.BUT_stop.Text = "Restart Process";
			this.BUT_stop.UseVisualStyleBackColor = true;
			this.BUT_stop.Click += new System.EventHandler(this.BUT_stop_Click);
			// 
			// GUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 262);
			this.Controls.Add(this.BUT_stop);
			this.Controls.Add(this.LAB_lp2);
			this.Controls.Add(this.LAB_lp1);
			this.Controls.Add(this.LAB_gameID2);
			this.Controls.Add(this.LAB_gameid1);
			this.Controls.Add(this.LAB_id2);
			this.Controls.Add(this.LAB_id1);
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
		private System.Windows.Forms.Label LAB_id1;
		private System.Windows.Forms.Label LAB_id2;
		private System.Windows.Forms.Label LAB_gameid1;
		private System.Windows.Forms.Label LAB_gameID2;
		private System.Windows.Forms.Label LAB_lp1;
		private System.Windows.Forms.Label LAB_lp2;
		private System.Windows.Forms.Button BUT_stop;
	}
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Player
{
	public partial class GUI : Form
	{

		private static Actors.Brain brain;

		void updateStatus(string str)
		{
			LAB_game_status.Text = str;
			LAB_game_status.Update();
		}

		public GUI()
		{
			InitializeComponent();
		}

		private void BUT_start_brain_Click(object sender, EventArgs e)
		{
			BUT_start_brain.Enabled = false;

			brain = new Actors.Brain();

			updateStatus("Connecting to Webservice...");
			while (brain.isUninitialized()) ;

			updateStatus("Searching for Games...");
			while (brain.isSearching()) ;

			updateStatus("Joining Game...");
			while (brain.isJoining()) ;

			updateStatus("In Game!");
			while (brain.isInGame()) ;

			if (brain.isInError())
			{
				updateStatus("Error... Could not connect to a game");
				BUT_start_brain.Enabled = true;
				brain.clear();
			}
		}

		private void GUI_FormClosed(object sender, FormClosedEventArgs e)
		{
			brain.clear();
		}

	}
}

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

		private void updateStatus(string str)
		{
			LAB_game_status.Text = str;
			LAB_game_status.Update();
		}

		private void resetGameInfo() {
			LAB_id1.Text = " ";
			LAB_id2.Text = " ";
			LAB_gameid1.Text = " ";
			LAB_gameID2.Text = " ";
			LAB_lp1.Text = " ";
			LAB_lp2.Text = " ";
			LAB_id1.Update();
			LAB_id2.Update();
			LAB_gameid1.Update();
			LAB_gameID2.Update();
			LAB_lp1.Update();
			LAB_lp2.Update();
		}

		private void updateGameInfo(short pid, short gid, short lp)
		{
			LAB_id1.Text = "Process ID: ";
			LAB_id2.Text = "" + pid;
			LAB_gameid1.Text = "Game ID: ";
			LAB_gameID2.Text = "" + gid;
			LAB_lp1.Text = "Lifepoints: ";
			LAB_lp2.Text = "" + lp;
			LAB_id1.Update();
			LAB_id2.Update();
			LAB_gameid1.Update();
			LAB_gameID2.Update();
			LAB_lp1.Update();
			LAB_lp2.Update();
		}

		public GUI()
		{
			InitializeComponent();
		}

		private void BUT_start_brain_Click(object sender, EventArgs e)
		{
			BUT_start_brain.Enabled = false;
			resetGameInfo();

			brain = new Actors.Brain();

			updateStatus("Connecting to Webservice...");
			while (brain.isUninitialized()) ;

			updateStatus("Searching for Games...");
			while (brain.isSearching()) ;

			updateStatus("Joining Game...");
			while (brain.isJoining()) ;

			updateStatus("In Game!");
			updateGameInfo(brain.getProcessID(), brain.getGameID(), brain.getLifepoints());
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject
{
    public partial class LeaderboardsForm : Form
    {
        public LeaderboardsForm()
        {
            InitializeComponent();
            LeaderboardManager lb = new LeaderboardManager();
            string[] entries = lb.GetTopTenEntries();

            listViewLeaderboard.Columns.Clear();
            listViewLeaderboard.View = View.Details;

            listViewLeaderboard.Columns.Add("Rank", 50);
            listViewLeaderboard.Columns.Add("Username",120);
            listViewLeaderboard.Columns.Add("Score", 60);
            listViewLeaderboard.Columns.Add("Time", 50);


            int rank = 1;

            foreach (string line in entries)
            {
                string[] parts = line.Split(';');
                var item = new ListViewItem(rank.ToString());
                item.SubItems.Add(parts[0]);   // username
                item.SubItems.Add(parts[1]);   // score
                item.SubItems.Add(parts[2]);   // time

                listViewLeaderboard.Items.Add(item);
                rank++;
            }
        }

        private void ExitBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit_Clicked;
        }

        private void ExitBtn_MouseHover(object sender, EventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit_Clicked;
        }

        private void ExitBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit1;
        }

        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit1;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

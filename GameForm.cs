using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace GameProject
{
    public partial class GameForm : Form
    {
        private InGame newgame;
        private string User;
        public GameForm()
        {
            InitializeComponent();
            GameBackground.Controls.Add(TextTitle);
            TextTitle.Location = new Point(172, 12);
            TextTitle.BackColor = Color.Transparent;

        }

        private void PlayBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PlayBtn.Image = Properties.Resources.Play;
        }

        private void PlayBtn_MouseUp(object sender, MouseEventArgs e)
        {
            PlayBtn.Image = Properties.Resources.Play_Clicked;
        }

        private void ExitBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit1;
        }

        private void ExitBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit_Clicked;
        }

        private void PlayBtn_MouseHover(object sender, EventArgs e)
        {
            PlayBtn.Image = Properties.Resources.Play;
        }

        private void PlayBtn_MouseLeave(object sender, EventArgs e)
        {
            PlayBtn.Image = Properties.Resources.Play_Clicked;
        }

        private void ExitBtn_MouseHover(object sender, EventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit_Clicked;
        }

        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            ExitBtn.Image = Properties.Resources.Exit1;
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            string User = textBox1.Text;
            if (string.IsNullOrWhiteSpace(User))
            {
                MessageBox.Show("Please enter a username before playing!");
                return;
            }
            newgame = new InGame(User);
            newgame.Show();
            this.Hide();

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GameBackground_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    string User = textBox1.Text;
                    if (string.IsNullOrWhiteSpace(User))
                    {
                        MessageBox.Show("Please enter a username before playing!");
                        return;
                    }
                    newgame = new InGame(User);
                    newgame.Show();
                    this.Hide();
                    break;
            }
        }

        private void LDBtn_MouseDown(object sender, MouseEventArgs e)
        {
            LDBtn.Image = Properties.Resources.Leaderboard;
        }

        private void LDBtn_MouseUp(object sender, MouseEventArgs e)
        {
            LDBtn.Image = Properties.Resources.Leaderboard_Clicked;
        }

        private void LDBtn_MouseHover(object sender, EventArgs e)
        {
            LDBtn.Image = Properties.Resources.Leaderboard;
        }

        private void LDBtn_MouseLeave(object sender, EventArgs e)
        {
            LDBtn.Image = Properties.Resources.Leaderboard_Clicked;
        }

        private void LDBtn_Click(object sender, EventArgs e)
        {
            LeaderboardsForm ld = new LeaderboardsForm();
            ld.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using NAudio.Wave;

namespace GameProject
{
    public partial class InGame : Form
    {
        private SoundPlayer backgroundMusic;
        private Player userPlayer;
        private string username;
        public int time = 45;
        private bool safeClose = false;
        int seconds;
        int minutes;

        private List<Items> gameObjects = new List<Items>();

        public InGame(string User = "")
        {
            InitializeComponent();
            gameTimer.Start();

            Luna.Parent = BGImage;
            ScoreText.Parent = BGImage;
            lblScore.Parent = BGImage;
            lblTimer.Parent = BGImage;
            DoorSprite.Parent = BGImage;
            Heart1.Parent = BGImage;
            Heart2.Parent = BGImage;
            Heart3.Parent = BGImage;

            username = User;

            Luna.BackColor = Color.Transparent;
            Luna.BringToFront();
            Luna.Location = new Point(0, 420);

            try
            {
                backgroundMusic = new SoundPlayer("BGMMusic.wav");
                backgroundMusic.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load background music. Error: {ex.Message}");
            }

            userPlayer = new Player(false, false, false, false, false, 8, 20, 0, 0, this, username, Luna, lblScore);

            LoadObjects();
        }

        private void InGame_KeyDown(object sender, KeyEventArgs e)
        {
            userPlayer.KeysPressed(e);
        }

        private void InGame_KeyUp(object sender, KeyEventArgs e)
        {
            userPlayer.KeysUnpressed(e);
        }

        private void LoadObjects()
        {
            foreach (PictureBox control in this.Controls.OfType<PictureBox>())
            {
                if (control.Tag == null) continue;

                switch (control.Tag.ToString())
                {
                    case "blocks":
                        gameObjects.Add(new Block(control));
                        break;

                    case "coin":
                        gameObjects.Add(new Coin(control));
                        break;

                    case "key":
                        gameObjects.Add(new KeyItem(control)); 
                        break;

                    case "door":
                        gameObjects.Add(new Door(control)); 
                        break;
                }
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            userPlayer.InGround = false;
            userPlayer.JumpMove();
            userPlayer.GoMove(ClientSize, this, mainTimer);

            foreach (Items obj in gameObjects.ToList())
            {
                if (obj.Sprite.Visible &&
                    userPlayer.MainPlayer.Bounds.IntersectsWith(obj.Sprite.Bounds))
                {
                    obj.OnCollide(userPlayer, this);
                }

            }
            if (gameTimer.Enabled) 
            {
                if (userPlayer.MainPlayer.Bottom >= this.ClientSize.Height + 180)
                {
                    HandleLifeLoss();
                    LeaderboardManager ldboard = new LeaderboardManager();
                    ldboard.SetRecord(userPlayer.PlayerName, userPlayer.Score, GetRemainingTime());
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            time--;

            lblTimer.Text = FormatTime(time);


            if (time <= 0)
            {
                gameTimer.Stop();
                mainTimer.Stop();

                if (backgroundMusic != null)
                {
                    backgroundMusic.Stop();
                    backgroundMusic.Dispose();
                }

                MessageBox.Show("Time's up! You failed to finish the game. Better luck next time!");
                GameLose retry = new GameLose();
                DialogResult result = retry.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    safeClose = true;
                    InGame newGame = new InGame(userPlayer.PlayerName);
                    newGame.Show();
                }
                this.Hide();
            }
        }
        public string FormatTime(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes}:{seconds}";
        }
        public string GetRemainingTime()
        {
            int minutes = time / 60;
            int seconds = time % 60;
            return $"{minutes}:{seconds}";
        }

        private void HandleLifeLoss()
        {
            mainTimer.Stop();
            gameTimer.Stop();

            bool stillAlive = userPlayer.LoseLife();

            UpdateHearts();

            if (stillAlive)
            {
                userPlayer.MainPlayer.Location = new Point(0, 420);

                mainTimer.Start();
                gameTimer.Start();
            }
            else
            {
                GameOver();
            }
        }

        private void UpdateHearts()
        {
            Heart1.Image = Properties.Resources.HeartIcon;
            Heart2.Image = Properties.Resources.HeartIcon;
            Heart3.Image = Properties.Resources.HeartIcon;

            if (userPlayer.Lives <= 2)
                Heart3.Image = Properties.Resources.HeartIcon_Empty;

            if (userPlayer.Lives <= 1)
                Heart2.Image = Properties.Resources.HeartIcon_Empty;

            if (userPlayer.Lives <= 0)
                Heart1.Image = Properties.Resources.HeartIcon_Empty;
        }
        private void GameOver()
        {
            mainTimer.Stop();
            gameTimer.Stop();

            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
                backgroundMusic.Dispose(); 
            }

            GameLose retry = new GameLose();
            DialogResult result = retry.ShowDialog();

            if (result == DialogResult.Yes)
            {
                safeClose = true; 
                InGame newGame = new InGame(userPlayer.PlayerName);
                newGame.Show();
            }
            this.Hide();
        }

        private void InGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace GameProject
{
    public class Player
    {
        public int Lives { get; set; } = 3;
        public bool HasKey { get; set; } = false;
        public bool GoLeft { get; set; } 
        public bool GoRight { get; set; }
        public bool GoJump { get; set; } 
        public bool InGround { get; set; } 
        public bool OnPowerUp { get; set; } 
        public int PlayerSpeed { get; set; }
        public int JumpSpeed { get; set; } 
        public int Force { get; set; } 
        public int Score { get; set; } 
        public InGame Game { get; set; }
        public string PlayerName { get; set; }
        public PictureBox MainPlayer { get; set; }
        public Label Points { get; set; }

        public bool GoRetry;

        public Player(bool Left, bool Right, bool Jump, bool Ground, bool Power, int playerSpeed, int jumpSpeed, int force, int score, InGame gameref, string name, PictureBox player, Label points) 
        {
            GoLeft = Left;
            GoRight = Right;
            GoJump = Jump;
            InGround = Ground;
            OnPowerUp = Power;
            PlayerSpeed = playerSpeed;
            JumpSpeed = jumpSpeed;
            Force = force;
            Score = score;
            PlayerName = name;
            MainPlayer = player;
            Points = points;
            Game = gameref;

        }

        public void KeysPressed(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                case Keys.Right:
                    GoRight = true;
                    MainPlayer.Image = Properties.Resources.LunaRunRight;
                    break;
                case Keys.A:
                case Keys.Left:
                    GoLeft = true;
                    MainPlayer.Image = Properties.Resources.LunaRunLeft;
                    break;
                case Keys.Space:
                case Keys.Up:
                    if (!GoJump && InGround)
                    {
                        GoJump = true;
                        InGround = false;
                        Force = JumpSpeed;
                    }
                    break;
            }
        }

        public void KeysUnpressed(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                case Keys.Right:
                    GoRight = false;
                    MainPlayer.Image = Properties.Resources.LunaInRight;
                    break;
                case Keys.A:
                case Keys.Left:
                    GoLeft = false;
                    MainPlayer.Image = Properties.Resources.LunaInLeft;
                    break;
            }
        }

        public void JumpMove()
        {
            if (GoJump)
            {
                MainPlayer.Top -= Force;
                Force--;
            }
        }

        public void GoMove(Size clientSize, Form form, System.Windows.Forms.Timer timer)
        {
            if (!InGround) 
            { 
                MainPlayer.Top += PlayerSpeed;
            }

            if (GoLeft && MainPlayer.Left > 0) 
            { 
                MainPlayer.Left -= PlayerSpeed+1;
            }

            if (GoRight && MainPlayer.Right < clientSize.Width)
            {
                MainPlayer.Left += PlayerSpeed+2;
            }
        }

        public void CollisionsHitBlock(Control control, PictureBox specialBlock = null)
        {
            int fromTop = MainPlayer.Bottom - control.Top; 
            int fromLeft = MainPlayer.Right - control.Left;     
            int fromBottom = control.Bottom - MainPlayer.Top;  
            int fromRight = control.Right - MainPlayer.Left;    

            int[] OverlapWithObj = { fromTop, fromLeft, fromBottom, fromRight };
            int side = Array.IndexOf(OverlapWithObj, OverlapWithObj.Min());

            switch (side)
            {
                case 0: 
                    MainPlayer.Top = control.Top + 1 - MainPlayer.Height;
                    Force = 0;
                    InGround = true;
                    GoJump = false;
                    break;

                case 1: 
                    MainPlayer.Left = control.Left - MainPlayer.Width;
                    break;

                case 2:
                    MainPlayer.Top = control.Bottom;
                    GoJump = false;
                    break;

                case 3: 
                    MainPlayer.Left = control.Right;
                    break;
            }

        }
        public bool LoseLife()
        {
            Lives--;


            return Lives > 0;
        }

    }
}

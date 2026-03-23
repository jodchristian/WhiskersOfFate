using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject
{
    public class Door : Items
    {
        public Door(PictureBox sprite) : base(sprite) { }

        public override void OnCollide(Player player, InGame game)
        {
            if (player.HasKey)
            {
                Sprite.Visible = false;
                player.MainPlayer.Visible = false;
                game.Controls.Remove(Sprite);
                game.Controls.Remove(player.MainPlayer);

                SoundManager.PlaySFX("8-BitTeleportSFX.mp3");

                LeaderboardManager ld = new LeaderboardManager();
                LeaderboardsForm ld1 = new LeaderboardsForm();
                ld.SetRecord(player.PlayerName, player.Score, game.GetRemainingTime());

                MessageBox.Show("Game Complete!");
                ld1.ShowDialog();
                Application.Exit();

            }
            else
            {
                MessageBox.Show("You need the key!");
            }
        }
    }
}

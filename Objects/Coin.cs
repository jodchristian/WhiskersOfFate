using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace GameProject
{
    public class Coin : Items
    {
        public Coin(PictureBox sprite) : base(sprite) { }

        public override void OnCollide(Player player, InGame game)
        {
            player.Score++;
            player.Points.Text = player.Score.ToString();

            SoundManager.PlaySFX("8-bitCoinSFX.wav");

            Sprite.Visible = false;
            game.Controls.Remove(Sprite);
        }
    }
}

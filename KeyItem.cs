using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject
{
    public class KeyItem : Items
    {
        public KeyItem(PictureBox sprite) : base(sprite) { }

        public override void OnCollide(Player player, InGame game)
        {
            SoundManager.PlaySFX("8-BitKeySFX.wav");

            player.HasKey = true;
            Sprite.Visible = false;
        }
    }
}

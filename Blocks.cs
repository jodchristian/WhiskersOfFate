using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject
{
    public class Block : Items
    {
        public Block(PictureBox sprite) : base(sprite) { }

        public override void OnCollide(Player player, InGame game)
        {
            player.CollisionsHitBlock(Sprite);
        }
    }
}

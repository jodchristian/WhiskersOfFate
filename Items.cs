using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject
{
    public abstract class Items
    {
        public PictureBox Sprite { get; private set; }

        public Items(PictureBox sprite)
        {
            Sprite = sprite;
        }
        public abstract void OnCollide(Player player, InGame game);
    }
}

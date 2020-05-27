using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Data.Entities
{
    public class SlotRopa
    {
        public int slot { get; set; }
        public int drawable { get; set; }
        public int texture { get; set; }
        public Player player { get; set; }
    
        public SlotRopa(Player player, int slot)
        {
            this.texture = player.GetClothesTexture(slot);
            this.drawable = player.GetClothesDrawable(slot);
            this.slot = slot;
            this.player = player;
        }
        public SlotRopa()
        {
            this.texture = -1;
            this.drawable = -1;
            this.slot = -1;
            this.player = null;
        }

        public SlotRopa(int slot, int drawable, int texture)
        {
            this.slot = slot;
            this.texture = texture;
            this.drawable = drawable;
        }

        public void setRopa(bool reset)
        {
            player.SetClothes(this.slot, this.drawable, this.texture);
            if (reset)
            {
                this.drawable = -1;
                this.slot = -1;
                this.texture = -1;
            }
        }

        public void setRopa(bool reset, Player pl)
        {
            pl.SetClothes(this.slot, this.drawable, this.texture);
            if (reset)
            {
                this.drawable = -1;
                this.slot = -1;
                this.texture = -1;
            }
        }

    }
}



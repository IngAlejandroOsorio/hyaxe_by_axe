using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game
{
    public class anims : Events.Script
    {
        public anims()
        {
            Events.Add("PlayAnimation", PlayAnimation);
        }

        private void PlayAnimation(object[] args)
        {
            foreach (var player in RAGE.Elements.Entities.Players.All)
            {
                if(player.Name == args[0].ToString()) 
                    player.PlayAnim(args[1].ToString(), args[2].ToString(), 0, false, true, true, 0, 1);
            }
        }
    }
}

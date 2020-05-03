using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.character
{
    public class Pj : Events.Script
    {
        private bool isfreezed = false;
        public Pj()
        {
            Events.Add("freeze_player", freeze_player);
        }

        private void freeze_player(object[] args)
        {
            if (!isfreezed)
            {
                RAGE.Elements.Player.LocalPlayer.FreezePosition(true);
                isfreezed = true;
                return;
            }
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            isfreezed = false;
        }
    }
}

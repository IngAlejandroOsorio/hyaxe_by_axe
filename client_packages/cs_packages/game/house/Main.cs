using RAGE;
using RAGE.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.house
{
    public class Main : Events.Script
    {
        public Main()
        {
            Events.Add("HouseAddStreetName", HouseAddStreetName);
        }

        private void HouseAddStreetName(object[] args)
        {
            string name = RAGE.Game.Zone.GetNameOfZone(Player.LocalPlayer.Position.X, Player.LocalPlayer.Position.Y, Player.LocalPlayer.Position.Z);
            Events.CallRemote("HouseFinishCreation", name);
        }
    }
}

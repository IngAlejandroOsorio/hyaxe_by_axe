using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Utilities
{
    public class PlayerId : Script
    {
        public static Client FindPlayerById(int id)
        {
            foreach(var user in Data.Lists.playersConnected)
            {
                if (user.entity.Value == id) return user.entity;
                else return null;
            }
            return null;
        }

        [Command("id")]
        public void CMD_id(Client player)
        {
            player.SendChatMessage($"Tu id es {player.Value}");
        }
    }
}

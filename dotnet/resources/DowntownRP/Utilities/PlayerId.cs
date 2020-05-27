using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Utilities
{
    public class PlayerId : Script
    {
        public static Player FindPlayerById(int id)
        {
            return Data.Lists.playersConnected.Find(x => x.entity.Value == id).entity;
        }

        public static Data.Entities.User FindUserById (int id)
        {
            return Data.Lists.playersConnected.Find(x => x.entity.Value == id);
        }

        [Command("id")]
        public void CMD_id(Player player, string nombre)
        {
            Data.Entities.User user = Data.Lists.playersConnected.Find(x => x.entity.Name == nombre);
            if (user != null) player.SendChatMessage($"La ID de {nombre} es {user.entity.Value}");
            else Utilities.Notifications.SendNotificationERROR(player, "No existe ningún jugador con este nombre");
        }

        
    }
}

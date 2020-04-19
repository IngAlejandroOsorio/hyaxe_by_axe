using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Events
{
    public class Disconnect : Script
    {
        [ServerEvent(Event.PlayerDisconnected)]
        public async Task Event_PlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            Data.Info.playersConnected = Data.Info.playersConnected - 1;
            NAPI.ClientEvent.TriggerClientEventForAll("update_hud_players", Data.Info.playersConnected);

            Data.Lists.playersConnected.Remove(Data.Lists.playersConnected.Find(x => x.idIg == player.Value));

            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            await Axe.Creador.Main.UpdateUserPosition(user.idpj, player.Position.X, player.Position.Y, player.Position.Z);
        }
    }
}

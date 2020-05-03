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
        public async Task Event_PlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            Data.Info.playersConnected = Data.Info.playersConnected - 1;
            NAPI.ClientEvent.TriggerClientEventForAll("update_hud_players", Data.Info.playersConnected);

            Data.Entities.User user = Data.Lists.playersConnected.Find(x => x.idIg == player.Value);
            await Game.CharacterSelector.CharacterSelector.UpdateUserPosition(user.idpj, player.Position.X, player.Position.Y, player.Position.Z);
            await Game.CharacterSelector.CharacterSelector.UpdateUserDimension(user.idpj, (int)player.Dimension);

            foreach (var veh in user.vehicles)
            {
                await Game.Vehicles.DbHandler.UpdateVehicleCharacterPosition(veh);
                veh.entity.Delete();
            }

            Data.Lists.playersConnected.Remove(user);
        }
    }
}

using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.Vehicles
{
    public class VehHandler : Script
    {
        [ServerEvent(Event.PlayerEnterVehicleAttempt)]
        public void SE_PlayerTryEnterVehicleFaction(Player player, Vehicle vehicle, sbyte seatID)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (vehicle.HasData("VEHICLE_FACTION_DATA"))
            {
                Data.Entities.VehicleFaction veh = vehicle.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");

                if(seatID == 0)
                {
                    if (veh.faction != user.faction & user.adminLv == 0)
                    {
                        vehicle.Locked = true;
                        player.WarpOutOfVehicle();
                        Utilities.Notifications.SendNotificationERROR(player, "No perteneces a esta facción");
                    }
                    else vehicle.Locked = false;
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void SE_PlayerExitVehicleFaction (Player player, Vehicle vehicle)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (vehicle.HasData("VEHICLE_FACTION_DATA"))
            {
                Data.Entities.VehicleFaction veh = vehicle.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");
                if (veh == null) return;

            }
        }
        
        [Command ("guardarFacVehs")]
        public async Task CMD_GuardarFacVehs (Player player, int id = -1)
        {
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");

            if(id == -1)
            {
                id = usr.faction;
            }
            if((usr.faction == id & usr.rank >= 5)|Utilities.AdminLVL.PuedeUsarComando(player, 2))
            {
                foreach (Data.Entities.VehicleFaction x in Data.Lists.factions.Find(y => y.id == id).vehicles)
                {
                    DbFunctions.UpdateFVehPosAndDim(x.id, x.entity.Position, x.entity.Dimension);
                }
            }
        }
    }
}

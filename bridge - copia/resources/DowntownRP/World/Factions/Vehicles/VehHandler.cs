using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.World.Factions.Vehicles
{
    public class VehHandler : Script
    {
        [ServerEvent(Event.PlayerEnterVehicleAttempt)]
        public void SE_PlayerTryEnterVehicleFaction(Client player, Vehicle vehicle, sbyte seatID)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (vehicle.HasData("VEHICLE_FACTION_DATA"))
            {
                Data.Entities.VehicleFaction veh = vehicle.GetData("VEHICLE_FACTION_DATA");

                if (veh.faction != user.faction)
                {
                    vehicle.Locked = true;
                    Utilities.Notifications.SendNotificationERROR(player, "No perteneces a esta facción");
                }
                else vehicle.Locked = false;
            }
        }
    }
}

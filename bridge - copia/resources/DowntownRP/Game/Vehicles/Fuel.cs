using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Vehicles
{
    public class Fuel : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public async Task Fuel_PlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seatID)
        {
            if (!vehicle.HasSharedData("FUEL")) vehicle.SetSharedData("FUEL", 100);
            if (vehicle.HasData("VEHICLE_COMPANY_DATA")) return;
            if(seatID == -1) // Cambiar en la 1.1 a 0
            {
                while (player.IsInVehicle)
                {
                    if (vehicle.EngineStatus)
                    {
                        int actualFuel = vehicle.GetSharedData("FUEL");
                        if (actualFuel <= 0)
                        {
                            vehicle.EngineStatus = false;
                            Utilities.Notifications.SendNotificationINFO(player, "El vehículo no tiene gasolina.");
                            break;
                        }
                        await Task.Delay(5000);
                        vehicle.SetSharedData("FUEL", actualFuel - 1);
                    }
                    break;
                }
            }
        }

        [Command("checkfuel")]
        public void CMD_checkfuel(Client player)
        {
            if (player.IsInVehicle)
            {
                player.SendChatMessage($"La gasolina del vehículo es {player.Vehicle.GetSharedData("FUEL")}");
            }
        }
    }
}

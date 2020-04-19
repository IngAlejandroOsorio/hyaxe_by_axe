using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Vehicles
{
    public class Engine : Script
    {
        [ServerEvent(Event.PlayerEnterVehicleAttempt)]
        public void Engine_PlayerEnterVehicleAttempt(Client player, Vehicle vehicle, sbyte seatID)
        {
            if (vehicle.EngineStatus == false) player.TriggerEvent("tipEngineVehicle");
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void Engine_PlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (vehicle.HasSharedData("ENGINE_STATUS"))
            {
                if (vehicle.GetSharedData("ENGINE_STATUS") == true) vehicle.EngineStatus = true;
            }
        }

        [RemoteEvent("ActionEngineVehicle")]
        public async Task RE_ActionEngineVehicle(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (!user.chatStatus)
            {
                if (player.IsInVehicle)
                {
                    if (player.Vehicle.EngineStatus)
                    {
                        player.Vehicle.EngineStatus = false;
                        Utilities.Chat.EntornoMe(player, "ha apagado el motor");
                        player.Vehicle.SetSharedData("ENGINE_STATUS", false);
                        return;
                    }
                    else
                    {
                        Utilities.Chat.EntornoMe(player, "está encendiendo el motor del vehículo");
                        await Task.Delay(1000);
                        player.Vehicle.EngineStatus = true;
                        Utilities.Chat.EntornoDo(player, "Motor encendido");
                        player.Vehicle.SetSharedData("ENGINE_STATUS", true);

                        if (!player.Vehicle.HasSharedData("FUEL")) player.Vehicle.SetSharedData("FUEL", 100);
                        if (player.Vehicle.HasData("VEHICLE_COMPANY_DATA")) return;
                        while (player.IsInVehicle)
                        {
                            if (player.Vehicle.EngineStatus)
                            {
                                int actualFuel = player.Vehicle.GetSharedData("FUEL");
                                if (actualFuel <= 0)
                                {
                                    player.Vehicle.EngineStatus = false;
                                    Utilities.Notifications.SendNotificationINFO(player, "El vehículo no tiene gasolina.");
                                    break;
                                }
                                await Task.Delay(5000);
                                player.Vehicle.SetSharedData("FUEL", actualFuel - 1);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}

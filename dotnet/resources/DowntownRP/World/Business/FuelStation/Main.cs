using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Business.FuelStation
{
    public class Main : Script
    {
        [ServerEvent(Event.PlayerEnterColshape)]
        public void SE_EnterColShapeFuel(ColShape shape, Player player)
        {
            if (!shape.HasData("BUSINESS_FUEL_POINT")) return;
            player.SetData("BUSINESS_FUEL_POINT", shape.GetData<Data.Entities.Business>("BUSINESS_FUEL_POINT"));
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void SE_ExitColShapeFuel(ColShape shape, Player player)
        {
            if (!shape.HasData("BUSINESS_FUEL_POINT")) return;
            player.ResetData("BUSINESS_FUEL_POINT");
        }

        [RemoteEvent("ActionFuelGas")]
        public void RE_ActionFuelGas(Player player)
        {
            if (player.HasData("BUSINESS_FUEL_POINT"))
            {
                if (player.IsInVehicle)
                {
                    if (!player.Vehicle.EngineStatus)
                    {
                        // 1 litro = $2
                        int fuel = player.Vehicle.GetSharedData<int>("FUEL");
                        if (fuel == 100) Utilities.Notifications.SendNotificationERROR(player, "El tanque de tu vehículo está lleno");
                        else
                        {
                            int fulltank = 100 - fuel;
                            int pricefulltank = fulltank * 2;
                            player.TriggerEvent("OpenGasoilMenu", fulltank, pricefulltank);
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "Para poder llenar el tanque tu vehículo debe de estar apagado");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo");
            }
        }

        [RemoteEvent("PlayerPayFuel")]
        public async Task RE_PlayerPayFuel(Player player)
        {
            if (player.IsInVehicle)
            {
                int fuel; int money;
                fuel = 100 - player.Vehicle.GetSharedData<int>("FUEL"); money = (100 - player.Vehicle.GetSharedData<int>("FUEL")) * 2;
                if (fuel + player.Vehicle.GetSharedData<int>("FUEL") <= 100)
                {
                    if (await Game.Money.MoneyModel.SubMoney(player, money) == false) Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
                    else
                    {
                        player.Vehicle.SetSharedData("FUEL", player.Vehicle.GetSharedData<int>("FUEL") + fuel);
                        Utilities.Notifications.SendNotificationINFO(player, "Se está rellenando el tanque del vehículo, un momento...");
                        await Task.Delay(2000);
                        Utilities.Notifications.SendNotificationOK(player, $"Has repostado {fuel} litros por ~g~${money}");
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "La capacidad de tu tanque es de 100 litros");
            }
        }
    }
}

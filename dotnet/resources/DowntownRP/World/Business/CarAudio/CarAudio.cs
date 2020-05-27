using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarAudio
{
    public class Main : Script
    {
        public bool CmenuActive = false;

        [RemoteEvent("RepararTallerServ")]
        public async Task RepararTallerServ(Player player)
        {
            if (await DowntownRP.Game.Money.MoneyModel.SubMoney(player, 500))
            {
                Vehicle car = NAPI.Player.GetPlayerVehicle(player);
                NAPI.Vehicle.RepairVehicle(car);
            }
            else DowntownRP.Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
        }

        [RemoteEvent("CobrarTallerServ")]
        public async Task CobrarTallerServ(Player player, int precio)
        {
            if (await DowntownRP.Game.Money.MoneyModel.SubMoney(player, precio))
            {
                player.TriggerEvent("guardarTaller");
            }
            else DowntownRP.Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
        }

      
        [RemoteEvent("CmenuColor")]
        public void OnCmenuColor(Player player, int red, int green, int blue, Vehicle car)
        {
            //Vehicle car = NAPI.Player.GetPlayerVehicle(player);
            NAPI.Vehicle.SetVehicleCustomPrimaryColor(car, red, green, blue);
            NAPI.Vehicle.SetVehicleCustomSecondaryColor(car, red, green, blue);
        }

        [RemoteEvent("CmenuColor2")]
        public void OnCmenuColor2(Player player, int red, int green, int blue, Vehicle car)
        {
            //Vehicle car = NAPI.Player.GetPlayerVehicle(player);
            NAPI.Vehicle.SetVehicleCustomSecondaryColor(car, red, green, blue);
        }


        [RemoteEvent("CarAudioPerlaCol")]
        public void CarAudioPerlaCol(Player player, int color, Vehicle car)
        {
            //Vehicle car = NAPI.Player.GetPlayerVehicle(player);
            NAPI.Vehicle.SetVehiclePearlescentColor(car.Handle, color);
        }

    }
}

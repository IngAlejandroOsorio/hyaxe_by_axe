using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Companies.Taxi
{
    public class Main : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void Taxi_PlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatID)
        {
            if (seatID == 0) return;
            if (!vehicle.HasData("ON_RACE")) return;

            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Data.Entities.TaxiRace race = vehicle.GetData<Data.Entities.TaxiRace>("ON_RACE");
            if(race.solicitador == player)
            {
                player.TriggerEvent("StartTaximeter", 1);
                race.driver.TriggerEvent("StartTaximeter", 0);
                race.driver.TriggerEvent("DestruirBlipCliente");
                user.taxiRace = race;

                Utilities.Chat.EntornoDo(race.driver, "El taximetro se ha puesto a funcionar");
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void Taxi_PlayerExitVehicle(Player player, Vehicle vehicle)
        {
            if (!vehicle.HasData("ON_RACE")) return;

            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Data.Entities.TaxiRace race = vehicle.GetData<Data.Entities.TaxiRace>("ON_RACE");

            player.Vehicle.ResetData("ON_RACE");
            race.driver.TriggerEvent("StopTaximeter");
            race.solicitador.TriggerEvent("StopTaximeter");

            Utilities.Notifications.SendNotificationINFO(race.driver, "Se acabó la carrera.");
            Utilities.Notifications.SendNotificationINFO(user.taxiRace.driver, "Se acabó la carrera.");
        }

        [RemoteEvent("ChargeTaxiRide")]
        public async Task RE_ChargeTaxiRide(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(await Game.Money.MoneyModel.SubMoney(player, 10)) Game.Money.MoneyModel.AddMoney(user.taxiRace.driver, 10);
            else
            {
                player.Vehicle.ResetData("ON_RACE");
                player.TriggerEvent("StopTaximeter");
                user.taxiRace.driver.TriggerEvent("StopTaximeter");

                player.WarpOutOfVehicle();
                Utilities.Notifications.SendNotificationERROR(player, "No tienes mas dinero para pagar");
                Utilities.Notifications.SendNotificationINFO(user.taxiRace.driver, "El cliente no tiene mas dinero para continuar");
            }
        }

        [Command("testeazo")]
        public void CMD_testeazo(Player player)
        {
            player.TriggerEvent("StartTaximeter", 1);
        }
    }
}

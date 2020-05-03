using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.Ilegal
{
    public class Safebox : Script
    {
        [RemoteEvent("OpenSafeboxFaction")]
        public void RE_OpenSafeboxFaction(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            player.TriggerEvent("OpenSafeBoxFaction", user.ilegalFactionInterior.safeBox);
        }

        [RemoteEvent("SafeBoxFactionDepositar")]
        public async Task RE_SafeBoxFactionDepositar(Player player, string canti)
        {
            int cantidad = Convert.ToInt32(canti);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (await Game.Money.MoneyModel.SubMoney(player, cantidad))
            {
                await AddMoneyFactionSafebox(player, cantidad);
                Utilities.Notifications.SendNotificationOK(player, $"Has agregado ${cantidad} a lu caja fuerte");
                player.TriggerEvent("CloseSafeBoxFaction");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
        }

        [RemoteEvent("SafeBoxFactionRetirar")]
        public async Task RE_SafeBoxFactionRetirar(Player player, string canti)
        {
            int cantidad = Convert.ToInt32(canti);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (await RemoveMoneyFactionSafebox(player, cantidad))
            {
                await Game.Money.MoneyModel.AddMoney(player, cantidad);
                Utilities.Notifications.SendNotificationOK(player, $"Has retirado ${cantidad} de tu caja fuerte");
                player.TriggerEvent("CloseSafeBoxFaction");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero en tu caja fuerte");
        }

        public async static Task AddMoneyFactionSafebox(Player player, int quantity)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            int bank = user.ilegalFactionInterior.safeBox;
            user.ilegalFactionInterior.safeBox = bank + quantity;
            await DbFunctions.UpdateFactionSafebox(user.ilegalFactionInterior.id, bank + quantity);
        }

        public async static Task<bool> RemoveMoneyFactionSafebox(Player player, int quantity)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return false;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (quantity <= 0)
                return false;

            int actual_money = user.ilegalFactionInterior.safeBox;

            if (quantity > actual_money)
                return false;

            if (user.ilegalFactionInterior.safeBox < 0)
            {
                user.ilegalFactionInterior.safeBox = 0;
                await DbFunctions.UpdateFactionSafebox(user.ilegalFactionInterior.id, 0);
            }

            int money = actual_money -= quantity;
            user.ilegalFactionInterior.safeBox = money;
            await DbFunctions.UpdateFactionSafebox(user.ilegalFactionInterior.id, money);
            return true;
        }
    }
}

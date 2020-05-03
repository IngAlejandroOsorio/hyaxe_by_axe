using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Houses
{
    public class Safebox : Script
    {
        [RemoteEvent("OpenSafeboxHouse")]
        public void RE_OpenSafeboxHouse(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            player.TriggerEvent("OpenSafeBoxHouse", user.houseInterior.safeBox);
        }

        [RemoteEvent("SafeBoxHouseDepositar")]
        public async Task RE_SafeBoxHouseDepositar(Player player, string canti)
        {
            int cantidad = Convert.ToInt32(canti);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (await Game.Money.MoneyModel.SubMoney(player, cantidad))
            {
                await AddMoneyHouseSafebox(player, cantidad);
                Utilities.Notifications.SendNotificationOK(player, $"Has agregado ${cantidad} a tu caja fuerte");
                player.TriggerEvent("CloseSafeBoxHouse");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
        }

        [RemoteEvent("SafeBoxHouseRetirar")]
        public async Task RE_SafeBoxHouseRetirar(Player player, string canti)
        {
            int cantidad = Convert.ToInt32(canti);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (await RemoveMoneyHouseSafebox(player, cantidad))
            {
                await Game.Money.MoneyModel.AddMoney(player, cantidad);
                Utilities.Notifications.SendNotificationOK(player, $"Has retirado ${cantidad} de tu caja fuerte");
                player.TriggerEvent("CloseSafeBoxHouse");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero en tu caja fuerte");
        }

        public async static Task AddMoneyHouseSafebox(Player player, int quantity)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            int bank = user.houseInterior.safeBox;
            user.houseInterior.safeBox = bank + quantity;
            await DbFunctions.UpdateHouseSafebox(user.houseInterior.id, bank + quantity);
        }

        public async static Task<bool> RemoveMoneyHouseSafebox(Player player, int quantity)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return false;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (quantity <= 0)
                return false;

            int actual_money = user.houseInterior.safeBox;

            if (quantity > actual_money)
                return false;

            if (user.houseInterior.safeBox < 0)
            {
                user.houseInterior.safeBox = 0;
                await DbFunctions.UpdateHouseSafebox(user.houseInterior.id, 0);
            }

            int money = actual_money -= quantity;
            user.houseInterior.safeBox = money;
            await DbFunctions.UpdateHouseSafebox(user.houseInterior.id, money);
            return true;
        }
    }
}

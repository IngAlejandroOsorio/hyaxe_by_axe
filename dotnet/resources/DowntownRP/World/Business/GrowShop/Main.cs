using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Business.GrowShop
{
    public class Main : Script
    {
        [RemoteEvent("BuyGrowShop")]
        public async Task RE_BuyGrowShop(Player player, string type)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            int price = 0;
            switch (type)
            {
                case "Amnesia Haze":
                    price = 30;
                    break;

                case "Moby Dick":
                    price = 30;
                    break;

                case "OG Kush":
                    price = 30;
                    break;

                case "Blueberry":
                    price = 30;
                    break;

                case "Black domina":
                    price = 30;
                    break;

                case "Cheese":
                    price = 30;
                    break;

                case "Crecimiento organico":
                    price = 15;
                    break;

                case "Floracion organica":
                    price = 30;
                    break;
            }

            if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
            {
                if (await Game.Money.MoneyModel.SubMoney(player, price))
                {
                    Data.Entities.Item itemCheck = await Game.Inventory.Inventory.CheckIfHasItem(user, type);
                    if (itemCheck == null)
                    {
                        Data.Entities.Item itemm = new Data.Entities.Item(0, type, 2, 1);
                        if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm)) Utilities.Notifications.SendNotificationOK(player, $"Has comprado 1 {type} por ${price}");
                        else Utilities.Notifications.SendNotificationERROR(player, "Se ha producido un error. Contacta con el staff.");
                    }
                    else itemCheck.quantity++;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
        }
    }
}

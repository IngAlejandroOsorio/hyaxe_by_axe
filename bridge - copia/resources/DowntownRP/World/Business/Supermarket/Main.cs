using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Business.Supermarket
{
    public class Main : Script
    {
        [RemoteEvent("BuyItemSupermarket")]
        public async Task RE_BuyItemSupermarket(Client player, string item)
        {
            player.SendChatMessage("lol" + item);
            if (!player.HasData("USER_CLASS")) return;

            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.isInBusiness)
            {
                int price = 0;
                string nameitem = item;
                int typeitem = 0;
                int quantity = 0;

                switch (item)
                {
                    case "Cigarros":
                        price = 20;
                        nameitem = "Cigarro";
                        typeitem = 1;
                        quantity = 20;
                        break;

                    case "Agua":
                        price = 20;
                        typeitem = 1;
                        quantity = 1;
                        break;

                    case "Manzana":
                        price = 20;
                        typeitem = 1;
                        quantity = 1;
                        break;

                    case "Botiquin":
                        price = 20;
                        typeitem = 1;
                        quantity = 1;
                        break;
                }

                if(await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    if (await Game.Money.MoneyModel.SubMoney(player, price))
                    {
                        Data.Entities.Item itemm = new Data.Entities.Item(0, nameitem, typeitem, quantity);
                        if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm)) Utilities.Notifications.SendNotificationOK(player, $"Has comprado {quantity} {item} por ${price}");
                        else Utilities.Notifications.SendNotificationERROR(player, "Se ha producido un error. Contacta con el staff.");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
            }
        }
    }
}

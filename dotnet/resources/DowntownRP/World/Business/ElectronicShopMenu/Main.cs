using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Business.ElectronicShopMenu
{
    public class Main : Script
    {
        [RemoteEvent("BuyMobilePhone")]
        public async Task RE_BuyMobilePhone(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.phone == 0)
            {
                if (await Game.Money.MoneyModel.SubMoney(player, 100))
                {
                    int phone = int.Parse(Utilities.Generate.CreatePhoneNumber());
                    Game.CharacterSelector.CharacterSelector.UpdateUserPhone(user.idpj, phone);
                    user.phone = phone;
                    Utilities.Notifications.SendNotificationOK(player, $"Has comprado un teléfono | Número {phone}");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "Ya tienes un movil");
        }

        [RemoteEvent("BuyBoombox")]
        public async Task RE_BuyBoombox(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            Console.WriteLine("hola");
            try
            {
                if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    Console.WriteLine("adios");
                    if (await Game.Money.MoneyModel.SubMoney(player, 500))
                    {
                        Console.WriteLine("buenas");
                        Data.Entities.Item itemm = new Data.Entities.Item(0, "Boombox", 1, 1);
                        if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm)) Utilities.Notifications.SendNotificationOK(player, $"Has comprado un boombox por $500");
                        else Utilities.Notifications.SendNotificationERROR(player, "Se ha producido un error. Contacta con el staff.");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes dinero suficiente");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio suficiente en tu inventario");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

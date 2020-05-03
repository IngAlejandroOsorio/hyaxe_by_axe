using DowntownRP.Data.Entities;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DowntownRP.Game.Inventory
{
    public class Inventory : Script
    {
        [RemoteEvent("ActionInventory")]
        public void RE_ActionInventory(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (!user.isInventoryOpen)
            {
                player.TriggerEvent("OpenInventory", JsonConvert.SerializeObject(user.inventory));
                user.isInventoryOpen = true;
                return;
            }
            else
            {
                player.TriggerEvent("CloseInventory");
                user.isInventoryOpen = false;
                return;
            }
        }

        [RemoteEvent("debuginv")]
        public void RE_debuginv(Player player, string lol)
        {
            Console.WriteLine($"eso es {lol}");
        }

        public async static Task<bool> CheckIfPlayerHasSlot(Data.Entities.User user)
        {
            if (user.inventory.slot1.name == "NO") return true;
            if (user.inventory.slot2.name == "NO") return true;
            if (user.inventory.slot3.name == "NO") return true;
            if (user.inventory.slot4.name == "NO") return true;
            if (user.inventory.slot5.name == "NO") return true;
            if (user.inventory.slot6.name == "NO") return true;
            if (user.inventory.slot7.name == "NO") return true;
            if (user.inventory.slot8.name == "NO") return true;
            if (user.inventory.slot9.name == "NO") return true;
            if (user.inventory.slot10.name == "NO") return true;
            if (user.inventory.slot11.name == "NO") return true;
            if (user.inventory.slot12.name == "NO") return true;

            return false;
        }

        public async static Task<Item> CheckIfHasItem(Data.Entities.User user, string itemName)
        {
            if (user.inventory.slot1.name == itemName) return user.inventory.slot1;
            if (user.inventory.slot2.name == itemName) return user.inventory.slot2;
            if (user.inventory.slot3.name == itemName) return user.inventory.slot3;
            if (user.inventory.slot4.name == itemName) return user.inventory.slot4;
            if (user.inventory.slot5.name == itemName) return user.inventory.slot5;
            if (user.inventory.slot6.name == itemName) return user.inventory.slot6;
            if (user.inventory.slot7.name == itemName) return user.inventory.slot7;
            if (user.inventory.slot8.name == itemName) return user.inventory.slot8;
            if (user.inventory.slot9.name == itemName) return user.inventory.slot9;
            if (user.inventory.slot10.name == itemName) return user.inventory.slot10;
            if (user.inventory.slot11.name == itemName) return user.inventory.slot11;
            if (user.inventory.slot12.name == itemName) return user.inventory.slot12;

            return null;
        }

        [RemoteEvent("UseItemInventory")]
        public async Task RE_UseItemInventory(Player player, int slot)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            Data.Entities.Item item;

            player.TriggerEvent("CloseInventory");
            user.isInventoryOpen = false;

            switch (slot)
            {
                case 1:
                    item = user.inventory.slot1;
                    if (user.inventory.slot1.quantity > 1)
                    {
                        user.inventory.slot1.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot1.id, user.idpj, user.inventory.slot1.name, user.inventory.slot1.type, user.inventory.slot1.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot1 = new Item(0, "NO", 0, 0);
                        user.inventory.slot1.slot = 1;
                    }
                    break;

                case 2:
                    item = user.inventory.slot2;
                    if (user.inventory.slot2.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot2.id, user.idpj, user.inventory.slot2.name, user.inventory.slot2.type, user.inventory.slot2.quantity, slot);
                        user.inventory.slot2.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot2 = new Item(0, "NO", 0, 0);
                        user.inventory.slot2.slot = 2;
                    }
                    break;

                case 3:
                    item = user.inventory.slot3;
                    if (user.inventory.slot3.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot3.id, user.idpj, user.inventory.slot3.name, user.inventory.slot3.type, user.inventory.slot3.quantity, slot);
                        user.inventory.slot3.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot3 = new Item(0, "NO", 0, 0);
                        user.inventory.slot3.slot = 3;
                    }
                    break;

                case 4:
                    item = user.inventory.slot4;
                    if (user.inventory.slot4.quantity > 1)
                    {
                        user.inventory.slot4.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot4.id, user.idpj, user.inventory.slot4.name, user.inventory.slot4.type, user.inventory.slot4.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot4 = new Item(0, "NO", 0, 0);
                        user.inventory.slot4.slot = 4;
                    }
                    break;

                case 5:
                    item = user.inventory.slot5;
                    if (user.inventory.slot5.quantity > 1)
                    {
                        user.inventory.slot5.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot5.id, user.idpj, user.inventory.slot5.name, user.inventory.slot5.type, user.inventory.slot5.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot5 = new Item(0, "NO", 0, 0);
                        user.inventory.slot5.slot = 5;
                    }
                    break;

                case 6:
                    item = user.inventory.slot6;
                    if (user.inventory.slot6.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot6.id, user.idpj, user.inventory.slot6.name, user.inventory.slot6.type, user.inventory.slot6.quantity, slot);
                        user.inventory.slot6.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot6 = new Item(0, "NO", 0, 0);
                        user.inventory.slot6.slot = 6;
                    }
                    break;

                case 7:
                    item = user.inventory.slot7;
                    if (user.inventory.slot7.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot7.id, user.idpj, user.inventory.slot7.name, user.inventory.slot7.type, user.inventory.slot7.quantity, slot);
                        user.inventory.slot7.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot7 = new Item(0, "NO", 0, 0);
                        user.inventory.slot7.slot = 7;
                    }
                    break;

                case 8:
                    item = user.inventory.slot8;
                    if (user.inventory.slot8.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot8.id, user.idpj, user.inventory.slot8.name, user.inventory.slot8.type, user.inventory.slot8.quantity, slot);
                        user.inventory.slot8.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot8 = new Item(0, "NO", 0, 0);
                        user.inventory.slot8.slot = 8;
                    }
                    break;

                case 9:
                    item = user.inventory.slot9;
                    if (user.inventory.slot9.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot9.id, user.idpj, user.inventory.slot9.name, user.inventory.slot9.type, user.inventory.slot9.quantity, slot);
                        user.inventory.slot9.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot9 = new Item(0, "NO", 0, 0);
                        user.inventory.slot9.slot = 9;
                    }
                    break;

                case 10:
                    item = user.inventory.slot10;
                    if (user.inventory.slot10.quantity > 1)
                    {
                        user.inventory.slot10.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot10.id, user.idpj, user.inventory.slot10.name, user.inventory.slot10.type, user.inventory.slot10.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot10 = new Item(0, "NO", 0, 0);
                        user.inventory.slot10.slot = 10;
                    }
                    break;

                case 11:
                    item = user.inventory.slot11;
                    if (user.inventory.slot11.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot11.id, user.idpj, user.inventory.slot11.name, user.inventory.slot11.type, user.inventory.slot11.quantity, slot);
                        user.inventory.slot11.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot11 = new Item(0, "NO", 0, 0);
                        user.inventory.slot11.slot = 11;
                    }
                    break;

                case 12:
                    item = user.inventory.slot12;
                    if (user.inventory.slot12.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot12.id, user.idpj, user.inventory.slot12.name, user.inventory.slot12.type, user.inventory.slot12.quantity, slot);
                        user.inventory.slot12.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot12 = new Item(0, "NO", 0, 0);
                        user.inventory.slot12.slot = 12;
                    }
                    break;

                default:
                    return;
            }

            if(item.bullets != 0)
            {
                player.GiveWeapon(Utilities.Weapon.GetWeaponHashByName(item.name), item.bullets);
            }

            switch (item.name)
            {
                case "Cigarro":
                    player.SendChatMessage("Has usado un cigarro");
                    break;

                case "Agua":
                    player.SendChatMessage("Has usado un agua");
                    break;

                case "Manzana":
                    player.SendChatMessage("Has usado una manzana");
                    break;

                case "Botiquin":
                    player.Health = 100;
                    Utilities.Notifications.SendNotificationOK(player, "Has usado un botiquín");
                    break;

            }

        }

        [RemoteEvent("DropItemInventory")]
        public async Task RE_DropItemInventory(Player player, int slot)
        {
            if (player.HasData("ITEM_SHAPE"))
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes tirar un objeto cerca de otro");
                return;
            }


            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            Data.Entities.Item item;

            player.TriggerEvent("CloseInventory");
            user.isInventoryOpen = false;

            switch (slot)
            {
                case 1:
                    item = user.inventory.slot1;
                    if (user.inventory.slot1.quantity > 1)
                    {
                        user.inventory.slot1.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot1.id, user.idpj, user.inventory.slot1.name, user.inventory.slot1.type, user.inventory.slot1.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot1 = new Item(0, "NO", 0, 0);
                        user.inventory.slot1.slot = 1;
                    }
                    break;

                case 2:
                    item = user.inventory.slot2;
                    if (user.inventory.slot2.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot2.id, user.idpj, user.inventory.slot2.name, user.inventory.slot2.type, user.inventory.slot2.quantity, slot);
                        user.inventory.slot2.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot2 = new Item(0, "NO", 0, 0);
                        user.inventory.slot2.slot = 2;
                    }
                    break;

                case 3:
                    item = user.inventory.slot3;
                    if (user.inventory.slot3.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot3.id, user.idpj, user.inventory.slot3.name, user.inventory.slot3.type, user.inventory.slot3.quantity, slot);
                        user.inventory.slot3.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot3 = new Item(0, "NO", 0, 0);
                        user.inventory.slot3.slot = 3;
                    }
                    break;

                case 4:
                    item = user.inventory.slot4;
                    if (user.inventory.slot4.quantity > 1)
                    {
                        user.inventory.slot4.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot4.id, user.idpj, user.inventory.slot4.name, user.inventory.slot4.type, user.inventory.slot4.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot4 = new Item(0, "NO", 0, 0);
                        user.inventory.slot4.slot = 4;
                    }
                    break;

                case 5:
                    item = user.inventory.slot5;
                    if (user.inventory.slot5.quantity > 1)
                    {
                        user.inventory.slot5.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot5.id, user.idpj, user.inventory.slot5.name, user.inventory.slot5.type, user.inventory.slot5.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot5 = new Item(0, "NO", 0, 0);
                        user.inventory.slot5.slot = 5;
                    }
                    break;

                case 6:
                    item = user.inventory.slot6;
                    if (user.inventory.slot6.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot6.id, user.idpj, user.inventory.slot6.name, user.inventory.slot6.type, user.inventory.slot6.quantity, slot);
                        user.inventory.slot6.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot6 = new Item(0, "NO", 0, 0);
                        user.inventory.slot6.slot = 6;
                    }
                    break;

                case 7:
                    item = user.inventory.slot7;
                    if (user.inventory.slot7.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot7.id, user.idpj, user.inventory.slot7.name, user.inventory.slot7.type, user.inventory.slot7.quantity, slot);
                        user.inventory.slot7.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot7 = new Item(0, "NO", 0, 0);
                        user.inventory.slot7.slot = 7;
                    }
                    break;

                case 8:
                    item = user.inventory.slot8;
                    if (user.inventory.slot8.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot8.id, user.idpj, user.inventory.slot8.name, user.inventory.slot8.type, user.inventory.slot8.quantity, slot);
                        user.inventory.slot8.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot8 = new Item(0, "NO", 0, 0);
                        user.inventory.slot8.slot = 8;
                    }
                    break;

                case 9:
                    item = user.inventory.slot9;
                    if (user.inventory.slot9.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot9.id, user.idpj, user.inventory.slot9.name, user.inventory.slot9.type, user.inventory.slot9.quantity, slot);
                        user.inventory.slot9.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot9 = new Item(0, "NO", 0, 0);
                        user.inventory.slot9.slot = 9;
                    }
                    break;

                case 10:
                    item = user.inventory.slot10;
                    if (user.inventory.slot10.quantity > 1)
                    {
                        user.inventory.slot10.quantity--;
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot10.id, user.idpj, user.inventory.slot10.name, user.inventory.slot10.type, user.inventory.slot10.quantity, slot);
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot10 = new Item(0, "NO", 0, 0);
                        user.inventory.slot10.slot = 10;
                    }
                    break;

                case 11:
                    item = user.inventory.slot11;
                    if (user.inventory.slot11.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot11.id, user.idpj, user.inventory.slot11.name, user.inventory.slot11.type, user.inventory.slot11.quantity, slot);
                        user.inventory.slot11.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot11 = new Item(0, "NO", 0, 0);
                        user.inventory.slot11.slot = 11;
                    }
                    break;

                case 12:
                    item = user.inventory.slot12;
                    if (user.inventory.slot12.quantity > 1)
                    {
                        await Game.Inventory.DatabaseFunctions.UpdateItemDatabase(user.inventory.slot12.id, user.idpj, user.inventory.slot12.name, user.inventory.slot12.type, user.inventory.slot12.quantity, slot);
                        user.inventory.slot12.quantity--;
                    }
                    else
                    {
                        await DatabaseFunctions.RemoveItemDatabase(item.id);
                        user.inventory.slot12 = new Item(0, "NO", 0, 0);
                        user.inventory.slot12.slot = 12;
                    }
                    break;

                default:
                    return;
            }

            uint idObjeto = 0;
            switch (item.name)
            {
                case "Cigarro":
                    idObjeto = 175300549;
                    break;

                case "Agua":
                    idObjeto = 746336278;
                    break;

                case "Manzana":
                    player.SendChatMessage("Has usado una manzana");
                    break;

                case "Botiquin":
                    idObjeto = 678958360;
                    break;
            }

            Utilities.Notifications.SendNotificationOK(player, "Has tirado un " + item.name);
            GTANetworkAPI.Object iitem = NAPI.Object.CreateObject(idObjeto, player.Position.Subtract(new Vector3(0, 0, 0.9)), new Vector3(0, 0, 0));
            ColShape shape = NAPI.ColShape.CreateCylinderColShape(player.Position, 1, 1);
            TextLabel label = NAPI.TextLabel.CreateTextLabel("Pulsa ~g~Y ~w~para recoger", player.Position.Subtract(new Vector3(0, 0, 0.9)), 2, 1, 0, new Color(255, 255, 255));

            if (item.bullets != 0) shape.SetData<int>("ITEM_BULLETS", item.bullets);
            shape.SetData("ITEM_PROP", iitem);
            shape.SetData("ITEM_LABEL", label);
            shape.SetData("ITEM_NAME", item.name);
        }

        [RemoteEvent("ActionPickItem")]
        public async Task ActionPickItem(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.chatStatus) return;
            if (player.HasData("ITEM_SHAPE"))
            {
                if (!player.HasData("ITEM_SECURITY"))
                {
                    player.SetData("ITEM_SECURITY", true);
                    ColShape shape = player.GetData<ColShape>("ITEM_SHAPE");
                    string name = shape.GetData<string>("ITEM_NAME");
                    // HAY QUE AGREGAR LOS TIPOS DE ITEM
                    if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                    {
                        Data.Entities.Item itemm = new Data.Entities.Item(0, name, 1, 1);
                        if (shape.HasData("ITEM_BULLETS"))
                        {
                            if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm, true, shape.GetData<int>("ITEM_BULLETS")))
                            {
                                NAPI.Entity.DeleteEntity(shape.GetData<GTANetworkAPI.Object>("ITEM_PROP"));
                                NAPI.Entity.DeleteEntity(shape.GetData<TextLabel>("ITEM_LABEL"));
                                NAPI.Entity.DeleteEntity(shape);
                                Utilities.Notifications.SendNotificationOK(player, "Has recogido un " + name.ToLower());
                                player.ResetData("ITEM_SHAPE");
                                await Task.Delay(3000);
                                player.ResetData("ITEM_SECURITY");
                            }
                            else Utilities.Notifications.SendNotificationERROR(player, "Se ha producido un error. Contacta con el staff.");
                        }
                        else
                        {
                            if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm))
                            {
                                NAPI.Entity.DeleteEntity(shape.GetData<GTANetworkAPI.Object>("ITEM_PROP"));
                                NAPI.Entity.DeleteEntity(shape.GetData<TextLabel>("ITEM_LABEL"));
                                NAPI.Entity.DeleteEntity(shape);
                                Utilities.Notifications.SendNotificationOK(player, "Has recogido un " + name.ToLower());
                                player.ResetData("ITEM_SHAPE");
                                await Task.Delay(3000);
                                player.ResetData("ITEM_SECURITY");
                            }
                            else Utilities.Notifications.SendNotificationERROR(player, "Se ha producido un error. Contacta con el staff.");
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Espera unos segundos para volver a coger otro item");
            }
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void EnterCol_Item(ColShape shape, Player player)
        {
            if (shape.HasData("ITEM_PROP"))
            {
                player.SetData("ITEM_SHAPE", shape);
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void ExitCol_Item(ColShape shape, Player player)
        {
            if (shape.HasData("ITEM_PROP"))
            {
                player.ResetData("ITEM_SHAPE");
            }
        }

    }
}

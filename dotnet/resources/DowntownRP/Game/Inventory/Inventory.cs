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
            string iName = "";
            switch (slot)
            {
                case 1:
                    iName = user.inventory.slot1.name;
                    if(iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if(iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot2.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot3.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot4.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot5.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot6.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot7.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot8.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot9.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

                    if (iName == "Crecimiento organico" || iName == "Floracion organica")
                    {
                        if (!player.HasData("WEED_CLASS"))
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "No hay ninguna planta cerca");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot10.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot11.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }
                    }

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
                    iName = user.inventory.slot12.name;
                    if (iName == "Amnesia Haze" || iName == "Moby Dick" || iName == "OG Kush" || iName == "Blueberry" || iName == "Black domina" || iName == "Cheese")
                    {
                        if (!user.isInHouseInterior)
                        {
                            Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en una casa para poder plantar");
                            return;
                        }

                    }
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

            // Weed vars
            Data.Entities.Weed weedData;
            TextLabel laabel;
            GTANetworkAPI.Object weed;
            int idweed;
            ColShape shaape;

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
                case "Caña":
                    //player.SendChatMessage("Has usado una manzana");
                    player.TriggerEvent("equiparPesca");
                    break;

                //pesca 
                case "Merluzo":
                    player.SendChatMessage("Debes vender el Merluzo en el Wiki Wiki o procesarlo como alimento en la Pesquera, para llevarlo al Mercado o a tu Negocio");
                    break;
                case "Salmon":
                    player.SendChatMessage("Debes vender el Salmón en el Wiki Wiki o procesarlo como alimento en la Pesquera, para llevarlo al Mercado o a tu Negocio");
                    break;
                case "Trucha":
                    player.SendChatMessage("Debes vender la Trucha en el Wiki Wiki o procesarlo como alimento en la Pesquera, para llevarlo al Mercado o a tu Negocio");
                    break;

                // marihuana
                case "Amnesia Haze":
                    idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, item.name);
                    weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                    laabel = NAPI.TextLabel.CreateTextLabel($"~g~{item.name}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                    weedData = new Data.Entities.Weed() { id = idweed, type = item.name, dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
                    shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
                    weed.Dimension = player.Dimension;
                    laabel.Dimension = player.Dimension;
                    weedData.prop = weed;
                    weedData.label = laabel;
                    Data.Lists.weedPlants.Add(weedData);
                    break;

                case "Moby Dick":
                    idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, item.name);
                    weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                    laabel = NAPI.TextLabel.CreateTextLabel($"~g~{item.name}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                    weedData = new Data.Entities.Weed() { id = idweed, type = item.name, dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
                    shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
                    weed.Dimension = player.Dimension;
                    laabel.Dimension = player.Dimension;
                    weedData.prop = weed;
                    weedData.label = laabel;
                    Data.Lists.weedPlants.Add(weedData);
                    break;

                case "OG Kush":
                    idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, item.name);
                    weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                    laabel = NAPI.TextLabel.CreateTextLabel($"~g~{item.name}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                    weedData = new Data.Entities.Weed() { id = idweed, type = item.name, dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
                    shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
                    weed.Dimension = player.Dimension;
                    laabel.Dimension = player.Dimension;
                    weedData.prop = weed;
                    weedData.label = laabel;
                    Data.Lists.weedPlants.Add(weedData);
                    break;

                case "Blueberry":
                    idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, item.name);
                    weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                    laabel = NAPI.TextLabel.CreateTextLabel($"~g~{item.name}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                    weedData = new Data.Entities.Weed() { id = idweed, type = item.name, dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
                    shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
                    weed.Dimension = player.Dimension;
                    laabel.Dimension = player.Dimension;
                    weedData.prop = weed;
                    weedData.label = laabel;
                    Data.Lists.weedPlants.Add(weedData);
                    break;

                case "Black domina":
                    idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, item.name);
                    weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                    laabel = NAPI.TextLabel.CreateTextLabel($"~g~{item.name}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                    weedData = new Data.Entities.Weed() { id = idweed, type = item.name, dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
                    shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
                    weed.Dimension = player.Dimension;
                    laabel.Dimension = player.Dimension;
                    weedData.prop = weed;
                    weedData.label = laabel;
                    Data.Lists.weedPlants.Add(weedData);
                    break;

                case "Cheese":
                    idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, item.name);
                    weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                    laabel = NAPI.TextLabel.CreateTextLabel($"~g~{item.name}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                    weedData = new Data.Entities.Weed() { id = idweed, type = item.name, dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
                    shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
                    weed.Dimension = player.Dimension;
                    laabel.Dimension = player.Dimension;
                    weedData.prop = weed;
                    weedData.label = laabel;
                    Data.Lists.weedPlants.Add(weedData);
                    break;

                // riego marihuana
                case "Crecimiento organico":
                    await Drugs.Weed.Main.RegarPlanta(player, player.GetData<Data.Entities.Weed>("WEED_CLASS"));
                    break;

                case "Floracion organica":
                    await Drugs.Weed.Main.RegarPlanta(player, player.GetData<Data.Entities.Weed>("WEED_CLASS"));
                    break;

                case "Botiquin":
                    player.Health = 100;
                    Utilities.Notifications.SendNotificationOK(player, "Has usado un botiquín");
                    break;

                case "Boombox":
                    Utilities.Notifications.SendNotificationOK(player, "Usa /boombox cerca del boombox par acceder al menú");
                    GTANetworkAPI.Object iitem = NAPI.Object.CreateObject(1729911864, player.Position.Subtract(new Vector3(0, 0, 0.9)), new Vector3(0, 0, 0));
                    ColShape shape = NAPI.ColShape.CreateCylinderColShape(player.Position, 5, 5);
                    TextLabel label = NAPI.TextLabel.CreateTextLabel("Usa ~b~/boombox", player.Position.Subtract(new Vector3(0, 0, 0.9)), 2, 1, 0, new Color(255, 255, 255));

                    shape.SetData("BOOMBOX_PROP", iitem);
                    shape.SetData("BOOMBOX_LABEL", label);
                    shape.SetData("BOOMBOX_OWNER", player);
                    break;
            }

        }

        [Command("weedtest")]
        public async Task weedtest(Player player)
        {
            int idweed = await Game.Drugs.Weed.DbFunctions.CreateWeedDb(player, "Cheese");
            GTANetworkAPI.Object weed = NAPI.Object.CreateObject(1595624552, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3());
            TextLabel laabel = NAPI.TextLabel.CreateTextLabel($"~g~Cheese~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", player.Position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
            Data.Entities.Weed weedData = new Data.Entities.Weed() { id = idweed, type = "Cheese", dimension = (int)player.Dimension, position = player.Position, status = 1, label = laabel, prop = weed };
            ColShape shaape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
            shaape.SetData<Data.Entities.Weed>("WEED_CLASS", weedData);
        }

        [Command("testweed")]
        public async Task testweed(Player player)
        {
            await Drugs.Weed.Main.PaydayMarihuana();
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

                case "Boombox":
                    idObjeto = 1729911864;
                    break;
                    //case "Salmon" 3822287123
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

            if (shape.HasData("BOOMBOX_PROP"))
            {
                player.SetData("BOOMBOX_COL", shape);
            }

        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void ExitCol_Item(ColShape shape, Player player)
        {
            if (shape.HasData("ITEM_PROP"))
            {
                player.ResetData("ITEM_SHAPE");
            }

            if (shape.HasData("BOOMBOX_PROP"))
            {
                player.ResetData("BOOMBOX_COL");
            }

        }

        [Command("boombox")]
        public void CMD_boombox(Player player)
        {
            if (player.HasData("BOOMBOX_COL"))
            {
                if (player.GetData<ColShape>("BOOMBOX_COL").GetData<Player>("BOOMBOX_OWNER") == player) player.TriggerEvent("OpenBoomboxMenu");
                else Utilities.Notifications.SendNotificationERROR(player, "No eres el dueño del boombox");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes ningún boombox cerca");
        }

        [RemoteEvent("PickUpBoombox")]
        public async Task RE_PickUpBoombox(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            
            if (player.HasData("BOOMBOX_COL"))
            {
                if (player.GetData<ColShape>("BOOMBOX_COL").GetData<Player>("BOOMBOX_OWNER") == player)
                {
                    if(await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                    {
                        Data.Entities.Item itemm = new Data.Entities.Item(0, "Boombox", 1, 1);
                        if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm))
                        {
                            ColShape shape = player.GetData<ColShape>("BOOMBOX_COL");
                            shape.GetData<GTANetworkAPI.Object>("iitem").Delete();
                            shape.GetData<TextLabel>("BOOMBOX_LABEL").Delete();
                            shape.Delete();
                            Utilities.Notifications.SendNotificationOK(player, $"Has recogido tu boombox");
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "Se ha producido un error. Contacta con el staff.");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres el dueño del boombox");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes ningún boombox cerca");
        }


        [RemoteEvent("DarPescado")]
        public async Task DarPescado(Player player, string item)
        {
            if (!player.HasData("USER_CLASS")) return;

            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

                int price = 0;
                string nameitem = item;
                int typeitem = 0;
                int quantity = 0;

                switch (item)
                {
                    case "Merluzo":
                        price = 50;
                        nameitem = "Merluzo";
                        typeitem = 5;
                        quantity = 1;
                        break;
                    case "Salmon":
                        price = 50;
                        nameitem = "Salmon";
                        typeitem = 5;
                        quantity = 1;
                        break;
                    case "Trucha":
                        price = 50;
                        nameitem = "Trucha";
                        typeitem = 5;
                        quantity = 1;
                        break;
                    case "Caña":
                        price = 500;
                        nameitem = "Caña";
                        typeitem = 2;
                        quantity = 1;
                        break;
                }

                if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                        Data.Entities.Item itemCheck = await Game.Inventory.Inventory.CheckIfHasItem(user, nameitem);
                        if (itemCheck == null)
                        {
                            Data.Entities.Item itemm = new Data.Entities.Item(0, nameitem, typeitem, quantity);
                            if (await Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm) && item != "Caña") Utilities.Notifications.SendNotificationOK(player, $"Has pescado {quantity} {item}");                            
                        }
                        else itemCheck.quantity++;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");

        }


        [RemoteEvent("comproPescaServer")]
        public async Task pagoRoboAsync(Player player, string item, int ctd)
        {
            if (!player.HasData("USER_CLASS")) return;
            string asd = "esto llega como ctd " + ctd;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            string nameitem = item;

                Data.Entities.Item itemCheck = await Game.Inventory.Inventory.CheckIfHasItem(user, nameitem);
                if (itemCheck != null)
                {
                    Item slot;
                    switch (itemCheck.slot)
                    {
                        case 1:
                            slot = user.inventory.slot1;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot1 = new Item(0, "NO", 0, 0);
                            user.inventory.slot1.slot = 1;
                            break;
                        case 2:
                            slot = user.inventory.slot2;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot2 = new Item(0, "NO", 0, 0);
                            user.inventory.slot2.slot = 2;
                            break;
                        case 3:
                            slot = user.inventory.slot3;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot3 = new Item(0, "NO", 0, 0);
                            user.inventory.slot3.slot = 3;
                            break;
                        case 4:
                            slot = user.inventory.slot4;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot4 = new Item(0, "NO", 0, 0);
                            user.inventory.slot4.slot = 4;
                            break;
                        case 5:
                            slot = user.inventory.slot5;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot5 = new Item(0, "NO", 0, 0);
                            user.inventory.slot5.slot = 5;
                            break;
                        case 6:
                            slot = user.inventory.slot6;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot6 = new Item(0, "NO", 0, 0);
                            user.inventory.slot6.slot = 6;
                            break;
                        case 7:
                            slot = user.inventory.slot7;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot7 = new Item(0, "NO", 0, 0);
                            user.inventory.slot7.slot = 7;
                            break;
                        case 8:
                            slot = user.inventory.slot8;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot8 = new Item(0, "NO", 0, 0);
                            user.inventory.slot8.slot = 8;
                            break;
                        case 9:
                            slot = user.inventory.slot9;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot9 = new Item(0, "NO", 0, 0);
                            user.inventory.slot9.slot = 9;
                            break;
                        case 10:
                            slot = user.inventory.slot10;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot10 = new Item(0, "NO", 0, 0);
                            user.inventory.slot10.slot = 10;
                            break;
                        case 11:
                            slot = user.inventory.slot11;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot11 = new Item(0, "NO", 0, 0);
                            user.inventory.slot11.slot = 11;
                            break;
                        case 12:
                            slot = user.inventory.slot12;
                            await DatabaseFunctions.RemoveItemDatabase(itemCheck.id);
                            user.inventory.slot12 = new Item(0, "NO", 0, 0);
                            user.inventory.slot12.slot = 12;
                            break;

                        default:
                            return;
                    }

                int pagox = itemCheck.quantity;
                itemCheck.quantity = itemCheck.quantity - ctd;

                int price = pagox * 500;
                    await Game.Money.MoneyModel.AddMoney(player, price);
                    Utilities.Notifications.SendNotificationOK(player, $"Has recibido {price} por tu venta de pescados.");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes esos pescados en tu inventario.");

        }



    }
}

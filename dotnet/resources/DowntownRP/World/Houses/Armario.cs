using DowntownRP.Data.Entities;
using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Houses
{
    public class Armario : Script
    {
        [RemoteEvent("OpenInventoryHouse")]
        public void RE_OpenInventoryHouse(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.isInHouseInterior) player.TriggerEvent("OpenHouseInventory", JsonConvert.SerializeObject(user.inventory), JsonConvert.SerializeObject(user.houseInterior.inventory));
            if (user.isInFactionInterior) player.TriggerEvent("OpenHouseInventory", JsonConvert.SerializeObject(user.inventory), JsonConvert.SerializeObject(user.ilegalFactionInterior.inventory));
        }

        [RemoteEvent("ItemFromInvMoveH")]
        public async Task RE_ItemFromInvMoveH(Player player, int slot)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.isInHouseInterior)
            {
                if (await CheckIfHouseHasSlot(user.houseInterior))
                {
                    Data.Entities.House house = user.houseInterior;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot1);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot1.id, 0, house.id);
                            user.inventory.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot2);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot2.id, 0, house.id);
                            user.inventory.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot3);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot3.id, 0, house.id);
                            user.inventory.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot4);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot4.id, 0, house.id);
                            user.inventory.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot5);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot5.id, 0, house.id);
                            user.inventory.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot6);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot6.id, 0, house.id);
                            user.inventory.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot7);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot7.id, 0, house.id);
                            user.inventory.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot8);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot8.id, 0, house.id);
                            user.inventory.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot9);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot9.id, 0, house.id);
                            user.inventory.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot10);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot10.id, 0, house.id);
                            user.inventory.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot11);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot11.id, 0, house.id);
                            user.inventory.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot12);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, user.inventory.slot12.id, 0, house.id);
                            user.inventory.slot12 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en el armario");
                    player.TriggerEvent("CloseHouseInventory");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Tu armario no tiene mas espacio");
            }
            else
            {
                if (await CheckIfFacHasSlot(user.ilegalFactionInterior))
                {
                    Data.Entities.Faction house = user.ilegalFactionInterior;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot1);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot1.id, 0, house.id);
                            user.inventory.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot2);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot2.id, 0, house.id);
                            user.inventory.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot3);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot3.id, 0, house.id);
                            user.inventory.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot4);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot4.id, 0, house.id);
                            user.inventory.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot5);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot5.id, 0, house.id);
                            user.inventory.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot6);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot6.id, 0, house.id);
                            user.inventory.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot7);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot7.id, 0, house.id);
                            user.inventory.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot8);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot8.id, 0, house.id);
                            user.inventory.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot9);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot9.id, 0, house.id);
                            user.inventory.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot10);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot10.id, 0, house.id);
                            user.inventory.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot11);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot11.id, 0, house.id);
                            user.inventory.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemHouseInventory(house.inventory, user.inventory.slot12);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(2, user.inventory.slot12.id, 0, house.id);
                            user.inventory.slot12 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en el armario");
                    player.TriggerEvent("CloseHouseInventory");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Tu armario no tiene mas espacio");
            }
        }

        [RemoteEvent("ItemToInvMoveH")]
        public async Task RE_ItemToInvMove(Player player, int slot)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.isInHouseInterior)
            {
                if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    Data.Entities.House house = user.houseInterior;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot1);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot1.id, user.idpj, 0);
                            house.inventory.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot2);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot2.id, user.idpj, 0);
                            house.inventory.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot3);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot3.id, user.idpj, 0);
                            house.inventory.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot4);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot4.id, user.idpj, 0);
                            house.inventory.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot5);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot5.id, user.idpj, 0);
                            house.inventory.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot6);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot6.id, user.idpj, 0);
                            house.inventory.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot7);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot7.id, user.idpj, 0);
                            house.inventory.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot8);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot8.id, user.idpj, 0);
                            house.inventory.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot9);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot9.id, user.idpj, 0);
                            house.inventory.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot10);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot10.id, user.idpj, 0);
                            house.inventory.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot11);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot11.id, user.idpj, 0);
                            house.inventory.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot12);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot12.id, user.idpj, 0);
                            house.inventory.slot12 = new Item(0, "NO", 0, 0);
                            break;

                        case 13:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot13);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot13.id, user.idpj, 0);
                            house.inventory.slot13 = new Item(0, "NO", 0, 0);
                            break;

                        case 14:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot14);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot14.id, user.idpj, 0);
                            house.inventory.slot14 = new Item(0, "NO", 0, 0);
                            break;

                        case 15:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot15);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot15.id, user.idpj, 0);
                            house.inventory.slot15 = new Item(0, "NO", 0, 0);
                            break;

                        case 16:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot16);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot16.id, user.idpj, 0);
                            house.inventory.slot16 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en tu inventario");
                    player.TriggerEvent("CloseHouseInventory");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
            }
            else
            {
                if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    Data.Entities.Faction house = user.ilegalFactionInterior;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot1);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot1.id, user.idpj, 0);
                            house.inventory.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot2);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot2.id, user.idpj, 0);
                            house.inventory.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot3);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot3.id, user.idpj, 0);
                            house.inventory.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot4);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot4.id, user.idpj, 0);
                            house.inventory.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot5);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot5.id, user.idpj, 0);
                            house.inventory.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot6);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot6.id, user.idpj, 0);
                            house.inventory.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot7);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot7.id, user.idpj, 0);
                            house.inventory.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot8);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot8.id, user.idpj, 0);
                            house.inventory.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot9);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot9.id, user.idpj, 0);
                            house.inventory.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot10);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot10.id, user.idpj, 0);
                            house.inventory.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot11);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot11.id, user.idpj, 0);
                            house.inventory.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot12);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot12.id, user.idpj, 0);
                            house.inventory.slot12 = new Item(0, "NO", 0, 0);
                            break;

                        case 13:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot13);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot13.id, user.idpj, 0);
                            house.inventory.slot13 = new Item(0, "NO", 0, 0);
                            break;

                        case 14:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot14);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot14.id, user.idpj, 0);
                            house.inventory.slot14 = new Item(0, "NO", 0, 0);
                            break;

                        case 15:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot15);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot15.id, user.idpj, 0);
                            house.inventory.slot15 = new Item(0, "NO", 0, 0);
                            break;

                        case 16:
                            await SetNewItemInventoryFromHouse(user, house.inventory.slot16);
                            await DbFunctions.UpdateUserOrHouseOwnerItem(1, house.inventory.slot16.id, user.idpj, 0);
                            house.inventory.slot16 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en tu inventario");
                    player.TriggerEvent("CloseHouseInventory");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
            }
        }

        public async static Task<bool> SetNewItemInventoryFromHouse(Data.Entities.User user, Data.Entities.Item item)
        {
            if (user.inventory.slot1.name == "NO")
            {
                user.inventory.slot1 = item;
                user.inventory.slot1.slot = 1;
                return true;
            }

            if (user.inventory.slot2.name == "NO")
            {
                user.inventory.slot2 = item;
                user.inventory.slot2.slot = 2;
                return true;
            }

            if (user.inventory.slot3.name == "NO")
            {
                user.inventory.slot3 = item;
                user.inventory.slot3.slot = 3;
                return true;
            }

            if (user.inventory.slot4.name == "NO")
            {
                user.inventory.slot4 = item;
                user.inventory.slot4.slot = 4;
                return true;
            }

            if (user.inventory.slot5.name == "NO")
            {
                user.inventory.slot5 = item;
                user.inventory.slot5.slot = 5;
                return true;
            }

            if (user.inventory.slot6.name == "NO")
            {
                user.inventory.slot6 = item;
                user.inventory.slot6.slot = 6;
                return true;
            }

            if (user.inventory.slot7.name == "NO")
            {
                user.inventory.slot7 = item;
                user.inventory.slot7.slot = 7;
                return true;
            }

            if (user.inventory.slot8.name == "NO")
            {
                user.inventory.slot8 = item;
                user.inventory.slot8.slot = 8;
                return true;
            }

            if (user.inventory.slot9.name == "NO")
            {
                user.inventory.slot9 = item;
                user.inventory.slot9.slot = 9;
                return true;
            }

            if (user.inventory.slot10.name == "NO")
            {
                user.inventory.slot10 = item;
                user.inventory.slot10.slot = 10;
                return true;
            }

            if (user.inventory.slot11.name == "NO")
            {
                user.inventory.slot11 = item;
                user.inventory.slot11.slot = 11;
                return true;
            }

            if (user.inventory.slot12.name == "NO")
            {
                user.inventory.slot12 = item;
                user.inventory.slot12.slot = 12;
                return true;
            }

            return false; // Slots llenos
        }

        public async static Task<bool> SetNewItemHouseInventory(Data.Entities.HouseInventory trunk, Data.Entities.Item item)
        {
            if (trunk.slot1.name == "NO")
            {
                trunk.slot1 = item;
                trunk.slot1.slot = 1;
                return true;
            }

            if (trunk.slot2.name == "NO")
            {
                trunk.slot2 = item;
                trunk.slot2.slot = 2;
                return true;
            }

            if (trunk.slot3.name == "NO")
            {
                trunk.slot3 = item;
                trunk.slot3.slot = 3;
                return true;
            }

            if (trunk.slot4.name == "NO")
            {
                trunk.slot4 = item;
                trunk.slot4.slot = 4;
                return true;
            }

            if (trunk.slot5.name == "NO")
            {
                trunk.slot5 = item;
                trunk.slot5.slot = 5;
                return true;
            }

            if (trunk.slot6.name == "NO")
            {
                trunk.slot6 = item;
                trunk.slot6.slot = 6;
                return true;
            }

            if (trunk.slot7.name == "NO")
            {
                trunk.slot7 = item;
                trunk.slot7.slot = 7;
                return true;
            }

            if (trunk.slot8.name == "NO")
            {
                trunk.slot8 = item;
                trunk.slot8.slot = 8;
                return true;
            }

            if (trunk.slot9.name == "NO")
            {
                trunk.slot9 = item;
                trunk.slot9.slot = 9;
                return true;
            }

            if (trunk.slot10.name == "NO")
            {
                trunk.slot10 = item;
                trunk.slot10.slot = 10;
                return true;
            }

            if (trunk.slot11.name == "NO")
            {
                trunk.slot11 = item;
                trunk.slot11.slot = 11;
                return true;
            }

            if (trunk.slot12.name == "NO")
            {
                trunk.slot12 = item;
                trunk.slot12.slot = 12;
                return true;
            }

            if (trunk.slot13.name == "NO")
            {
                trunk.slot13 = item;
                trunk.slot13.slot = 13;
                return true;
            }

            if (trunk.slot14.name == "NO")
            {
                trunk.slot14 = item;
                trunk.slot14.slot = 14;
                return true;
            }

            if (trunk.slot15.name == "NO")
            {
                trunk.slot15 = item;
                trunk.slot15.slot = 15;
                return true;
            }

            if (trunk.slot16.name == "NO")
            {
                trunk.slot16 = item;
                trunk.slot16.slot = 16;
                return true;
            }

            return false; // Slots llenos
        }

        public async static Task<bool> CheckIfHouseHasSlot(Data.Entities.House house)
        {
            if (house.inventory.slot1.name == "NO") return true;
            if (house.inventory.slot2.name == "NO") return true;
            if (house.inventory.slot3.name == "NO") return true;
            if (house.inventory.slot4.name == "NO") return true;
            if (house.inventory.slot5.name == "NO") return true;
            if (house.inventory.slot6.name == "NO") return true;
            if (house.inventory.slot7.name == "NO") return true;
            if (house.inventory.slot8.name == "NO") return true;
            if (house.inventory.slot9.name == "NO") return true;
            if (house.inventory.slot10.name == "NO") return true;
            if (house.inventory.slot11.name == "NO") return true;
            if (house.inventory.slot12.name == "NO") return true;
            if (house.inventory.slot13.name == "NO") return true;
            if (house.inventory.slot14.name == "NO") return true;
            if (house.inventory.slot15.name == "NO") return true;
            if (house.inventory.slot16.name == "NO") return true;

            return false;
        }

        public async static Task<bool> CheckIfFacHasSlot(Data.Entities.Faction house)
        {
            if (house.inventory.slot1.name == "NO") return true;
            if (house.inventory.slot2.name == "NO") return true;
            if (house.inventory.slot3.name == "NO") return true;
            if (house.inventory.slot4.name == "NO") return true;
            if (house.inventory.slot5.name == "NO") return true;
            if (house.inventory.slot6.name == "NO") return true;
            if (house.inventory.slot7.name == "NO") return true;
            if (house.inventory.slot8.name == "NO") return true;
            if (house.inventory.slot9.name == "NO") return true;
            if (house.inventory.slot10.name == "NO") return true;
            if (house.inventory.slot11.name == "NO") return true;
            if (house.inventory.slot12.name == "NO") return true;
            if (house.inventory.slot13.name == "NO") return true;
            if (house.inventory.slot14.name == "NO") return true;
            if (house.inventory.slot15.name == "NO") return true;
            if (house.inventory.slot16.name == "NO") return true;

            return false;
        }
    }
}

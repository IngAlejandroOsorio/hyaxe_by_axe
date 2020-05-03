using DowntownRP.Data.Entities;
using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Vehicles
{
    public class Truck : Script
    {
        [RemoteEvent("OpenTruckInventory")]
        public async Task RE_OpenTruckInventory(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.chatStatus) return;

            if (!user.isTruckerOpen)
            {
                if(user.faction != 0)
                {
                    foreach (var vh in Data.Lists.factions.Find(x => x.id == user.faction).vehicles)
                    {
                        if(player.Position.DistanceTo(vh.entity.Position) < 5f)
                        {
                            if (vh.entity.HasData("TRUCK"))
                            {
                                if (vh.entity.GetData<bool>("TRUCK"))
                                {
                                    player.TriggerEvent("OpenVehicleInventory", JsonConvert.SerializeObject(user.inventory), JsonConvert.SerializeObject(vh.trucker));
                                    user.vehicleActualInvFac = vh;
                                    user.typeVehInv = 2;
                                    user.isTruckerOpen = true;
                                    return;
                                }
                            }
                        }
                    }
                }
                foreach (var veh in user.vehicles)
                {
                    if (player.Position.DistanceTo(veh.entity.Position) < 5f)
                    {
                        if (veh.entity.HasData("TRUCK"))
                        {
                            if (veh.entity.GetData<bool>("TRUCK"))
                            {
                                player.TriggerEvent("OpenVehicleInventory", JsonConvert.SerializeObject(user.inventory), JsonConvert.SerializeObject(veh.trunk));
                                user.vehicleActualInv = veh;
                                user.typeVehInv = 1;
                                user.isTruckerOpen = true;
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                player.TriggerEvent("CloseVehicleInventory");
                user.isTruckerOpen = false;
                user.vehicleActualInv = null;
            }
        }

        [RemoteEvent("ItemFromInvMove")]
        public async Task RE_ItemFromInvMove(Player player, int slot)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.typeVehInv == 2)
            {
                if (await CheckIfVehicleFacHasSlot(user.vehicleActualInvFac))
                {
                    Data.Entities.VehicleFaction veh = user.vehicleActualInvFac;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot1);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot1.id, 0, veh.id);
                            user.inventory.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot2);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot2.id, 0, veh.id);
                            user.inventory.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot3);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot3.id, 0, veh.id);
                            user.inventory.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot4);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot4.id, 0, veh.id);
                            user.inventory.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot5);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot5.id, 0, veh.id);
                            user.inventory.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot6);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot6.id, 0, veh.id);
                            user.inventory.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot7);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot7.id, 0, veh.id);
                            user.inventory.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot8);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot8.id, 0, veh.id);
                            user.inventory.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot9);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot9.id, 0, veh.id);
                            user.inventory.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot10);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot10.id, 0, veh.id);
                            user.inventory.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot11);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot11.id, 0, veh.id);
                            user.inventory.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemVehicleInventory(veh.trucker, user.inventory.slot12);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot12.id, 0, veh.id);
                            user.inventory.slot12 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en el maletero");
                    player.TriggerEvent("CloseVehicleInventory");
                    user.isTruckerOpen = false;
                    user.vehicleActualInvFac = null;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Tu vehículo no tiene mas espacio");
            }
            else
            {
                if (await CheckIfVehicleHasSlot(user.vehicleActualInv))
                {
                    Data.Entities.VehicleCharacter veh = user.vehicleActualInv;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot1);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot1.id, 0, veh.id);
                            user.inventory.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot2);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot2.id, 0, veh.id);
                            user.inventory.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot3);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot3.id, 0, veh.id);
                            user.inventory.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot4);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot4.id, 0, veh.id);
                            user.inventory.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot5);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot5.id, 0, veh.id);
                            user.inventory.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot6);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot6.id, 0, veh.id);
                            user.inventory.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot7);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot7.id, 0, veh.id);
                            user.inventory.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot8);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot8.id, 0, veh.id);
                            user.inventory.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot9);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot9.id, 0, veh.id);
                            user.inventory.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot10);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot10.id, 0, veh.id);
                            user.inventory.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot11);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot11.id, 0, veh.id);
                            user.inventory.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemVehicleInventory(veh.trunk, user.inventory.slot12);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, user.inventory.slot12.id, 0, veh.id);
                            user.inventory.slot12 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en el maletero");
                    player.TriggerEvent("CloseVehicleInventory");
                    user.isTruckerOpen = false;
                    user.vehicleActualInv = null;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "El vehículo no tiene mas espacio");
            }
        }

        [RemoteEvent("ItemToInvMove")]
        public async Task RE_ItemToInvMove(Player player, int slot)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.typeVehInv == 2)
            {
                if (await Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    Data.Entities.VehicleFaction veh = user.vehicleActualInvFac;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot1);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot1.id, user.idpj, 0);
                            veh.trucker.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot2);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot2.id, user.idpj, 0);
                            veh.trucker.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot3);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot3.id, user.idpj, 0);
                            veh.trucker.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot4);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot4.id, user.idpj, 0);
                            veh.trucker.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot5);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot5.id, user.idpj, 0);
                            veh.trucker.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot6);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot6.id, user.idpj, 0);
                            veh.trucker.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot7);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot7.id, user.idpj, 0);
                            veh.trucker.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot8);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot8.id, user.idpj, 0);
                            veh.trucker.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot9);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot9.id, user.idpj, 0);
                            veh.trucker.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot10);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot10.id, user.idpj, 0);
                            veh.trucker.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot11);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot11.id, user.idpj, 0);
                            veh.trucker.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemInventoryFromVehicle(user, veh.trucker.slot12);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trucker.slot12.id, user.idpj, 0);
                            veh.trucker.slot12 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en tu inventario");
                    player.TriggerEvent("CloseVehicleInventory");
                    user.isTruckerOpen = false;
                    user.vehicleActualInv = null;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
            }
            else
            {
                if (await Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    Data.Entities.VehicleCharacter veh = user.vehicleActualInv;
                    switch (slot)
                    {
                        case 1:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot1);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot1.id, user.idpj, 0);
                            veh.trunk.slot1 = new Item(0, "NO", 0, 0);
                            break;

                        case 2:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot2);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot2.id, user.idpj, 0);
                            veh.trunk.slot2 = new Item(0, "NO", 0, 0);
                            break;

                        case 3:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot3);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot3.id, user.idpj, 0);
                            veh.trunk.slot3 = new Item(0, "NO", 0, 0);
                            break;

                        case 4:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot4);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot4.id, user.idpj, 0);
                            veh.trunk.slot4 = new Item(0, "NO", 0, 0);
                            break;

                        case 5:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot5);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot5.id, user.idpj, 0);
                            veh.trunk.slot5 = new Item(0, "NO", 0, 0);
                            break;

                        case 6:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot6);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot6.id, user.idpj, 0);
                            veh.trunk.slot6 = new Item(0, "NO", 0, 0);
                            break;

                        case 7:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot7);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot7.id, user.idpj, 0);
                            veh.trunk.slot7 = new Item(0, "NO", 0, 0);
                            break;

                        case 8:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot8);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot8.id, user.idpj, 0);
                            veh.trunk.slot8 = new Item(0, "NO", 0, 0);
                            break;

                        case 9:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot9);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot9.id, user.idpj, 0);
                            veh.trunk.slot9 = new Item(0, "NO", 0, 0);
                            break;

                        case 10:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot10);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot10.id, user.idpj, 0);
                            veh.trunk.slot10 = new Item(0, "NO", 0, 0);
                            break;

                        case 11:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot11);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot11.id, user.idpj, 0);
                            veh.trunk.slot11 = new Item(0, "NO", 0, 0);
                            break;

                        case 12:
                            await SetNewItemInventoryFromVehicle(user, veh.trunk.slot12);
                            await DbHandler.UpdateUserOrVehicleOwnerItem(user.typeVehInv, veh.trunk.slot12.id, user.idpj, 0);
                            veh.trunk.slot12 = new Item(0, "NO", 0, 0);
                            break;
                    }

                    Utilities.Notifications.SendNotificationOK(player, "Has guardado un objeto en tu inventario");
                    player.TriggerEvent("CloseVehicleInventory");
                    user.isTruckerOpen = false;
                    user.vehicleActualInv = null;
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
            }
        }

        public async static Task<bool> SetNewItemInventoryFromVehicle(Data.Entities.User user, Data.Entities.Item item)
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

        public async static Task<bool> SetNewItemVehicleInventory(Data.Entities.Inventory trunk, Data.Entities.Item item)
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

            return false; // Slots llenos
        }

        public async static Task<bool> CheckIfVehicleHasSlot(Data.Entities.VehicleCharacter vehicle)
        {
            if (vehicle.trunk.slot1.name == "NO") return true;
            if (vehicle.trunk.slot2.name == "NO") return true;
            if (vehicle.trunk.slot3.name == "NO") return true;
            if (vehicle.trunk.slot4.name == "NO") return true;
            if (vehicle.trunk.slot5.name == "NO") return true;
            if (vehicle.trunk.slot6.name == "NO") return true;
            if (vehicle.trunk.slot7.name == "NO") return true;
            if (vehicle.trunk.slot8.name == "NO") return true;
            if (vehicle.trunk.slot9.name == "NO") return true;
            if (vehicle.trunk.slot10.name == "NO") return true;
            if (vehicle.trunk.slot11.name == "NO") return true;
            if (vehicle.trunk.slot12.name == "NO") return true;

            return false;
        }

        public async static Task<bool> CheckIfVehicleFacHasSlot(Data.Entities.VehicleFaction vehicle)
        {
            if (vehicle.trucker.slot1.name == "NO") return true;
            if (vehicle.trucker.slot2.name == "NO") return true;
            if (vehicle.trucker.slot3.name == "NO") return true;
            if (vehicle.trucker.slot4.name == "NO") return true;
            if (vehicle.trucker.slot5.name == "NO") return true;
            if (vehicle.trucker.slot6.name == "NO") return true;
            if (vehicle.trucker.slot7.name == "NO") return true;
            if (vehicle.trucker.slot8.name == "NO") return true;
            if (vehicle.trucker.slot9.name == "NO") return true;
            if (vehicle.trucker.slot10.name == "NO") return true;
            if (vehicle.trucker.slot11.name == "NO") return true;
            if (vehicle.trucker.slot12.name == "NO") return true;

            return false;
        }

        public async static Task<Item> CheckIfVehicleHasItem(VehicleCharacter vehicle, string itemName)
        {
            if (vehicle.trunk.slot1.name == itemName) return vehicle.trunk.slot1;
            if (vehicle.trunk.slot2.name == itemName) return vehicle.trunk.slot2;
            if (vehicle.trunk.slot3.name == itemName) return vehicle.trunk.slot3;
            if (vehicle.trunk.slot4.name == itemName) return vehicle.trunk.slot4;
            if (vehicle.trunk.slot5.name == itemName) return vehicle.trunk.slot5;
            if (vehicle.trunk.slot6.name == itemName) return vehicle.trunk.slot6;
            if (vehicle.trunk.slot7.name == itemName) return vehicle.trunk.slot7;
            if (vehicle.trunk.slot8.name == itemName) return vehicle.trunk.slot8;
            if (vehicle.trunk.slot9.name == itemName) return vehicle.trunk.slot9;
            if (vehicle.trunk.slot10.name == itemName) return vehicle.trunk.slot10;
            if (vehicle.trunk.slot11.name == itemName) return vehicle.trunk.slot11;
            if (vehicle.trunk.slot12.name == itemName) return vehicle.trunk.slot12;

            return null;
        }
    }
}

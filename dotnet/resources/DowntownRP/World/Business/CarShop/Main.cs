using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Business.CarShop
{
    public class Main : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void PlayerEnterVehicle_Conce(Player player, Vehicle vehicle, sbyte seatID)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (vehicle.HasData("VEHICLE_BUSINESS_DATA"))
            {
                Data.Entities.VehicleBusiness veh = vehicle.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");

                if (veh.isNormalSelling) player.TriggerEvent("OpenBuyCarMenu", veh.business.name, veh.model, veh.price);
                if (veh.isRentSelling) player.TriggerEvent("OpenRentCarMenu", veh.business.name, veh.model, veh.price);
            }
        }


        [RemoteEvent("RentVehicleCarshop")]
        public async Task RentVehicleCarshop(Player player, string color)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Vehicle vehicle;
            if (player.IsInVehicle) vehicle = player.Vehicle;
            else return;

            if (vehicle.HasData("VEHICLE_BUSINESS_DATA"))
            {
                Data.Entities.VehicleBusiness veh = vehicle.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");

                if (veh.isRentSelling)
                {                    
                        player.WarpOutOfVehicle();
                        await Task.Delay(100);
                        NAPI.Task.Run(async () =>
                        {
                            Vehicle new_veh = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(veh.model), veh.business.spawn, veh.business.spawnRot, 1, 1);
                            player.SetIntoVehicle(new_veh, -1);

                            player.TriggerEvent("DestroyCarshopBrowser");

                            switch (color)
                            {
                                case "Negro":
                                    new_veh.PrimaryColor = 0;
                                    new_veh.SecondaryColor = 0;
                                    break;

                                case "Blanco":
                                    new_veh.PrimaryColor = 5;
                                    new_veh.SecondaryColor = 5;
                                    break;

                                case "Amarillo":
                                    new_veh.PrimaryColor = 42;
                                    new_veh.SecondaryColor = 42;
                                    break;

                                case "Rojo":
                                    new_veh.PrimaryColor = 27;
                                    new_veh.SecondaryColor = 27;
                                    break;

                                case "Azul":
                                    new_veh.PrimaryColor = 80;
                                    new_veh.SecondaryColor = 80;
                                    break;

                                case "Verde":
                                    new_veh.PrimaryColor = 55;
                                    new_veh.SecondaryColor = 55;
                                    break;

                            }

                            player.TriggerEvent("chat_goal", "¡Felicidades!", "Has rentado un nuevo vehículo personal, para devolverlo debes presionar F3");

                            new_veh.NumberPlate = Utilities.Generate.CreateIBANBank();                            

                            Data.Entities.VehicleCharacter veh_company = new Data.Entities.VehicleCharacter()
                            {
                                id = 9999,
                                entity = new_veh,
                                owner = user
                            };

                            veh_company.trunk = new Data.Entities.Inventory()
                            {
                                slot1 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot2 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot3 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot4 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot5 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot6 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot7 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot8 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot9 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot10 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot11 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot12 = new Data.Entities.Item(0, "NO", 0, 0)
                            };

                            user.vehicles.Add(veh_company);

                            new_veh.SetData("VEHICLE_CHARACTER_DATA", veh_company);

                            new_veh.SetData("RENT", true);
                            player.SetData("RENT_CAR", (double)veh.price);
                            player.TriggerEvent("IniciaContadorRentaCar");
                        });
                    
                }
            }
        }

        [RemoteEvent("RentVehiclePin")]
        public async Task RentVehiclePin(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            
            /*if (player.IsInVehicle && player.HasData("RENT_CAR"))
            {
                Vehicle vehicle = player.Vehicle;
                double price = player.GetData("RENT_CAR");
                if (await Game.Money.MoneyModel.SubMoney(player, price))
                {
                    Utilities.Notifications.SendNotificationINFO(player, "RentaCar: Se te ha descontado el costo por minuto por la renta de tu coche.");                    
                }
                else
                {
                    player.TriggerEvent("chat_goal", "¡No tienes dinero para pagar la renta!", "Has Rentado un vehículo personal, pero no tienes dinero para seguir pagandolo. En 10 segundos desaparecera por arte de magia.");
                    await Task.Delay(10000);
                    player.WarpOutOfVehicle();
                    NAPI.Task.Run(async () =>
                    {
                        vehicle.Delete();
                    });
                    player.ResetData("RENT_CAR");
                }

            }
            else
            {
                
            }     */                  
        }

        [RemoteEvent("RentVehicleFinish")]
        public async Task RentVehicleFinish(Player player)
        {

                if (!player.HasData("USER_CLASS")) return;
                Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");


                if (player.IsInVehicle && player.HasData("RENT_CAR"))
                {
                    Vehicle vehicle = player.Vehicle;
                        player.TriggerEvent("TerminarContadorRenta");
                        player.TriggerEvent("chat_goal", "¡Cobro por Renta Finalizado!", "El cobro por renta de tu vehiculo ha finalizado. En 10 segundos desaparecera por arte de magia.");
                        await System.Threading.Tasks.Task.Delay(10000); // Blocks the thread for 2 seconds before running the rest of the code
                        NAPI.Task.Run(() =>
                        {
                            NAPI.Entity.DeleteEntity(vehicle);
                        });
                    player.ResetData("RENT_CAR");
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "O no estas en un coche, o no es rentado.");
                }
        }

        [RemoteEvent("BuyVehicleCarshop")]
        public async Task RE_BuyCarCompany(Player player, string color)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Vehicle vehicle;
            if (player.IsInVehicle) vehicle = player.Vehicle;
            else return;

            if (vehicle.HasData("VEHICLE_BUSINESS_DATA"))
            {
                Data.Entities.VehicleBusiness veh = vehicle.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");

                if (veh.isNormalSelling)
                {
                    if (await Game.Money.MoneyModel.SubMoney(player, (double)veh.price))
                    {
                        player.WarpOutOfVehicle();
                        await Task.Delay(100);
                        NAPI.Task.Run(async () =>
                        {
                            Vehicle new_veh = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(veh.model), veh.business.spawn, veh.business.spawnRot, 1, 1);
                            player.SetIntoVehicle(new_veh, -1);

                            player.TriggerEvent("DestroyCarshopBrowser");

                            switch (color)
                            {
                                case "Negro":
                                    new_veh.PrimaryColor = 0;
                                    new_veh.SecondaryColor = 0;
                                    break;

                                case "Blanco":
                                    new_veh.PrimaryColor = 5;
                                    new_veh.SecondaryColor = 5;
                                    break;

                                case "Amarillo":
                                    new_veh.PrimaryColor = 42;
                                    new_veh.SecondaryColor = 42;
                                    break;

                                case "Rojo":
                                    new_veh.PrimaryColor = 27;
                                    new_veh.SecondaryColor = 27;
                                    break;

                                case "Azul":
                                    new_veh.PrimaryColor = 80;
                                    new_veh.SecondaryColor = 80;
                                    break;

                                case "Verde":
                                    new_veh.PrimaryColor = 55;
                                    new_veh.SecondaryColor = 55;
                                    break;

                            }

                            player.TriggerEvent("chat_goal", "¡Felicidades!", "Has comprado un nuevo vehículo personal");

                            new_veh.NumberPlate = Utilities.Generate.CreateIBANBank();
                            int vehid = await Game.Vehicles.DbHandler.CreateVehicleCharacter(user.idpj, veh.model, veh.price, new_veh.PrimaryColor, new_veh.SecondaryColor, new_veh.NumberPlate, veh.business.spawn.X, veh.business.spawn.Y, veh.business.spawn.Z, veh.business.spawnRot);

                            Data.Entities.VehicleCharacter veh_company = new Data.Entities.VehicleCharacter()
                            {
                                id = vehid,
                                entity = new_veh,
                                owner = user
                            };

                            veh_company.trunk = new Data.Entities.Inventory()
                            {
                                slot1 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot2 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot3 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot4 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot5 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot6 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot7 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot8 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot9 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot10 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot11 = new Data.Entities.Item(0, "NO", 0, 0),
                                slot12 = new Data.Entities.Item(0, "NO", 0, 0)
                            };

                            user.vehicles.Add(veh_company);

                            new_veh.SetData("VEHICLE_CHARACTER_DATA", veh_company);
                        });
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
                }
            }
        }
    }
}

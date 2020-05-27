using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using DowntownRP.Utilities;

namespace DowntownRP.Game.Commands
{
    public class aveh : Script {
        [Command("aveh")]
        public void CMD_aveh(Player player, string arg, string arg2 = "")
        {
         Vehicle veh = null;
        if (!player.IsInVehicle)
        {
                foreach (var vh in NAPI.Pools.GetAllVehicles())
                {
                    if (player.Position.DistanceTo(vh.Position) < 5f)
                    {
                        veh = vh;
                    }

                }
                    if (veh == null)
                    {
                        Notifications.SendNotificationERROR(player, "ERROR");
                    }
            }
            else
            {
                veh = player.Vehicle;
            }
                
            if (arg == "reparar")
                {
                if (AdminLVL.PuedeUsarComando(player, 2))
                {
                    veh.Repair();
                    Notifications.SendNotificationOK(player, "Has reparado este vehículo");
                }
                else Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");

            }else if (arg == "livery")
            {
                if (AdminLVL.PuedeUsarComando(player, 3))
                {
                    int num;
                    int.TryParse(arg2, out num);
                    if (num == null) { Notifications.SendNotificationERROR(player, "Pon un segundo argumento (num livery)"); return; }

                    veh.Livery = num;
                    Notifications.SendNotificationOK(player, $"Has puesto la livery {num} en el vehículo. ");
                }
                else Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");
            }
            else if (arg == "color")
                {
                if (AdminLVL.PuedeUsarComando(player, 4))
                {
                    int color;
                    int.TryParse(arg2, out color);

                    if (color == null)
                    {
                        Notifications.SendNotificationERROR(player, "Pon un numero como segundo argumento");
                    }
                    veh.PrimaryColor = color;
                    Notifications.SendNotificationOK(player, "Has pintado este vehículo");

                    if (veh.HasData("VEHICLE_FACTION_DATA"))
                    {
                        Data.Entities.VehicleFaction fveh = veh.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");
                        World.Factions.Vehicles.DbFunctions.UpdateFVehPriColor(fveh.id, color);
                    }
                    else if (veh.HasData("VEHICLE_CHARACTER_DATA"))
                    {
                        Data.Entities.VehicleCharacter cveh = veh.GetData<Data.Entities.VehicleCharacter>("VEHICLE_CHARACTER_DATA");
                        Game.Vehicles.DbHandler.UpdateFVehPriColor(cveh.id, color);
                    }
                    else if (veh.HasData("VEHICLE_BUSINESS_DATA"))
                    {
                        Data.Entities.VehicleBusiness cveh = veh.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");
                        World.Business.DbFunctions.UpdateFVehPriColor(cveh.id, color);
                    }
                    else if (veh.HasData("VEHICLE_COMPANY_DATA"))
                    {
                        Data.Entities.VehicleCompany cveh = veh.GetData<Data.Entities.VehicleCompany>("VEHICLE_COMPANY_DATA");
                        World.Companies.DbFunctions.UpdateFVehPriColor(cveh.id, color);
                    }
                }
                else Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");

                }


                else if (arg == "color2")
                {
                if (AdminLVL.PuedeUsarComando(player, 4))
                {
                    int color;
                    int.TryParse(arg2, out color);

                    if (color == null)
                    {
                        Notifications.SendNotificationERROR(player, "Pon un numero como segundo argumento");
                    }
                    veh.SecondaryColor = color;
                    Notifications.SendNotificationOK(player, "Has pintado este vehículo");

                    if (veh.HasData("VEHICLE_FACTION_DATA"))
                    {
                        Data.Entities.VehicleFaction fveh = veh.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");
                        World.Factions.Vehicles.DbFunctions.UpdateFVehSecColor(fveh.id, color);
                    }
                    else if (veh.HasData("VEHICLE_CHARACTER_DATA"))
                    {
                        Data.Entities.VehicleCharacter cveh = veh.GetData<Data.Entities.VehicleCharacter>("VEHICLE_CHARACTER_DATA");
                        Game.Vehicles.DbHandler.UpdateFVehSecColor(cveh.id, color);
                    }
                    else if (veh.HasData("VEHICLE_BUSINESS_DATA"))
                    {
                        Data.Entities.VehicleBusiness cveh = veh.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");
                        World.Business.DbFunctions.UpdateFVehSecColor(cveh.id, color);
                    }
                    else if (veh.HasData("VEHICLE_COMPANY_DATA"))
                    {
                        Data.Entities.VehicleCompany cveh = veh.GetData<Data.Entities.VehicleCompany>("VEHICLE_COMPANY_DATA");
                        World.Companies.DbFunctions.UpdateFVehSecColor(cveh.id, color);
                    }
                }
                else Notifications.SendNotificationERROR(player, "No puedes usar este subcomando"); 
                

                }




                else if (arg == "info")
                {

                    if (veh.HasData("VEHICLE_FACTION_DATA"))
                    {
                        Data.Entities.VehicleFaction fveh = veh.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");
                        player.SendChatMessage("-----INFORMACIÓN:-------");
                        player.SendChatMessage($"ID: {fveh.id}");
                        player.SendChatMessage($"Tipo: Faccionario de {World.Factions.Main.GetFactionName(fveh.faction)} ({fveh.faction})");
                        player.SendChatMessage($"Modelo: {fveh.model}");
                        player.SendChatMessage($"Color: {fveh.entity.PrimaryColor} / {fveh.entity.SecondaryColor}");
                        player.SendChatMessage($"Matricula: {fveh.entity.NumberPlate}");
                        player.SendChatMessage($"Livery: {fveh.entity.Livery}");
                        player.SendChatMessage($"Dimension: {fveh.entity.Dimension}");
                        player.SendChatMessage("------------------------");
                    }
                    else if (veh.HasData("VEHICLE_CHARACTER_DATA"))
                    {
                        Data.Entities.VehicleCharacter cveh = veh.GetData<Data.Entities.VehicleCharacter>("VEHICLE_CHARACTER_DATA");

                        player.SendChatMessage("-----INFORMACIÓN:-------");
                        player.SendChatMessage($"ID: {cveh.id}");
                        player.SendChatMessage($"Tipo: De personaje {cveh.owner.name} ({cveh.owner.idpj} - IdPj || {cveh.owner.dni} Documentación)");
                        player.SendChatMessage($"Color: {cveh.entity.PrimaryColor} / {cveh.entity.SecondaryColor}");
                        player.SendChatMessage($"Matricula: {cveh.entity.NumberPlate}");
                        player.SendChatMessage($"Livery: {cveh.entity.Livery}");
                    player.SendChatMessage($"Dimension: {cveh.entity.Dimension}");
                    player.SendChatMessage("------------------------");
                    }
                    else if (veh.HasData("VEHICLE_BUSINESS_DATA"))
                    {
                        Data.Entities.VehicleBusiness bveh = veh.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");

                        player.SendChatMessage("-----INFORMACIÓN:-------");
                        player.SendChatMessage($"ID: {bveh.id}");
                        player.SendChatMessage($"Tipo: De empresa {bveh.business.name} ({bveh.business.id} de Pj {bveh.business.owner})");
                        if (bveh.isCompanySelling) player.SendChatMessage($"Venta compañías");
                        if (bveh.isNormalSelling) player.SendChatMessage($"Venta Normal");
                        if (bveh.isRentSelling) player.SendChatMessage($"Venta Alquiler");
                        player.SendChatMessage($"Color: {bveh.vehicle.PrimaryColor} / {bveh.vehicle.SecondaryColor}");
                        player.SendChatMessage($"Matricula: {bveh.vehicle.NumberPlate}");
                        player.SendChatMessage($"Livery: {bveh.vehicle.Livery}");
                    player.SendChatMessage($"Dimension: {bveh.vehicle.Dimension}");
                    player.SendChatMessage("------------------------");
                    }
                    if (veh.HasData("VEHICLE_COMPANY_DATA"))
                    {
                        Data.Entities.VehicleCompany bveh = veh.GetData<Data.Entities.VehicleCompany>("VEHICLE_COMPANY_DATA");

                        player.SendChatMessage("-----INFORMACIÓN:-------");
                        player.SendChatMessage($"ID: {bveh.id}");
                        player.SendChatMessage($"Tipo: De empresa {bveh.company.name} ({bveh.company.id}) de Pj num {bveh.company.owner})");
                        player.SendChatMessage($"Color: {bveh.vehicle.PrimaryColor} / {bveh.vehicle.SecondaryColor}");
                        player.SendChatMessage($"Livery: {bveh.vehicle.Livery}");
                    player.SendChatMessage($"Dimension: {bveh.vehicle.Dimension}");
                    player.SendChatMessage("------------------------");
                    }

                } else if (arg == "traerf")
            {
                if(AdminLVL.PuedeUsarComando(player, 3))
                {
                    foreach (Vehicle v in NAPI.Pools.GetAllVehicles())
                    {
                        if (v.HasData("VEHICLE_FACTION_DATA"))
                        {
                            if (v.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA").id.ToString() == arg2)
                            {
                                veh = v;
                            }
                        }

                    }
                        veh.Position = player.Position;
                }
                else
                {
                    Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");
                }
            }
            else if (arg == "traerp") //Traer Personal
            {
                if (AdminLVL.PuedeUsarComando(player, 3))
                {
                    foreach (Vehicle v in NAPI.Pools.GetAllVehicles())
                    {
                        if (v.HasData("VEHICLE_CHARACTER_DATA"))
                        {
                            if (v.GetData<Data.Entities.VehicleCharacter>("VEHICLE_CHARACTER_DATA").id.ToString() == arg2)
                            {
                                veh = v;
                            }
                        }

                    }
                    veh.Position = player.Position;
                }
                else
                {
                    Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");
                }
            }
            else if (arg == "traerc") //Traer Company
            {
                if (AdminLVL.PuedeUsarComando(player, 3))
                {
                    foreach (Vehicle v in NAPI.Pools.GetAllVehicles())
                    {
                        if (v.HasData("VEHICLE_COMPANY_DATA"))
                        {
                            if (v.GetData<Data.Entities.VehicleCompany>("VEHICLE_COMPANY_DATA").id.ToString() == arg2)
                            {
                                veh = v;
                            }
                        }

                    }
                    veh.Position = player.Position;
                }
                else
                {
                    Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");
                }
            }
            else if (arg == "traerb") //Traer Empresa
            {
                if (AdminLVL.PuedeUsarComando(player, 3))
                {
                    foreach (Vehicle v in NAPI.Pools.GetAllVehicles())
                    {
                        if (v.HasData("VEHICLE_BUSINESS_DATA"))
                        {
                            if (v.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA").id.ToString() == arg2)
                            {
                                veh = v;
                            }
                        }

                    }
                    veh.Position = player.Position;
                }
                else
                {
                    Notifications.SendNotificationERROR(player, "No puedes usar este subcomando");
                }
            }
        }      
    }
}

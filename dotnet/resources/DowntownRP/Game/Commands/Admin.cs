using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using DowntownRP.Utilities.Outfits;

namespace DowntownRP.Game.Commands
{
    public class Admin : Script
    {
        [Command("ayudastaff")]
        public void CMD_ayudastaff(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 2) player.SendChatMessage("/fly - /getpos - /irpos - /setmarca - /irmarca - /repararveh - /mutear");
            if (user.adminLv >= 3) player.SendChatMessage("/crearveh - /borrarveh - /getid - /revivir - /setgb - /ir - /traer - /kick - /bloquearn");
            if (user.adminLv >= 4) player.SendChatMessage("/creafaccion - /cambiarfacrank - /crearfacveh - /ban - /dararma - /setfac");
            if (user.adminLv >= 5)
            {
                player.SendChatMessage("/dardinero - /crearbanco - /borrarbanco - /crearcasa - /crearempresa - /borrarempresa - /crearminerpoint");
                player.SendChatMessage("/crearnegocio - /borrarnegocio - /ponerropa - /ponerprop - /poneroutfit - /anuncioa - /crearfuelpoint");
                player.SendChatMessage("/crearDNI");
            }
        }
        // Main commands
        [Command("crearveh")]
        public void CMD_crearveh(Player player, string vehicle_name)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                uint hash = NAPI.Util.GetHashKey(vehicle_name);
                Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, player.Position.Around(5), 0, 0, 0);
                veh.EngineStatus = true;
                return;
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("borrarveh")]
        public void CMD_borrarveh(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (player.IsInVehicle)
                {
                    Vehicle veh = player.Vehicle;
                    veh.Delete();
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("fly")]
        public void CMD_fly(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 5)
            {
                player.TriggerEvent("flyModeStart");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("dardinero")]
        public async Task CMD_dardinero(Player player, int id, double dinero)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 5)
            {
                Player target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                else
                {
                    await Money.MoneyModel.AddMoney(target, dinero);
                    Utilities.Notifications.SendNotificationOK(player, $"Le has dado ${dinero} a {target.Name}");
                    Utilities.Notifications.SendNotificationOK(target, $"Un administrador te ha entregado ${dinero}");
                    Utilities.Webhooks.sendWebHook(1, $"💲 [{DateTime.Now.ToString()}] {player.Name} le ha dado ${dinero} a {target.Name}");
                }
            }
        }

        [Command("getpos")]
        public void CMD_obtenerpos(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 1)
            {
                player.SendChatMessage($"{player.Position}");
                Console.WriteLine($"{player.Position}, {player.Heading}");
            }
        }

        [Command("irpos")]
        public void CMD_irpos(Player player, float x, float y, float z)
        {
            player.Position = new Vector3(x, y, z);
        }

        [Command("getid")]
        public void CMD_getid(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                player.SendChatMessage("<font color='yellow'>INFO:</font> si no sale ninguna información es porque no se pudo obtener la id");
                if (user.isInBank) player.SendChatMessage($"La id del banco es {user.bankEntity.id}");
                if (user.isInBusiness) player.SendChatMessage($"La id del negocio es {user.business.id}");
                if (user.isInCompany) player.SendChatMessage($"La id de la empresa es {user.company.id}");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        // Bank commands
        [Command("crearbanco")]
        public async Task CMD_crearnegocio(Player player, int type)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (type == 1 || type == 2)
                {
                    int bankId = await World.Banks.DatabaseFunctions.CreateBank(player, type);

                    ColShape bank = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 6);
                    TextLabel label = NAPI.TextLabel.CreateTextLabel("Pulsa ~y~F5 ~w~para interactuar", player.Position.Subtract(new Vector3(0, 0, 0.1)), 15, 6, 2, new Color(255, 255, 255));
                    Marker marker = NAPI.Marker.CreateMarker(0, player.Position.Subtract(new Vector3(0, 0, 0.1)), new Vector3(), new Vector3(), 1, new Color(251, 244, 1));
                    Blip blip = NAPI.Blip.CreateBlip(player.Position);

                    if (type == 1)
                    {
                        blip.Sprite = 108;
                        blip.Scale = 1f;
                        blip.ShortRange = true;
                        blip.Name = "Banco";
                        blip.Color = 5;
                    }
                    else
                    {
                        blip.Sprite = 277;
                        blip.Scale = 1f;
                        blip.ShortRange = true;
                        blip.Name = "ATM";
                        blip.Color = 5;
                    }

                    Data.Entities.Bank banco = new Data.Entities.Bank
                    {
                        blip = blip,
                        marker = marker,
                        label = label,
                        id = bankId,
                        type = type
                    };

                    //bank.SetExternalData<Data.Entities.Bank>(0, banco);
                    bank.SetData("BANK_CLASS", banco);

                    Utilities.Notifications.SendNotificationOK(player, $"Has creado el banco con ID {bankId}");
                }
                else
                {
                    player.SendChatMessage("<font color='red'>ERROR</font> No existe ese tipo de banco");
                }
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("borrarbanco")]
        public async Task CMD_borrarbanco(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (user.isInBank)
                {
                    await World.Banks.DatabaseFunctions.DeleteBank(user.bankEntity.id);
                    user.bankEntity.label.Delete();
                    user.bankEntity.blip.Delete();
                    user.bankEntity.marker.Delete();

                    Utilities.Notifications.SendNotificationOK(player, $"Has borrado un banco correctamente");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en un banco");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        // Company commands
        [Command("crearempresa")]
        public void CMD_crearempresa(Player player, int type, int price)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 5)
            {
                if (type == 1 || type == 2 || type == 3 || type == 4 || type == 5)
                {
                    //player.SetData<int>("CreateCompanyType", type);
                    //player.SetData<int>("CreateCompanyPrice", price);

                    player.SetData("CreateCompanyType", type);
                    player.SetData("CreateCompanyPrice", price);
                    player.TriggerEvent("CompanyAddStreetName");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No existe ese tipo de empresa");
            }
        }

        [Command("borrarempresa")]
        public async Task CMD_borrarempresa(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (user.isInCompany)
                {
                    user.company.label.Delete();
                    user.company.blip.Delete();
                    user.company.marker.Delete();
                    user.company.shape.Delete();
                    Data.Lists.Companies.Add(user.company);
                    user.company = null;

                    await World.Companies.DbFunctions.DeleteCompany(user.company.id);

                    Utilities.Notifications.SendNotificationOK(player, $"Has borrado una empresa correctamente");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en una empresa");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        // Business commands
        [Command("crearnegocio")]
        public void CMD_crearnegocio(Player player, int type, int price)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 5)
            {
                if (type == 1 || type == 2 || type == 3 || type == 4 || type == 5 || type == 6 || type == 7 || type == 8 || type == 9)
                {
                    //player.SetData<int>("CreateCompanyType", type);
                    //player.SetData<int>("CreateCompanyPrice", price);

                    player.SetData("CreateBusinessType", type);
                    player.SetData("CreateBusinessPrice", price);
                    player.TriggerEvent("BusinessAddStreetName");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No existe ese tipo de negocio");
            }
        }

        [Command("borrarnegocio")]
        public async Task CMD_borrarnegocio(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (user.isInBusiness)
                {
                    user.business.label.Delete();
                    user.business.blip.Delete();
                    user.business.marker.Delete();
                    user.business.shape.Delete();
                    user.business = null;

                    await World.Business.DbFunctions.DeleteBusiness(user.business.id);

                    Utilities.Notifications.SendNotificationOK(player, $"Has borrado un negocio correctamente");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en un negocio");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("crearvehiculosnegocio")]
        public void CMD_crearvehiculosnegocio(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (user.isInBusiness)
                {
                    player.SetData("CREATE_VEHICLE_BUSINESS", user.business);
                    Utilities.Notifications.SendNotificationOK(player, "Ahora puedes usar /crearnegocioveh o /crearnegociospawn en el sitio donde quieras crearlo");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en un negocio");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("crearnegocioveh")]
        public async Task CMD_crearnegocioveh(Player player, string model, int price, int color1, int color2, string numberplate)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (!player.HasData("CREATE_VEHICLE_BUSINESS"))
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Debes usar primero /crearvehiculosnegocio en la entrada de un negocio");
                    return;
                }

                Data.Entities.Business business = player.GetData<Data.Entities.Business>("CREATE_VEHICLE_BUSINESS");

                uint hash = NAPI.Util.GetHashKey(model);
                Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, player.Position.Subtract(new Vector3(0, 0, 1)), player.Heading, color1, color2, numberplate, 255, false, false);
                TextLabel label;
                veh.NumberPlate = numberplate;
                int vehicle_id = await World.Business.DbFunctions.CreateBusinessVehicle(business.id, model, price, color1, color2, numberplate, player.Position.X, player.Position.Y, player.Position.Z, (double)player.Heading);

                bool isCompanySelling = false;
                bool isRentSelling = false;
                bool isNormalSelling = false;

                switch (business.type)
                {
                    case 6:
                        isRentSelling = true;
                        break;

                    case 7:
                        isNormalSelling = true;
                        break;

                    case 8:
                        isCompanySelling = true;
                        break;
                }

                if (isRentSelling)
                {
                    label = NAPI.TextLabel.CreateTextLabel($"~y~{model}~n~~w~Precio Renta: ~g~${price} x Minuto", player.Position.Subtract(new Vector3(0, 0, 1)), 3, 1, 0, new Color(255, 255, 255));
                }
                else
                {
                    label = NAPI.TextLabel.CreateTextLabel($"~y~{model}~n~~w~Precio: ~g~${price}", player.Position.Subtract(new Vector3(0, 0, 1)), 3, 1, 0, new Color(255, 255, 255));
                }

                Data.Entities.VehicleBusiness vehicle = new Data.Entities.VehicleBusiness()
                {
                    id = vehicle_id,
                    model = model,
                    vehicle = veh,
                    business = business,
                    price = price,
                    isCompanySelling = isCompanySelling,
                    isRentSelling = isRentSelling,
                    isNormalSelling = isNormalSelling,
                    label = label
                };

                veh.SetData("VEHICLE_BUSINESS_DATA", vehicle);

                veh.SetSharedData("BUSINESS_VEHICLE_SHARED", veh);
                veh.SetSharedData("IS_BUSINESS_VEHICLE", true);
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("borrarnegocioveh")]
        public async Task CMD_borrarnegocioveh(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (player.IsInVehicle)
                {
                    if (player.Vehicle.HasData("VEHICLE_BUSINESS_DATA"))
                    {
                        Data.Entities.VehicleBusiness veh = player.Vehicle.GetData<Data.Entities.VehicleBusiness>("VEHICLE_BUSINESS_DATA");
                        await World.Business.DbFunctions.DeleteBusinessVehicle(veh.id);
                        player.Vehicle.Delete();

                        Utilities.Notifications.SendNotificationOK(player, "Has borrado el vehículo del negocio correctamente");
                    }
                }
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("crearnegociospawn")]
        public async Task CMD_crearnegociospawn(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv >= 5)
            {
                if (!player.HasData("CREATE_VEHICLE_BUSINESS"))
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Debes usar primero /crearvehiculosnegocio en la entrada de un negocio");
                    return;
                }

                Data.Entities.Business business = player.GetData<Data.Entities.Business>("CREATE_VEHICLE_BUSINESS");

                if (business.spawn != null)
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Este negocio ya tiene punto de spawn");
                    return;
                }

                await World.Business.DbFunctions.CreateBusinessVehicleSpawn(business.id, player.Position.X, player.Position.Y, player.Position.Z, player.Heading);
                business.spawn = player.Position;
                business.spawnRot = player.Heading;

                Utilities.Notifications.SendNotificationOK(player, "Has creado el punto de spawn correctamente");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("crearfacveh")]
        public async Task CMD_crearfacveh(Player player, int faction, string model, int color1, int color2, string numberplate)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 4)
            {
                int idveh = await World.Factions.Vehicles.DbFunctions.CreateFactionVehicle(faction, model, color1, color2, numberplate, player.Position.X, player.Position.Y, player.Position.Z, player.Heading);
                uint hash = NAPI.Util.GetHashKey(model);

                NAPI.Task.Run(async () =>
                {
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, player.Position, player.Heading, color1, color2, numberplate);

                    Data.Entities.VehicleFaction vehdata = new Data.Entities.VehicleFaction()
                    {
                        id = idveh,
                        entity = veh,
                        model = model,
                        faction = faction
                    };

                    vehdata.trucker = new Data.Entities.Inventory()
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

                    Data.Lists.factions.Find(x => x.id == faction).vehicles.Add(vehdata);


                    veh.SetData("VEHICLE_FACTION_DATA", vehdata);
                });

                Utilities.Notifications.SendNotificationOK(player, $"Has creado un {model} para la facción {faction}");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("revivir")]
        public void CMD_revivir(Player player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 4 || (user.faction==2&user.factionDuty == true&user.rank >= 1))
            {
                Player target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null)
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No existe ningún jugador con esta ID");
                    return;
                }
                else
                {
                    if (!target.HasData("USER_CLASS")) return;
                    Data.Entities.User Player = target.GetData<Data.Entities.User>("USER_CLASS");
                    if (!Player.isDeath)
                    {
                        Player.isDeath = false;
                        Player.adviceLSMD = false;
                        Player.acceptDeath = false;

                        target.StopAnimation();
                        target.Health = 100;
                        //target.TriggerEvent("CloseDeathUI");
                        string tipo;
                        if (user.faction == 2 & user.factionDuty == true & user.rank >= 1) { tipo = "paramedico"; } else { tipo = "administrador"; }

                        Utilities.Notifications.SendNotificationINFO(target, "Has sido revivido por un "+tipo);
                        Utilities.Notifications.SendNotificationOK(player, "Has revivido a un jugador correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "Este usuario no está en estado de coma");
                }
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("setgb")]
        public void CMD_setgb(Player player, uint id_dimension, int playerid = -1)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 3))
            {
                player.Dimension = id_dimension;
                player.SendChatMessage($"Ahora estás en la dimensión {id_dimension}");
                Player target;
                if (playerid == -1)
                {
                    target = player;
                }
                else
                {
                    target = Utilities.PlayerId.FindPlayerById(playerid);
                }
                target.Dimension = id_dimension;
                target.SendChatMessage($"Ahora estás en la dimensión {id_dimension}");
            }

        }

        // House's cmds
        [Command("crearcasa")]
        public void CMD_crearcasa(Player player, int type, int price)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 5)
            {
                if (type == 1 || type == 2 || type == 3 || type == 4 || type == 5)
                {
                    //player.SetData<int>("CreateCompanyType", type);
                    //player.SetData<int>("CreateCompanyPrice", price);

                    player.SetData("CreateHouseType", type);
                    player.SetData("CreateHousePrice", price);
                    player.TriggerEvent("HouseAddStreetName");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No existe ese tipo de casa");
            }
        }

        [Command("creartruckerstart")]
        public async Task CMD_creartruckerstart(Player player, int companyid)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 5)
            {
                Vector3 position = player.Position;

                int id = await World.Companies.DbFunctions.CreateTruckerStartCompany(companyid, position.X, position.Y, position.Z);
                
                ColShape shape = NAPI.ColShape.CreateCylinderColShape(position, 2, 2);
                TextLabel label = NAPI.TextLabel.CreateTextLabel("~b~Carguero~n~~w~Pulsa ~b~Y ~w~para interactuar", position, 5, 1, 0, new Color(255, 255, 255));
                Marker marker = NAPI.Marker.CreateMarker(1, position.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(0, 116, 255));

                shape.SetData("COMPANY_ID", companyid);
            }
        }

        [Command("ponerropa")]
        public void CMD_ponerropa(Player player, int id, int slot, int drawable, int texture)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 1)
            {
                Player target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                else
                {
                    target.SetClothes(slot, drawable, texture);
                }
            }
        }

        [Command("ponerprop")]
        public void CMD_ponerprop(Player player, int id, int slot, int drawable, int texture)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 1)
            {
                Player target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                else
                {
                    target.SetAccessories(slot, drawable, texture);
                }
            }
        }

        [Command("poneroutfit")]
        public void CMD_poneroutfit(Player player, int id, int outfit)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 1)
            {
                Player target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                else
                {
                    target.SetOutfit(outfit);
                    
                }
            }
        }

        [Command("a", GreedyArg = true)]
        public void CMD_a(Player player, string mensaje ="")
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv != 0)
            {
                if (mensaje == "") 
                {
                    user.CanalChatA = !(user.CanalChatA);
                    if (user.CanalChatA == true) 
                    {
                        user.entity.SendChatMessage($"<font color='#FF0000'>[ADMIN]</font> Se ha habilitado la lectura del canal de chat del staff.");
                    }
                    else
                    {
                        user.entity.SendChatMessage($"<font color='#FF0000'>[ADMIN]</font> Se ha deshabilitado la lectura del canal de chat del staff.");
                    }
                }
                else
                {
                    string rank = Utilities.AdminLVL.getAdmLevelName(user.adminLv);

                    foreach (var Player in Data.Lists.playersConnected)
                    {
                        if (Player.adminLv != 0 & Player.CanalChatA == true)
                        {
                            Player.entity.SendChatMessage($"<font color='#FF0000'>[ADMIN]</font> {player.Name} ({rank}): {mensaje}");
                        }
                    }
                    Utilities.Webhooks.sendWebHook(2, mensaje, player.Name + "(" + rank + ")");
                }
                
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres miembro del staff");
        }

        [Command("anuncioa", GreedyArg = true)]
        public void CMD_anuncioa(Player player, string mensaje)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 3)
            {
                string rank = Utilities.AdminLVL.getAdmLevelName(user.adminLv);

                foreach (var Player in Data.Lists.playersConnected)
                {
                     Player.entity.SendChatMessage($"<font color='#802828'>[ANUNCIO - ADMIN]</font> {player.Name} ({rank}): {mensaje}");

                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres miembro del staff con nivel 3 o superior");
        }

        // Fuel station
        [Command("crearfuelpoint")]
        public async Task CMD_crearfuelpoint(Player player, int businessid)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 5)
            {
                Data.Entities.Business business = Data.Lists.business.Find(x => x.id == businessid);
                if (business != null)
                {
                    await World.Business.DbFunctions.CreateFuelPoint(player, businessid);
                    ColShape gaspoint = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    NAPI.Marker.CreateMarker(27, player.Position.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 2, new Color(255, 151, 0));
                    NAPI.TextLabel.CreateTextLabel("Presiona ~o~F5 ~w~para poner gasolina", player.Position.Subtract(new Vector3(0, 0, 0.1)), 15, 6, 2, new Color(255, 255, 255));
                    gaspoint.SetData("BUSINESS_FUEL_POINT", business);
                }
                else Utilities.Notifications.SendNotificationERROR(player, "El negocio no existe.");
            }
        }

        // Miner point
        [Command("crearminerpoint")]
        public async Task CMD_crearminerpoint(Player player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 5)
            {
                int id = await World.Companies.DbFunctions.CreateMinerPoint(player.Position.X, player.Position.Y, player.Position.Z, (double)player.Heading);
                
                ColShape shape = NAPI.ColShape.CreateCylinderColShape(player.Position, 1, 1);
                GTANetworkAPI.Object obj = NAPI.Object.CreateObject(-1625949270, player.Position.Subtract(new Vector3(0, 0, 1.2)), player.Rotation);
                TextLabel label = NAPI.TextLabel.CreateTextLabel("Pulsa ~g~E~w~ para picar piedra~n~Recursos: ~g~25/25", player.Position, 2, 1, 0, new Color(255, 255, 255));

                Data.Entities.MinerPoint miner = new Data.Entities.MinerPoint()
                {
                    id = id,
                    label = label,
                    shape = shape
                };

                shape.SetData("MINER_POINT", miner);
                Data.Lists.minerPoints.Add(miner);
            }
        }

        // Comandos admin sin protección
        [Command("ir")]
        public void CMD_ir(Player player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv == 0) return;

            Player target = Utilities.PlayerId.FindPlayerById(id);
            if (target != null) player.Position = target.Position;
            else Utilities.Notifications.SendNotificationERROR(player, "Esta ID no corresponde a ningún jugador");
        }

        [Command("traer")]
        public void CMD_traer(Player player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.adminLv == 0) return;

            Player target = Utilities.PlayerId.FindPlayerById(id);
            if (target != null) target.Position = player.Position;
            else Utilities.Notifications.SendNotificationERROR(player, "Esta ID no corresponde a ningún jugador");
        }

       /* [Command ("dararma")]
        public void CMD_darArma (Player player, string NombreArma, int municion, int target = -1)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player,3)) 
            {
                Player obj;
                if(target == -1)
                {
                    obj = player;
                }
                else
                {
                    obj = Utilities.PlayerId.FindPlayerById(target);
                    if(obj == null)
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No hay ningún usuario con este ID");
                        return;
                    }
                }

                WeaponHash wh = new WeaponHash();
                    wh = NAPI.Util.WeaponNameToModel(NombreArma);

                Data.Entities.Item itemm = new Data.Entities.Item(0, Utilities.Weapon.GetWeaponNameByHash(wh), 1, 1);
                itemm.bullets = municion;
                itemm.isAWeapon = true;
                itemm.weaponHash = wh;
                Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemm);
                Utilities.Notifications.SendNotificationOK(player, $"Has cogido armamento de la armeria");
                //player.GiveWeapon(WeaponHash.Pistol, 36);

                player.SendChatMessage($"<font color='#FF0000'>[ADMIN]</font> Has dado una {NombreArma} con {municion} balas a {obj.Name}");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando");
            }
        }*/

        [Command("setmarca")]
        public void CMD_setMarca(Player player)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

                user.Marca = player.Position;

                player.SendChatMessage($"Marca Configurada");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando");
            }
        }

        [Command("irmarca")]
        public void CMD_irMarca(Player player)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

                player.Position = user.Marca;

                player.SendChatMessage($"Marca Configurada");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando");
            }
        }

        [Command ("repararveh")]
        public void CMD_repararveh (Player player)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                var veh = player.Vehicle;
                if (veh == null)
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo");
                }
                else
                {
                    veh.Repair();
                }
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando");
            }
        }

        [Command("creardni")]
        public void CMD_repararveh(Player player, int target)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 5))
            {
                var t = Utilities.PlayerId.FindPlayerById(target);
                Data.Entities.User tdata = t.GetData<Data.Entities.User>("USER_CLASS");
                Game.CharacterSelector.CharacterSelector.UpdateDni(tdata.idpj);
                Utilities.Notifications.SendNotificationOK(player, "Has creado un DNI correctamente, reloguea para que funcione");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando");
            }
        }

        // Faction commands
        [Command("crearfaccion")]
        public async Task CMD_crearfaccion(Player player, string name, int type, int idowner)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Player target = Utilities.PlayerId.FindPlayerById(idowner);
            if (target == null)
            {
                Utilities.Notifications.SendNotificationERROR(player, "No hay ningún usuario en linea con esa ID");
                return;
            }

            if (!target.HasData("USER_CLASS")) return;
            Data.Entities.User targ = target.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv == 5)
            {
                int idfaction = await World.Factions.DbFunctions.CreateFaction(name, type, targ.idpj, player.Position.X, player.Position.Y, player.Position.Z);
                Data.Entities.Faction faction = new Data.Entities.Faction() { id = idfaction, name = name, type = type, owner = targ.idpj };
                await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(targ.idpj, idfaction);
                await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(targ.idpj, 6);

                targ.faction = idfaction;
                targ.rank = 6;

                Vector3 exit = new Vector3(266.1425, -1006.98, -100.8834);

                NAPI.Task.Run(async () =>
                {
                    ColShape entrada = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                    TextLabel label = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~F5 ~w~para entrar", player.Position, 2, 1, 0, new Color(255, 255, 255));

                    ColShape salida = NAPI.ColShape.CreateCylinderColShape(exit, 2, 2);
                    salida.Dimension = (uint)idfaction;

                    entrada.SetData("FACTION_CLASS", faction);
                    salida.SetData("SALIDA_FACTION", player.Position);

                    Data.Lists.factions.Add(faction);
                });

                Utilities.Notifications.SendNotificationOK(player, $"Has creado la facción {name}");
                Utilities.Notifications.SendNotificationOK(target, $"Ahora eres lider de {name}");
            }
        }

        [Command("cambiarfacrank")]
        public async Task CMD_cambiarfacrank(Player player, int idfacc, string rank1, string rank2, string rank3, string rank4, string rank5, string rank6)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if(user.faction == 5)
            {
                if (idfacc >= 10)
                {
                    Data.Entities.Faction faction = Data.Lists.factions.Find(x => x.id == idfacc);
                    if (faction != null)
                    {
                        World.Factions.DbFunctions.UpdateIlegalFactionRanks(player, idfacc, rank1, rank2, rank3, rank4, rank5, rank6);
                        faction.rank1 = rank1;
                        faction.rank2 = rank2;
                        faction.rank3 = rank3;
                        faction.rank4 = rank4;
                        faction.rank5 = rank5;
                        faction.rank6 = rank6;

                        Utilities.Notifications.SendNotificationOK(player, $"Has cambiado los rangos de {faction.name} correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "Esta facción no existe");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Este comando solo se puede utilizar con facciones ilegales");
            }
        }
       
    }
}

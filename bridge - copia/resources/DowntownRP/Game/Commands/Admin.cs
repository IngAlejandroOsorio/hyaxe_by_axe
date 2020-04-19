using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace DowntownRP.Game.Commands
{
    public class Admin : Script
    {
        // Main commands
        [Command("crearveh")]
        public void CMD_crearveh(Client player, string vehicle_name)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
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
        public void CMD_borrarveh(Client player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
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
        public void CMD_fly(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 5)
            {
                player.TriggerEvent("flyModeStart");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("dardinero")]
        public async Task CMD_dardinero(Client player, int id, double dinero)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 5)
            {
                Client target = Utilities.PlayerId.FindPlayerById(id);
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
        public void CMD_obtenerpos(Client player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv >= 1)
            {
                player.SendChatMessage($"{player.Position}");
                Console.WriteLine($"{player.Position}, {player.Heading}");
            }
        }

        [Command("irpos")]
        public void CMD_irpos(Client player, float x, float y, float z)
        {
            player.Position = new Vector3(x, y, z);
        }

        [Command("getid")]
        public void CMD_getid(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
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
        public async Task CMD_crearnegocio(Client player, int type)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
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
        public async Task CMD_borrarbanco(Client player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
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
        public void CMD_crearempresa(Client player, int type, int price)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv == 5)
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
        public async Task CMD_borrarempresa(Client player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
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
        public void CMD_crearnegocio(Client player, int type, int price)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv == 5)
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
        public async Task CMD_borrarnegocio(Client player)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
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
        public void CMD_crearvehiculosnegocio(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
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
        public async Task CMD_crearnegocioveh(Client player, string model, int price, int color1, int color2, string numberplate)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
            {
                if (!player.HasData("CREATE_VEHICLE_BUSINESS"))
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Debes usar primero /crearvehiculosnegocio en la entrada de un negocio");
                    return;
                }

                Data.Entities.Business business = player.GetData("CREATE_VEHICLE_BUSINESS");

                uint hash = NAPI.Util.GetHashKey(model);
                Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, player.Position.Subtract(new Vector3(0, 0, 1)), player.Heading, color1, color2, numberplate, 255, false, false);
                TextLabel label = NAPI.TextLabel.CreateTextLabel($"~y~{model}~n~~w~Precio: ~g~${price}", player.Position.Subtract(new Vector3(0, 0, 1)), 3, 1, 0, new Color(255, 255, 255));
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
        public async Task CMD_borrarnegocioveh(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
            {
                if (player.IsInVehicle)
                {
                    if (player.Vehicle.HasData("VEHICLE_BUSINESS_DATA"))
                    {
                        Data.Entities.VehicleBusiness veh = player.Vehicle.GetData("VEHICLE_BUSINESS_DATA");
                        await World.Business.DbFunctions.DeleteBusinessVehicle(veh.id);
                        player.Vehicle.Delete();

                        Utilities.Notifications.SendNotificationOK(player, "Has borrado el vehículo del negocio correctamente");
                    }
                }
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("crearnegociospawn")]
        public async Task CMD_crearnegociospawn(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (user.adminLv == 5)
            {
                if (!player.HasData("CREATE_VEHICLE_BUSINESS"))
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Debes usar primero /crearvehiculosnegocio en la entrada de un negocio");
                    return;
                }

                Data.Entities.Business business = player.GetData("CREATE_VEHICLE_BUSINESS");

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
        public async Task CMD_crearfacveh(Client player, int faction, string model, int color1, int color2, string numberplate)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv > 4)
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

                    veh.SetData("VEHICLE_FACTION_DATA", vehdata);
                });

                Utilities.Notifications.SendNotificationOK(player, $"Has creado un {model} para la facción {faction}");
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("revivir")]
        public void CMD_revivir(Client player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv > 4)
            {
                Client target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null)
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No existe ningún jugador con esta ID");
                    return;
                }
                else
                {
                    if (!target.HasData("USER_CLASS")) return;
                    Data.Entities.User client = target.GetData("USER_CLASS");
                    if (!client.isDeath)
                    {
                        client.isDeath = false;
                        client.adviceLSMD = false;
                        client.acceptDeath = false;

                        target.StopAnimation();
                        target.Health = 100;
                        target.TriggerEvent("CloseDeathUI");

                        Utilities.Notifications.SendNotificationINFO(target, "Has sido revivido por un administrador");
                        Utilities.Notifications.SendNotificationOK(player, "Has revivido a un jugador correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "Este usuario no está en estado de coma");
                }
            }
            else player.SendChatMessage("<font color='red'>[ERROR]</font> El comando no existe. (/ayuda para mas información)");
        }

        [Command("setgb")]
        public void CMD_setgb(Client player, uint id_dimension)
        {
            player.Dimension = id_dimension;
            player.SendChatMessage($"Ahora estás en la dimensión {id_dimension}");
        }

        // House cmds
        [Command("crearcasa")]
        public void CMD_crearcasa(Client player, int type, int price)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv == 5)
            {
                if (type == 1 || type == 2 || type == 3 || type == 4 || type == 5)
                {
                    //player.SetData<int>("CreateCompanyType", type);
                    //player.SetData<int>("CreateCompanyPrice", price);

                    player.SetData("CreateHouseType", type);
                    player.SetData("CreateHousePrice", price);
                    player.TriggerEvent("HouseAddStreetName");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No existe ese tipo de empresa");
            }
        }

        [Command("creartruckerstart")]
        public async Task CMD_creartruckerstart(Client player, int companyid)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv == 5)
            {
                Vector3 position = player.Position;

                int id = await World.Companies.DbFunctions.CreateTruckerStartCompany(companyid, position.X, position.Y, position.Z);
                
                ColShape shape = NAPI.ColShape.CreateCylinderColShape(position, 2, 2);
                TextLabel label = NAPI.TextLabel.CreateTextLabel("~b~Carguero~n~~w~Pulsa ~b~Z ~w~para interactuar", position, 5, 1, 0, new Color(255, 255, 255));
                Marker marker = NAPI.Marker.CreateMarker(1, position.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(0, 116, 255));

                shape.SetData("COMPANY_ID", companyid);
            }
        }

        [Command("testt")]
        public void CMD_testt(Client player)
        {
            NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
            NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
        }

        [Command("ponerropa")]
        public void CMD_ponerropa(Client player, int id, int slot, int drawable, int texture)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (user.adminLv >= 1)
            {
                Client target = Utilities.PlayerId.FindPlayerById(id);
                if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                else
                {
                    NAPI.Player.SetPlayerClothes(target, slot, drawable, texture);
                }
            }
        }

        [Command("sc", GreedyArg = true)]
        public void CMD_sc(Client player, string message)
        {
            Data.Entities.User adm = player.GetData("USER_CLASS");
            if (adm.adminLv == 0) return;
            var msg = "<font color='00B8FF'>STAFF"+adm.adminLv+" "+ player.SocialClubName + " " + message + "</font>";
            var allPlayers = NAPI.Pools.GetAllPlayers();

            foreach (var obj in allPlayers)
            {
                if (!obj.HasData("USER_CLASS")) return;
                Data.Entities.User user = player.GetData("USER_CLASS");
                if (user.adminLv >= 1)
                {
                    NAPI.Chat.SendChatMessageToPlayer(obj, msg);
                }
            }
        }

        [Command("anuncioADM", GreedyArg = true)]
        public void CMD_anuncioADM(Client player, string message)
        {
            Data.Entities.User adm = player.GetData("USER_CLASS");
            if (adm.adminLv >= 3) return;
            var msg = "<font color='00B8FF'>STAFF" + adm.adminLv + " " + player.SocialClubName + " " + message + "</font>";
            var allPlayers = NAPI.Pools.GetAllPlayers();

            foreach (var obj in allPlayers)
            {
                NAPI.Chat.SendChatMessageToPlayer(obj, msg);
            }

            Utilities.Webhooks.sendWebHook(2, msg);
        }

        // Fuel station
        [Command("crearfuelpoint")]
        public async Task CMD_crearfuelpoint(Client player, int businessid)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv == 5)
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

        // Comandos admin sin protección
        [Command("ir")]
        public void CMD_ir(Client player, int id)
        {
            Client target = Utilities.PlayerId.FindPlayerById(id);
            if (target != null) player.Position = target.Position;
            else Utilities.Notifications.SendNotificationERROR(player, "Esta ID no corresponde a ningún jugador");
        }

        [Command("traer")]
        public void CMD_traer(Client player, int id)
        {
            Client target = Utilities.PlayerId.FindPlayerById(id);
            if (target != null) target.Position = player.Position;
            else Utilities.Notifications.SendNotificationERROR(player, "Esta ID no corresponde a ningún jugador");
        }
    }
}

using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DowntownRP.Utilities;

namespace DowntownRP.Game.Commands
{
    public class Main : Script
    {
        [ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(Player player, string message)
        {
            var msg = $"{player.Name} dice: {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("ayuda")]
        public void CMD_ayuda(Player player)
        {
            player.SendChatMessage("Comandos básicos: /me - /do - /g - /s - /mp - /n - /guardar - /menu - /reportar - /canaln - /id - /localizar - /b - /stats - /ale - /fix - /taxi");
            player.SendChatMessage("Para comandos faccionarios usa /ayudafaccion");
        }

        [Command("reportar", GreedyArg = true)]
        public void CMD_reportar(Player player, string idOrName, string razon)
        {
            int idplayer = player.Value;

            Data.Entities.User target = Utilities.PlayerId.FindUserById(Convert.ToInt32(idOrName));
            if (target == null)
            {
                Utilities.Notifications.SendNotificationERROR(player, "Ese jugador no existe.");
            }
            else
            {
                bool adm = false;
                var m = new Data.Entities.Reporte();
                m.userid = player.Value;
                m.id = Data.Lists.aStaff.Count;
                m.Pj = player.Name;
                m.posicion = player.Position;
                m.mensaje = razon;
                Data.Lists.aStaff.Add(m);

                foreach (var user in Data.Lists.playersConnected)
                {
                    if (user.adminLv >= 1 & user.CanalChatA)
                    {
                        user.entity.SendChatMessage($"~p~[ADMIN]~w~ REPORTE {m.id} | El id {idplayer} ha reportado al id {idOrName} | {razon}");
                        adm = true;
                    }
                }

                player.SendChatMessage("~r~[!]~w~ Se ha enviado el reporte a los administradores. Te contestarán en breve");

                if (!adm)
                {
                    Utilities.Discord.Webhooks.Webhooks.SendReporte($"{player.Name}({player.GetData<Data.Entities.User>("USER_CLASS").idpj})", $"{target.name} ({target.idpj})", razon);
                }
            }
        }


        [Command("ayudafaccion")]
        public void CMD_ayudafaccion(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1)
            {
                player.SendChatMessage("Abrir puertas de la comisaría: Tecla Y");
                player.SendChatMessage("/f - /r - /controlpd - /borrarcontrol - /borrarcontroltodo - /ref - /multar - /esposar - /cachear - /placa - /taquillapd");
                if (user.rank >= 7) player.SendChatMessage("/contratar - /despedir - /ascender - /armero");
            }
            else if (user.rank == 6) player.SendChatMessage("/contratar - /despedir");
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en una facción o tu facción no tiene comandos disponibles");
        }

        [Command("me", GreedyArg = true)]
        public void CMD_me(Player player, string message)
        {
            var msg = "~p~" + player.Name + " " + message;
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("b", GreedyArg = true)]
        public void b(Player player, string message)
        {
            var msg = "~r~ [OOC]" + player.Name + $" ({player.Value}) : " + message + "";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("do", GreedyArg = true)]
        public void CMD_do(Player player, string message)
        {
            var msg = $"~g~ [{player.Name}] - {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("g", GreedyArg = true)]
        public void CMD_g(Player player, string message)
        {
            var msg = $"{player.Name} grita: {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(30, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("s", GreedyArg = true)]
        public void CMD_s(Player player, string message)
        {
            var msg = $"{player.Name} susurra: {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(7, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("mp", GreedyArg = true)]
        public void CMD_mp(Player player, int idplayer, string message)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Player target = Utilities.PlayerId.FindPlayerById(idplayer);
            if(target != null)
            {
                if (!target.HasData("USER_CLASS")) return;
                Data.Entities.User utarget = target.GetData<Data.Entities.User>("USER_CLASS");
                if (utarget.mpStatus == 0)
                {
                    target.SendChatMessage($"~p~[MP]~w~ ({player.Value}){player.Name} a {target.Name}: {message}");
                    player.SendChatMessage($"~p~[MP]~w~ {player.Name} a ({target.Value}) {target.Name}: {message}");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Este jugador tiene los mensajes privados desactivados");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No existe ningun jugador con esa ID");
        }

        [Command("guardar")]
        public async Task CMD_Guardar(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.CurrentWeapon.ToString() != "Unarmed")
            {
                if(await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
                {
                    WeaponHash wh = player.CurrentWeapon;
                    Data.Entities.Item itemn = new Data.Entities.Item(0, Utilities.Weapon.GetWeaponNameByHash(wh), 1, 1);
                    itemn.bullets = player.GetWeaponAmmo(wh);
                    itemn.isAWeapon = true;
                    itemn.weaponHash = wh;

                    Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, itemn, true, itemn.bullets);
                    player.RemoveWeapon(wh);
                    Utilities.Notifications.SendNotificationOK(player, $"Has guardado en tu inventario tu {Utilities.Weapon.GetWeaponNameByHash(wh)} con {itemn.bullets} balas");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No tienes slots en el inventario");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes ninguna arma en mano");
        }

        [Command("estacionarveh")]
        public async Task CMD_estacionarveh(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.IsInVehicle)
            {
                if (user.companyProperty != null)
                {
                    Vehicle vehicle = player.Vehicle;

                    if (vehicle.HasData("VEHICLE_COMPANY_DATA"))
                    {
                        Data.Entities.VehicleCompany veh = vehicle.GetData<Data.Entities.VehicleCompany>("VEHICLE_COMPANY_DATA");
                        if (veh.company == user.companyProperty)
                        {
                            veh.spawn = player.Position;
                            veh.spawnRot = player.Rotation.Z;

                            await World.Companies.DbFunctions.UpdateCompanyVehicleSpawn(veh.id, player.Position.X, player.Position.Y, player.Position.Z, player.Heading);

                            Utilities.Notifications.SendNotificationOK(player, "Has actualizado las coordenadas de spawn de tu vehículo");
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo de tu empresa");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo de empresa");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres dueño de una empresa");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo");
        }


        [Command("documentacion")]
        public void CMD_documentacion(Player player, int targetid)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(5, player);
            var target = Utilities.PlayerId.FindUserById(targetid);
            if (target.ViendoDocumentacion)
            {

                player.SendChatMessage($"~r~ {target.entity.Name} Está viendo otra documentación");

            }
            else
            {
                target.entity.TriggerEvent("documentacion", user.entity.Name, user.dni, user.edad.ToString());
                target.ViendoDocumentacion = true;
            }

        }


        [RemoteEvent("debugTest")]
        public void Ev_debug(Player player, string msg)
        {
            player.SendChatMessage(msg);
        }


        [RemoteEvent("cerrarDoc")]
        public void Ev_cerrarDOC(Player player)
        {
            player.getClass().ViendoDocumentacion = false;
        }

        /*[Command("ame", GreedyArg = true)]
        public void CMD_ame(Player player, string msg)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            Vector3 p = player.Position;
            p.Z = player.Position.Z + 1;
            if(user.ame != null)
            {
                NAPI.Entity.DeleteEntity(user.ame);
            }
            user.ame = NAPI.TextLabel.CreateTextLabel(msg, p, 10, 3, 1, new Color(0,255,255));
            foreach(var Player in Data.Lists.playersConnected)
            {
                Player.entity.TriggerEvent("AdjuntarAME", player, user.ame);
            }
        }*/

        [Command ("megafono", Alias ="m", GreedyArg = true, Description ="Requiere tener el objecto en el inventario")]
        public void CMD_megafono(Player player, string msg)
        {
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");

            if (Inventory.Inventory.CheckIfHasItem(usr, "Megafono") != null)
            {
                var mensaje=$"{player.Name} dice: {msg}";
                var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(50, player);

                foreach (var players in playersInRadius)
                {
                    NAPI.Chat.SendChatMessageToPlayer(players, mensaje);
                }
            }
            else
            {
                player.SendChatMessage("No llevas un megáfono");
            }
        }

        // Menu hq faction and house
        [Command("menu")]
        public async Task CMD_menu(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.isInHouseInterior)
            {
                if (user.houseInterior.owner == user.idpj) player.TriggerEvent("OpenHouseMenu");
                else Utilities.Notifications.SendNotificationERROR(player, "No eres el dueño de esta propiedad");
                return;
            }

            if (user.isInFactionInterior)
            {
                if (user.ilegalFactionInterior.id == user.faction)
                {
                    if (user.rank >= 4) player.TriggerEvent("OpenFactionMenu");
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para usar este comando");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de esta facción");
                return;
            }

            Utilities.Notifications.SendNotificationERROR(player, "No te encuentras en ninguna propiedad que permita este comando");
        }

        [Command ("sanciones")]
        public void CMD_sanciones (Player player, int target = -1)
        {
            Data.Entities.User obj = player.GetData<Data.Entities.User>("USER_CLASS");

            if(target != -1)
            {
                if (target != player.Value) 
                { 
                    if(obj.adminLv >= 0)
                    {
                        obj = Utilities.PlayerId.FindUserById(target);
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando sobre otras personas.");
                        return;
                    }
                }
            }

            player.SendChatMessage("-----SANCIONES:-------");

            foreach(Data.Entities.Minisancion ms in obj.minisanciones)
            {
                string tipo = "";
                switch (ms.paraElBalance)
                {
                    case -2:
                        tipo = "Kick";
                        break;
                    case -1:
                        tipo = "Punto Rol -";
                        break;
                    case 0:
                        tipo = "Warn";
                        break;
                    case 1:
                        tipo = "Punto Rol +";
                        break;
                }
                string minutos = ms.FechaHora.Minute.ToString();
                if(ms.FechaHora.Minute < 10) minutos = "0"+ ms.FechaHora.Minute.ToString();

                player.SendChatMessage($"~g~{tipo} ~p~{ms.Staff} ~b~ {ms.FechaHora.Day}/{ms.FechaHora.Month }/{ms.FechaHora.Year} {ms.FechaHora.Hour}:{minutos} ~r~ {ms.razon}");
            }

            player.SendChatMessage("------------------------");
        }

        [Command("stats")]
        public void CMD_stats(Player player, int target = -1)
        {
            Data.Entities.User obj = player.GetData<Data.Entities.User>("USER_CLASS");

            if (target != -1)
            {
                if (target != player.Value)
                {
                    if (obj.adminLv >= 0)
                    {
                        obj = Utilities.PlayerId.FindUserById(target);
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ejecutar este comando sobre otras personas.");
                        return;
                    }
                }
            }

            player.SendChatMessage("-----INFORMACIÓN:-------");
            player.SendChatMessage($"Nombre: {obj.entity.Name}");
            player.SendChatMessage($"Dimensión: {obj.entity.Dimension}");
            player.SendChatMessage($"ID Card: {obj.dni}");
            player.SendChatMessage($"SocialClubName: {obj.entity.SocialClubName}");
            if (obj.faction != 0) player.SendChatMessage($"Facción: {World.Factions.Main.GetFactionName(obj.faction)} ({obj.faction})   RANGO: {World.Factions.Main.GetFactionRankName(obj.faction, obj.rank)} ({obj.rank})");
            if (obj.faction == 1 | obj.faction == 2) player.SendChatMessage($"De Servicio: {obj.factionDuty}");
            if (obj.phone != 0) player.SendChatMessage($"Teléfono: {obj.phone}");
            if (obj.adminLv != 0) player.SendChatMessage($"~r~ {Utilities.AdminLVL.getAdmLevelName(obj.adminLv)}");
            player.SendChatMessage("------------------------");
        }

        [Command("localizar")]
        public void CMD_localizar(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.vehicles.Count != 0)
            {
                foreach(var veh in user.vehicles) player.TriggerEvent("LocalizarVehBlip", veh.entity.Position);
                Utilities.Notifications.SendNotificationOK(player, "Se ha marcado la posición de tus vehículos en el GPS");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes vehículos");
        }

        [Command("localizarfac")]
        public void CMD_localizarFac(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.faction == 0)
            {
                Utilities.Notifications.SendNotificationERROR(player, "No estás en una facción");
            }

            Data.Entities.Faction fac = Data.Lists.factions.Find(x => x.id == user.faction);

            if (fac.vehicles.Count != 0)
            {
                foreach (var veh in fac.vehicles) player.TriggerEvent("LocalizarVehBlip", veh.entity.Position);
                Utilities.Notifications.SendNotificationOK(player, "Se ha marcado la posición de los vehículos de tu facción en el GPS");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "Tu facción no tiene vehículos");
        }

        [Command("ale", Description = "/ale [maxNum] Saca un número aleatorio y lo muestra a los mismos usuarios que el /do, por defecto es menor de 6.")]
        public void CMD_ale(Player player, int numero = 6)
        {
            var msg = $"~g~ [{player.Name}] - saca aleatoriamente el número  {new Random().Next(numero)} de {numero}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("fix")]
        public void CMD_fix(Player player)
        {
            player.TriggerEvent("unfreeze_player");
            player.Dimension = 0;
            player.SendChatMessage("Fix aplicado.");
        }

        [Command("taxi", GreedyArg = true)]
        public void CMD_taxi(Player player, string mensaje)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (Data.Lists.taxistas.Count != 0)
            {
                foreach(var taxi in Data.Lists.taxistas)
                {
                    Data.Lists.taxiRaces.Add(new Data.Entities.TaxiRace() { id = Data.Info.AiTaxiRaces, position = player.Position, mensaje = mensaje, solicitador = player });
                    taxi.entity.SendChatMessage($"~y~[TAXI] ~w~Nuevo cliente a {player.Position.DistanceTo(taxi.entity.Position)} metros | {mensaje}");
                    taxi.entity.SendChatMessage($"~y~[TAXI] ~w~Usa /aceptartaxi {Data.Info.AiTaxiRaces} para responder la llamada");
                    Data.Info.AiTaxiRaces++;
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No hay taxistas en linea");
        }

        [Command("aceptartaxi", GreedyArg = true)]
        public void CMD_aceptartaxi(Player player, string raceid)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.companyMember != null)
            {
                if(user.companyMember.type == 1)
                {
                    if (user.isCompanyDuty)
                    {
                        if (player.IsInVehicle)
                        {
                            Data.Entities.TaxiRace race = Data.Lists.taxiRaces.Find(x => x.id == int.Parse(raceid));
                            if (race != null)
                            {
                                if (!race.isAccepted)
                                {
                                    player.TriggerEvent("CrearBlipCliente", race.position);
                                    Utilities.Notifications.SendNotificationOK(player, "Has aceptado la carrera, se han marcado las coordenadas en el GPS");
                                    Utilities.Notifications.SendNotificationOK(race.solicitador, "Un taxista ha respondido tu llamada, irá al punto donde lo llamaste");
                                    race.isAccepted = true;
                                    race.driver = player;
                                    player.Vehicle.SetData<Data.Entities.TaxiRace>("ON_RACE", race);
                                    return;
                                }

                                Utilities.Notifications.SendNotificationERROR(player, "Esta solicitud ya fue aceptada");
                            }
                            else Utilities.Notifications.SendNotificationERROR(player, "No existe ninguna solicitud con esta ID");
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "Debes de estar en un taxi para poder aceptar solicitudes");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres taxista");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres taxista");
        }

        [Command ("pcu")]
        public void pcu(Player pl)
        {
            //pl.TriggerEvent("abrirpcu", "http://v.hyaxe.com");
        }

    }
}


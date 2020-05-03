using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            player.SendChatMessage("Comandos básicos: /me - /do - /g - /s - /mp - /n - /guardar - /menu - /reportar - /canaln - /id - /localizar - /b");
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
                foreach (var user in Data.Lists.playersConnected)
                {
                    if(user.adminLv >= 1) user.entity.SendChatMessage($"<font color='purple'>[ADMIN]</font> REPORTE | El id {idplayer} ha reportado al id {idOrName} | {razon}");
                }

                player.SendChatMessage("<font color='red'>[!]</font> Se ha enviado el reporte a los administradores. Te contestarán en breve");
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
                player.SendChatMessage("/f - /r - /controlpd - /borrarcontrol - /borrarcontroltodo - /ref - /multar - /esposar - /cachear - /placa");
                if (user.rank == 15) player.SendChatMessage("/contratar - /despedir");
            }
            else if (user.rank == 6) player.SendChatMessage("/contratar - /despedir");
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en una facción o tu facción no tiene comandos disponibles");
        }

        [Command("me", GreedyArg = true)]
        public void CMD_me(Player player, string message)
        {
            var msg = "<font color='B950C3'>" + player.Name + " " + message + "</font>";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("b", GreedyArg = true)]
        public void b(Player player, string message)
        {
            var msg = "(OOC) " + player.Name + $" ({player.Value}) : " + message + "";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("do", GreedyArg = true)]
        public void CMD_do(Player player, string message)
        {
            var msg = $"<font color ='#65C350'> [{player.Name}] - {message}</font>";
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
                    target.SendChatMessage($"<font color='purple'>[MP]</font> ({player.Value}){player.Name} a {target.Name}: {message}");
                    player.SendChatMessage($"<font color='purple'>[MP]</font> {player.Name} a ({target.Value}) {target.Name}: {message}");
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
                            veh.spawnRot = player.Heading;

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


        /*[Command("documentacion")]
        public void CMD_documentacion(Player player, int targetid)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(30, player);
            var target = Utilities.PlayerId.FindPlayerById(targetid);
            target.TriggerEvent("CrearTrato", 1, $"Documentacion de:  {player.Name}", "{ \"nombre\":\""+player.Name+"\", \"Nac\":\""+user.edad.ToString()+"\", \"Num\":\""+user.dni+"\", \"tipo\": Tarjeta de Identidad} ");
        }*/

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

        [Command ("stats")]
        public void CMD_stats (Player player, int target = -1)
        {
            Data.Entities.User obj;
            if(target == -1)
            {
                obj = player.GetData<Data.Entities.User>("USER_CLASS");
            }
            else
            {
                if(Utilities.AdminLVL.PuedeUsarComando(player, 1)){
                    obj = Utilities.PlayerId.FindUserById(target);
                }
                else{

                    if (player.Value == target)
                    {
                        obj = player.GetData<Data.Entities.User>("USER_CLASS");
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No tienes permiso para usar este comando sobre otras personas");
                        return;
                    }
                }
            }

            player.SendChatMessage("-----INFORMACIÓN:-------");
            player.SendChatMessage($"Nombre: {obj.entity.Name}");
            player.SendChatMessage($"Dimensión: {obj.entity.Dimension}");
            player.SendChatMessage($"SocialClubName: {obj.entity.SocialClubName}");
            player.SendChatMessage($"ID CARD: {obj.dni}");
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
    }
}


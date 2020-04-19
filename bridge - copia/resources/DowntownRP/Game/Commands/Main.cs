﻿using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Commands
{
    public class Main : Script
    {
        [ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(Client player, string message)
        {
            var msg = $"{player.Name} dice: {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }


        [Command("me", GreedyArg = true)]
        public void CMD_me(Client player, string message)
        {
            var msg = "<font color='B950C3'>" + player.Name + " " + message + "</font>";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("do", GreedyArg = true)]
        public void CMD_do(Client player, string message)
        {
            var msg = "<font color='65C350'>" + message + " (" + player.Name + ")</font>";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("g", GreedyArg = true)]
        public void CMD_g(Client player, string message)
        {
            var msg = $"{player.Name} grita: {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(30, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("s", GreedyArg = true)]
        public void CMD_s(Client player, string message)
        {
            var msg = $"{player.Name} susurra: {message}";
            var playersInRadius = NAPI.Player.GetPlayersInRadiusOfPlayer(7, player);

            foreach (var players in playersInRadius)
            {
                NAPI.Chat.SendChatMessageToPlayer(players, msg);
            }
        }

        [Command("mp", GreedyArg = true)]
        public void CMD_mp(Client player, int idplayer, string message)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            Client target = Utilities.PlayerId.FindPlayerById(idplayer);
            if(target != null)
            {
                if (!target.HasData("USER_CLASS")) return;
                Data.Entities.User utarget = target.GetData("USER_CLASS");
                if (utarget.mpStatus == 0)
                {
                    target.SendChatMessage($"~p~[MP] ~w~{player.Name}: {message}");
                    player.SendChatMessage($"~p~[MP] [{target.Name}] ~w~{player.Name}: {message}");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Este jugador tiene los mensajes privados desactivados");
            }
            Utilities.Notifications.SendNotificationERROR(player, "No existe ningun jugador con esa ID");
        }

        [Command("estacionarveh")]
        public async Task CMD_estacionarveh(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (player.IsInVehicle)
            {
                if (user.companyProperty != null)
                {
                    Vehicle vehicle = player.Vehicle;

                    if (vehicle.HasData("VEHICLE_COMPANY_DATA"))
                    {
                        Data.Entities.VehicleCompany veh = vehicle.GetData("VEHICLE_COMPANY_DATA");
                        if(veh.company == user.companyProperty)
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
    }
}

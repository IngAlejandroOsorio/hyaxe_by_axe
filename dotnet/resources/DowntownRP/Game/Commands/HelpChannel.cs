using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Game.Commands
{
    public class HelpChannel : Script
    {
        bool isHelpChannelActive = true;

        [Command("n", "Uso: /n(ayuda) [mensaje] (Envias un mensaje al canal de dudas)", GreedyArg = true)]
        public void HelpChannelCommand(Player player, string message)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (NAPI.Data.HasEntityData(player, "HelpChannelCooldown"))
            {
                DateTime cooldown = NAPI.Data.GetEntityData(player, "HelpChannelCooldown");
                if (cooldown > DateTime.Now)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, $"~r~[ERROR]~w~ Necesitas esperar {Math.Round((cooldown - DateTime.Now).TotalSeconds)} segundos para usar este canal.");
                    return;
                }
            }
            string rank = "";
            if (user.adminLv >= 1) rank = Utilities.AdminLVL.getAdmLevelName(user.adminLv);
            else player.SetData("HelpChannelCooldown", DateTime.Now.AddMinutes(1));
            foreach (var p in NAPI.Pools.GetAllPlayers())
            {
                if (p.HasData("HelpChannelDisabled")) continue;

                p.SendChatMessage($"~g~[Dudas] ~o~{rank}~w~ {player.Name} ({player.Value}): {message}");
            }
        }

        [Command("bloquearn")]
        public void ToggleHelpChannelCommand(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 3)
            {
                isHelpChannelActive = !isHelpChannelActive;
                string newState = isHelpChannelActive ? "desbloqueado" : "bloqueado";
                NAPI.Chat.SendChatMessageToAll($"~r~[INFO]~w~ Un administrador ha {newState} el canal de ayuda.");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(player, "~r~[ERROR]~w~ El comando no existe. (/ayuda para mas información)");
            }
        }

        [Command("canaln", "Uso: /canaln (Activas / desactivas el canal de dudas.)")]
        public void ToggleHelpChannelUserCommand(Player player)
        {
            bool state = NAPI.Data.HasEntityData(player, "HelpChannelDisabled");
            if (state) NAPI.Data.ResetEntityData(player, "HelpChannelDisabled"); else NAPI.Data.SetEntityData(player, "HelpChannelDisabled", true);

            string newState = state ? "Activaste" : "Desactivaste";
            NAPI.Chat.SendChatMessageToPlayer(player, $"~r~[INFO]~w~ {newState} el canal de ayuda.");
        }

        [Command("mutear")]
        public void Mute(Player player, string idOrName)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.adminLv >= 2)
            {
                var target = Utilities.PlayerId.FindPlayerById(Convert.ToInt32(idOrName));
                if (target == null) Utilities.Notifications.SendNotificationERROR(player, "Ningún jugador encontrado.");
                else if (player == target) Utilities.Notifications.SendNotificationERROR(player, "No te puedes silenciar a ti mismo.");
                else
                {
                    if (NAPI.Data.HasEntityData(target, "IsMutedFromHelpChannel")) NAPI.Data.ResetEntityData(target, "IsMutedFromHelpChannel");
                    else NAPI.Data.SetEntityData(target, "IsMutedFromHelpChannel", true);

                    string toggleText = NAPI.Data.HasEntityData(target, "IsMutedFromHelpChannel") ? "muteado" : "desmuteado";
                    NAPI.Notification.SendNotificationToPlayer(target, $"~r~ADMIN: ~w~{player.Name} te ha {toggleText} del canal de ayuda.");
                    NAPI.Notification.SendNotificationToPlayer(player, $"~r~ADMIN: ~w~Has {toggleText} {target.Name} del canal de ayuda.");
                }
                return;
            }
            else NAPI.Chat.SendChatMessageToPlayer(player, "~r~[ERROR]~w~ El comando no existe. (/ayuda para mas información)");
            return;
        }

    }
}

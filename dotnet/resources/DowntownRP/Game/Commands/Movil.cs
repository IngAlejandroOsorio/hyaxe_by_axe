using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Game.Commands
{
    public class Movil : Script
    {
        [Command("llamar")]
        public void CMD_llamar(Player player, int numero)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.phone != 0)
            {
                Data.Entities.User target = Data.Lists.playersConnected.Find(x => x.phone == numero);
                if (target != null)
                {
                    if (!target.isInPhoneCall)
                    {
                        player.SendChatMessage($"~g~[MOVIL]~w~ Llamando al {numero}...");
                        target.entity.SendChatMessage($"~g~[MOVIL]~w~ Llamada del número {user.phone}, usa /contestar para coger la llamada");
                        Utilities.Chat.EntornoDo(target.entity, "El teléfono está sonando");
                        target.lastCallUser = user;
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "El remitente se encuentra en otra llamada");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No existe ninguna linea con ese número de teléfono");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes un teléfono");
        }

        [Command("contestar")]
        public void CMD_contestar(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.phone != 0)
            {
                if (!user.isInPhoneCall)
                {
                    if (user.lastCallUser != null)
                    {
                        if(user.lastCallUser.entity != null)
                        {
                            Data.Entities.User target = user.lastCallUser;
                            player.SendChatMessage($"Has contestado la llamada, usa /c (texto) para hablar por llamada, usa /colgar para colgar la llamada");
                            target.entity.SendChatMessage($"Han contestado a la llamada, usa /c (texto) para hablar por llamada, usa /colgar para colgar la llamada");
                            target.actualCall = user;
                            user.actualCall = target;
                            user.isInPhoneCall = true;
                            target.isInPhoneCall = true;
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "No tienes llamadas");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes llamadas");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Ya estás en una llamada");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes un teléfono");
        }

        [Command("c", GreedyArg = true)]
        public void CMD_c(Player player, string texto)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (!user.isInPhoneCall)
            {
                if(user.actualCall != null)
                {
                    Data.Entities.User target = user.actualCall;
                    player.SendChatMessage($"~g~[MOVIL]~w~ ({target.phone}) | Tu: {texto}");
                    target.entity.SendChatMessage($"~g~[MOVIL]~w~ ({user.phone}) | Teléfono: {texto}");
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en una conversación telefónica");
        }

        [Command("colgar")]
        public void CMD_colgar(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (!user.isInPhoneCall)
            {
                if (user.actualCall != null)
                {
                    Data.Entities.User target = user.actualCall;
                    player.SendChatMessage($"La llamada se ha cortado.");
                    target.entity.SendChatMessage($"La llamada se ha cortado.");

                    target.actualCall = null;
                    user.actualCall = null;
                    user.isInPhoneCall = false;
                    target.isInPhoneCall = false;
                    target.lastCallUser = null;
                    user.lastCallUser = null;
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en una conversación telefónica");
        }
    }
}

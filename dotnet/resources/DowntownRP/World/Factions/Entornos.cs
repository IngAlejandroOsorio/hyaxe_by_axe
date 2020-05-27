using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Game.Commands
{
    public class Entornos : Script
    {                   //<font color='#4169e1'>
        [Command("entorno",Description = "Manda la ubicación y un mensaje a LSPD.", GreedyArg = true)]
        public void CMD_entorno(Player player, string msg)
        {
            bool tieneAbierto = false;
            foreach (Data.Entities.Entorno aS in Data.Lists.entornos)
            {
                if ((aS.estado == 0) &(aS.Pj == player.Name))
                {
                    tieneAbierto = true;
                    Utilities.Notifications.SendNotificationERROR(player, "Ya tienes una ayuda en espera");
                    return;
                }
            }

            var entorno = new Data.Entities.Entorno(player, msg);

            entorno.pd = true;

            Data.Lists.entornos.Add(entorno);

            foreach(Data.Entities.User u in Data.Lists.playersConnected)
            {
                if(u.faction == 1 & u.factionDuty == true & entorno.pd)
                {
                    player.SendChatMessage($"[ENTORNOS] ID: {entorno.userid} - {entorno.Pj} => {msg}");
                }else if (u.faction == 2 & u.factionDuty == true & entorno.md)
                {
                    player.SendChatMessage($"[ENTORNOS] ID: {entorno.userid} - {entorno.Pj} => {msg}");
                }
            }

            Utilities.Notifications.SendNotificationOK(player, "Tu mensaje ha sido enviado a las autoridades");
        }

        [Command ("entornos")]
        public void CMD_entornos (Player player, string arg = "", int arg1 = 0)
        {
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");
                if (arg == "")
                {
                    player.SendChatMessage($"---------ENTORNOS---------");
                    foreach (Data.Entities.Entorno aS in Data.Lists.entornos)
                    {
                        if (aS.estado == 0)
                        {
                            if(aS.pd && usr.faction == 1) player.SendChatMessage($"ID: {aS.id} - {aS.Pj} = {aS.mensaje}");
                            if(aS.md && usr.faction == 2) player.SendChatMessage($"ID: {aS.id} - {aS.Pj} = {aS.mensaje}");
                    }
                    }
                    player.SendChatMessage($"--------------------------------------------");
                }else if (arg == "t")
                {
                    Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
                    user.CanalAyudasADM = !user.CanalAyudasADM;
                    Utilities.Notifications.SendNotificationINFO(player, $"El canal de ayudas está en {user.CanalAyudasADM}");
                 }
                else
                {
                    Data.Entities.Entorno ent = Data.Lists.entornos.Find(x => x.id == arg1);
                    //player.TriggerEvent("createWayPoint", ent.posicion.X, ent.posicion.Y);
                }
            }

        [Command ("cent")]
        public void CMD_cent (Player player, int id)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if((user.faction == 1|user.faction == 2) & user.factionDuty)
            {
                Data.Lists.entornos.Find(x => x.id == id).cerrar(player);
            }
        }

        
    }
}

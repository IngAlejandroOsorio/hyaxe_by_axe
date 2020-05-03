using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Game.Commands
{
    public class Entornos : Script
    {                   //<font color='#4169e1'>
        [Command ("entorno",Description = "Manda la ubicación y un mensaje al staff.", GreedyArg = true)]
        public void CMD_entorno(Player player, string tipo, string msg)
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

            if (tipo.Contains("pd"))
            {
                entorno.pd = true;
            }
            if (tipo.Contains("md"))
            {
                entorno.md = true;
            }

            Data.Lists.entornos.Add(entorno);

            foreach(Data.Entities.User u in Data.Lists.playersConnected)
            {
                if(u.faction == 1 & u.factionDuty == true & entorno.pd)
                {
                    player.SendChatMessage($"<font color='#ff0000'> [ENTORNOS]</font> <font color='#33cc33'>ID: {entorno.userid} - {entorno.Pj} </font> <font color='#FF0000'>=></font> <font color='#66ffff'>{msg} </font>");
                }else if (u.faction == 2 & u.factionDuty == true & entorno.md)
                {
                    player.SendChatMessage($"<font color='#ff0000'> [ENTORNOS]</font> <font color='#33cc33'>ID: {entorno.userid} - {entorno.Pj} </font> <font color='#FF0000'>=></font> <font color='#66ffff'>{msg} </font>");
                }
            }

            Utilities.Notifications.SendNotificationOK(player, "Tu mensaje ha sido enviado al staff");
        }

        [Command ("entornos")]
        public void CMD_entornos (Player player, string arg = "", int arg1 = 0)
        {
            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");
                if (arg == "")
                {
                    player.SendChatMessage($"<font color='#FF0000'>---------ENTORNOS---------</font>");
                    foreach (Data.Entities.Entorno aS in Data.Lists.entornos)
                    {
                        if (aS.estado == 0)
                        {
                            if(aS.pd && usr.faction == 1) player.SendChatMessage($"<font color='#00FF00'>ID: {aS.id} - {aS.Pj}</font> <font color='#FF0000'> = </font>   <font color='#1111FF'> {aS.mensaje}</font>");
                            if(aS.md && usr.faction == 2) player.SendChatMessage($"<font color='#00FF00'>ID: {aS.id} - {aS.Pj}</font> <font color='#FF0000'> = </font>   <font color='#1111FF'> {aS.mensaje}</font>");
                    }
                    }
                    player.SendChatMessage($"<font color='#FF0000'>--------------------------------------------</font>");
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

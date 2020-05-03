using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Game.Commands
{
    public class AyudaADM : Script
    {                   //<font color='#4169e1'>
        [Command ("ayudaadm",Description = "Manda la ubicación y un mensaje al staff.", GreedyArg = true)]
        public void CMD_ayudaADM(Player player, string msg)
        {
            bool tieneAbierto = false;
            foreach (Data.Entities.AsistenciaStaff aS in Data.Lists.aStaff)
            {
                if ((aS.estado == 0) &(aS.Pj == player.Name))
                {

                    tieneAbierto = true;
                }
            }
            if(tieneAbierto == true)
            {
                Utilities.Notifications.SendNotificationERROR(player, "Ya tienes una ayuda en espera");
                return;
            }


            var m = new Data.Entities.AsistenciaStaff();
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            m.userid = player.Value;
            m.Pj = player.Name;
            m.posicion = player.Position;
            m.mensaje = msg;
            Data.Lists.aStaff.Add(m);

            foreach(Data.Entities.User u in Data.Lists.playersConnected)
            {
                if(u.adminLv != 0 & u.CanalAyudasADM == true)
                {
                    player.SendChatMessage($"<font color='#ff0000'> [AYUDASTAFF]</font> <font color='#33cc33'>ID: {m.userid} - {m.Pj} </font> <font color='#FF0000'>=></font> <font color='#66ffff'>{msg} </font>");
                }
            }

            Utilities.Notifications.SendNotificationOK(player, "Tu mensaje ha sido enviado al staff");
        }

        [Command ("ayudasadm")]
        public void CMD_ayudasADM (Player player, string arg = "")
        {
            if(Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                if (arg == "")
                {
                    player.SendChatMessage($"<font color='#FF0000'>---------AYUDAS SOLICITADAS---------</font>");
                    foreach (Data.Entities.AsistenciaStaff aS in Data.Lists.aStaff)
                    {
                        if (aS.estado == 0)
                        {

                            player.SendChatMessage($"<font color='#00FF00'>ID: {aS.userid} - {aS.Pj}</font> <font color='#FF0000'> = </font>   <font color='#1111FF'> {aS.mensaje}</font>");
                        }
                    }
                    player.SendChatMessage($"<font color='#FF0000'>--------------------------------------------</font>");
                }else if (arg == "t")
                {
                    Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
                    user.CanalAyudasADM = !user.CanalAyudasADM;
                    Utilities.Notifications.SendNotificationINFO(player, $"El canal de ayudas está en {user.CanalAyudasADM}");
                }
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permiso para usar este comando");
            }
        }

        [Command ("cayuda")]
        public void CMD_cayuda (Player player, int id)
        {
            if(Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
                var caso = Data.Lists.aStaff.Find(x => x.userid == id);
                caso.estado = 1;
                caso.idStaff = user.id;
                Utilities.Notifications.SendNotificationOK(player, "Se ha cerrado la ayuda correctamente");
                Utilities.Notifications.SendNotificationOK(Utilities.PlayerId.FindPlayerById(caso.userid), $"Tu asistencia ha sido cerrada por un {Utilities.AdminLVL.getAdmLevelName(player)}");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permiso para usar este comando");
            }
        }

        
    }
}

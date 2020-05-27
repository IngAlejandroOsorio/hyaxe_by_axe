using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.Game.Commands
{
    public class AyudaADM : Script
    {                   //<font color='#4169e1'>
       
        [Command ("reportes")]
        public void CMD_reportes (Player player)
        {
            if(Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                    player.SendChatMessage($"~h~ ~o~---------REPORTES---------");
                    foreach (Data.Entities.Reporte aS in Data.Lists.aStaff)
                    {
                        if (aS.estado == 0)
                        {

                            player.SendChatMessage($"~r~{aS.id}: ~q~{aS.userid} - {aS.Pj}  a {aS.reportado} = ~g~{aS.mensaje}");
                        }
                    }
                    player.SendChatMessage($"~h~ ~o~--------------------------------------------");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permiso para usar este comando");
            }
        }

        [Command ("creporte")]
        public void CMD_cayuda (Player player, int id)
        {
            if(Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
                var caso = Data.Lists.aStaff[id];
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

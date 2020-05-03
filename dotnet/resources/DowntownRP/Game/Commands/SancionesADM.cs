using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.Game.Commands
{
    public class SancionesADM : Script
    {
        [Command("kick", GreedyArg = true, Description ="Comando para expulsar a un sujeto administrativamente")]
        public void CMD_kick(Player player, int obj, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 2))
            {
                var target = Utilities.PlayerId.FindPlayerById(obj);
                Data.Entities.User t = target.GetData<Data.Entities.User>("USER_CLASS");
                RegistrarKick(t.idpj, player.Name, razon);
                Utilities.Notifications.SendNotificationERROR(player, $"Has sido expulsado por un {Utilities.AdminLVL.getAdmLevelName(player)} por { razon}.");
                target.Kick($"Has sido expulsado por un {Utilities.AdminLVL.getAdmLevelName(player)} por {razon}.");

            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para usar este comando");
            }
        }
            //Timestamp - UserId - Staff Name - Razon

         public async static Task RegistrarKick (int UserId, string StaffName, string razon)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO kicks (PlayerID, Razon, Staff) VALUES (@PlayerID, @Razon, @Staff)";
                command.Parameters.AddWithValue("@PlayerID", UserId);
                command.Parameters.AddWithValue("@Razon", razon);
                command.Parameters.AddWithValue("@Staff", StaffName);


                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        [Command("ban", GreedyArg = true, Description = "Comando para expulsar a un sujeto administrativamente")]
        public void CMD_tban(Player player, int obj, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 3))
            {
                var target = Utilities.PlayerId.FindPlayerById(obj);
                Data.Entities.User t = target.GetData<Data.Entities.User>("USER_CLASS");
                RegistrarBan(target.SocialClubName, target.Name, razon);
                Utilities.Notifications.SendNotificationERROR(target, $"Has sido baneado por un {Utilities.AdminLVL.getAdmLevelName(player)} por { razon}.");
                Utilities.Notifications.SendNotificationERROR(target, $"Pasate por el foro o ts a negociar tu ban");
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para usar este comando");
            }
        }
        //Timestamp - UserId - Staff Name - Razon

        public async static Task RegistrarBan(string SocialClubName, string StaffName, string razon)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO bans (SocialClubName, Razon, Staff) VALUES (@SocialClubName, @Razon, @Staff)";
                command.Parameters.AddWithValue("@SocialClubName", SocialClubName);
                command.Parameters.AddWithValue("@Razon", razon);
                command.Parameters.AddWithValue("@Staff", StaffName);


                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}

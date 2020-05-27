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
                RegistrarMini((ulong) t.id, player.Name, razon, -2);
                Utilities.Notifications.SendNotificationERROR(target, $"Has sido expulsado por un {Utilities.AdminLVL.getAdmLevelName(player)} por { razon}.");
                target.SendChatMessage($"Has sido expulsado por un {Utilities.AdminLVL.getAdmLevelName(player)} por {razon}.");
                target.Kick();

            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para usar este comando");
            }
        }
            //Timestamp - UserId - Staff Name - Razon

        [Command("ban", GreedyArg = true, Description = "Comando para banear a un sujeto administrativamente")]
        public void CMD_pban(Player player, int obj, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 3))
            {
                var target = Utilities.PlayerId.FindPlayerById(obj);
                Data.Entities.User t = target.GetData<Data.Entities.User>("USER_CLASS");
                RegistrarBan(target.SocialClubId, target.Name, razon);
                Utilities.Notifications.SendNotificationERROR(target, $"Has sido baneado por un {Utilities.AdminLVL.getAdmLevelName(player)} por { razon}.");
                Utilities.Notifications.SendNotificationERROR(target, $"Pasate por el foro o ts a negociar tu ban");
                target.Kick();
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para usar este comando");
            }
        }
        //Timestamp - UserId - Staff Name - Razon
        [Command("tban", GreedyArg = true, Description = "Comando para banear a un sujeto temporalmente. (player tiempo[dias], razon)")]
        public void CMD_tban(Player player, int obj, int tiempo, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 3))
            {
                DateTime dt = DateTime.UtcNow.AddDays(tiempo);

                var target = Utilities.PlayerId.FindPlayerById(obj);
                Data.Entities.User t = target.GetData<Data.Entities.User>("USER_CLASS");
                RegistrarBan(target.SocialClubId, target.Name, razon, dt);
                Utilities.Notifications.SendNotificationERROR(target, $"Has sido baneado por un {Utilities.AdminLVL.getAdmLevelName(player)} por { razon} durante {tiempo} días");
                Utilities.Notifications.SendNotificationERROR(target, $"Pasate por el foro o ts a negociar tu tempban");
                target.Kick();
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para usar este comando");
            }
        }

        public async static Task RegistrarBan(ulong SocialClubId, string StaffName, string razon)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO bans (SocialClubId, Razon, Staff) VALUES (@SocialClubId, @Razon, @Staff)";
                command.Parameters.AddWithValue("@SocialClubId", SocialClubId);
                command.Parameters.AddWithValue("@Razon", razon);
                command.Parameters.AddWithValue("@Staff", StaffName);

                

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                connection.Close();
            }
        }
        public async static Task RegistrarBan(ulong SocialClubId, string StaffName, string razon, DateTime hasta)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO bans (SocialClubId, Razon, Staff, Hasta) VALUES (@SocialClubId, @Razon, @Staff, @Hasta)";
                command.Parameters.AddWithValue("@SocialClubId", SocialClubId);
                command.Parameters.AddWithValue("@Razon", razon);
                command.Parameters.AddWithValue("@Staff", StaffName);
                command.Parameters.AddWithValue("@Hasta", hasta);
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                connection.Close();
            }
        }

        [Command("warn", GreedyArg = true)]
        public async Task CMD_warn (Player player, int target, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 1))
            {
                Data.Entities.User t = Utilities.PlayerId.FindUserById(target);
                if(Utilities.AdminLVL.PuedeUsarComando(player, /*t.adminLv + */1))
                {
                    Data.Entities.Minisancion ms = new Data.Entities.Minisancion(DateTime.Now, player.Name, razon, 0);
                    t.minisanciones.Add(ms);
                    RegistrarMini((ulong) t.id, player.Name, razon, 0);
                    t.entity.SendChatMessage($" ~g~El {Utilities.AdminLVL.getAdmLevelName(player)} {player.Name} te ha avisado por {razon} ");
                    player.SendNotification("Aviso puesto a" + t.entity.Name);
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No puedes minisancionar a miembros del staff con mayor rango que tú");
                }
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes usar este comando");
            }
        }


        [Command("prn", GreedyArg = true)]
        public async Task CMD_prn(Player player, int target, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 2))
            {
                Data.Entities.User t = Utilities.PlayerId.FindUserById(target);
                if (Utilities.AdminLVL.PuedeUsarComando(player, /*t.adminLv + */1))
                {
                    Data.Entities.Minisancion ms = new Data.Entities.Minisancion(DateTime.Now, player.Name, razon, -1);
                    t.minisanciones.Add(ms);
                    RegistrarMini((ulong)t.id, player.Name, razon, -1);
                    t.entity.SendChatMessage($"~g~El {Utilities.AdminLVL.getAdmLevelName(player)} {player.Name} te ha puesto un punto de rol negativo por {razon} ");
                    player.SendNotification("Punto rol negativo puesto a" + t.entity.Name);
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No puedes minisancionar a miembros del staff con mayor rango que tú");
                }
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes usar este comando");
            }
        }

        [Command("prp", GreedyArg = true)]
        public async Task CMD_prp(Player player, int target, string razon)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 2))
            {
                Data.Entities.User t = Utilities.PlayerId.FindUserById(target);
                if (Utilities.AdminLVL.PuedeUsarComando(player, /*t.adminLv*/ + 1))
                {
                    Data.Entities.Minisancion ms = new Data.Entities.Minisancion(DateTime.Now, player.Name, razon, 1);
                    t.minisanciones.Add(ms);
                    RegistrarMini((ulong)t.id, player.Name, razon, 1);

                    t.entity.SendChatMessage($"~g~El {Utilities.AdminLVL.getAdmLevelName(player)} {player.Name} te ha puesto un punto de rol positivo por {razon} ");

                    player.SendNotification("Punto rol positivo puesto a"+t.entity.Name);
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No puedes minisancionar a miembros del staff con mayor rango que tú");
                }
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes usar este comando");
            }
        }

        public async static Task RegistrarMini(ulong SocialClubId, string StaffName, string razon, int tipo)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO minisanciones (SocialClubID, Razon, Staff, Tipo) VALUES (@SocialClubId, @Razon, @Staff, @tipo)";
                command.Parameters.AddWithValue("@SocialClubId", SocialClubId);
                command.Parameters.AddWithValue("@Razon", razon);
                command.Parameters.AddWithValue("@Staff", StaffName);
                command.Parameters.AddWithValue("@tipo", tipo);




                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                connection.Close();
            }
        }
    }
}

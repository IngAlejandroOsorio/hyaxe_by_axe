using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DowntownRP.Utilities
{
    public class Log
    {

        /*
         TIPOS:
             0- Dar armas/dinero administrativamente
             1- Traficos ilegales
             
             
             
             */
        public Log (String text, int tipo)
        {
            apuntar(text, tipo);
        }

        private async Task apuntar(String text, int tipo)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO log (Mensaje, tipo, DateTime) VALUES (@PlayerID, @Razon, @dt)";
                command.Parameters.AddWithValue("@PlayerID", text);
                command.Parameters.AddWithValue("@Razon", tipo);
                command.Parameters.AddWithValue("@dt", DateTime.UtcNow);


                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}

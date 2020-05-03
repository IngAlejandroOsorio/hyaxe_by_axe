using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.Data.Entities
{
    public class puntoTraficoIl
    {
        public Vector3 posiciones { get; set; }
        public float heading { get; set; }
        public bool activo { get; set; }

        public puntoTraficoIl(float x, float y, float z, float heading, bool r = false)
        {
            this.posiciones = new Vector3(x, y, z);
            this.heading = heading;

            if (r)
            {
                registrar(x, y, z, heading);
            }

        }

        public async Task registrar(float x, float y, float z, float heading)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO puntostrafico (x, y, z, heading) VALUES (@x, @y, @z, @h);";

                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@z", z);
                command.Parameters.AddWithValue("@h", heading);


                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}
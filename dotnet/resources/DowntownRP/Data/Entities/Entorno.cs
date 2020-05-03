using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace DowntownRP.Data.Entities
{
    public class Entorno
    {
        public int userid { get; set; }
        public string mensaje { get; set; }
        public Vector3 posicion { get; set; }
        public string Pj { get; set; }
        public int estado { get; set; } = 0; //0-Sin leer || 1- Solucionado
        public bool pd { get; set; } = false;
        public bool md { get; set; } = false;
        public int id { get; set; } = 0;
        public Player player { get; set; }
        public Entities.User usr { get; set; }

        public Entorno(Player player, string msg, bool pd = false, bool md = false)
        {
            this.userid = player.Value;
            this.mensaje = msg;
            this.posicion = player.Position;
            this.Pj = player.Name;
            this.pd = pd;
            this.md = md;
            this.player = player;
            this.usr = player.GetData<Data.Entities.User>("USER_CLASS");
            //RAsync();

        }

        public async Task RAsync()
        {
            this.id = await Registrar(this.player.Position.X, this.player.Position.Y, this.player.Position.Z, usr.idpj, this.pd, this.md, this.mensaje);
        }

        public async static Task<int> Registrar(float x, float y, float z, int idPj, bool pd, bool md, string msg)
        {
            int pdv = 0;
            int mdv = 0;
            if (pd) pdv = 1;
            if (md) mdv = 1;

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO entornos (x, y, z, idPj, pd, md, msg) VALUES (@x, @y, @z, @idPj, @pd, @md, @msg);)";
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@z", z);
                command.Parameters.AddWithValue("@pd", pdv);
                command.Parameters.AddWithValue("@md", mdv);
                command.Parameters.AddWithValue("@msg", msg);
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                MySqlCommand getId = connection.CreateCommand();
                getId.CommandText = "SELECT LAST_INSERT_ID();";
                var r = await getId.ExecuteReaderAsync().ConfigureAwait(false);
                while (r.Read())
                {
                    return r.GetInt32(0);
                }

                
            }

            return 0;
        }

        public async Task cerrar (Player player)
        {
            this.estado = 1;
            Data.Entities.User d = player.GetData<Data.Entities.User>("USER_CLASS");

            /*using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE entornos SET CerradoPor = @money WHERE id = @id";
                command.Parameters.AddWithValue("@money", d.idpj);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }*/
        }
    }
}

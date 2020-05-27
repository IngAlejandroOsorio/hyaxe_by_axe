using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.World.Factions.PD
{
    public class CargarPolicia
    {
        public CargarPolicia (Player Player)
        {
            //CrearMadero(Player);
        }

        public static async Task CrearMadero(Player player)
        {
            /*Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "SELECT * FROM policias WHERE idPj = @idpj";
                cmd.Parameters.AddWithValue("@idpj", user.idpj);
                DbDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    Data.Entities.Policia madero = player.GetData<Data.Entities.User>("USER_CLASS").madero;
                    int Placa = reader.GetInt32(reader.GetOrdinal("Placa"));
                    int Rank = player.GetData<Data.Entities.User>("USER_CLASS").rank;
                    int puedeContratar = reader.GetInt32(reader.GetOrdinal("PuedeContratar"));
                        if (puedeContratar == 1) madero.PuedeContratar = true;
                    int puedeTocar = reader.GetInt32(reader.GetOrdinal("PuedeTocar"));
                    int SWAT = reader.GetInt32(reader.GetOrdinal("SWAT"));
                        if (SWAT == 1) madero.PuedeContratar = true;
                    string taquilla = reader.GetString(reader.GetOrdinal("Taquilla"));

                    madero.Placa = Placa;
                    madero.Rango = Rank;
                    madero.PuedeTocar = puedeTocar;

                    
                }

            }*/
        }
    }

}

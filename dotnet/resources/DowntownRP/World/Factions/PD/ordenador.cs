using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using DowntownRP.Utilities;

namespace DowntownRP.World.Factions.PD
{
    public class ordenador : Script
    {

        [Command ("pdpersona")]
        public async Task CMD_PdPersona (Player player, int idCard)
        {
            if (idCard == 0) return;
            Data.Entities.User user = player.getClass();
            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    Data.Entities.FichaPolicial ficha = await GetFicha(idCard);
                    if(ficha.Nombre == "-1")
                    {
                        Notifications.SendNotificationERROR(player, "No existe nadie con este ID.");
                        return;
                    }

                    int multasPorPagar = 0;
                    int pmultasPorPagar = 0;
                    foreach(Data.Entities.FineLSPD multa in ficha.multas)
                    {
                        if (!multa.isPaid)
                        {
                            multasPorPagar++;
                            pmultasPorPagar = pmultasPorPagar + multa.price;
                        }
                    }

                    player.SendChatMessage($"-----{ficha.idCard}-------------");
                    
                    player.SendChatMessage("~b~Nombre: ~w~"+ficha.Nombre);
                    player.SendChatMessage("~b~Vehículos: ");
                    foreach(string veh in ficha.Vehículos) 
                    {
                        player.SendChatMessage("     "+veh);
                    }
                    player.SendChatMessage("~b~Propiedades: ");
                    foreach (int prop in ficha.propiedades)
                    {
                        player.SendChatMessage("     " + prop);
                    }
                    player.SendChatMessage($"~b~ Multas por pagar: ~w~ {multasPorPagar}({pmultasPorPagar}$) ~b~ de un total de ~w~ {ficha.multas.Count} ");
                    player.SendChatMessage($"-------------------------------");
                }
                else Notifications.SendNotificationERROR(player, "No estás de servicio");
            }
            else Notifications.SendNotificationERROR(player, "No formas parte del departamento de Policia");
        }

        public async Task<Data.Entities.FichaPolicial> GetFicha (int IdCard)
        {
            Data.Entities.FichaPolicial ficha = new Data.Entities.FichaPolicial();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characters WHERE dni = @id";
                command.Parameters.AddWithValue("@id", IdCard);

                var items = new List<dynamic>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            int phone = reader.GetInt32(reader.GetOrdinal("Phone"));
                            string Nombre = reader.GetString(reader.GetOrdinal("Name"));

                            ficha.AdminIDpj = id;
                            ficha.idCard = IdCard;
                            ficha.Nombre = Nombre;
                            ficha.phone = phone;
                            ficha.Vehículos = await GetVehs(id);
                            ficha.multas = await Game.CharacterAlpha.CharacterAlpha.CargarMultas(ficha.AdminIDpj);
                            ficha.propiedades = await GetProps(ficha.AdminIDpj);

                        }
                    }
                    else ficha.Nombre = "-1";
                }

                connection.Close();
            }
        

            return ficha;
        }

        public async Task<List<string>> GetVehs(int id)
        {
            List<string> ficha = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM vehicles_characters WHERE owner = @id";
                command.Parameters.AddWithValue("@id", id);

                var items = new List<dynamic>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ficha.Add(reader.GetString(reader.GetOrdinal("numberplate")));
                        }
                    }
                    
                }

                connection.Close();
            }


            return ficha;
        }

        public async Task<List<int>> GetProps(int id)
        {
            List<int> propiedades = new List<int>();

            foreach (Data.Entities.House casa in Data.Lists.houses)
            {
                if(casa.owner == id)
                {
                    propiedades.Add(casa.id);
                }
            }

            return propiedades;
        }
    }
}

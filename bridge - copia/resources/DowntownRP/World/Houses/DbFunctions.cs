using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Houses
{
    public class DbFunctions
    {
        public static async void SpawnHouses()
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * from houses";

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Data.Entities.House house = new Data.Entities.House()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            owner = reader.GetInt32(reader.GetOrdinal("owner")),
                            type = reader.GetInt32(reader.GetOrdinal("type")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            number = reader.GetInt32(reader.GetOrdinal("number")),
                            area = reader.GetString(reader.GetOrdinal("area")),
                            safeBox = reader.GetInt32(reader.GetOrdinal("safebox")),
                            position = new Vector3(reader.GetDouble(reader.GetOrdinal("x")), reader.GetDouble(reader.GetOrdinal("y")), reader.GetDouble(reader.GetOrdinal("z")))
                        };

                        NAPI.Task.Run(async () =>
                        {
                            ColShape shape = NAPI.ColShape.CreateCylinderColShape(house.position.Subtract(new Vector3(0, 0, 1)), 2, 2);
                            TextLabel label;
                            //Marker marker = NAPI.Marker.CreateMarker(0, house.position.Subtract(new Vector3(0, 0, 0.1)), new Vector3(), new Vector3(), 1, new Color(251, 244, 1));
                            if(house.owner == 0)
                            {
                               label = NAPI.TextLabel.CreateTextLabel($"~b~Propiedad en venta~n~~w~Precio: ~g~${house.price}~w~~n~Pulsa ~b~F5 ~w~para interactuar~n~~p~{house.area}, {house.number}", house.position, 3, 1, 0, new Color(255, 255, 255));
                               Blip blip = NAPI.Blip.CreateBlip(374, house.position, 1, 2, "Propiedad en venta", 255, 0, true);
                               house.blip = blip;
                            }
                            else
                            {
                                if(house.isOpen) label = NAPI.TextLabel.CreateTextLabel($"~b~Propiedad~n~Pulsa ~b~F5 ~w~para timbrar~n~~p~{house.area}", house.position, 3, 1, 0, new Color(255, 255, 255));
                                else label = NAPI.TextLabel.CreateTextLabel($"~b~Propiedad~n~~r~Cerrado~n~~p~{house.area}, {house.number}", house.position, 3, 1, 0, new Color(255, 255, 255));
                            }

                            shape.SetData("HOUSE_CLASS", house);
                            house.shape = shape;
                            house.label = label;
                            //house.marker = marker;
                        });

                        Data.Lists.houses.Add(house);
                        Data.Info.housesSpawned++;
                    }
                }


            }
        }

        public async static Task<int> CreateHouse(Client player, int type, int price, string area, int number)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO houses (type, price, x, y, z, area, number) VALUES (@type, @price, @x, @y, @z, @area, @number)";
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@x", player.Position.X);
                    command.Parameters.AddWithValue("@y", player.Position.Y);
                    command.Parameters.AddWithValue("@z", player.Position.Z);
                    command.Parameters.AddWithValue("@area", area);
                    command.Parameters.AddWithValue("@number", number);

                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return (int)command.LastInsertedId;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public async static Task DeleteHouse(int id)
        {

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM houses WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task<int> GetLastStreetNumberFromHouse(string streetname)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM houses WHERE area = @streetname ORDER BY number DESC LIMIT 1";
                command.Parameters.AddWithValue("@streetname", streetname);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("number"));
                        connection.Close();
                        return id;
                    }

                }
            }
            return 0;
        }

        public async static Task UpdateHouseOwner(int id, int owner)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE houses SET owner = @owner WHERE id = @id";
                command.Parameters.AddWithValue("@owner", owner);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}

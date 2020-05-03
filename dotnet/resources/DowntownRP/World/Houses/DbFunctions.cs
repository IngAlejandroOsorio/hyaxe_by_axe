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
                               //Blip blip = NAPI.Blip.CreateBlip(374, house.position, 1, 2, "Propiedad en venta", 255, 0, true);
                               //house.blip = blip;
                            }
                            else
                            {
                                if(house.isOpen) label = NAPI.TextLabel.CreateTextLabel($"~b~Propiedad~n~Pulsa ~b~F5 ~w~para timbrar~n~~p~{house.area}", house.position, 3, 1, 0, new Color(255, 255, 255));
                                else label = NAPI.TextLabel.CreateTextLabel($"~b~Propiedad~n~~r~Cerrado~n~~p~{house.area}, {house.number}", house.position, 3, 1, 0, new Color(255, 255, 255));
                            }

                            if(house.type == 1)
                            {
                                Vector3 vectorCasaType1 = new Vector3(266.1425, -1006.98, -100.8834);
                                TextLabel text = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~F5 ~w~para salir", vectorCasaType1, 2, 1, 4, new Color());
                                house.labelExit = text;
                            }


                            shape.SetData("HOUSE_CLASS", house);
                            house.shape = shape;
                            house.label = label;
                            //house.marker = marker;
                        });
                        house.inventory = await SpawnHouseInventory(house.id);
                        Data.Lists.houses.Add(house);
                        Data.Info.housesSpawned++;
                    }
                }


            }
        }

        public async static Task<int> CreateHouse(Player player, int type, int price, string area, int number)
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

        public async static Task UpdateHouseSafebox(int idhouse, int bank)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE houses SET safeBox = @bank WHERE id = @id";
                command.Parameters.AddWithValue("@id", idhouse);
                command.Parameters.AddWithValue("@bank", bank);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task<Data.Entities.HouseInventory> SpawnHouseInventory(int houseid)
        {
            Data.Entities.HouseInventory inventory = new Data.Entities.HouseInventory();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items WHERE houseid = @houseid";
                command.Parameters.AddWithValue("@houseid", houseid);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        int type = reader.GetInt32(reader.GetOrdinal("type"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                        int slot = reader.GetInt32(reader.GetOrdinal("slot"));
                        int ammo = reader.GetInt32(reader.GetOrdinal("ammo"));

                        Data.Entities.Item item = new Data.Entities.Item(id, name, type, quantity);
                        item.bullets = ammo;

                        switch (slot)
                        {
                            case 1:
                                inventory.slot1 = item;
                                inventory.slot1.slot = 1;
                                break;

                            case 2:
                                inventory.slot2 = item;
                                inventory.slot2.slot = 2;
                                break;

                            case 3:
                                inventory.slot3 = item;
                                inventory.slot3.slot = 3;
                                break;

                            case 4:
                                inventory.slot4 = item;
                                inventory.slot4.slot = 4;
                                break;

                            case 5:
                                inventory.slot5 = item;
                                inventory.slot5.slot = 5;
                                break;

                            case 6:
                                inventory.slot6 = item;
                                inventory.slot6.slot = 6;
                                break;

                            case 7:
                                inventory.slot7 = item;
                                inventory.slot7.slot = 7;
                                break;

                            case 8:
                                inventory.slot8 = item;
                                inventory.slot8.slot = 8;
                                break;

                            case 9:
                                inventory.slot9 = item;
                                inventory.slot9.slot = 9;
                                break;

                            case 10:
                                inventory.slot10 = item;
                                inventory.slot10.slot = 10;
                                break;

                            case 11:
                                inventory.slot11 = item;
                                inventory.slot11.slot = 11;
                                break;

                            case 12:
                                inventory.slot12 = item;
                                inventory.slot12.slot = 12;
                                break;

                            case 13:
                                inventory.slot13 = item;
                                inventory.slot13.slot = 13;
                                break;

                            case 14:
                                inventory.slot14 = item;
                                inventory.slot14.slot = 14;
                                break;

                            case 15:
                                inventory.slot15 = item;
                                inventory.slot15.slot = 15;
                                break;

                            case 16:
                                inventory.slot16 = item;
                                inventory.slot16.slot = 16;
                                break;
                        }
                    }
                }
            }

            return inventory;
        }

        public async static Task UpdateUserOrHouseOwnerItem(int type, int iditem, int ownerid, int houseid)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                if (type == 1) command.CommandText = "UPDATE items SET userid = @ownerid, houseid = @houseid WHERE id = @iditem";
                else command.CommandText = "UPDATE items SET userid = @ownerid, factionid = @houseid WHERE id = @iditem";
                command.Parameters.AddWithValue("@iditem", iditem);
                command.Parameters.AddWithValue("@ownerid", ownerid);
                command.Parameters.AddWithValue("@houseid", houseid);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}

using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Vehicles
{
    public class DbHandler : Script
    {
        public async static Task SpawnCharacterVehicles(Data.Entities.User user)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM vehicles_characters WHERE owner = @user";
                command.Parameters.AddWithValue("@user", user.idpj);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string type = reader.GetString(reader.GetOrdinal("model"));
                        int color1 = reader.GetInt32(reader.GetOrdinal("color1"));
                        int color2 = reader.GetInt32(reader.GetOrdinal("color2"));
                        string numberplate = reader.GetString(reader.GetOrdinal("numberplate"));

                        double x = reader.GetDouble(reader.GetOrdinal("x"));
                        double y = reader.GetDouble(reader.GetOrdinal("y"));
                        double z = reader.GetDouble(reader.GetOrdinal("z"));
                        double rot = reader.GetDouble(reader.GetOrdinal("rot"));

                        Vector3 position = new Vector3(x, y, z);

                        NAPI.Task.Run(async () =>
                        {
                            uint hash = NAPI.Util.GetHashKey(type);
                            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(hash, position, (float)rot, color1, color2, numberplate, 255, false, false);
                            vehicle.NumberPlate = numberplate;

                            Data.Entities.VehicleCharacter veh = new Data.Entities.VehicleCharacter()
                            {
                                id = id,
                                entity = vehicle,
                                owner = user
                            };

                            veh.trunk = await SpawnVehicleTrunk(id);

                            user.vehicles.Add(veh);
                            vehicle.SetData("VEHICLE_CHARACTER_DATA", veh);
                        });
                    }
                }

            }
        }

        public async static Task<int> CreateVehicleCharacter(int owner, string model, int price, int color1, int color2, string numberplate, double x, double y, double z, double rot)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO vehicles_characters (owner, model, price, color1, color2, numberplate, x, y, z, rot) VALUES (@owner, @model, @price, @color1, @color2, @numberplate, @x, @y, @z, @rot)";
                command.Parameters.AddWithValue("@owner", owner);
                command.Parameters.AddWithValue("@model", model);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@color1", color1);
                command.Parameters.AddWithValue("@color2", color2);
                command.Parameters.AddWithValue("@numberplate", numberplate);
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@z", z);
                command.Parameters.AddWithValue("@rot", rot);


                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return (int)command.LastInsertedId;
            }
        }

        public async static Task UpdateVehicleCharacterPosition(Data.Entities.VehicleCharacter vehicle)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE vehicles_characters SET x = @x, y = @y, z = @z, rot = @rot WHERE id = @id";
                command.Parameters.AddWithValue("@id", vehicle.id);
                command.Parameters.AddWithValue("@x", vehicle.entity.Position.X);
                command.Parameters.AddWithValue("@y", vehicle.entity.Position.Y);
                command.Parameters.AddWithValue("@z", vehicle.entity.Position.Z);
                command.Parameters.AddWithValue("@rot", (double)vehicle.entity.Heading);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task<Data.Entities.Inventory> SpawnVehicleTrunk(int vehicleid)
        {
            Data.Entities.Inventory inventory = new Data.Entities.Inventory();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items WHERE vehicleid = @vehicleid";
                command.Parameters.AddWithValue("@vehicleid", vehicleid);

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
                        }
                    }
                }
            }

            return inventory;
        }

        public async static Task UpdateUserOrVehicleOwnerItem(int type, int iditem, int ownerid, int vehicleid)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                if(type == 2) command.CommandText = "UPDATE items SET userid = @ownerid, facvehid = @vehicleid WHERE id = @iditem";
                else command.CommandText = "UPDATE items SET userid = @ownerid, vehicleid = @vehicleid WHERE id = @iditem";
                command.Parameters.AddWithValue("@iditem", iditem);
                command.Parameters.AddWithValue("@ownerid", ownerid);
                command.Parameters.AddWithValue("@vehicleid", vehicleid);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}

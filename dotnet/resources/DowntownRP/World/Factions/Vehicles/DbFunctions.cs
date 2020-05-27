using BCrypt;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.Vehicles
{
    public class DbFunctions : Script
    {
        public async static Task<int> CreateFactionVehicle(int faction, string model, int color1, int color2, string numberplate, double x, double y, double z, double rot, uint dim)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO faction_vehicles (faction, vehicle, color1, color2, plate, x, y, z, rot, dimension) VALUES (@faction, @model, @color1, @color2, @numberplate, @x, @y, @z, @rot, @dim)";
                command.Parameters.AddWithValue("@faction", faction);
                command.Parameters.AddWithValue("@model", model);
                command.Parameters.AddWithValue("@color1", color1);
                command.Parameters.AddWithValue("@color2", color2);
                command.Parameters.AddWithValue("@numberplate", numberplate);
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@z", z);
                command.Parameters.AddWithValue("@rot", rot);
                command.Parameters.AddWithValue("@dim", dim);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return (int)command.LastInsertedId;
            }
        }

        public static async void  SpawnFactionVehicles()
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM faction_vehicles";

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        int faction = reader.GetInt32(reader.GetOrdinal("faction"));
                        string vehicle = reader.GetString(reader.GetOrdinal("vehicle"));
                        int color1 = reader.GetInt32(reader.GetOrdinal("color1"));
                        int color2 = reader.GetInt32(reader.GetOrdinal("color1"));
                        
                        string numberplate = reader.GetString(reader.GetOrdinal("plate"));

                        double x = reader.GetDouble(reader.GetOrdinal("x"));
                        double y = reader.GetDouble(reader.GetOrdinal("y"));
                        double z = reader.GetDouble(reader.GetOrdinal("z"));
                        double rot = reader.GetDouble(reader.GetOrdinal("rot"));
                        uint dimension = (uint) reader.GetInt64(reader.GetOrdinal("dimension"));

                        Vector3 position = new Vector3(x, y, z);

                        uint model = NAPI.Util.GetHashKey(vehicle);
                        Data.Entities.VehicleFaction vehdata;
                        NAPI.Task.Run(async () =>
                        {
                            Vehicle veh = NAPI.Vehicle.CreateVehicle(model, position, (float)rot, color1, color2, numberplate, dimension: dimension);

                            vehdata = new Data.Entities.VehicleFaction()
                            {
                                id = id,
                                entity = veh,
                                model = vehicle,
                                faction = faction
                            };

                            vehdata.trucker = await SpawnVehicleFactionTrunk(id);

                            Data.Lists.factions.Find(t => t.id == faction).vehicles.Add(vehdata);

                            veh.SetData("VEHICLE_FACTION_DATA", vehdata);
                            Data.Info.vehiclesFactionSpawned++;
                        });
                    }
                }


            }
        }

        public async static Task<Data.Entities.Inventory> SpawnVehicleFactionTrunk(int vehicleid)
        {
            Data.Entities.Inventory inventory = new Data.Entities.Inventory();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items WHERE facvehid = @vehicleid";
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

        public async static Task UpdateFVehPosAndDim(int id, Vector3 pos, uint dim)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE faction_vehicles SET (x, y, z, dim) VALUES (@x, @y, @z, @dim) WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@x", pos.X);
                command.Parameters.AddWithValue("@y", pos.Y);
                command.Parameters.AddWithValue("@z", pos.Z);
                command.Parameters.AddWithValue("@dim", dim);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
        //Data.Entities.VehicleFaction v = player.Vehicle.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");
        public async static Task UpdateFVehPosAndDim(Vehicle v)
        {
            if (!v.HasData("VEHICLE_FACTION_DATA")) return;
            Data.Entities.VehicleFaction vf = v.GetData<Data.Entities.VehicleFaction>("VEHICLE_FACTION_DATA");
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE faction_vehicles SET (x, y, z, dim) VALUES (@x, @y, @z, @dim) WHERE id = @id";
                command.Parameters.AddWithValue("@id", vf.id);
                command.Parameters.AddWithValue("@x", v.Position.X);
                command.Parameters.AddWithValue("@y", v.Position.Y);
                command.Parameters.AddWithValue("@z", v.Position.Z);
                command.Parameters.AddWithValue("@dim", v.Dimension);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }


        public async static Task UpdateFVehPriColor(int id, int color)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE faction_vehicles SET color1 = @c1 WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@c1", color);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateFVehSecColor(int id, int color)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE faction_vehicles SET color2 = @c1 WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@c1", color);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}

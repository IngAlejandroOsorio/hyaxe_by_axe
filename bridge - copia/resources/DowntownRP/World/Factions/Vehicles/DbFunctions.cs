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
        public async static Task<int> CreateFactionVehicle(int faction, string model, int color1, int color2, string numberplate, double x, double y, double z, double rot)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO faction_vehicles (faction, vehicle, color1, color2, plate, x, y, z, rot) VALUES (@faction, @model, @color1, @color2, @numberplate, @x, @y, @z, @rot)";
                command.Parameters.AddWithValue("@faction", faction);
                command.Parameters.AddWithValue("@model", model);
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

        public static async void SpawnFactionVehicles()
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

                        Vector3 position = new Vector3(x, y, z);

                        uint model = NAPI.Util.GetHashKey(vehicle);

                        NAPI.Task.Run(async () =>
                        {
                            Vehicle veh = NAPI.Vehicle.CreateVehicle(model, position, (float)rot, color1, color2, numberplate);

                            Data.Entities.VehicleFaction vehdata = new Data.Entities.VehicleFaction()
                            {
                                id = id,
                                entity = veh,
                                model = vehicle,
                                faction = faction
                            };

                            veh.SetData("VEHICLE_FACTION_DATA", vehdata);
                            Data.Info.vehiclesFactionSpawned++;
                        });
                    }
                }


            }
        }
    }
}

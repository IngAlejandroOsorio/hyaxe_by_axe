using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Drugs.Weed
{
    public class DbFunctions : Script
    {
        public static async void SpawnWeed()
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM plants_weed";

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string type = reader.GetString(reader.GetOrdinal("type"));
                        int status = reader.GetInt32(reader.GetOrdinal("status"));

                        double x = reader.GetDouble(reader.GetOrdinal("x"));
                        double y = reader.GetDouble(reader.GetOrdinal("y"));
                        double z = reader.GetDouble(reader.GetOrdinal("z"));

                        int dimension = reader.GetInt32(reader.GetOrdinal("dimension"));

                        Vector3 position = new Vector3(x, y, z);

                        NAPI.Task.Run(async () =>
                        {
                            Data.Entities.Weed weed = new Data.Entities.Weed()
                            {
                                id = id,
                                type = type,
                                status = status,
                                position = position,
                                dimension = dimension
                            };

                            Data.Lists.weedPlants.Add(weed);

                            GTANetworkAPI.Object weedObject;
                            TextLabel label;

                            switch (status)
                            {
                                case 1:
                                    weedObject = NAPI.Object.CreateObject(1595624552, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255), false, (uint)dimension);
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 2:
                                    weedObject = NAPI.Object.CreateObject(1595624552, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~g~sana", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 3:
                                    weedObject = NAPI.Object.CreateObject(1595624552, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~r~necesita riego", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 4:
                                    weedObject = NAPI.Object.CreateObject(1595624552, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~crecimiento~n~~w~Estado: ~g~sana", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 5:
                                    weedObject = NAPI.Object.CreateObject(3989082015, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~r~necesita riego", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 6:
                                    weedObject = NAPI.Object.CreateObject(3989082015, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~g~sana", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 7:
                                    weedObject = NAPI.Object.CreateObject(3989082015, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~r~necesita riego", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 8:
                                    weedObject = NAPI.Object.CreateObject(3989082015, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~floración~n~~w~Estado: ~g~sana", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;

                                case 9:
                                    weedObject = NAPI.Object.CreateObject(452618762, position.Subtract(new Vector3(0, 0, 1)), new Vector3());
                                    label = NAPI.TextLabel.CreateTextLabel($"~g~{type}~n~~w~Fase: ~b~lista para cortar~n~~w~Pulsa ~b~K~w~ para cortar", position.Subtract(new Vector3(0, 0, 0.4)), 3, 1, 0, new Color(255, 255, 255));
                                    weedObject.Dimension = (uint)dimension;
                                    label.Dimension = (uint)dimension;
                                    weed.prop = weedObject;
                                    weed.label = label;
                                    break;
                            }
                            ColShape shape = NAPI.ColShape.CreateCylinderColShape(position, 2, 2);
                            shape.SetData<Data.Entities.Weed>("WEED_CLASS", weed);
                        });
                    }
                }


            }
        }

        public async static Task<int> CreateWeedDb(Player player, string type)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO plants_weed (type, x, y, z, dimension) VALUES (@type, @x, @y, @z, @dimension)";
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@x", player.Position.X);
                command.Parameters.AddWithValue("@y", player.Position.Y);
                command.Parameters.AddWithValue("@z", player.Position.Z);
                command.Parameters.AddWithValue("@dimension", (int)player.Dimension);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return (int)command.LastInsertedId;
            }
        }
    }
}

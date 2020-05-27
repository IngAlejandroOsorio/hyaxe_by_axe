using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions
{
    public class DbFunctions : Script
    {
        public async static void SpawnFactions()
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM factions";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            if (id < 10) continue;
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            int type = reader.GetInt32(reader.GetOrdinal("type"));
                            int owner = reader.GetInt32(reader.GetOrdinal("owner"));
                            int safeBox = reader.GetInt32(reader.GetOrdinal("safeBox"));
                            string rank1 = reader.GetString(reader.GetOrdinal("rank1"));
                            string rank2 = reader.GetString(reader.GetOrdinal("rank2"));
                            string rank3 = reader.GetString(reader.GetOrdinal("rank3"));
                            string rank4 = reader.GetString(reader.GetOrdinal("rank4"));
                            string rank5 = reader.GetString(reader.GetOrdinal("rank5"));
                            string rank6 = reader.GetString(reader.GetOrdinal("rank6"));


                            double x = reader.GetDouble(reader.GetOrdinal("x"));
                            double y = reader.GetDouble(reader.GetOrdinal("y"));
                            double z = reader.GetDouble(reader.GetOrdinal("z"));

                            int armasCortas = reader.GetInt32(reader.GetOrdinal("traficaArmasCortas"));
                            bool Ac = false;
                            if (armasCortas == 1) Ac = true;

                            int armasLargas = reader.GetInt32(reader.GetOrdinal("traficaArmasLargas"));
                            bool Al = false;
                            if (armasLargas == 1) Al = true;

                            DateTime cooldown = reader.GetDateTime(reader.GetOrdinal("cooldown"));

                            Vector3 position = new Vector3(x, y, z);
                            Vector3 exit = new Vector3(266.1425, -1006.98, -100.8834);

                            NAPI.Task.Run(async () =>
                            {
                                ColShape entrada = NAPI.ColShape.CreateCylinderColShape(position, 2, 2);
                                TextLabel label = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~F5 ~w~para entrar", position, 2, 1, 0, new Color(255, 255, 255));

                                ColShape salida = NAPI.ColShape.CreateCylinderColShape(exit, 2, 2);
                                salida.Dimension = (uint)id;

                                Data.Entities.Faction faction = new Data.Entities.Faction()
                                {
                                    id = id,
                                    name = name,
                                    safeBox = safeBox,
                                    type = type,
                                    owner = owner,
                                    rank1 = rank1,
                                    rank2 = rank2,
                                    rank3 = rank3,
                                    rank4 = rank4,
                                    rank5 = rank5,
                                    rank6 = rank6,
                                    position = new Vector3(x, y, z),
                                    armasCortas = Ac,
                                    armasLargas = Al,
                                    coolDown = cooldown
                                };

                                entrada.SetData("FACTION_CLASS", faction);
                                salida.SetData("SALIDA_FACTION", position);

                                //faction.inventory = await SpawnFactionInventory(faction.id);

                                Data.Lists.factions.Add(faction);
                            });
                        }
                    }
                }
            }
        }

        public async static Task<int> CreateFaction(string name, int type, int owner, double x, double y, double z)
        {
            int aL = 0;
            if (type == 2)
            {
                aL = 1;
            }
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO factions (name, type, owner, x, y, z, traficaArmasCortas, traficaArmasLargas) VALUES (@name, @type, @owner, @x, @y, @z, 1, @aL)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@owner", owner);
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@z", z);
                command.Parameters.AddWithValue("@aL", aL);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return (int)command.LastInsertedId;
            }
        }

        public async static Task UpdateIlegalFactionRanks(Player player, int idfacc, string rank1, string rank2, string rank3, string rank4, string rank5, string rank6)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET rank1 = @rank1, rank2 = @rank2, rank3 = @rank3, rank4 = @rank4, rank5 = @rank5, rank6 = @rank6 WHERE id = @idfacc";
                command.Parameters.AddWithValue("@idfacc", idfacc);
                command.Parameters.AddWithValue("@rank1", rank1);
                command.Parameters.AddWithValue("@rank2", rank2);
                command.Parameters.AddWithValue("@rank3", rank3);
                command.Parameters.AddWithValue("@rank4", rank4);
                command.Parameters.AddWithValue("@rank5", rank5);
                command.Parameters.AddWithValue("@rank6", rank6);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateFactionSafebox(int idhouse, int bank)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE factions SET safeBox = @bank WHERE id = @id";
                command.Parameters.AddWithValue("@id", idhouse);
                command.Parameters.AddWithValue("@bank", bank);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateFactionTrafico(int faccion, int aC, int aL)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE factions SET traficaArmasCortas = @aC, traficaArmasLargas = @aL WHERE id = @id";
                command.Parameters.AddWithValue("@aC", aC);
                command.Parameters.AddWithValue("@aL", aL);
                command.Parameters.AddWithValue("@id", faccion);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateFactionCooldown(int faccion, DateTime hasta)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE factions SET cooldown = @cooldown WHERE id = @id";
                command.Parameters.AddWithValue("@cooldown", hasta);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task<Data.Entities.HouseInventory> SpawnFactionInventory(int factionid)
        {
            Data.Entities.HouseInventory inventory = new Data.Entities.HouseInventory();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items WHERE factionid = @factionid";
                command.Parameters.AddWithValue("@factionid", factionid);

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
    }
}

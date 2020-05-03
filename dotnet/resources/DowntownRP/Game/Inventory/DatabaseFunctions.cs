using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using DowntownRP.Data.Entities;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.Game.Inventory
{
    public class DatabaseFunctions : Script
    {
        public async static Task<Data.Entities.Item> SpawnCharacterItem(int itemid)
        {
            Data.Entities.Item item = new Data.Entities.Item(0, "NO", 0, 0);

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items WHERE id = @itemid";
                command.Parameters.AddWithValue("@itemid", itemid);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        item.id = reader.GetInt32(reader.GetOrdinal("id"));
                        item.name = reader.GetString(reader.GetOrdinal("name"));
                        item.quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                        item.type = reader.GetInt32(reader.GetOrdinal("type"));
                    }
                }
            }

            return item;
        }

        public async static Task<Data.Entities.Inventory> SpawnInventoryItems(int idpj)
        {
            Data.Entities.Inventory inventory = new Data.Entities.Inventory();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM items WHERE userid = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);

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

        public async static Task<int> CreateItemDatabase(int userid, string name, int type, int quantity, int slot)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO items (userid, name, type, quantity, slot) VALUES (@userid, @name, @type, @quantity, @slot)";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@slot", slot);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                return (int)command.LastInsertedId;
            }
        }

        public async static Task UpdateItemDatabase(int id, int userid, string name, int type, int quantity, int slot)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE items SET userid = @userid, name = @name, type = @type, quantity = @quantity, slot = @slot WHERE id = @id";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@slot", slot);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateItemQuantityDatabase(int id, int quantity)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE items SET quantity = @quantity WHERE id = @id";
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateItemAmmo(int id, int ammo)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE items SET ammo = @ammo WHERE id = @id";
                command.Parameters.AddWithValue("@ammo", ammo);
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task RemoveItemDatabase(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM items WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task<bool> SetNewItemInventory(Player player, Item item, bool isAWeapon = false, int bullets = 0)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return false;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.inventory.slot1.name == "NO")
            {
                user.inventory.slot1 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 1);
                user.inventory.slot1.id = itemid;
                user.inventory.slot1.slot = 1;

                if (isAWeapon)
                {
                    user.inventory.slot1.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot2.name == "NO")
            {
                user.inventory.slot2 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 2);
                user.inventory.slot2.id = itemid;
                user.inventory.slot2.slot = 2;

                if (isAWeapon)
                {
                    user.inventory.slot2.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot3.name == "NO")
            {
                user.inventory.slot3 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 3);
                user.inventory.slot3.id = itemid;
                user.inventory.slot3.slot = 3;

                if (isAWeapon)
                {
                    user.inventory.slot3.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot4.name == "NO")
            {
                user.inventory.slot4 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 4);
                user.inventory.slot4.id = itemid;
                user.inventory.slot4.slot = 4;

                if (isAWeapon)
                {
                    user.inventory.slot4.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot5.name == "NO")
            {
                user.inventory.slot5 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 5);
                user.inventory.slot5.id = itemid;
                user.inventory.slot5.slot = 5;

                if (isAWeapon)
                {
                    user.inventory.slot5.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot6.name == "NO")
            {
                user.inventory.slot6 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 6);
                user.inventory.slot6.id = itemid;
                user.inventory.slot6.slot = 6;

                if (isAWeapon)
                {
                    user.inventory.slot6.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot7.name == "NO")
            {
                user.inventory.slot7 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 7);
                user.inventory.slot7.id = itemid;
                user.inventory.slot7.slot = 7;

                if (isAWeapon)
                {
                    user.inventory.slot7.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot8.name == "NO")
            {
                user.inventory.slot8 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 8);
                user.inventory.slot8.id = itemid;
                user.inventory.slot8.slot = 8;

                if (isAWeapon)
                {
                    user.inventory.slot8.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot9.name == "NO")
            {
                user.inventory.slot9 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 9);
                user.inventory.slot9.id = itemid;
                user.inventory.slot9.slot = 9;

                if (isAWeapon)
                {
                    user.inventory.slot9.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot10.name == "NO")
            {
                user.inventory.slot10 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 10);
                user.inventory.slot10.id = itemid;
                user.inventory.slot10.slot = 10;

                if (isAWeapon)
                {
                    user.inventory.slot10.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot11.name == "NO")
            {
                user.inventory.slot11 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 11);
                user.inventory.slot11.id = itemid;
                user.inventory.slot11.slot = 11;

                if (isAWeapon)
                {
                    user.inventory.slot11.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            if (user.inventory.slot12.name == "NO")
            {
                user.inventory.slot12 = item;
                int itemid = await CreateItemDatabase(user.idpj, item.name, item.type, item.quantity, 12);
                user.inventory.slot12.id = itemid;
                user.inventory.slot12.slot = 12;

                if (isAWeapon)
                {
                    user.inventory.slot12.bullets = bullets;
                    UpdateItemAmmo(itemid, bullets);
                }
                return true;
            }

            return false; // Slots llenos
        }

       /* public async static Task<bool> SetItemInventory(Player player, Item item)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return false;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.inventory.slot1.name == "NO")
            {
                user.inventory.slot1 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 1);
                return true;
            }

            if (user.inventory.slot2.name == "NO")
            {
                user.inventory.slot2 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 2);
                return true;
            }

            if (user.inventory.slot3.name == "NO")
            {
                user.inventory.slot3 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 3);
                return true;
            }

            if (user.inventory.slot4.name == "NO")
            {
                user.inventory.slot4 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 4);
                return true;
            }

            if (user.inventory.slot5.name == "NO")
            {
                user.inventory.slot5 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 5);
                return true;
            }

            if (user.inventory.slot6.name == "NO")
            {
                user.inventory.slot6 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 6);
                return true;
            }

            if (user.inventory.slot7.name == "NO")
            {
                user.inventory.slot7 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 7);
                return true;
            }

            if (user.inventory.slot8.name == "NO")
            {
                user.inventory.slot8 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 8);
                return true;
            }

            if (user.inventory.slot9.name == "NO")
            {
                user.inventory.slot9 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 9);
                return true;
            }

            if (user.inventory.slot10.name == "NO")
            {
                user.inventory.slot10 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 10);
                return true;
            }

            if (user.inventory.slot11.name == "NO")
            {
                user.inventory.slot11 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 11);
                return true;
            }

            if (user.inventory.slot12.name == "NO")
            {
                user.inventory.slot12 = item;
                await UpdateItemDatabase(item.id, user.idpj, item.name, item.type, item.quantity, 12);
                return true;
            }

            return false;
        }*/

    }
}
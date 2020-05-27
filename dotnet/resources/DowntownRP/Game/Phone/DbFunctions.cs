using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.Phone
{
    public class DbFunctions : Script
    {
        public async Task AddPhoneBook(int idpj, string name, int phone)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO phone_book (owner, name, phone) VALUES (@owner, @name, @phone);";
                command.Parameters.AddWithValue("@owner", idpj);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@phone", phone);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public static Task<List<Data.Entities.PhoneContact>> SpawnPhoneBooks(int idpj)
        {
            List<Data.Entities.PhoneContact> phoneBook = new List<Data.Entities.PhoneContact>();

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM phone_book WHERE owner = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            phoneBook.Add(new Data.Entities.PhoneContact() { id = reader.GetInt32(reader.GetOrdinal("id")), name = reader.GetString(reader.GetOrdinal("name")), phone = reader.GetInt32(reader.GetOrdinal("phone")) });
                        }
                    }
                }
            }
            return Task.FromResult(phoneBook);
        }

        public async Task CreatePhoneMessage(int idpj, int tidpj, string message)
        {

        }
    }
}

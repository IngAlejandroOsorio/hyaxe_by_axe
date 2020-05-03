﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.Game.Character
{
    public class CharacterSelector : Script
    {
        [RemoteEvent("ShowCharacterInSelector")]
        public async Task RE_ShowCharacterInSelector(Player player, int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characters WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", id);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                    {
                        // CARGAR APARIENCIA
                    }
                }
            }
        }
    
    [RemoteEvent ("obtPjData")]
    public void obtPjData(Player pj)
        {
            Data.Entities.User user = pj.GetData<Data.Entities.User>("USER_CLASS");

            string JSON = "{\"dni\": \" " + user.dni + " \"}";

            pj.TriggerEvent("pjData", JSON);
        }
    
    
    }
}

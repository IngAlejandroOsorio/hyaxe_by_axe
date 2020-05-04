using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.World.Business.Clothes
{
    class CharacterData
    {

        public int torso { get; set; }

        public int topshirt { get; set; }

        public int topshirtTexture { get; set; }

        public int undershirt { get; set; }

        public int legs { get; set; }

        public int feet { get; set; }

    }

    public class Ropa : Script
    {

        [RemoteEvent("S_abrirTienda")]
        public static void S_abrirTienda(Player player, string tienda, string price, int o_id, string owner_name)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            //utilities.Notification.SendNotificationERROR(player, "Llamo evento tienda");
            try
            {
                if (!user.chatStatus)
                {
                    if (!player.HasData("TIENDAROPA_STATUS"))
                    {
                        player.SetData("TIENDAROPA_STATUS", true);
                        int? player_id = user.idpj;
                        if (player_id == 0) return;

                        using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                        {
                            //NAPI.Chat.SendChatMessageToPlayer(player, "<font color='yellow'>[PlayerID:]</font>: " + player_id);
                            connection.Open();
                            MySqlCommand command = connection.CreateCommand();
                            command.CommandText = "SELECT * FROM characters WHERE id = @id LIMIT 1";
                            command.Parameters.AddWithValue("@id", player_id);
                            var items = new List<dynamic>();
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    int genero = reader.GetInt32("gender");
                                    int torso = reader.GetInt32("torso");
                                    int topshirt = reader.GetInt32("topshirt");
                                    int topshirtTexture = reader.GetInt32("topshirtTexture");
                                    int undershirt = reader.GetInt32("undershirt");
                                    int legs = reader.GetInt32("legs");
                                    int feet = reader.GetInt32("feet");
                                    //NAPI.Chat.SendChatMessageToPlayer(player, "<font color='yellow'>[Log:]</font> Enviado a " + Convert.ToString(genero) + Convert.ToString(torso) + Convert.ToString(topshirt) + Convert.ToString(topshirtTexture) + Convert.ToString(undershirt) + Convert.ToString(legs) + Convert.ToString(feet)); beard beardColor makeupColor lipstick lipstickColor hairType hairColor hairHighlight 
                                    NAPI.ClientEvent.TriggerClientEvent(player, "AbrirTiendaRopa", Convert.ToString(genero), tienda, Convert.ToString(torso), Convert.ToString(topshirt), Convert.ToString(topshirtTexture), Convert.ToString(undershirt), Convert.ToString(legs), Convert.ToString(feet), price, Convert.ToString(o_id), owner_name);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }


        [RemoteEvent("cobroTienda")]
        public static async System.Threading.Tasks.Task cobroTiendaAsync(Player player)
        {
            if (await Game.Money.MoneyModel.SubMoney(player, 500))
            {
                Utilities.Notifications.SendNotificationINFO(player, "Gracias por tu compra vuelve pronto.");
            }
        }

        [RemoteEvent("cobroPeluqueria")]
        public static async System.Threading.Tasks.Task cobroPeluqueria(Player player)
        {
            if (await Game.Money.MoneyModel.SubMoney(player, 300))
            {
                Utilities.Notifications.SendNotificationINFO(player, "Gracias por tu compra vuelve pronto.");
            }
        }

        [Command("camx")]
        public void CMD_camx(Player player, string x, string y, string z, string rx, string ry, string rz)
        {
            Utilities.Notifications.SendNotificationERROR(player, "ajusto la camara en: " + x + " " + y + " " + z);
            NAPI.ClientEvent.TriggerClientEvent(player, "camx", x, y, z, rx, ry, rz);

        }

        [Command("camp")]
        public void CMD_camp(Player player, string x, string y, string z)
        {
            Utilities.Notifications.SendNotificationERROR(player, "ajusto la camara en: " + x + " " + y + " " + z);
            NAPI.ClientEvent.TriggerClientEvent(player, "camp", x, y, z);
        }


        [RemoteEvent("S_CerrarTiendaData")]
        public async static void S_CerrarTiendaData(Player player)
        {
            player.ResetData("TIENDAROPA_STATUS");
            NAPI.ClientEvent.TriggerClientEvent(player, "ApplyCharacterFeatures", 0);
        }

        [Command("camh")]
        public void CMD_camp(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "camh");

        }

        [Command("camb")]
        public void CMD_camb(Player player, string x, string y, string z, string bone)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "camb", x, y, z, bone);

        }

        [RemoteEvent("ComprarRopa")]
        public async void ComprarRopa(Player player, int pri, string arguments)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            int? player_id = user.idpj;
            if (player_id == 0) return;

            var characterData = NAPI.Util.FromJson<CharacterData>(arguments);

            try
            {
                using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"UPDATE characters SET torso = @torso, topshirt = @topshirt, topshirtTexture = @topshirtTexture, undershirt =  @undershirt, legs = @legs, feet = @feet WHERE id = @id";

                    command.Parameters.AddWithValue("@id", player_id);
                    command.Parameters.AddWithValue("@torso", characterData.torso);
                    command.Parameters.AddWithValue("@topshirt", characterData.topshirt);
                    command.Parameters.AddWithValue("@topshirtTexture", characterData.topshirtTexture);
                    command.Parameters.AddWithValue("@undershirt", characterData.undershirt);
                    command.Parameters.AddWithValue("@legs", characterData.legs);
                    command.Parameters.AddWithValue("@feet", characterData.feet);
                    /*command.Parameters.AddWithValue("@hairType", characterData.hairType);
                    command.Parameters.AddWithValue("@hairColor", characterData.hairColor);
                    command.Parameters.AddWithValue("@legs", characterData.legs);
                    command.Parameters.AddWithValue("@hairHighlight", characterData.hairHighlight);
                    command.Parameters.AddWithValue("@beard", characterData.beard);*/

                    command.ExecuteNonQuery();
                    connection.Close();
                    //NAPI.Chat.SendChatMessageToPlayer(player, "<font color='yellow'>[Feet:]</font>: " + characterData.feet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            player.ResetData("TIENDAROPA_STATUS");
        }


    }

}

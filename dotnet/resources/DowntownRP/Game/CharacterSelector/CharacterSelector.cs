using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.CharacterSelector
{
    public class CharacterSelector : Script
    {
        [RemoteEvent("RetrieveCharactersList")]
        public static void RetrieveCharactersList(Player player)
        {
            //int? player_id = player.GetExternalData<Data.Entities.User>(0).id;
            //if (player_id == null) return;
            if (!player.HasData("USER_CLASS")) return;

            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            int player_id = user.id;

            uint player__id = Convert.ToUInt32(player_id);

            player.Dimension = player__id + 1;

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characters WHERE user_id = @id";
                command.Parameters.AddWithValue("@id", player_id);

                var items = new List<dynamic>();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new Dictionary<string, object>(reader.FieldCount - 1);
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] = reader.GetValue(i);
                            }
                            items.Add(item);
                        }
                    }
                }

                player.TriggerEvent("UpdateCharactersList", JsonConvert.SerializeObject(items));
                connection.Close();
            }
        }

        [RemoteEvent("SelectCharacter")]
        public async Task SelectCharacter(Player player, int characterId) //iniciador principal del personaje.
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characters WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", characterId);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                    double x = reader.GetDouble(reader.GetOrdinal("x"));
                    double y = reader.GetDouble(reader.GetOrdinal("y"));
                    double z = reader.GetDouble(reader.GetOrdinal("z"));
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    string register_date = reader.GetString(reader.GetOrdinal("register_date"));
                    string last_login_date = reader.GetString(reader.GetOrdinal("last_login_date"));
                    double money = reader.GetDouble(reader.GetOrdinal("money"));
                    double bank = reader.GetDouble(reader.GetOrdinal("bank"));
                    int hycoin = reader.GetInt32(reader.GetOrdinal("hycoin"));
                    int faction = reader.GetInt32(reader.GetOrdinal("faction"));
                    int rank = reader.GetInt32(reader.GetOrdinal("rank"));
                    int job = reader.GetInt32(reader.GetOrdinal("job"));
                    int level = reader.GetInt32(reader.GetOrdinal("level"));
                    int exp = reader.GetInt32(reader.GetOrdinal("exp"));
                    int health = reader.GetInt32(reader.GetOrdinal("health"));
                    int armour = reader.GetInt32(reader.GetOrdinal("armour"));
                    double rotation = reader.GetDouble(reader.GetOrdinal("rotation"));
                    string dni = reader.GetString(reader.GetOrdinal("dni"));
                    int age = reader.GetInt32(reader.GetOrdinal("age"));
                    string height = reader.GetString(reader.GetOrdinal("height"));
                    int gender = reader.GetInt32(reader.GetOrdinal("gender"));
                    int voiceMode = reader.GetInt32(reader.GetOrdinal("voiceMode"));
                    int mpStatus = reader.GetInt32(reader.GetOrdinal("mpStatus"));
                    int bankAccount = reader.GetInt32(reader.GetOrdinal("bankAccount"));
                    string IBAN = reader.GetString(reader.GetOrdinal("IBAN"));
                    int seguroMedico = reader.GetInt32(reader.GetOrdinal("seguroMedico"));

                    // Character
                    int faceFirst = reader.GetInt32(reader.GetOrdinal("faceFirst"));
                    int faceSecond = reader.GetInt32(reader.GetOrdinal("faceSecond"));
                    int faceMix = reader.GetInt32(reader.GetOrdinal("faceMix"));
                    int skinFirst = reader.GetInt32(reader.GetOrdinal("skinFirst"));
                    int skinSecond = reader.GetInt32(reader.GetOrdinal("skinSecond"));
                    int skinMix = reader.GetInt32(reader.GetOrdinal("skinSecond"));
                    int eyeColor = reader.GetInt32(reader.GetOrdinal("eyeColor"));
                    int hairColor = reader.GetInt32(reader.GetOrdinal("hairColor"));
                    int hairHighlight = reader.GetInt32(reader.GetOrdinal("hairHighLight"));

                    /*int[] faceSettings = { faceFirst, faceSecond, faceMix, skinFirst, skinSecond, skinMix };
                    player.SetSharedData("FACE_SETTINGS", faceSettings);
                    NAPI.ClientEvent.TriggerClientEventForAll("SetFaceSettings", player.Value, player.GetSharedData("FACE_SETTINGS"));
                    player.TriggerEvent("SetFaceSettingsSpawn");*/

                    // Character slots
                    int slot1 = reader.GetInt32(reader.GetOrdinal("slot1"));
                    int slot2 = reader.GetInt32(reader.GetOrdinal("slot2"));
                    int slot3 = reader.GetInt32(reader.GetOrdinal("slot3"));
                    int slot4 = reader.GetInt32(reader.GetOrdinal("slot4"));
                    int slot5 = reader.GetInt32(reader.GetOrdinal("slot5"));
                    int slot6 = reader.GetInt32(reader.GetOrdinal("slot6"));

                    //Permisos
                    int CarnetCoche = reader.GetInt32(reader.GetOrdinal("CarnetCoche"));
                    int CarnetMoto = reader.GetInt32(reader.GetOrdinal("CarnetMoto"));
                    int CarnetCamion = reader.GetInt32(reader.GetOrdinal("CarnetCamion"));


                    player.Dimension = 0;

                    NAPI.Entity.SetEntityPosition(player, new Vector3(x, y, z));
                    NAPI.Entity.SetEntityRotation(player, new Vector3(0, 0, rotation));
                    NAPI.Player.SetPlayerHealth(player, health);
                    NAPI.Player.SetPlayerArmor(player, armour);
                    NAPI.Player.SetPlayerName(player, name);

                    //var user = player.GetExternalData<Data.Entities.User>(0);

                    if (!player.HasData("USER_CLASS")) return;
                    Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

                    user.idpj = characterId;
                    user.edad = age;
                    user.enableMicrophone = voiceMode;
                    user.mpStatus = mpStatus;
                    user.bankAccount = bankAccount;
                    user.IBAN = IBAN;
                    user.money = money;
                    user.bank = bank;
                    user.hycoin = hycoin;
                    user.dni = dni;
                    user.level = level;
                    user.exp = exp;
                    user.adminLv = 5;
                    user.job = job;
                    user.faction = faction;
                    user.rank = rank;
                    user.inventory = await Inventory.DatabaseFunctions.SpawnInventoryItems(characterId);
                    player.SetSharedData("isLogged", true);

                    // Ilegal faction check
                    Data.Entities.Faction facc = Data.Lists.factions.Find(t => t.id == user.faction);
                    if (facc != null) user.ilegalFaction = facc;

                    if (seguroMedico == 1) user.seguroMedico = true;

                    CheckIfUserHasCompany(user);
                    CheckIfUserWorksInCompany(user);

                    user.slot1 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot1);
                    user.slot2 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot2);
                    user.slot3 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot3);
                    user.slot4 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot4);
                    user.slot5 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot5);
                    user.slot6 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot6);

                    await Game.Vehicles.DbHandler.SpawnCharacterVehicles(user);

                    user.CarLicense = CarnetCoche;
                    user.MotorbikeLicense = CarnetMoto;
                    user.TruckLicense = CarnetCamion;

                    if(user.faction == 1)
                    {
                        World.Factions.PD.CargarPolicia.CrearMadero(player);
                    }

                    player.Name = name;
                    player.TriggerEvent("GetPlayerReadyToPlay");
                    player.TriggerEvent("showHUD");
                    player.TriggerEvent("UpdateMoneyHUD", money.ToString(), "set");
                    player.TriggerEvent("enableMicrophone", voiceMode);
                    player.TriggerEvent("update_hud_player", player.Value);
                    player.TriggerEvent("update_hud_players", Data.Info.playersConnected);
                    player.TriggerEvent("update_hud_microphone", 0);
                    player.TriggerEvent("update_hud_bank", bank.ToString());

                    player.SendChatMessage($"{Data.Info.serverName} || {Data.Info.serverVersion}");
                    if(gender == 0)
                    {
                        user.hombre = true;
                    }
                }
                
                
                connection.Close();
            }
        }

        [RemoteEvent("FinishCharacterCreation")]
        public async Task FinishCharacterCreation(Player player, string arguments, string arr)
        {
            //int? player_id = player.GetExternalData<Data.Entities.User>(0).id;
            //if (player_id == null) return;

            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            int player_id = user.id;
            var dni = Utilities.Generate.GenerateDNI();

            int character_id;

            var characterData = NAPI.Util.FromJson<CharacterData>(arguments);
            var ArrayCara = arr;

            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO characters(user_id, level, name, register_date, last_login_date, x, y, z, money,
                bank, gender, faceFirst, faceSecond, faceMix, skinFirst, skinSecond, skinMix, hairType, hairColor,
                hairHighlight, eyeColor, eyebrows, eyebrowsColor1, eyebrowsColor2, beard, beardColor, makeup, makeupColor,
                lipstick, lipstickColor, torso, topshirt, topshirtTexture, undershirt, legs, feet, accessory, ArrayCara, dni) 
                VALUES(@user_id, @level, @name, @register_date, @last_login_date, @x, @y, @z, @money, @bank, @gender, @faceFirst, @faceSecond,
                @faceMix, @skinFirst, @skinSecond, @skinMix, @hairType, @hairColor, @hairHighlight, @eyeColor, @eyebrows, @eyebrowsColor1,
                @eyebrowsColor2, @beard, @beardColor, @makeup, @makeupColor, @lipstick, @lipstickColor, @torso, @topshirt,
                @topshirtTexture, @undershirt, @legs, @feet, @accessory, @ArrayCara, @dni)";

                command.Parameters.AddWithValue("@user_id", player_id);
                command.Parameters.AddWithValue("@level", 1);
                command.Parameters.AddWithValue("@name", characterData.name);
                command.Parameters.AddWithValue("@register_date", DateTime.Now.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@last_login_date", DateTime.Now.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@x", 169.3792f);
                command.Parameters.AddWithValue("@y", -967.8402f);
                command.Parameters.AddWithValue("@z", 29.98808f);
                command.Parameters.AddWithValue("@money", 0);
                command.Parameters.AddWithValue("@bank", 0);
                command.Parameters.AddWithValue("@gender", characterData.gender);
                command.Parameters.AddWithValue("@faceFirst", characterData.faceFirst);
                command.Parameters.AddWithValue("@faceSecond", characterData.faceSecond);
                command.Parameters.AddWithValue("@faceMix", characterData.faceMix);
                command.Parameters.AddWithValue("@skinFirst", characterData.skinFirst);
                command.Parameters.AddWithValue("@skinSecond", characterData.skinSecond);
                command.Parameters.AddWithValue("@skinMix", characterData.skinMix);
                command.Parameters.AddWithValue("@hairType", characterData.hairType);
                command.Parameters.AddWithValue("@hairColor", characterData.hairColor);
                command.Parameters.AddWithValue("@hairHighlight", characterData.hairHighlight);
                command.Parameters.AddWithValue("@eyeColor", characterData.eyeColor);
                command.Parameters.AddWithValue("@eyebrows", characterData.eyebrows);
                command.Parameters.AddWithValue("@eyebrowsColor1", characterData.eyebrowsColor1);
                command.Parameters.AddWithValue("@eyebrowsColor2", characterData.eyebrowsColor2);
                command.Parameters.AddWithValue("@beard", characterData.beard);
                command.Parameters.AddWithValue("@beardColor", characterData.beardColor);
                command.Parameters.AddWithValue("@makeup", characterData.makeup);
                command.Parameters.AddWithValue("@makeupColor", characterData.makeupColor);
                command.Parameters.AddWithValue("@lipstick", characterData.lipstick);
                command.Parameters.AddWithValue("@lipstickColor", characterData.lipstickColor);
                command.Parameters.AddWithValue("@torso", characterData.torso);
                command.Parameters.AddWithValue("@topshirt", characterData.topshirt);
                command.Parameters.AddWithValue("@topshirtTexture", characterData.topshirtTexture);
                command.Parameters.AddWithValue("@undershirt", characterData.undershirt);
                command.Parameters.AddWithValue("@legs", characterData.legs);
                command.Parameters.AddWithValue("@feet", characterData.feet);
                command.Parameters.AddWithValue("@accessory", characterData.accessory);
                command.Parameters.AddWithValue("@ArrayCara", ArrayCara);
                command.Parameters.AddWithValue("@dni", dni);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                character_id = (int)command.LastInsertedId;
                connection.Close();
            }

            player.SendChatMessage($"{Data.Info.serverName} || {Data.Info.serverVersion}");
            player.Dimension = 0;
            player.Position = new Vector3(169.3792, -967.8402, 29.98808);

            //var user = player.GetExternalData<Data.Entities.User>(0);

            user.idpj = character_id;
            user.edad = 25;
            user.enableMicrophone = 0;
            user.mpStatus = 0;
            user.bankAccount = 0;
            user.IBAN = "0";
            user.money = 100000;
            user.bank = 0;
            user.hycoin = 0;
            user.dni = "0";
            user.level = 1;
            user.exp = 0;
            user.adminLv = 1;
            user.job = 0;
            user.faction = 0;
            user.rank = 1;
            player.SetSharedData("isLogged", true);

            user.seguroMedico = false;

            CheckIfUserHasCompany(user);
            CheckIfUserWorksInCompany(user);

            await Game.Vehicles.DbHandler.SpawnCharacterVehicles(user);

            user.CarLicense = 0;
            user.MotorbikeLicense = 0;
            user.TruckLicense = 0;

            player.Name = characterData.name;
            player.TriggerEvent("GetPlayerReadyToPlay");
            player.TriggerEvent("showHUD");
            player.TriggerEvent("UpdateMoneyHUD", 100000, "set");
            player.TriggerEvent("enableMicrophone", 0);
            player.TriggerEvent("update_hud_player", player.Value);
            player.TriggerEvent("update_hud_players", Data.Info.playersConnected);
            player.TriggerEvent("update_hud_microphone", 0);
            player.TriggerEvent("update_hud_bank", 0);

            player.SendChatMessage($"{Data.Info.serverName} || {Data.Info.serverVersion}");
        }

        [Command("caillou")]
        public void caillou(Player player)
        {
            player.SetSkin(0x705E61F2);
        }

        [RemoteEvent("SetPlayerPos")]
        public void SetPlayerPos(Player player, object[] arguments)
        {
            if (arguments.Length < 3) return;
            NAPI.Entity.SetEntityPosition(player.Handle, new Vector3((float)arguments[0], (float)arguments[1], (float)arguments[2]));

            bool freeze = arguments.Length == 4 ? (bool)arguments[3] : false;
        }

        [RemoteEvent("SetPlayerRot")]
        public void SetPlayerRot(Player player, object[] arguments)
        {
            if (arguments.Length < 1) return;
            NAPI.Entity.SetEntityRotation(player.Handle, new Vector3(0f, 0f, (float)arguments[0]));
        }

        [RemoteEvent("SetPlayerSkin")]
        public void SetPlayerSkin(Player player, int genre)
        {
            if (genre == 0) player.SetSkin(0x705E61F2);
            else player.SetSkin(0x9C9EFFD8);
        }

        [RemoteEvent("SetPlayerClothes")]
        public void SetPlayerClothes(Player player, int slot, int drawable, int texture)
        {
            var DicRopa = new Dictionary<int, ComponentVariation>();
            DicRopa.Add(slot, new ComponentVariation { Drawable = drawable, Texture = texture });
            NAPI.Player.SetPlayerClothes(player, DicRopa);
        }

        [RemoteEvent("SetPlayerAccessory")]
        public void SetPlayerAccessory(Player player, int slot, int drawable, int texture)
        {
            NAPI.Player.SetPlayerAccessory(player, slot, drawable, texture);
        }
        public static void CheckIfUserHasCompany(Data.Entities.User user)
        {
            Data.Entities.Company company = Data.Lists.Companies.Find(x => x.owner == user.idpj);
            if (company != null) user.companyProperty = company;
        }

        public static void CheckIfUserWorksInCompany(Data.Entities.User user)
        {
            Data.Entities.Company company = Data.Lists.Companies.Find(x => x.id == user.job);
            if (company != null) user.companyMember = company;
        }

        public async static Task UpdateUserCompany(int idpj, int company)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET job = @company WHERE id = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);
                command.Parameters.AddWithValue("@company", company);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateSeguroMedico(int idpj, int type)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET seguroMedico = @type WHERE id = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);
                command.Parameters.AddWithValue("@type", type);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateDni(int idpj)
        {
            var dni = Utilities.Generate.CreateDNI();
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET dni = @dni WHERE id = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);
                command.Parameters.AddWithValue("@dni", dni);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateUserFaction(int idpj, int faction)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET faction = @faction WHERE id = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);
                command.Parameters.AddWithValue("@faction", faction);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateUserDimension(int idpj, int dimension)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET dimension = @dimension WHERE id = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);
                command.Parameters.AddWithValue("@dimension", dimension);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateUserFactionRank(int idpj, int rank)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE characters SET rank = @rank WHERE id = @idpj";
                command.Parameters.AddWithValue("@idpj", idpj);
                command.Parameters.AddWithValue("@rank", rank);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async static Task UpdateUserPosition(int idpj, double x, double y, double z)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "UPDATE characters SET x = @x, y = @y, z = @z WHERE id = @idpj";
                    command.Parameters.AddWithValue("@idpj", idpj);
                    command.Parameters.AddWithValue("@x", x);
                    command.Parameters.AddWithValue("@y", y);
                    command.Parameters.AddWithValue("@z", z);

                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}


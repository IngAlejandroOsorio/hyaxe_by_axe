using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Game.CharacterAlpha
{
    public class CharacterAlpha : Script
    {
        [RemoteEvent("PjAlphaCreationFinish")]
        public async void RE_PjAlphaCreationFinish(Player player, string name)
        {
            player.Name = name;

            string dni = Utilities.Generate.CreateDNI();

            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

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

                command.Parameters.AddWithValue("@user_id", user.id);
                command.Parameters.AddWithValue("@level", 1);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@register_date", DateTime.Now.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@last_login_date", DateTime.Now.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@x", 169.3792f);
                command.Parameters.AddWithValue("@y", -967.8402f);
                command.Parameters.AddWithValue("@z", 29.98808f);
                command.Parameters.AddWithValue("@money", 8000);
                command.Parameters.AddWithValue("@bank", 0);
                command.Parameters.AddWithValue("@gender", 0);
                command.Parameters.AddWithValue("@faceFirst", 0);
                command.Parameters.AddWithValue("@faceSecond", 0);
                command.Parameters.AddWithValue("@faceMix", 0);
                command.Parameters.AddWithValue("@skinFirst", 0);
                command.Parameters.AddWithValue("@skinSecond", 0);
                command.Parameters.AddWithValue("@skinMix", 0);
                command.Parameters.AddWithValue("@hairType", 0);
                command.Parameters.AddWithValue("@hairColor", 0);
                command.Parameters.AddWithValue("@hairHighlight", 0);
                command.Parameters.AddWithValue("@eyeColor", 0);
                command.Parameters.AddWithValue("@eyebrows", 0);
                command.Parameters.AddWithValue("@eyebrowsColor1", 0);
                command.Parameters.AddWithValue("@eyebrowsColor2", 0);
                command.Parameters.AddWithValue("@beard", 0);
                command.Parameters.AddWithValue("@beardColor", 0);
                command.Parameters.AddWithValue("@makeup", 0);
                command.Parameters.AddWithValue("@makeupColor", 0);
                command.Parameters.AddWithValue("@lipstick", 0);
                command.Parameters.AddWithValue("@lipstickColor", 0);
                command.Parameters.AddWithValue("@torso", 0);
                command.Parameters.AddWithValue("@topshirt", 0);
                command.Parameters.AddWithValue("@topshirtTexture", 0);
                command.Parameters.AddWithValue("@undershirt", 0);
                command.Parameters.AddWithValue("@legs", 0);
                command.Parameters.AddWithValue("@feet", 0);
                command.Parameters.AddWithValue("@accessory", 0);
                command.Parameters.AddWithValue("@ArrayCara", "0");
                command.Parameters.AddWithValue("@dni", dni);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                user.idpj = (int)command.LastInsertedId;
                connection.Close();
            }

            user.name = name;
            user.edad = 25;
            user.enableMicrophone = 0;
            user.mpStatus = 0;
            user.bankAccount = 0;
            user.IBAN = "0";
            user.money = 8000;
            user.bank = 0;
            user.hycoin = 0;
            user.dni = dni;
            user.level = 1;
            user.exp = 0;
            user.adminLv = 0;
            user.job = 0;
            user.faction = 0;
            user.rank = 1;
            user.seguroMedico = false;
            user.CarLicense = 0;
            user.MotorbikeLicense = 0;
            user.TruckLicense = 0;

            player.SetSharedData("isLogged", true);
        }

        [RemoteEvent("SelectCharacter")]
        public async static Task<bool> SelectCharacterAlpha(Player player, int uId) 
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characters WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", uId);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                    int characterId = reader.GetInt32(reader.GetOrdinal("id"));
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
                    int dimension = reader.GetInt32(reader.GetOrdinal("dimension"));
                    int phone = reader.GetInt32(reader.GetOrdinal("phone"));

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

                    string bindeos = reader.GetString(reader.GetOrdinal("bindeos"));

                    player.Dimension = 0;
                    player.TriggerEvent("DestroyWindow");
                    NAPI.Entity.SetEntityPosition(player, new Vector3(x, y, z));
                    NAPI.Entity.SetEntityRotation(player, new Vector3(0, 0, rotation));
                    NAPI.Player.SetPlayerHealth(player, health);
                    NAPI.Player.SetPlayerArmor(player, armour);
                    NAPI.Player.SetPlayerName(player, name);

                    //var user = player.GetExternalData<Data.Entities.User>(0);

                    if (!player.HasData("USER_CLASS")) return false;
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
                    user.job = job;
                    user.faction = faction;
                    user.rank = rank;
                    user.phone = phone;
                    user.inventory = await Inventory.DatabaseFunctions.SpawnInventoryItems(characterId);
                    player.SetSharedData("isLogged", true);

                    // Ilegal faction check
                    Data.Entities.Faction facc = Data.Lists.factions.Find(t => t.id == user.faction);
                    if (facc != null) user.ilegalFaction = facc;

                    if (seguroMedico == 1) user.seguroMedico = true;

                    Game.CharacterSelector.CharacterSelector.CheckIfUserHasCompany(user);
                    Game.CharacterSelector.CharacterSelector.CheckIfUserWorksInCompany(user);

                    user.slot1 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot1);
                    user.slot2 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot2);
                    user.slot3 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot3);
                    user.slot4 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot4);
                    user.slot5 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot5);
                    user.slot6 = await Inventory.DatabaseFunctions.SpawnCharacterItem(slot6);

                    await Game.Vehicles.DbHandler.SpawnCharacterVehicles(user);
                    if(phone != 0)
                    {
                        user.phoneBook = await Game.Phone.DbFunctions.SpawnPhoneBooks(characterId);
                    }

                    user.CarLicense = CarnetCoche;
                    user.MotorbikeLicense = CarnetMoto;
                    user.TruckLicense = CarnetCamion;

                    if (user.faction == 1)
                    {
                        World.Factions.PD.CargarPolicia.CrearMadero(player);
                    }

                    user.minisanciones = await CargarMinisanciones((ulong)user.id);
                    user.multas = await CargarMultas(user.idpj);

                    if (user.dni == "0") user.dni = await Game.CharacterSelector.CharacterSelector.UpdateDni(user.idpj);

                    player.Name = name;
                    player.TriggerEvent("PonerNombre",name);
                    player.TriggerEvent("LoadCharacterFace");
                    player.TriggerEvent("CargoBindeosClie", JsonConvert.SerializeObject(bindeos));
                    player.TriggerEvent("GetPlayerReadyToPlay");
                    player.TriggerEvent("showHUD");
                    player.TriggerEvent("UpdateMoneyHUD", money.ToString(), "set");
                    player.TriggerEvent("enableMicrophone", voiceMode);
                    player.TriggerEvent("update_hud_player", player.Value);
                    player.TriggerEvent("update_hud_players", Data.Info.playersConnected);
                    player.TriggerEvent("update_hud_microphone", 0);
                    player.TriggerEvent("update_hud_bank", bank.ToString());
                    player.TriggerEvent("returnDebugActive");                    

                    player.SendChatMessage($"¡Bienvenido de vuelta a {Data.Info.serverName} Roleplay! || Versión {Data.Info.serverVersion}");
                    if (gender == 0)
                    {
                        user.hombre = true;
                    }

                    return true;
                }


                connection.Close();
            }
            return false;
        }

        [RemoteEvent("PjFinishAlphaCreation")]
        public void RE_PjFinishAlphaCreation(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            player.TriggerEvent("animFinCreador");
            player.Dimension = 0;
            player.Position = new Vector3(-1015.162, -2752.169, 0.8003625);            
            player.TriggerEvent("GetPlayerReadyToPlay");
            player.TriggerEvent("showHUD");
            player.TriggerEvent("UpdateMoneyHUD", 8000, "set");
            player.TriggerEvent("enableMicrophone", 0);
            player.TriggerEvent("update_hud_player", player.Value);
            player.TriggerEvent("update_hud_players", Data.Info.playersConnected);
            player.TriggerEvent("update_hud_microphone", 0);
            player.TriggerEvent("update_hud_bank", 0);
            player.TriggerEvent("returnDebugActive");

            player.SendChatMessage($"¡Bienvenido a {Data.Info.serverName} Roleplay! (versión {Data.Info.serverVersion})");
            player.SendChatMessage("El servidor se encuentra en fase Alpha, ¡deja tu feedback en el foro y ayúdanos a mejorar!");
            player.SendChatMessage("Usa /ayuda para ver los comandos básicos y pulsa F2 para acceder al menú del jugador");
        }


        [RemoteEvent("updateBindeos")]
        public static async void updateBindeos(Player player, string bindeo)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            int id = user.idpj;
            string bindeos = JsonConvert.SerializeObject(bindeo);
            Utilities.Notifications.SendNotificationINFO(player, $"Debes relogear para aplicar los cambios en tus bindeos.");
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "UPDATE characters SET characters.bindeos=@bindeo WHERE id = @id";
                    command.Parameters.AddWithValue("@bindeo", bindeos);
                    command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

        }


        static async Task<List<Data.Entities.Minisancion>> CargarMinisanciones(ulong socialclubid)
        {
            List<Data.Entities.Minisancion> minisanciones = new List<Data.Entities.Minisancion>();



            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM minisanciones WHERE SocialClubID = @SCid";
                command.Parameters.AddWithValue("@SCid", socialclubid);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {

                        int tipo = reader.GetInt32(reader.GetOrdinal("tipo"));
                        string razon = reader.GetString(reader.GetOrdinal("Razon"));
                        DateTime dt = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                        string staff = reader.GetString(reader.GetOrdinal("staff"));

                        Data.Entities.Minisancion minisancion = new Data.Entities.Minisancion(dt, staff, razon, tipo); ;

                        minisanciones.Add(minisancion);
                    }
                }
            }
            return minisanciones;

        }

        public static async Task<List<Data.Entities.FineLSPD>> CargarMultas(int id)
        {
            List<Data.Entities.FineLSPD> Multas = new List<Data.Entities.FineLSPD>();



            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM multas WHERE PjID = @PjID";
                command.Parameters.AddWithValue("@PjID", id);

                DbDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        bool p = false;

                        int idDB = reader.GetInt32(reader.GetOrdinal("id"));
                        int pagada = reader.GetInt32(reader.GetOrdinal("pagada"));
                        int precio = reader.GetInt32(reader.GetOrdinal("precio"));
                        string razon = reader.GetString(reader.GetOrdinal("razon"));

                        if (pagada == 1) p = true;

                        Multas.Add(new Data.Entities.FineLSPD() { IdDatabase = idDB, isPaid = p, reason = razon, price = precio, userid = id });
                    }
                }
            }
            return Multas;

        }
    }
}


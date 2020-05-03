using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace DowntownRP.Game.Authentication
{
    public class Login : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            player.SetSharedData("FACE_SETTINGS", 0);
            player.SetSharedData("EntityAlpha", 255);

            Data.Info.playersConnected += 1;
            NAPI.ClientEvent.TriggerClientEventForAll("update_hud_players", Data.Info.playersConnected);
            player.TriggerEvent("ShowLoginWindow");
        }

        [RemoteEvent("RE_LoginPlayerAccount")]
        public async Task RE_LoginPlayerAccount(Player player, string username, string password)
        {
            if (await DatabaseFunctions.CheckIfPlayerRegistered(username) == true)
            {
                int playerid = await DatabaseFunctions.LoginPlayer(username, password);
                if (playerid != 0)
                {
                    if(Data.Lists.playersConnected.Find(x => x.id == playerid) != null)
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No puedes iniciar sesión porque ya hay alguien jugando en esta cuenta");
                        return;
                    }

                    player.TriggerEvent("DestroyWindow");


                    if(await DatabaseFunctions.CheckIfBanned(player.SocialClubName))
                    {
                        player.Kick("Estás baneado del servidor, ve al foro o al ts para más información");
                        return;
                    }



                    Data.Entities.User user = new Data.Entities.User
                    {
                        id = playerid,
                        idIg = player.Value,
                        entity = player,
                        adminLv = await DatabaseFunctions.GetAdminLevel(playerid)
                    };

                    Data.Lists.playersConnected.Add(user);

    
                    player.SetData("USER_CLASS", user);
                    if(! await Game.CharacterAlpha.CharacterAlpha.SelectCharacterAlpha(player, playerid))
                    {
                        player.TriggerEvent("DestroyBrowserPjAlphaPj");
                        player.TriggerEvent("CreatePjAlphaEvent");
                    }
                    //player.SetExternalData<Data.Entities.User>(0, user);
                    //CharacterSelector.CharacterSelector.RetrieveCharactersList(player);
                    // await Character.DbFunctions.ShowCharacterList(player); <- NUEVO CHARACTER SELECTOR
                }
                else player.TriggerEvent("ShowErrorAlert", 1);
            }
            else player.TriggerEvent("ShowErrorAlert", 1);
        }

        [RemoteEvent("RE_RegisterPlayerAccount")]
        public async void RE_RegisterPlayerAccount(Player player, string username, string email, string password, string repassword)
        {
            if (await DatabaseFunctions.CheckIfPlayerRegistered(username) == false)
            {
                if (await DatabaseFunctions.CheckIfEmailRegistered(email) == false)
                {
                    if (password == repassword)
                    {
                        int playerId = await DatabaseFunctions.RegisterPlayer(username, password, email, player.SocialClubName, player.Address);

                        Data.Entities.User user = new Data.Entities.User
                        {
                            id = playerId,
                            idIg = player.Value,
                            entity = player
                        };

                        Data.Lists.playersConnected.Add(user);

                        //player.SetExternalData<Data.Entities.User>(0, user);
                        player.SetData("USER_CLASS", user);

                        Console.WriteLine($"Registrado {playerId}");
                        //player.TriggerEvent("SwitchLoginRegister",1);
                        player.TriggerEvent("DestroyBrowserPjAlphaPj");
                        player.TriggerEvent("CreatePjAlphaEvent");
                    }
                    else player.TriggerEvent("ShowErrorAlert", 4);
                }
                else player.TriggerEvent("ShowErrorAlert", 3);
            }
            else player.TriggerEvent("ShowErrorAlert", 2);
        }
    }
}

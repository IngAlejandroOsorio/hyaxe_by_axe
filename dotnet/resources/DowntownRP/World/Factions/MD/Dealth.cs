using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.MD
{
    public class Dealth : Script
    {
        [ServerEvent(Event.PlayerDeath)]
        public async Task SE_PlayerDeathGeneral(Player player, Player killer, uint reason)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Vector3 dPosition = player.Position;
            user.isDeath = true;

            //player.TriggerEvent("OpenDeathUI");
            player.TriggerEvent("freeze_player");
            Utilities.Notifications.SendNotificationINFO(player, "En un minuto podrás aceptar muerte");
            NAPI.Player.SpawnPlayer(player, dPosition);
            player.PlayAnimation("dead", "dead_d", (int)(Utilities.AnimationFlags.Loop));

            //await Task.Delay(1000);

            /*if (user.isDeath)
            {
                user.adviceLSMD = true;
                Utilities.Notifications.SendNotificationINFO(player, "Ya puedes avisar a LSMD");
            }*/

            await Task.Delay(60000); //1 min
            if (user.isDeath)
            {
                user.acceptDeath = true;
                Utilities.Notifications.SendNotificationINFO(player, "Ya puedes aceptar la muerte usando /aceptarmuerte");
            }
            
        }

        [ServerEvent(Event.PlayerDamage)]
        public void Death_PlayerDamage(Player player, float float1, float float2)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.isDeath) player.PlayAnimation("dead", "dead_d", (int)(Utilities.AnimationFlags.Loop));
        }

        [RemoteEvent("SS_AcceptDeath")] // ESTO ES AVISAR A LSMD
        public void SS_AdviceLSMD(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Utilities.Notifications.SendNotificationERROR(player, "Función no implementada");

            /*if (user.isDeath)
            {
                if (user.adviceLSMD)
                {
                    foreach (var Player in Data.Lists.playersConnected)
                    {
                        if(Player.faction == 2 && Player.factionDuty)
                        {
                            Player.entity.SendChatMessage($"<font color='red'>[LSMD]</font> Se ha recibido un nuevo aviso para recibir atención médica. /aceptaravisolsmd");
                            Data.Info.lastDeathAdviceLSMD = player.Position;
                            Utilities.Notifications.SendNotificationOK(player, "Has enviado un aviso a LSMD.");
                        }
                    }
                }
                Utilities.Notifications.SendNotificationERROR(player, "Todavía no puedes avisar a LSMD");
            }*/
        }

        public int Lowest(params int[] inputs)
        {
            return inputs.Min();
        }

        [Command("aceptarmuerte")] // ESTO ES ACEPTAR MUERTE
        public async Task SS_AcceptDeath(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.isDeath)
            {
                if (user.acceptDeath)
                {
                    player.TriggerEvent("playerDeathh");

                    await Task.Delay(8000);

                    player.TriggerEvent("playerSpawn");
                    player.TriggerEvent("unfreeze_player");

                    int hospital1 = (int)player.Position.DistanceTo(new Vector3(347.9363, -1370.1932, 32.50962));
                    int hospital2 = (int)player.Position.DistanceTo(new Vector3(365.5317, -570.0759, 28.791481));
                    int hospital3 = (int)player.Position.DistanceTo(new Vector3(1820.1287, 3673.4202, 34.270065));
                    int hospital4 = (int)player.Position.DistanceTo(new Vector3(-258.4666, 6311.663, 32.4089));
                    int hospitalCheck = Lowest(hospital1, hospital2, hospital3, hospital4);

                    if (hospitalCheck == hospital1) player.Position = new Vector3(347.9363, -1370.1932, 32.50962);
                    else if (hospitalCheck == hospital2) player.Position = new Vector3(365.5317, -570.0759, 28.791481);
                    else if (hospitalCheck == hospital3) player.Position = new Vector3(1820.1287, 3673.4202, 34.270065);
                    else if (hospitalCheck == hospital4) player.Position = new Vector3(-258.4666, 6311.663, 32.4089);
                    else player.Position = new Vector3(347.9363, -1370.1932, 32.50962);

                    player.Health = 100;
                    player.Armor = 0;
                    player.RemoveAllWeapons();

                    user.isDeath = false;
                    user.acceptDeath = false;

                    if (user.seguroMedico)
                    {
                        await Game.Money.MoneyModel.SubMoney(player, 50);
                        Utilities.Notifications.SendNotificationINFO(player, "Se te han restado $50 por la atención médica");
                    }
                    else
                    {
                        await Game.Money.MoneyModel.SubMoney(player, 300);
                        Utilities.Notifications.SendNotificationINFO(player, "Se te han restado $300 por la atención médica");
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Todavía no puedes aceptar la muerte");
            }
        }

        [Command("aceptaravisolsmd")]
        public void CMD_aceptaravisolsmd(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 2)
            {
                if (user.factionDuty)
                {
                    foreach (var Player in Data.Lists.playersConnected)
                    {
                        if (Player.faction == 2 && Player.factionDuty)
                        {
                            Player.entity.SendChatMessage($"~r~[LSMD]~w~ {player.Name} ha aceptado el último aviso.");
                        }
                    }

                    player.TriggerEvent("AdviceLSMDBlip", Data.Info.lastDeathAdviceLSMD);
                    Utilities.Notifications.SendNotificationOK(player, "Se han marcado las coordenadas de la llamada en tu GPS. Recuerda usar /quitaravisolsmd para quitarlo.");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSMD");
        }

        [Command("quitaravisolsmd")]
        public void CMD_quitaravisolsmd(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 2)
            {
                if (user.factionDuty)
                {
                    Utilities.Notifications.SendNotificationOK(player, "Has quitado las últimas coordenadas marcadas en tu GPS");
                    player.TriggerEvent("DestroyAdviceLSMDBlip");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSMD");
        }
    }
}

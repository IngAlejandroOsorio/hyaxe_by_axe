using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.MD
{
    public class Dealth : Script
    {
        [ServerEvent(Event.PlayerDeath)]
        public async Task SE_PlayerDeathGeneral(Client player, Client killer, uint reason)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            Vector3 dPosition = player.Position;
            user.isDeath = true;

            player.TriggerEvent("OpenDeathUI");
            NAPI.Player.SpawnPlayer(player, dPosition);
            player.PlayAnimation("dead", "dead_d", (int)(Utilities.AnimationFlags.Loop));

            await Task.Delay(1000);

            if (user.isDeath)
            {
                user.adviceLSMD = true;
                Utilities.Notifications.SendNotificationINFO(player, "Ya puedes avisar a LSMD");
            }

            await Task.Delay(1000);
            if (user.isDeath)
            {
                user.acceptDeath = true;
                Utilities.Notifications.SendNotificationINFO(player, "Ya puedes aceptar la muerte");
            }
            
        }

        [RemoteEvent("SS_AdviceLSMD")]
        public void SS_AdviceLSMD(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.isDeath)
            {
                if (user.adviceLSMD)
                {
                    foreach (var client in Data.Lists.playersConnected)
                    {
                        if(client.faction == 2 && client.factionDuty)
                        {
                            client.entity.SendChatMessage($"<font color='red'>[LSMD]</font> Se ha recibido un nuevo aviso para recibir atención médica. /aceptaravisolsmd");
                            Data.Info.lastDeathAdviceLSMD = player.Position;
                            Utilities.Notifications.SendNotificationOK(player, "Has enviado un aviso a LSMD.");
                        }
                    }
                }
                Utilities.Notifications.SendNotificationERROR(player, "Todavía no puedes avisar a LSMD");
            }
        }

        [RemoteEvent("SS_AcceptDeath")]
        public async Task SS_AcceptDeath(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.isDeath)
            {
                if (user.acceptDeath)
                {
                    player.TriggerEvent("CloseDeathUI");
                    player.TriggerEvent("playerDeathh");

                    await Task.Delay(8000);

                    player.TriggerEvent("playerSpawn");
                    player.Position = new Vector3(358.358, -1382.471, 32.51111);
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
                Utilities.Notifications.SendNotificationERROR(player, "Todavía no puedes aceptar la muerte");
            }
        }

        [Command("aceptaravisolsmd")]
        public void CMD_aceptaravisolsmd(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 2)
            {
                if (user.factionDuty)
                {
                    foreach (var client in Data.Lists.playersConnected)
                    {
                        if (client.faction == 2 && client.factionDuty)
                        {
                            client.entity.SendChatMessage($"<font color='red'>[LSMD]</font> {player.Name} ha aceptado el último aviso.");
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
        public void CMD_quitaravisolsmd(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

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

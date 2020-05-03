using DowntownRP.Utilities.Outfits;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.MD
{
    public class Hospital : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void SE_HospitalInit()
        {
            Vector3 pHillbox = new Vector3(355.0597, -1415.898, 32.51043);

            Blip blip = NAPI.Blip.CreateBlip(153, pHillbox, 1, 1, "Hospital", 255, 0, true);
            ColShape shape = NAPI.ColShape.CreateCylinderColShape(pHillbox.Subtract(new Vector3(0, 0, 1)), 2, 2);
            Marker marker = NAPI.Marker.CreateMarker(1, pHillbox.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(23, 217, 220));
            TextLabel label = NAPI.TextLabel.CreateTextLabel("Usa ~b~/comprarseguro ~w~para comprar el seguro medico", pHillbox, 1, 1, 0, new Color(255, 255, 255));

            Vector3 poss = new Vector3(375.5643, -1434.76, 32.51111);
            ColShape shape2 = NAPI.ColShape.CreateCylinderColShape(poss, 3, 3);
            shape2.SetData("LSMD_DUTY_POINT", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Presiona ~r~F6 ~w~para ponerte en servicio", new Vector3(460.1864, -990.8899, 30.6896), 4, 2, 0, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(1, poss.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));

            shape.SetData("HOSPITAL_LOBBY", true);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void SE_HospitalEnterColshape(ColShape shape, Player player)
        {
            if (shape.HasData("HOSPITAL_LOBBY"))
            {
                player.SetData("HOSPITAL_LOBBY", true);
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void SE_HospitalExitColshape(ColShape shape, Player player)
        {
            if (shape.HasData("HOSPITAL_LOBBY"))
            {
                player.ResetData("HOSPITAL_LOBBY");
            }
        }

        [Command("comprarseguro")]
        public void CMD_comprarseguro(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.HasData("HOSPITAL_LOBBY"))
            {
                if (!user.seguroMedico)
                {
                    player.TriggerEvent("OpenSeguroMedico");
                }
                Utilities.Notifications.SendNotificationERROR(player, "Ya tienes seguro médico");
            }
        }

        [RemoteEvent("RS_BuySeguroMedico")]
        public async Task RS_BuySeguroMedico(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.HasData("HOSPITAL_LOBBY"))
            {
                if (!user.seguroMedico)
                {
                    if(await Game.Money.MoneyModel.SubMoney(player, 1000))
                    {
                        player.TriggerEvent("CloseSeguroMedico");
                        await Game.CharacterSelector.CharacterSelector.UpdateSeguroMedico(user.idpj, 1);

                        player.TriggerEvent("chat_goal", "¡Felicidades!", "Ahora tienes un seguro médico");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes dinero para comprar el seguro médico");
                }
                Utilities.Notifications.SendNotificationERROR(player, "Ya tienes seguro médico");
            }
        }

        [RemoteEvent("ActionMDDuty")]
        public void ActionFactionDuty(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (!user.chatStatus)
            {
                if (player.HasData("LSMD_DUTY_POINT"))
                {
                    if (user.faction == 2)
                    {
                        if (!user.factionDuty)
                        {
                            user.factionDuty = true;
                            Utilities.Notifications.SendNotificationOK(player, "Estás en servicio");
                            player.SetOutfit(10);
                            return;
                        }else{
                            user.factionDuty = false;
                            Utilities.Notifications.SendNotificationOK(player, "No estás en servicio");
                            NAPI.ClientEvent.TriggerClientEvent(player, "ApplyCharacterFeatures", 0);
                            return;
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSMD");
                }
            }
        }
    }
}

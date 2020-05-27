using DowntownRP.Utilities;
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
            // Hospital ruso
            Vector3 pHillbox = new Vector3(355.0597, -1415.898, 32.51043);
            Blip blip = NAPI.Blip.CreateBlip(153, pHillbox, 1, 1, "Hospital", 255, 0, true);
            ColShape shape = NAPI.ColShape.CreateCylinderColShape(pHillbox.Subtract(new Vector3(0, 0, 1)), 2, 2);
            Marker marker = NAPI.Marker.CreateMarker(1, pHillbox.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(23, 217, 220));
            TextLabel label = NAPI.TextLabel.CreateTextLabel("Usa ~b~/comprarseguro ~w~para comprar el seguro medico", pHillbox, 1, 1, 0, new Color(255, 255, 255));

            World.Factions.PuntoFaccion psRuso = new World.Factions.PuntoFaccion("LSFD_DUTY_POINT", new Vector3(374.81348, -1434.0625, 32.511116 ), "Presiona ~r~F6 ~w~para ponerte en servicio");


           


            //374.81348, -1434.0625, 32.511116

            // Pillbox
            Vector3 ppHillbox = new Vector3(340.12732, -585.9587, 28.79146);
            Blip pblip = NAPI.Blip.CreateBlip(153, ppHillbox, 1, 1, "Hospital", 255, 0, true);
            ColShape pshape = NAPI.ColShape.CreateCylinderColShape(ppHillbox.Subtract(new Vector3(0, 0, 1)), 2, 2);
            Marker pmarker = NAPI.Marker.CreateMarker(1, ppHillbox.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(23, 217, 220));
            TextLabel plabel = NAPI.TextLabel.CreateTextLabel("Usa ~b~/comprarseguro ~w~para comprar el seguro medico", ppHillbox, 1, 1, 0, new Color(255, 255, 255));

            World.Factions.PuntoFaccion psHillBox = new PuntoFaccion("LSFD_DUTY_POINT", new Vector3(336.0169, -579.5659, 28.791044), "Presiona ~r~F6 ~w~para ponerte en servicio");

           
            // Sandy shores
            Vector3 spHillbox = new Vector3(1832.6058, 3682.7493, 34.270073);
            Blip sblip = NAPI.Blip.CreateBlip(153, spHillbox, 1, 1, "Hospital", 255, 0, true);
            ColShape sshape = NAPI.ColShape.CreateCylinderColShape(spHillbox.Subtract(new Vector3(0, 0, 1)), 2, 2);
            Marker smarker = NAPI.Marker.CreateMarker(1, spHillbox.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(23, 217, 220));
            TextLabel slabel = NAPI.TextLabel.CreateTextLabel("Usa ~b~/comprarseguro ~w~para comprar el seguro medico", spHillbox, 1, 1, 0, new Color(255, 255, 255));
            World.Factions.PuntoFaccion psSandy = new PuntoFaccion("LSFD_DUTY_POINT", new Vector3(1839.5349, 3689.3784, 34.26), "Presiona ~r~F6 ~w~para ponerte en servicio");

            // Paleto
            Vector3 cpHillbox = new Vector3(-256.0574, 6329.675, 32.408897);
            Blip cblip = NAPI.Blip.CreateBlip(153, cpHillbox, 1, 1, "Hospital", 255, 0, true);
            ColShape cshape = NAPI.ColShape.CreateCylinderColShape(cpHillbox.Subtract(new Vector3(0, 0, 1)), 2, 2);
            Marker cmarker = NAPI.Marker.CreateMarker(1, cpHillbox.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 1, new Color(23, 217, 220));
            TextLabel clabel = NAPI.TextLabel.CreateTextLabel("Usa ~b~/comprarseguro ~w~para comprar el seguro medico", cpHillbox, 1, 1, 0, new Color(255, 255, 255));
            World.Factions.PuntoFaccion psPaleto = new PuntoFaccion("LSFD_DUTY_POINT", new Vector3(-252.7572 ,6327.4756, 32.40891), "Presiona ~r~F6 ~w~para ponerte en servicio");


            // FD PALETO 
            World.Factions.PuntoFaccion psFdPaleto = new PuntoFaccion("LSFD_DUTY_POINT", new Vector3(-366.4411, 6102.6787, 31.449549), "Presiona ~r~F6 ~w~para ponerte en servicio");
            Blip fdpaleto = NAPI.Blip.CreateBlip(436, new Vector3(-376.56446, 6115.9736, 31.44953), 1, 1, "Bomberos", 255, 0, true);


            /*Vector3 poss = new Vector3(375.5643, -1434.76, 32.51111);
            ColShape shape2 = NAPI.ColShape.CreateCylinderColShape(poss, 3, 3);
            shape2.SetData("LSMD_DUTY_POINT", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Presiona ~r~F6 ~w~para ponerte en servicio", new Vector3(460.1864, -990.8899, 30.6896), 4, 2, 0, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(1, poss.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));*/

            shape.SetData("HOSPITAL_LOBBY", true);
            pshape.SetData("HOSPITAL_LOBBY", true);
            sshape.SetData("HOSPITAL_LOBBY", true);
            cshape.SetData("HOSPITAL_LOBBY", true);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void SE_HospitalEnterColshape(ColShape shape, Player player)
        {
            if (shape.HasData("HOSPITAL_LOBBY"))
            {
                player.SetData("HOSPITAL_LOBBY", true);

            }else if (shape.HasData("LSFD_DUTY_POINT"))
            {
                player.SetData("LSFD_DUTY_POINT", true);
            }
            
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void SE_HospitalExitColshape(ColShape shape, Player player)
        {
            if (shape.HasData("HOSPITAL_LOBBY"))
            {
                player.ResetData("HOSPITAL_LOBBY");

            }else if (shape.HasData("LSFD_DUTY_POINT"))
            {
                player.ResetData("LSFD_DUTY_POINT");
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
                    if (await Game.Money.MoneyModel.SubMoney(player, 1000))
                    {
                        player.TriggerEvent("CloseSeguroMedico");
                        await Game.CharacterSelector.CharacterSelector.UpdateSeguroMedico(user.idpj, 1);

                        player.TriggerEvent("chat_goal", "¡Felicidades!", "Ahora tienes un seguro médico");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes dinero para comprar el seguro médico");
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "Ya tienes seguro médico");
                }
            }
        }

        [RemoteEvent("ActionMDDuty")]
        public void ActionFactionDuty(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (!user.chatStatus)
            {
                if (player.HasData("LSFD_DUTY_POINT"))
                {
                    if (user.faction == 2)
                    {
                        if (!user.factionDuty)
                        {
                            user.factionDuty = true;
                            Utilities.Notifications.SendNotificationOK(player, "Estás en servicio");
                            Uniformidades.Default(player);
                            return;
                        }else{
                            user.factionDuty = false;
                            Utilities.Notifications.SendNotificationOK(player, "No estás en servicio");
                            
                            NAPI.ClientEvent.TriggerClientEvent(player, "ApplyCharacterFeatures", 0);
                            Clothes.ReturnUserClothes(user);
                            return;
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSMD");
                }
            }
        }

        [Command("taquillafd", Alias ="taquillamd")]
        public async Task CMD_Taquillamd(Player player)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.faction == 2 & user.factionDuty & player.HasData("LSFD_DUTY_POINT"))
            {
                Utilities.Notifications.SendNotificationINFO(player, "Abriendo taquilla");
                player.TriggerEvent("tfd", player);
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes hacer esto a no ser que formes parte de la FD, estés de servicio y en las taquillas");
            }
        }
    }
}

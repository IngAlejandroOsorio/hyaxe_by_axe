using DowntownRP.Utilities.Outfits;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.PD
{
    class PoliceStation : Script
    {
        public static double ConvertMinutesToMilliseconds(double minutes)
        {
            int milisecs = Convert.ToInt32(TimeSpan.FromMinutes(minutes).TotalMilliseconds);
            return milisecs;
        }

        [ServerEvent(Event.ResourceStart)]
        public void RS_ColShapePrison()
        {
            ColShape shape = NAPI.ColShape.CreateCylinderColShape(new Vector3(459.8182, -989.6442, 24.91484), 5, 5);
            shape.SetData("LSPD_CHOOSE_PRISON", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Usa ~r~/arrestar ~w~para encarcelar", new Vector3(459.8182, -989.6442, 24.91484), 7, 3, 0, new Color(255, 255, 255));

            // Mission row
            Vector3 poss = new Vector3(460.1864, -990.8899, 30.6896);
            ColShape shape2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(460.1864, -990.8899, 30.6896), 3, 3);
            shape2.SetData("LSPD_DUTY_POINT", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Presiona ~r~F6 ~w~para ponerte en servicio", new Vector3(460.1864, -990.8899, 30.6896), 4, 2, 0, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(1, poss.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));

            Blip blip = NAPI.Blip.CreateBlip(new Vector3(441.2899, -982.4244, 30.6896));
            blip.Sprite = 60;
            blip.Color = 3;
            blip.Name = "Los Santos Police Department";
            blip.ShortRange = true;

            Vector3 position = new Vector3(460.4095, -981.0943, 30.68958);
            ColShape col = NAPI.ColShape.CreateCylinderColShape(position, 2, 2);
            NAPI.Marker.CreateMarker(1, position.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Armero~n~~w~Usa /armero para interactuar", position, 3, 1, 0, new Color(255, 255, 255));
            col.SetData("LSPD_ARMERO", true);

            Vector3 positionn = new Vector3(440.975, -981.5007, 30.6896);
            ColShape coll = NAPI.ColShape.CreateCylinderColShape(positionn, 2, 2);
            NAPI.Marker.CreateMarker(1, positionn.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Multas~n~~w~Usa /pagarmulta para interactuar", positionn, 3, 1, 0, new Color(255, 255, 255));
            coll.SetData("LSPD_MULTAS", true);

            // Vespucci
            Vector3 vposs = new Vector3(-1098.793, -831.0986, 14.282785);
            ColShape vshape2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1098.793, -831.0986, 14.282785), 3, 3);
            vshape2.SetData("LSPD_DUTY_POINT", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Presiona ~r~F6 ~w~para ponerte en servicio", new Vector3(-1098.793, -831.0986, 14.282785), 4, 2, 0, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(1, vposs.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));

            Blip vblip = NAPI.Blip.CreateBlip(new Vector3(-1099.2672, -841.4212, 19.0015));
            vblip.Sprite = 60;
            vblip.Color = 3;
            vblip.Name = "Los Santos Police Department";
            vblip.ShortRange = true;

            Vector3 vposition = new Vector3(-1106.4178, -825.91266, 14.282788);
            ColShape vcol = NAPI.ColShape.CreateCylinderColShape(vposition, 2, 2);
            NAPI.Marker.CreateMarker(1, vposition.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Armero~n~~w~Usa /armero para interactuar", vposition, 3, 1, 0, new Color(255, 255, 255));
            vcol.SetData("LSPD_ARMERO", true);

            Vector3 vpositionn = new Vector3(-1099.2672, -841.4212, 19.0015);
            ColShape vcoll = NAPI.ColShape.CreateCylinderColShape(vpositionn, 2, 2);
            NAPI.Marker.CreateMarker(1, vpositionn.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Multas~n~~w~Usa /pagarmulta para interactuar", vpositionn, 3, 1, 0, new Color(255, 255, 255));
            vcoll.SetData("LSPD_MULTAS", true);

            // Carcel
            Vector3 cposs = new Vector3(1827.2056, 2582.7292, 45.890995);
            ColShape cshape2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(1827.2056, 2582.7292, 45.890995), 3, 3);
            cshape2.SetData("LSPD_DUTY_POINT", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Presiona ~r~F6 ~w~para ponerte en servicio", new Vector3(-1098.793, -831.0986, 14.282785), 4, 2, 0, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(1, cposs.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));

            Blip cblip = NAPI.Blip.CreateBlip(new Vector3(1842.2253, 2583.2366, 45.890953));
            cblip.Sprite = 60;
            cblip.Color = 3;
            cblip.Name = "Los Santos Police Department";
            cblip.ShortRange = true;

            Vector3 cposition = new Vector3(1834.1167, 2577.4856, 45.891);
            ColShape ccol = NAPI.ColShape.CreateCylinderColShape(cposition, 2, 2);
            NAPI.Marker.CreateMarker(1, cposition.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Armero~n~~w~Usa /armero para interactuar", cposition, 3, 1, 0, new Color(255, 255, 255));
            ccol.SetData("LSPD_ARMERO", true);

            Vector3 cpositionn = new Vector3(1842.2253, 2583.2366, 45.890953);
            ColShape ccoll = NAPI.ColShape.CreateCylinderColShape(cpositionn, 2, 2);
            NAPI.Marker.CreateMarker(1, cpositionn.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Multas~n~~w~Usa /pagarmulta para interactuar", cpositionn, 3, 1, 0, new Color(255, 255, 255));
            ccoll.SetData("LSPD_MULTAS", true);

            // Paleto
            Vector3 pposs = new Vector3(-457.16885, 6014.338, 31.716496);
            ColShape pshape2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(-457.16885, 6014.338, 31.716496), 3, 3);
            pshape2.SetData("LSPD_DUTY_POINT", true); // Configurado en Door para no meter mucho code innecesario
            NAPI.TextLabel.CreateTextLabel("Presiona ~r~F6 ~w~para ponerte en servicio", new Vector3(-457.16885, 6014.338, 31.716496), 4, 2, 0, new Color(255, 255, 255));
            NAPI.Marker.CreateMarker(1, pposs.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(202, 24, 24));

            Blip pblip = NAPI.Blip.CreateBlip(new Vector3(-445.99356, 6016.276, 31.716476));
            pblip.Sprite = 60;
            pblip.Color = 3;
            pblip.Name = "Los Santos Police Department";
            pblip.ShortRange = true;

            Vector3 pposition = new Vector3(-431.39264, 6001.1094, 31.716488);
            ColShape pcol = NAPI.ColShape.CreateCylinderColShape(pposition, 2, 2);
            NAPI.Marker.CreateMarker(1, pposition.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Armero~n~~w~Usa /armero para interactuar", pposition, 3, 1, 0, new Color(255, 255, 255));
            pcol.SetData("LSPD_ARMERO", true);

            Vector3 ppositionn = new Vector3(-445.99356, 6016.276, 31.716476);
            ColShape pcoll = NAPI.ColShape.CreateCylinderColShape(ppositionn, 2, 2);
            NAPI.Marker.CreateMarker(1, ppositionn.Subtract(new Vector3(0, 0, 0.9)), new Vector3(), new Vector3(), 1, new Color(0, 162, 255));
            NAPI.TextLabel.CreateTextLabel("~b~Multas~n~~w~Usa /pagarmulta para interactuar", ppositionn, 3, 1, 0, new Color(255, 255, 255));
            pcoll.SetData("LSPD_MULTAS", true);

            //DETECTOR METALES
                    //CARCEL
                    /*
                        ColShape jailScanner = NAPI.ColShape.CreateCylinderColShape(new Vector3(1827.2056, 2582.7292, 45.890995), 3, 3);
                        jailScanner.SetData("LSPD_SCANNER_POINT", true); // Configurado en Door para no meter mucho code innecesario*/


        }
        
        [Command("pagarmulta")]
        public async Task CMD_pagarmulta(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.multas.Count != 0)
            {
                bool pagadas = false;
                foreach (Data.Entities.FineLSPD multa in user.multas)
                {
                    if (!multa.isPaid)
                    {
                        if (await Game.Money.MoneyModel.SubMoney(player, (double)multa.price))
                        {
                            if (player.HasData("LSPD_MULTAS"))
                            {
                                player.SendChatMessage($"~g~[MULTA PAGADA {multa.IdDatabase}]~w~ {multa.reason} | ${multa.price}");
                                multa.isPaid = true;
                                await Main.RegistrarPagoMulta(multa.IdDatabase);
                                Utilities.Notifications.SendNotificationOK(player, "Has pagado tu multa correctamente");
                                pagadas = true;
                            }
                            else Utilities.Notifications.SendNotificationERROR(player, "Tienes que estar en comisaría para pagar tus multas.");

                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "No tienes dinero para pagar tu(s) multa(s)");
                    }
                    
                }
                if(!pagadas) Utilities.Notifications.SendNotificationERROR(player, "Tus multas ya están pagadas");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes multas registradas");
            
        }


        [Command("armero")]
        public void CMD_armero(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.faction == 1)
            {
                if (player.HasData("LSPD_ARMERO"))
                {
                    if (user.rank >= 2)
                    {
                        if (!user.factionDuty) Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
                        else player.TriggerEvent("armeriapd");
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No tienes rango suficiente");
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en el armero");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [RemoteEvent("ActionPDDuty")]
        public void ActionFactionDuty(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (!user.chatStatus)
            {
                if (player.HasData("LSPD_DUTY_POINT"))
                {
                    if (user.faction == 1)
                    {
                        if (!user.factionDuty)
                        {
                            user.factionDuty = true;
                            Utilities.Notifications.SendNotificationOK(player, "Estás en servicio");
                            if(user.hombre == true) {
                                player.SetClothes(6, 10, 0);
                                player.SetClothes(4, 35, 0);
                                player.SetClothes(8, 58, 0);
                                player.SetClothes(11, 16, 0);
                                player.SetClothes(10, 0, 0);
                                player.SetClothes(3, 0, 0);


                                    switch (user.rank)
                                    {
                                        case 1: //CADETE
                                            player.SetClothes(11, 58, 0);
                                            break;
                                        case 2: //PO I
                                            player.SetClothes(5, 9, 0);
                                            break;
                                        case 3: //PO II
                                            player.SetClothes(5, 9, 0);
                                            break;
                                        case 4: //PO III
                                            player.SetClothes(10, 11, 0);
                                            player.SetClothes(5, 9, 0);
                                            break;
                                        case 5: //PO III+
                                            player.SetClothes(10, 11, 1);
                                            player.SetClothes(5, 9, 0);
                                            break;
                                        case 6://Det1
                                            player.SetClothes(10, 11, 4);
                                            player.SetClothes(5, 9, 4);
                                            break;
                                        case 7: //Det2
                                            player.SetClothes(10, 11, 5);
                                            player.SetClothes(5, 9, 4);
                                            break;
                                        case 8: //Det3
                                            player.SetClothes(10, 11, 6);
                                            player.SetClothes(5, 9, 4);
                                            break;
                                        case 9: //Sgt1
                                            player.SetClothes(10, 11, 2);
                                            player.SetClothes(5, 9, 1);
                                            break;
                                        case 10: //Sgt2
                                            player.SetClothes(10, 11, 3);
                                            player.SetClothes(5, 9, 1);
                                            break;

                                        case 11: //Lt.
                                            player.SetClothes(10, 7, 0);
                                            player.SetClothes(5, 9, 2);
                                            break;

                                        case 12: //Cap1
                                            player.SetClothes(10, 7, 1);
                                            player.SetClothes(5, 9, 3);
                                            break;
                                        case 13: //Cap2
                                            player.SetClothes(10, 7, 1);
                                            player.SetClothes(5, 9, 3);
                                            break;

                                        case 14: //Cap3
                                            player.SetClothes(10, 7, 1);
                                            player.SetClothes(5, 9, 3);
                                            break;
                                        case 15: //Cmdte
                                            player.SetClothes(10, 7, 2);
                                            player.SetClothes(5, 9, 5);
                                            break;
                                        case 16: //JA
                                            player.SetClothes(10, 7, 3);
                                            player.SetClothes(5, 9, 6);
                                            break;

                                        case 17: //J
                                            player.SetClothes(10, 7, 4);
                                            player.SetClothes(5, 9, 8);
                                            break;
                                    }
                            }
                            else
                            {
                                player.SetClothes(11,16,0);
                                player.SetClothes(3, 153, 0);
                                player.SetClothes(8, 35, 0);
                                player.SetClothes(6, 24, 0);
                                player.SetClothes(4, 6, 0);
                                switch (user.rank)
                                {
                                    case 1: //CADETE
                                        player.SetClothes(11, 51, 0);
                                        break;
                                    case 2: //PO I
                                        player.SetClothes(5, 9, 0);
                                        break;
                                    case 3: //PO II
                                        player.SetClothes(5, 9, 0);
                                        break;
                                    case 4: //PO III
                                        player.SetClothes(10, 8, 0);
                                        player.SetClothes(5, 9, 0);
                                        break;
                                    case 5: //PO III+
                                        player.SetClothes(10, 8, 1);
                                        player.SetClothes(5, 9, 0);
                                        break;
                                    case 6://Det1
                                        player.SetClothes(10, 8, 4);
                                        player.SetClothes(5, 9, 4);
                                        break;
                                    case 7: //Det2
                                        player.SetClothes(10, 8, 5);
                                        player.SetClothes(5, 9, 4);
                                        break;
                                    case 8: //Det3
                                        player.SetClothes(10, 8, 6);
                                        player.SetClothes(5, 9, 4);
                                        break;
                                    case 9: //Sgt1
                                        player.SetClothes(10, 8, 2);
                                        player.SetClothes(5, 9, 1);
                                        break;
                                    case 10: //Sgt2
                                        player.SetClothes(10, 8, 3);
                                        player.SetClothes(5, 9, 1);
                                        break;

                                    case 11: //Lt.
                                        player.SetClothes(10, 7, 0);
                                        player.SetClothes(5, 9, 2);
                                        break;

                                    case 12: //Cap1
                                        player.SetClothes(10, 7, 1);
                                        player.SetClothes(5, 9, 3);
                                        break;
                                    case 13: //Cap2
                                        player.SetClothes(10, 7, 1);
                                        player.SetClothes(5, 9, 3);
                                        break;

                                    case 14: //Cap3
                                        player.SetClothes(10, 7, 1);
                                        player.SetClothes(5, 9, 3);
                                        break;
                                    case 15: //Cmdte
                                        player.SetClothes(10, 7, 2);
                                        player.SetClothes(5, 9, 5);
                                        break;
                                    case 16: //JA
                                        player.SetClothes(10, 7, 3);
                                        player.SetClothes(5, 9, 6);
                                        break;

                                    case 17: //J
                                        player.SetClothes(10, 7, 4);
                                        player.SetClothes(5, 9, 8);
                                        break;
                                }


                            }
                        } else  
                        {
                            user.factionDuty = false;
                            Utilities.Notifications.SendNotificationOK(player, "No estás en servicio");
                            Utilities.Clothes.ReturnUserClothes(user);
                            return;
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
                }
            }
        }

        [Command("arrestar", "<font color='yellow'>USO:</font> /arrestar (id del usuario) (prision) (minutos)")]
        public async void CMD_arrestar(Player player, int id, int prision, int minutos)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    if (player.HasData("LSPD_CHOOSE_PRISON"))
                    {
                        Player target = Utilities.PlayerId.FindPlayerById(id);
                        if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                        else
                        {
                            switch (prision)
                            {
                                case 1:
                                    target.Position = new Vector3(460.1933, -994.353, 24.91487);
                                    break;

                                case 2:
                                    target.Position = new Vector3(459.1659, -997.8828, 24.91485);
                                    break;

                                case 3:
                                    target.Position = new Vector3(459.4653, -1001.557, 24.91485);
                                    break;
                            }

                            Utilities.Notifications.SendNotificationOK(player, "Has encarcelado a " + target.Name + " por " + minutos + " minutos");
                            Utilities.Notifications.SendNotificationOK(target, "Has sido encarcelado por " + minutos + " minutos");

                            double min = Convert.ToDouble(minutos);
                            int milisecs = Convert.ToInt32(ConvertMinutesToMilliseconds(min));
                            await Task.Delay(milisecs);
                            target.Position = new Vector3(441.2899, -982.4244, 30.6896);
                            Utilities.Notifications.SendNotificationOK(target, "Has cumplido condena correctamente");
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No estás en la prisión");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres policía");
        }

        [Command ("taquillapd")]
        public async Task CMD_Taquillapd (Player player)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.faction == 1 & user.factionDuty & player.HasData("LSPD_DUTY_POINT"))
            {
                Utilities.Notifications.SendNotificationINFO(player, "Abriendo taquilla");
                player.TriggerEvent("tpd", player);
            }
            else
            {
                Utilities.Notifications.SendNotificationERROR(player, "No puedes hacer esto a no ser que formes parte de la PD, estés de servicio y en las taquillas");
            }
        }



        
    }
}

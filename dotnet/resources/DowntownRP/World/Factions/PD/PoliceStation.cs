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

            Vector3 garaje = new Vector3(1253.35, 223.0046, -48.48957);
            Vector3 gEntrada1 = new Vector3(434.0029, -1014.139, 28.75713);
            Vector3 gEntrada2 = new Vector3(450.1055, -1013.28, 28.48885);

            ColShape entrada1 = NAPI.ColShape.CreateCylinderColShape(gEntrada1, 2, 2);
            ColShape entrada2 = NAPI.ColShape.CreateCylinderColShape(gEntrada2, 2, 2);
            ColShape salida = NAPI.ColShape.CreateCylinderColShape(garaje, 2, 2);
            salida.Dimension = 0;
            ColShape salida2 = NAPI.ColShape.CreateCylinderColShape(garaje, 2, 2);
            salida2.Dimension = 2;

            TextLabel lEntrada = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~L ~w~para entrar", gEntrada1, 3, 1, 0, new Color(255, 255, 255));
            //TextLabel lEntrada2 = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~L ~w~para entrar", gEntrada2, 3, 1, 0, new Color(255, 255, 255));
            TextLabel lSalida = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~L ~w~para salir", garaje, 3, 1, 0, new Color(255, 255, 255));
            //TextLabel lSalida2 = NAPI.TextLabel.CreateTextLabel("Pulsa ~b~L ~w~para salir", garaje, 3, 1, 0, new Color(255, 255, 255));
            //lSalida2.Dimension = 2;

            entrada1.SetData("GARAGE_LSPD_ENTRANCE_1", true);
            entrada2.SetData("GARAGE_LSPD_ENTRANCE_2", true);
            salida.SetData("GARAGE_LSPD_EXIT_1", true);
            salida2.SetData("GARAGE_LSPD_EXIT_1", true);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape_PD(ColShape shape, Player player)
        {
            if (shape.HasData("GARAGE_LSPD_ENTRANCE_1"))
            {
                player.SetData("GARAGE_LSPD_ENTRANCE_1", true);
            }

            if (shape.HasData("GARAGE_LSPD_ENTRANCE_2"))
            {
                player.SetData("GARAGE_LSPD_ENTRANCE_2", true);
            }

            if (shape.HasData("GARAGE_LSPD_EXIT_1"))
            {
                player.SetData("GARAGE_LSPD_EXIT_1", true);
            }

            if (shape.HasData("GARAGE_LSPD_EXIT_1"))
            {
                player.SetData("GARAGE_LSPD_EXIT_1", true);
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void OnPlayerExitColShape_PD(ColShape shape, Player player)
        {
            if (shape.HasData("GARAGE_LSPD_ENTRANCE_1"))
            {
                player.ResetData("GARAGE_LSPD_ENTRANCE_1");
            }

            if (shape.HasData("GARAGE_LSPD_ENTRANCE_2"))
            {
                player.ResetData("GARAGE_LSPD_ENTRANCE_2");
            }

            if (shape.HasData("GARAGE_LSPD_EXIT_1"))
            {
                player.ResetData("GARAGE_LSPD_EXIT_1");
            }

            if (shape.HasData("GARAGE_LSPD_EXIT_1"))
            {
                player.ResetData("GARAGE_LSPD_EXIT_1");
            }
        }

        [RemoteEvent("ActionGaragePD")]
        public void RE_ActionGaragePD(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.HasData("GARAGE_LSPD_ENTRANCE_1"))
            {
                player.TriggerEvent("LoadAllIps");
                if (player.IsInVehicle) player.Vehicle.Position = new Vector3(1253.35, 223.0046, -48.48957);
                else player.Position = new Vector3(1253.35, 223.0046, -48.48957);
            }

            if (player.HasData("GARAGE_LSPD_ENTRANCE_2"))
            {
                player.TriggerEvent("LoadAllIps");
                if (player.IsInVehicle)
                {
                    player.Vehicle.Position = new Vector3(1253.35, 223.0046, -48.48957);
                    player.Vehicle.Dimension = 2;
                }
                else
                {
                    player.Position = new Vector3(1253.35, 223.0046, -48.48957); 
                    player.Dimension = 2;
                }
            }

            if (player.HasData("GARAGE_LSPD_EXIT_1"))
            {
                if (player.IsInVehicle) player.Vehicle.Position = new Vector3(434.0029, -1014.139, 28.75713);
                else player.Position = new Vector3(434.0029, -1014.139, 28.75713);
            }

            if (player.HasData("GARAGE_LSPD_EXIT_2"))
            {
                if (player.IsInVehicle)
                {
                    player.Vehicle.Position = new Vector3(450.1055, -1013.28, 28.48885);
                    player.Vehicle.Dimension = 0;
                }
                else
                {
                    player.Position = new Vector3(450.1055, -1013.28, 28.48885); 
                    player.Dimension = 0;
                }
            }
        }

        [Command("pagarmulta")]
        public async Task CMD_pagarmulta(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            Data.Entities.FineLSPD multa = Data.Lists.finesPD.Find(x => x.userid == user.idpj);

            if(multa != null)
            {
                if (!multa.isPaid)
                {
                    if(await Game.Money.MoneyModel.SubMoney(player, (double)multa.price))
                    {
                        player.SendChatMessage($"<font color='green'>[MULTA PAGADA]</font> {multa.reason} | ${multa.price}");
                        multa.isPaid = true;
                        Utilities.Notifications.SendNotificationOK(player, "Has pagado tu multa correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes dinero para pagar tu(s) multa(s)");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "Tus multas ya están pagadas");
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
                    if (user.rank >= 7)
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
                                player.SetOutfit(66);
                                player.SetAccessories(1, 0 ,0);
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
                            player.TriggerEvent("playerLoadPj");
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

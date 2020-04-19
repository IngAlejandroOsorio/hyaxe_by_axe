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
        }

        [Command("pagarmulta")]
        public async Task CMD_pagarmulta(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

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
        public void CMD_armero(Client player, int equipacion)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if(user.faction == 1)
            {
                if (player.HasData("LSPD_ARMERO"))
                {
                    if(!user.factionDuty) Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
                    else Utilities.Notifications.SendNotificationERROR(player, "Función no implementada");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en el armero");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [RemoteEvent("ActionPDDuty")]
        public void ActionFactionDuty(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

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
                            player.SetOutfit(66);
                            return;
                        }

                        if (user.factionDuty)
                        {
                            user.factionDuty = false;
                            Utilities.Notifications.SendNotificationOK(player, "No estás en servicio");
                            NAPI.ClientEvent.TriggerClientEvent(player, "ApplyCharacterFeatures", 0);
                            return;
                        }
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
                }
            }
        }

        [Command("arrestar", "<font color='yellow'>USO:</font> /arrestar (id del usuario) (prision) (minutos)")]
        public async void CMD_arrestar(Client player, int id, int prision, int minutos)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    if (player.HasData("LSPD_CHOOSE_PRISON"))
                    {
                        Client target = Utilities.PlayerId.FindPlayerById(id);
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

    }
}

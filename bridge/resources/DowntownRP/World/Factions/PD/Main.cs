using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.PD
{
    public class Main : Script
    {
        [Command("controlpd")]
        public void CMD_controlpd(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1 || user.adminLv > 0)
            {
                if (user.factionDuty || user.adminLv > 0)
                {
                    if (!player.HasData("LSPD_PROP_PANEL"))
                    {
                        player.SetData("LSPD_PROP_PANEL", true);
                        player.TriggerEvent("open_proplspd_panel");
                        return;
                    }
                    else
                    {
                        player.ResetData("LSPD_PROP_PANEL");
                        player.TriggerEvent("close_proplspd_panel");
                        return;
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres miembro de LSPD");
        }

        [RemoteEvent("S_PropsPolicia")]
        public void S_PropsPolicia(Client player, int type)
        {
            GTANetworkAPI.Object objecto;
            switch (type)
            {
                case 1:
                    objecto = NAPI.Object.CreateObject(2707666095, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 2:
                    objecto = NAPI.Object.CreateObject(939377219, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 3:
                    objecto = NAPI.Object.CreateObject(93871477, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 4:
                    objecto = NAPI.Object.CreateObject(24969275, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 5:
                    objecto = NAPI.Object.CreateObject(4089655941, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 6:
                    objecto = NAPI.Object.CreateObject(1867879106, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 1000:
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    break;
            }
        }

        [Command("borrarcontrol")]
        public void CMD_borrarcontrol(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1 || user.adminLv > 0)
            {
                if (user.factionDuty || user.adminLv > 0)
                {
                    foreach (var prop in NAPI.Pools.GetAllObjects())
                    {
                        if (prop.HasData("LSPD_PROP"))
                        {
                            if ((prop.Position.DistanceTo2D(player.Position) <= 0.5f))
                            {
                                prop.Delete();
                            }
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("ref")]
        public void CMD_ref(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    if (!player.HasData("LSPD_REF_ON"))
                    {
                        player.SetData("LSPD_REF_ON", true);
                        foreach (var lspd in NAPI.Pools.GetAllPlayers())
                        {
                            if (!lspd.HasData("USER_CLASS")) return;
                            Data.Entities.User lsspd = player.GetData("USER_CLASS");

                            if (lsspd.factionDuty)
                            {
                                lspd.TriggerEvent("rosco_blip", player.Name.ToString());
                            }
                        }
                    }
                    else
                    {
                        player.ResetData("LSPD_REF_ON");
                        foreach (var lspd in NAPI.Pools.GetAllPlayers())
                        {
                            if (!lspd.HasData("USER_CLASS")) return;
                            Data.Entities.User lsspd = player.GetData("USER_CLASS");

                            if (lsspd.factionDuty)
                            {
                                lspd.TriggerEvent("rosco_blip_destroy");
                            }
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("borrarcontroltodo")]
        public void CMD_borrarcontroltodo(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1 || user.adminLv > 0)
            {
                if (user.factionDuty || user.adminLv > 0)
                {
                    foreach (var prop in NAPI.Pools.GetAllObjects())
                    {
                        if (prop.HasData("LSPD_PROP"))
                        {
                            prop.Delete();
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("multar")]
        public void CMD_multar(Client player, int id, int dinero, string razon)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    Client target = Utilities.PlayerId.FindPlayerById(id);
                    if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                    else
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User targ = player.GetData("USER_CLASS");

                        Data.Entities.FineLSPD multa = new Data.Entities.FineLSPD()
                        {
                            userid = targ.idpj,
                            reason = razon,
                            price = dinero
                        };

                        Data.Lists.finesPD.Add(multa);

                        Utilities.Notifications.SendNotificationOK(player, $"Has multado al usuario ID {id}");
                        target.SendChatMessage($"<font color='red'>[MULTA]</font> {razon} | ${dinero}");
                        Utilities.Notifications.SendNotificationINFO(target, $"Has recibido una multa, dirígete a comisaría para pagarla");
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("esposar")]
        public async Task CMD_esposar(Client player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    Client target = Utilities.PlayerId.FindPlayerById(id);
                    if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                    else
                    {
                        if(target != player)
                        {
                            target.PlayAnimation("mp_arrest_paired", "cop_p2_back_left", (int)Utilities.AnimationFlags.StopOnLastFrame);
                            player.PlayAnimation("mp_arrest_paired", "crook_p2_back_left", (int)Utilities.AnimationFlags.StopOnLastFrame);
                        }
                        else Utilities.Notifications.SendNotificationERROR(player, "No puedes esposarte a ti mismo");
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }


    }
}

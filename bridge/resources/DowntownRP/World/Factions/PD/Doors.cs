using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.World.Factions.PD
{
    public class Doors : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void RS_DoorPolice()
        {
            ColShape door1 = NAPI.ColShape.CreateCylinderColShape(new Vector3(434.7479, -983.2151, 30.83926), 2, 2);
            door1.SetData("PD_DOOR", "MAIN");

            ColShape door2 = NAPI.ColShape.CreateCylinderColShape(new Vector3(469.9679, -1014.452, 26.53623), 1, 1);
            door2.SetData("PD_DOOR", "BACK");

            ColShape door3 = NAPI.ColShape.CreateCylinderColShape(new Vector3(463.4782, -1003.538, 25.00599), 1, 1);
            door3.SetData("PD_DOOR", "CELL_MAIN");

            ColShape door4 = NAPI.ColShape.CreateCylinderColShape(new Vector3(461.8065, -994.4086, 25.06443), 1, 1);
            door4.SetData("PD_DOOR", "CELL_1");

            ColShape door5 = NAPI.ColShape.CreateCylinderColShape(new Vector3(461.8065, -997.6583, 25.06443), 1, 1);
            door5.SetData("PD_DOOR", "CELL_2");

            ColShape door6 = NAPI.ColShape.CreateCylinderColShape(new Vector3(461.8065, -1001.302, 25.06443), 1, 1);
            door6.SetData("PD_DOOR", "CELL_3");

            ColShape door7 = NAPI.ColShape.CreateCylinderColShape(new Vector3(464.5701, -992.6641, 25.06443), 1, 1);
            door7.SetData("PD_DOOR", "CELL_BACK");

            ColShape door8 = NAPI.ColShape.CreateCylinderColShape(new Vector3(446.5728, -980.0106, 30.8393), 1, 1);
            door8.SetData("PD_DOOR", "OFFICE");

            ColShape door9 = NAPI.ColShape.CreateCylinderColShape(new Vector3(450.1041, -984.0915, 30.8393), 2, 2);
            door9.SetData("PD_DOOR", "ARMOR_DOUBLE");

            ColShape door10 = NAPI.ColShape.CreateCylinderColShape(new Vector3(453.0793, -983.1895, 30.83926), 1, 1);
            door10.SetData("PD_DOOR", "ARMOR");

            ColShape door11 = NAPI.ColShape.CreateCylinderColShape(new Vector3(450.1041, -985.7384, 30.8393), 1, 1);
            door11.SetData("PD_DOOR", "LOCKER");

            ColShape door12 = NAPI.ColShape.CreateCylinderColShape(new Vector3(452.6248, -987.3626, 30.8393), 1, 1);
            door12.SetData("PD_DOOR", "LOCKER1");

            ColShape door13 = NAPI.ColShape.CreateCylinderColShape(new Vector3(461.2865, -985.3206, 30.83926), 1, 1);
            door13.SetData("PD_DOOR", "ROOF");

            ColShape door14 = NAPI.ColShape.CreateCylinderColShape(new Vector3(464.3613, -984.678, 43.83443), 1, 1);
            door14.SetData("PD_DOOR", "ROOF1");

            ColShape door15 = NAPI.ColShape.CreateCylinderColShape(new Vector3(443.4078, -989.4454, 30.8393), 2, 2);
            door15.SetData("PD_DOOR", "BRIEF");

            ColShape door16 = NAPI.ColShape.CreateCylinderColShape(new Vector3(443.0298, -991.941, 30.8393), 2, 2);
            door16.SetData("PD_DOOR", "BRIEF1");

            ColShape door17 = NAPI.ColShape.CreateCylinderColShape(new Vector3(488.8923, -1011.67, 27.14583), 4, 4);
            door17.SetData("PD_DOOR", "GATE");
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void EnterCS_DoorPolice(ColShape shape, Client player)
        {
            if (shape.HasData("PD_DOOR"))
            {
                player.SetData("PD_DOOR", shape.GetData("PD_DOOR"));
                player.SetData("PD_DOOR_COL", shape);
            }

            if (shape.HasData("LSPD_CHOOSE_PRISON"))
            {
                player.SetData("LSPD_CHOOSE_PRISON", true);
            }

            if (shape.HasData("LSPD_DUTY_POINT"))
            {
                player.SetData("LSPD_DUTY_POINT", true);
            }

            if (shape.HasData("GOBIERNO_ENTRAR"))
            {
                player.SetData("GOBIERNO_ENTRAR", true);
            }

            if (shape.HasData("GOBIERNO_SALIR"))
            {
                player.SetData("GOBIERNO_SALIR", true);
            }

            if (shape.HasData("GOBIERNO_GOB"))
            {
                player.SetData("GOBIERNO_GOB", true);
            }

            if (shape.HasData("LSPD_ARMERO"))
            {
                player.SetData("LSPD_ARMERO", true);
            }

            if (shape.HasData("LSPD_MULTAS"))
            {
                player.SetData("LSPD_MULTAS", true);
            }

        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void ExitCS_DoorPolice(ColShape shape, Client player)
        {
            if (shape.HasData("PD_DOOR"))
            {
                player.ResetData("PD_DOOR");
                player.ResetData("PD_DOOR_COL");
            }

            if (shape.HasData("LSPD_CHOOSE_PRISON"))
            {
                player.ResetData("LSPD_CHOOSE_PRISON");
            }

            if (shape.HasData("LSPD_DUTY_POINT"))
            {
                player.ResetData("LSPD_DUTY_POINT");
            }

            if (shape.HasData("GOBIERNO_ENTRAR"))
            {
                player.ResetData("GOBIERNO_ENTRAR");
            }

            if (shape.HasData("GOBIERNO_SALIR"))
            {
                player.ResetData("GOBIERNO_SALIR");
            }

            if (shape.HasData("GOBIERNO_GOB"))
            {
                player.ResetData("GOBIERNO_GOB");
            }

            if (shape.HasData("LSPD_ARMERO"))
            {
                player.ResetData("LSPD_ARMERO");
            }

            if (shape.HasData("LSPD_MULTAS"))
            {
                player.ResetData("LSPD_MULTAS");
            }

        }

        private static ColShape sh;
        [RemoteEvent("PoliceDoorManager")]
        public void PoliceDoorManager(Client player)
        {
            if (!player.HasData("PD_DOOR"))
                return;

            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.chatStatus) return;

            if (user.faction == 1 || user.adminLv > 0)
            {
                switch (player.GetData("PD_DOOR"))
                {
                    case "MAIN":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "BACK":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_back");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_back");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_back");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "CELL_MAIN":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_main");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_cell_main");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_main");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "CELL_1":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_cell_1");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "CELL_2":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_2");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_cell_2");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_2");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "CELL_3":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_3");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_cell_3");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_3");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "CELL_BACK":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_back");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_cell_back");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_cell_back");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "OFFICE":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_office");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_office");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_office");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "ARMOR_DOUBLE":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_armor_double");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_armor_double");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_armor_double");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "ARMOR":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_armor");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_armor");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_armor");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "LOCKER":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_locker");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_locker");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_locker");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "LOCKER1":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_locker1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_locker1");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_locker1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "ROOF":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_roof");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_roof");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_roof");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "ROOF1":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_roof1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_roof1");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_roof1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "BRIEF":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_brief");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_brief");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_brief");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "BRIEF1":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_brief1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_brief1");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_brief1");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;

                    case "GATE":
                        sh = player.GetData("PD_DOOR_COL");
                        if (!sh.HasData("PD_DOOR_STATUS"))
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_gate");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == false)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "close_door_police_gate");
                            sh.SetData("PD_DOOR_STATUS", true);
                            Utilities.Notifications.SendNotificationOK(player, "Has cerrado la puerta");
                            return;
                        }
                        if (sh.GetData("PD_DOOR_STATUS") == true)
                        {
                            NAPI.ClientEvent.TriggerClientEventInDimension(0, "open_door_police_gate");
                            sh.SetData("PD_DOOR_STATUS", false);
                            Utilities.Notifications.SendNotificationOK(player, "Has abierto la puerta");
                            return;
                        }
                        break;
                }
            }
        }

    }
}

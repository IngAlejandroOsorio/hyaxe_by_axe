using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Game.Vehicles
{
    public class Menu : Script
    {
        [RemoteEvent("ActionMenuVehicle")]
        public void RE_ActionMenuVehicle(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                if (!user.isMenuVehicleCefOpen)
                {
                    user.isMenuVehicleCefOpen = true;
                    player.TriggerEvent("OpenVehicleMenu");
                    return;
                }
                else
                {
                    player.TriggerEvent("CloseVehicleMenu");
                    user.isMenuVehicleCefOpen = false;
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No estás en un vehículo");
        }

        [RemoteEvent("SS_CloseVehicleMenu")]
        public void SS_CloseVehicleMenu(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            player.TriggerEvent("CloseVehicleMenu");
            user.isMenuVehicleCefOpen = false;
        }

        [RemoteEvent("SS_SeatbellOn")]
        public void SS_SeatbellOn(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                //player.Seatbelt = true;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Te has puesto el cinturón");

            }
        }

        [RemoteEvent("ActionOpenVehicle")]
        public void RE_ActionOpenVehicle(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (!user.chatStatus)
            {
                foreach (var veh in user.vehicles)
                {
                    if (player.Position.DistanceTo(veh.entity.Position) < 5f)
                    {
                        if (!veh.entity.Locked)
                        {
                            veh.entity.Locked = true;
                            Utilities.Notifications.SendNotificationOK(player, "Has bloqueado tu vehículo");
                            return;
                        }
                        veh.entity.Locked = false;
                        Utilities.Notifications.SendNotificationOK(player, "Has desbloqueado tu vehículo");
                    }
                }
            }
        }

        [RemoteEvent("SS_SeatbellOff")]
        public void SS_SeatbellOff(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                //player.Seatbelt = false;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Te has quitado el cinturón");
            }
        }

        [RemoteEvent("SS_CapoOn")]
        public void SS_CapoOn(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                NAPI.ClientEvent.TriggerClientEventForAll("open_vehicle_hood", veh);
                veh.SetData("HOOD", true);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has abierto el capó");
            }
        }

        [RemoteEvent("SS_CapoOff")]
        public void SS_CapoOff(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                NAPI.ClientEvent.TriggerClientEventForAll("close_vehicle_hood", veh);
                veh.SetData("HOOD", false);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has cerrado el capó");
            }
        }

        [RemoteEvent("SS_MaleteroOn")]
        public void SS_MaleteroOn(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                NAPI.ClientEvent.TriggerClientEventForAll("open_vehicle_trunk", veh);
                veh.SetData("TRUCK", true);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has abierto el maletero");
            }
        }

        [RemoteEvent("SS_MaleteroOff")]
        public void SS_MaleteroOff(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                NAPI.ClientEvent.TriggerClientEventForAll("close_vehicle_trunk", veh);
                veh.SetData("TRUCK", false);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has cerrado el maletero");
            }
        }

        [RemoteEvent("SS_LockOff")] // Esto es cerrar
        public void SS_LockOn(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.Locked = true;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has cerrado el vehículo");
            }
        }

        [RemoteEvent("SS_LockOn")] // Esto es abrir
        public void SS_LockOff(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.Locked = false;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has abierto el vehículo");
            }
        }
    }
}

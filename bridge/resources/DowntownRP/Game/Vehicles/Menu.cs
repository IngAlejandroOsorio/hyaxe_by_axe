using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Game.Vehicles
{
    public class Menu : Script
    {
        [RemoteEvent("ActionMenuVehicle")]
        public void RE_ActionMenuVehicle(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
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
        public void SS_CloseVehicleMenu(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            player.TriggerEvent("CloseVehicleMenu");
            user.isMenuVehicleCefOpen = false;
        }

        [RemoteEvent("SS_SeatbellOn")]
        public void SS_SeatbellOn(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                player.Seatbelt = true;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Te has puesto el cinturón");

            }
        }

        [RemoteEvent("SS_SeatbellOff")]
        public void SS_SeatbellOff(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                player.Seatbelt = false;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Te has quitado el cinturón");
            }
        }

        [RemoteEvent("SS_CapoOn")]
        public void SS_CapoOn(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.OpenDoor(4);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has abierto el capó");
            }
        }

        [RemoteEvent("SS_CapoOff")]
        public void SS_CapoOff(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.CloseDoor(4);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has cerrado el capó");
            }
        }

        [RemoteEvent("SS_MaleteroOn")]
        public void SS_MaleteroOn(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.OpenDoor(5);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has abierto el maletero");
            }
        }

        [RemoteEvent("SS_MaleteroOff")]
        public void SS_MaleteroOff(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.CloseDoor(5);

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has cerrado el maletero");
            }
        }

        [RemoteEvent("SS_LockOn")]
        public void SS_LockOn(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
            if (player.IsInVehicle)
            {
                Vehicle veh = player.Vehicle;
                veh.Locked = true;

                player.TriggerEvent("CloseVehicleMenu");
                user.isMenuVehicleCefOpen = false;
                Utilities.Notifications.SendNotificationOK(player, "Has cerrado el vehículo");
            }
        }

        [RemoteEvent("SS_LockOff")]
        public void SS_LockOff(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");
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

using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Houses
{
    public class Interiors : Script
    {
        public static Vector3 vectorCasaType1 = new Vector3(266.1425, -1006.98, -100.8834);
        public async static void EnterHouse(Data.Entities.House house, Data.Entities.User user)
        {
            await Task.Delay(500);
            user.houseInterior = house;

            switch (house.type)
            {
                case 1:
                    user.entity.Position = vectorCasaType1;
                    break;

                case 2:
                    user.entity.Position = new Vector3(347.2686, -999.2955, -99.19622);
                    break;
            }

            user.entity.Dimension = (uint)house.id;

            user.isInHouseInterior = true;

            house.usersOnInterior.Add(user);

            if (house.owner == user.idpj) user.entity.TriggerEvent("tipOwnerCasa");
        }

        public static void ExitHouse(Data.Entities.House house, Data.Entities.User user)
        {
            user.entity.Position = user.houseInterior.position;
            user.houseInterior = null;
            user.isInHouseInterior = false;
            user.entity.Dimension = 0;
        }

        [RemoteEvent("ActionExitHouse")]
        public void RE_ActionExitHouse(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.isInHouseInterior)
            {
                if(player.Position.DistanceTo(user.houseInterior.labelExit.Position) < 5f)
                {
                    ExitHouse(user.houseInterior, user);
                }
            }
        }

        [RemoteEvent("OpenCloseHouse")]
        public void RE_OpenCloseHouse(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.isInHouse || user.isInHouseInterior)
            {
                if (!user.house.isOpen)
                {
                    if (user.house.owner == user.idpj)
                    {
                        user.house.isOpen = true;
                        Utilities.Notifications.SendNotificationOK(user.entity, "Has abierto la casa");
                    }
                    else Utilities.Notifications.SendNotificationERROR(user.entity, "No eres dueño de esta casa");
                }
                else
                {
                    if (user.house.owner == user.idpj)
                    {
                        user.house.isOpen = false;
                        Utilities.Notifications.SendNotificationOK(user.entity, "Has cerrado la casa");
                    }
                    else Utilities.Notifications.SendNotificationERROR(user.entity, "No eres dueño de esta casa");
                }
            }
        }

        [Command("mirilla")]
        public void CMD_mirilla(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if (user.isInHouseInterior)
            {
                if (!user.isInHouseMirilla)
                {
                    Vector3 position = user.houseInterior.position;

                    player.Position = position;
                    player.SetSharedData("EntityAlpha", 0);
                    player.TriggerEvent("Freeze_player");
                    player.GiveWeapon(WeaponHash.Unarmed,1);

                    user.isInHouseMirilla = true;
                    return;
                }
                EnterHouse(user.houseInterior, user);
                player.SetSharedData("EntityAlpha", 255);
                player.TriggerEvent("Freeze_player");
                player.Position = vectorCasaType1;

                user.isInHouseMirilla = false;
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No estas dentro de una casa para usar la mirilla.");
        }
    }
}

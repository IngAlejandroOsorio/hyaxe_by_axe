using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.World.Houses
{
    public class Interiors : Script
    {
        public static void EnterHouse(Data.Entities.House house, Data.Entities.User user)
        {
            Vector3 position;

            switch (house.type)
            {
                case 1:
                    position = new Vector3(261.4586, -998.8196, -99.00863);
                    break;

                case 2:
                    position = new Vector3(347.2686, -999.2955, -99.19622);
                    break;

                default:
                    position = new Vector3(0, 0, 0);
                    break;
            }

            user.houseInterior = house;
            user.entity.Position = position;
            user.isInHouseInterior = true;

            house.usersOnInterior.Add(user);

            if (house.owner == user.idpj) user.entity.TriggerEvent("tipOwnerCasa");
        }

        public static void ExitHouse(Data.Entities.House house, Data.Entities.User user)
        {
            user.entity.Position = user.houseInterior.position;
            user.houseInterior = null;
            user.isInHouseInterior = false;
        }
    }
}

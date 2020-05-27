using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Utilities
{
    public class Clothes : Script
    {
        public static void ReturnUserClothes(Data.Entities.User user)
        {
            user.entity.SetClothes(3, user.torso, 0);
            user.entity.SetClothes(11, user.topshirt, user.topshirtTexture);
            user.entity.SetClothes(8, user.undershirt, 1);
            user.entity.SetClothes(5, user.paracaidas, 1);
            user.entity.SetClothes(1, user.pasamontañas, 1);
            user.entity.SetAccessories(7, user.accesory, 1);
            user.entity.SetAccessories(4, user.legs, 1);
            user.entity.SetAccessories(6, user.feet, 1);
            user.entity.SetClothes(10, user.decal, 0);
        }
        public static void ReturnUserClothes(Player player)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            user.entity.SetClothes(3, user.torso, 0);
            user.entity.SetClothes(11, user.topshirt, user.topshirtTexture);
            user.entity.SetClothes(8, user.undershirt, 1);
            user.entity.SetClothes(5, user.paracaidas, 1);
            user.entity.SetClothes(1, user.pasamontañas, 1);
            user.entity.SetAccessories(7, user.accesory, 1);
            user.entity.SetAccessories(4, user.legs, 1);
            user.entity.SetAccessories(6, user.feet, 1);
            user.entity.SetClothes(10, user.decal, 0);
        }
    }
}

using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Utilities
{
    public class AdminLVL : Script 
    {

        public static string getAdmLevelName (int Numerico)
        {
            switch (Numerico)
            {
                case 1:
                    return "Soporte";
                case 2:
                    return "Moderador";
                case 3:
                    return "Moderador global";
                case 4:
                    return "Manager";
                case 5:
                    return "Administrador";
                default:
                    return "N/A";
            }
        }

        public static string getAdmLevelName(Player player)
        {
            if (!player.HasData("USER_CLASS")) return "Error";
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            int Numerico = user.adminLv;
            switch (Numerico)
            {
                case 1:
                    return "Ayudante";
                case 2:
                    return "Soporte";
                case 3:
                    return "Moderador";
                case 4:
                    return "Moderador Global";
                case 5:
                    return "Manager";
                case 6:
                    return "Administrador";
                default:
                    return "N/A";
            }
        }

        public static bool PuedeUsarComando (Player player, int level)
        {
            if (!player.HasData("USER_CLASS")) return false;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(user.adminLv >= level)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

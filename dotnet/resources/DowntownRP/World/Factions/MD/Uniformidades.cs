using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace DowntownRP.World.Factions.MD
{
    public class Uniformidades : Script
    {
        public static void Default(Player player)
        {
            if (!player.HasData("USER_CLASS") | player.GetData<Data.Entities.User>("USER_CLASS").faction != 0 )
            {
                return;
            }

            Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");
            if (usr.rank >= 13)
            {
                Blanca(player, usr.rank, usr.hombre);
            }
            else if (usr.rank >= 8 | usr.rank == 2)
            {
                AzulParaM(player, usr.rank, usr.hombre);
            }
            else if (usr.rank >= 3 | usr.rank == 1)
            {
                AzulBombero(player, usr.rank, usr.hombre);
            }
        }

        private static void AzulParaM(Player player, int rank, bool hombre)
        {
            DowntownRP.Utilities.Clothes.ReturnUserClothes(player);
            player.SetClothes(11, 17, 3);
            if (rank >= 8)
            {
                player.SetClothes(5, 9, 9);
            }
            else if (rank >= 3)
            {
                player.SetClothes(5, 9, 10);
            }

            int idCuello = 7;
            if (hombre)
            {
                player.SetClothes(6, 54, 0);
                player.SetClothes(8, 129, 0);
                player.SetClothes(4, 28, 0);
                player.SetClothes(3, 12, 0);

            }
            else
            {
                player.SetClothes(3, 3, 0);
                player.SetClothes(8, 159, 0);
                player.SetClothes(6, 24, 0);
                player.SetClothes(4, 6, 0);
            }

            switch (rank)
            {
                case 9: player.SetClothes(10, 10, 0); break;     //Med II
                case 10: player.SetClothes(10, 10, 2); break;     //Med III
                case 11: player.SetClothes(10, 10, 4); break;     //Esp I
                case 12: player.SetClothes(10, 10, 5); break;     //Esp II
                case 13: player.SetClothes(10, 10, 6); break;     //Supervisor
                case 14: player.SetClothes(10, 10, 3); break;     //Sargento
                case 15: player.SetClothes(10, idCuello, 6); break;     //tte.
                case 16: player.SetClothes(10, idCuello, 7); break;     //Cpt.
                case 17: player.SetClothes(10, idCuello, 11); break;     //Jefe. 
            }

        }


        private static void Blanca(Player player, int rank, bool hombre)
        {
            DowntownRP.Utilities.Clothes.ReturnUserClothes(player);
            player.SetClothes(11, 18, 3);
            if (rank >= 8)
            {
                player.SetClothes(5, 9, 9);
            }
            else if (rank >= 3)
            {
                player.SetClothes(5, 9, 10);
            }
            int idCuello = 9;
            if (hombre)
            {

                player.SetClothes(6, 54, 0);
                player.SetClothes(8, 129, 0);
                player.SetClothes(4, 28, 0);
                player.SetClothes(3, 12, 0);

            }
            else
            {
                player.SetClothes(3, 3, 0);
                player.SetClothes(8, 159, 0);
                player.SetClothes(6, 24, 0);
                player.SetClothes(4, 6, 0);

            }

            switch (rank)
            {
                case 4: player.SetClothes(10, 10, 0); break;     //Bombero II
                case 5: player.SetClothes(10, 10, 2); break;     //Bombero III
                case 6: player.SetClothes(10, 10, 4); break;     //Ing I
                case 7: player.SetClothes(10, 10, 5); break;     //Ing II

                case 9: player.SetClothes(10, 10, 0); break;     //Med II
                case 10: player.SetClothes(10, 10, 2); break;     //Med III
                case 11: player.SetClothes(10, 10, 4); break;     //Esp I
                case 12: player.SetClothes(10, 10, 5); break;     //Esp II
                case 13: player.SetClothes(10, 10, 6); break;     //Supervisor
                case 14: player.SetClothes(10, 10, 3); break;     //Sargento

                case 15: player.SetClothes(10, idCuello, 6); break;     //tte.
                case 16: player.SetClothes(10, idCuello, 7); break;     //Cpt.
                case 17: player.SetClothes(10, idCuello, 11); break;     //Jefe. 
            }
        }

        public static void AzulBombero(Player player, int rank, bool hombre)
        {
            DowntownRP.Utilities.Clothes.ReturnUserClothes(player);
            player.SetClothes(11, 17, 2);
            if (rank >= 8)
            {
                player.SetClothes(5, 9, 9);
            }
            else if (rank >= 3)
            {
                player.SetClothes(5, 9, 10);
            }
            int idCuello = 7;
            if (hombre)
            {

                player.SetClothes(6, 54, 0);
                player.SetClothes(8, 129, 0);
                player.SetClothes(4, 120, 0);
                player.SetClothes(3, 12, 0);

            }
            else
            {


            }

            switch (rank)
            {
                case 4: player.SetClothes(10, 10, 0); break;     //Bombero II
                case 5: player.SetClothes(10, 10, 2); break;     //Bombero III
                case 6: player.SetClothes(10, 10, 4); break;     //Ing I
                case 7: player.SetClothes(10, 10, 5); break;     //Ing II

                case 9: player.SetClothes(10, 10, 0); break;     //Med II
                case 10: player.SetClothes(10, 10, 2); break;     //Med III
                case 11: player.SetClothes(10, 10, 4); break;     //Esp I
                case 12: player.SetClothes(10, 10, 5); break;     //Esp II

                case 13: player.SetClothes(10, 10, 6); break;     //Supervisor
                case 14: player.SetClothes(10, 10, 3); break;     //Sargento
                case 15: player.SetClothes(10, idCuello, 6); break;     //tte.
                case 16: player.SetClothes(10, idCuello, 7); break;     //Cpt.
                case 17: player.SetClothes(10, idCuello, 11); break;     //Jefe. 
            }

        }
        
        public static void contraincendios (Player player, int rank, bool eshombre)
        {
            DowntownRP.Utilities.Clothes.ReturnUserClothes(player);

            if (eshombre)
            {
                player.SetClothes(4, 120, 0);
                player.SetClothes(10, 69, 0);
                player.SetClothes(11, 317, 0);
            }
            else
            {
                player.SetClothes(4, 127, 0);

            }
        }


        [RemoteEvent("RecTaquillaMD")]
        public static void RecTaquillaMD(Player player, int Categoria, int Item)
        {
            if(Categoria == 0)
            {
                Data.Entities.User usr = player.GetData<Data.Entities.User>("USER_CLASS");

                switch (Item)
                {
                    case 0:
                        AzulBombero(player, usr.rank, usr.hombre);
                        break;
                    case 1:
                        contraincendios(player, usr.rank, usr.hombre);
                        break;
                    case 2:
                        AzulParaM(player, usr.rank, usr.hombre);
                        break;
                    case 3:
                        Blanca(player, usr.rank, usr.hombre);
                        break;
                }
            }
            else
            {
                switch (Item)
                {
                    case 0: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Hatchet); break;
                    case 1: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Battleaxe); break;
                    case 2: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Wrench); break;
                    case 3: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Crowbar); break;
                    case 4: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Hammer); break;
                    case 5: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Flashlight); break;
                    case 6: DowntownRP.World.Factions.PD.Main.DarArma(player, WeaponHash.Fireextinguisher); break;

                }
            }
        }
    }
}

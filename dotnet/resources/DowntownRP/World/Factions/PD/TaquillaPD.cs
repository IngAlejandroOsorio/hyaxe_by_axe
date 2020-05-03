using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using DowntownRP.Utilities.Outfits;

namespace DowntownRP.World.Factions.PD
{
    public class TaquillaPD : Script
    {
        public void taquillamanual (Player pl, int Categoria, int Item)
        {
            EV_RecTaquillaPD(pl, Categoria, Item);
        }

        [RemoteEvent("RecTaquillaPD")]
        public async Task EV_RecTaquillaPD(Player player, int Categoria, int Item)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (Categoria == 0)
            {
                if (Item == 0)
                {
                    if (user.hombre == true)
                    {
                        player.SetOutfit(66);
                        player.SetAccessories(1, 0, 0);
                        player.SetClothes(11, 18, 0);
                        player.SetClothes(10, 0, 0);
                        player.SetClothes(3, 12, 0);


                        switch (user.rank)
                        {
                            case 1: //CADETE
                                break;
                            case 2: //PO I
                                player.SetClothes(5, 9, 0);
                                break;
                            case 3: //PO II
                                player.SetClothes(5, 9, 0);
                                break;
                            case 4: //PO III
                                player.SetClothes(10, 11, 0);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 5: //PO III+
                                player.SetClothes(10, 11, 1);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 6://Det1
                                player.SetClothes(10, 11, 4);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 7: //Det2
                                player.SetClothes(10, 11, 5);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 8: //Det3
                                player.SetClothes(10, 11, 6);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 9: //Sgt1
                                player.SetClothes(10, 11, 2);
                                player.SetClothes(5, 9, 1);
                                break;
                            case 10: //Sgt2
                                player.SetClothes(10, 11, 3);
                                player.SetClothes(5, 9, 1);
                                break;

                            case 11: //Lt.
                                player.SetClothes(10, 7, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 7, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 7, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 7, 4);
                                player.SetClothes(5, 9, 8);
                                break;
                        }
                    }
                    else
                    {
                        player.SetClothes(11, 18, 0);
                        player.SetClothes(3, 3, 0);
                        player.SetClothes(8, 35, 0);
                        player.SetClothes(6, 24, 0);
                        player.SetClothes(4, 6, 0);
                        switch (user.rank)
                        {
                            case 1: //CADETE
                                break;
                            case 2: //PO I
                                player.SetClothes(5, 9, 0);
                                break;
                            case 3: //PO II
                                player.SetClothes(5, 9, 0);
                                break;
                            case 4: //PO III
                                player.SetClothes(10, 8, 0);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 5: //PO III+
                                player.SetClothes(10, 8, 1);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 6://Det1
                                player.SetClothes(10, 8, 4);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 7: //Det2
                                player.SetClothes(10, 8, 5);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 8: //Det3
                                player.SetClothes(10, 8, 6);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 9: //Sgt1
                                player.SetClothes(10, 8, 2);
                                player.SetClothes(5, 9, 1);
                                break;
                            case 10: //Sgt2
                                player.SetClothes(10, 8, 3);
                                player.SetClothes(5, 9, 1);
                                break;

                            case 11: //Lt.
                                player.SetClothes(10, 7, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 7, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 7, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 7, 4);
                                player.SetClothes(5, 9, 8);
                                break;
                        }
                    }
                }
                else if (Item == 3)
                {
                    if (user.hombre == true)
                    {
                        player.SetOutfit(66);
                        player.SetAccessories(1, 0, 0);
                        player.SetClothes(11, 16, 0);
                        player.SetClothes(10, 0, 0);
                        player.SetClothes(3, 0, 0);


                        switch (user.rank)
                        {
                            case 1: //CADETE
                                player.SetClothes(11, 58, 0);
                                break;
                            case 2: //PO I
                                player.SetClothes(5, 9, 0);
                                break;
                            case 3: //PO II
                                player.SetClothes(5, 9, 0);
                                break;
                            case 4: //PO III
                                player.SetClothes(10, 11, 0);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 5: //PO III+
                                player.SetClothes(10, 11, 1);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 6://Det1
                                player.SetClothes(10, 11, 4);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 7: //Det2
                                player.SetClothes(10, 11, 5);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 8: //Det3
                                player.SetClothes(10, 11, 6);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 9: //Sgt1
                                player.SetClothes(10, 11, 2);
                                player.SetClothes(5, 9, 1);
                                break;
                            case 10: //Sgt2
                                player.SetClothes(10, 11, 3);
                                player.SetClothes(5, 9, 1);
                                break;

                            case 11: //Lt.
                                player.SetClothes(10, 7, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 7, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 7, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 7, 4);
                                player.SetClothes(5, 9, 8);
                                break;
                        }
                    }
                    else
                    {
                        player.SetClothes(11, 16, 0);
                        player.SetClothes(3, 153, 0);
                        player.SetClothes(8, 35, 0);
                        player.SetClothes(6, 24, 0);
                        player.SetClothes(4, 6, 0);
                        switch (user.rank)
                        {
                            case 1: //CADETE
                                player.SetClothes(11, 51, 0);
                                break;
                            case 2: //PO I
                                player.SetClothes(5, 9, 0);
                                break;
                            case 3: //PO II
                                player.SetClothes(5, 9, 0);
                                break;
                            case 4: //PO III
                                player.SetClothes(10, 8, 0);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 5: //PO III+
                                player.SetClothes(10, 8, 1);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 6://Det1
                                player.SetClothes(10, 8, 4);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 7: //Det2
                                player.SetClothes(10, 8, 5);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 8: //Det3
                                player.SetClothes(10, 8, 6);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 9: //Sgt1
                                player.SetClothes(10, 8, 2);
                                player.SetClothes(5, 9, 1);
                                break;
                            case 10: //Sgt2
                                player.SetClothes(10, 8, 3);
                                player.SetClothes(5, 9, 1);
                                break;

                            case 11: //Lt.
                                player.SetClothes(10, 7, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 7, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 7, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 7, 4);
                                player.SetClothes(5, 9, 8);
                                break;
                        }
                    }

                }
                else if (Item == 4)
                {
                    if (user.hombre == true)
                    {
                        player.SetOutfit(66);
                        player.SetAccessories(1, 0, 0);
                        player.SetClothes(11, 16, 1);
                        player.SetClothes(10, 0, 0);
                        player.SetClothes(3, 0, 0);


                        switch (user.rank)
                        {
                            case 1: //CADETE
                                player.SetClothes(11, 58, 0);
                                break;
                            case 2: //PO I
                                player.SetClothes(5, 9, 0);
                                break;
                            case 3: //PO II
                                player.SetClothes(5, 9, 0);
                                break;
                            case 4: //PO III
                                player.SetClothes(10, 11, 0);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 5: //PO III+
                                player.SetClothes(10, 11, 1);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 6://Det1
                                player.SetClothes(10, 11, 4);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 7: //Det2
                                player.SetClothes(10, 11, 5);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 8: //Det3
                                player.SetClothes(10, 11, 6);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 9: //Sgt1
                                player.SetClothes(10, 11, 2);
                                player.SetClothes(5, 9, 1);
                                break;
                            case 10: //Sgt2
                                player.SetClothes(10, 11, 3);
                                player.SetClothes(5, 9, 1);
                                break;

                            case 11: //Lt.
                                player.SetClothes(10, 7, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 7, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 7, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 7, 4);
                                player.SetClothes(5, 9, 8);
                                break;
                        }
                    }
                    else
                    {
                        player.SetClothes(11, 16, 1);
                        player.SetClothes(3, 153, 0);
                        player.SetClothes(8, 35, 0);
                        player.SetClothes(6, 24, 0);
                        player.SetClothes(4, 6, 0);
                        switch (user.rank)
                        {
                            case 1: //CADETE
                                player.SetClothes(11, 51, 0);
                                break;
                            case 2: //PO I
                                player.SetClothes(5, 9, 0);
                                break;
                            case 3: //PO II
                                player.SetClothes(5, 9, 0);
                                break;
                            case 4: //PO III
                                player.SetClothes(10, 8, 0);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 5: //PO III+
                                player.SetClothes(10, 8, 1);
                                player.SetClothes(5, 9, 0);
                                break;
                            case 6://Det1
                                player.SetClothes(10, 8, 4);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 7: //Det2
                                player.SetClothes(10, 8, 5);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 8: //Det3
                                player.SetClothes(10, 8, 6);
                                player.SetClothes(5, 9, 4);
                                break;
                            case 9: //Sgt1
                                player.SetClothes(10, 8, 2);
                                player.SetClothes(5, 9, 1);
                                break;
                            case 10: //Sgt2
                                player.SetClothes(10, 8, 3);
                                player.SetClothes(5, 9, 1);
                                break;

                            case 11: //Lt.
                                player.SetClothes(10, 7, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 7, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 7, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 7, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 7, 4);
                                player.SetClothes(5, 9, 8);
                                break;
                        }
                    }
                }
            }
        }
    }
}

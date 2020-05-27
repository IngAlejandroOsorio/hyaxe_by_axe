using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions.PD
{
    public class TaquillaPD : Script
    {
        public void taquillamanual (Player pl, int Categoria, int Item)
        {
            EV_RecTaquillaPD(pl, Categoria, Item);
        }

        [Command ("tpd")]
        public void tpd (Player pl, int Uniformidad = -1)
        {
            if (pl.GetData<Data.Entities.User>("USER_CLASS").faction != 1 |!(pl.HasData("LSPD_DUTY_POINT")|!pl.GetData<Data.Entities.User>("USER_CLASS").factionDuty)) return;
            if(Uniformidad == -1)
            {
                pl.SendChatMessage("~r~UNIFORMIDADES");
                pl.SendChatMessage("~g~0 ~b~ A");
                pl.SendChatMessage("~g~1 ~b~ B");
                pl.SendChatMessage("~g~2 ~b~ BM");
                pl.SendChatMessage("~g~3 ~b~ C");
                pl.SendChatMessage("~g~4 ~b~ CM");
                pl.SendChatMessage("~g~5 ~b~ Paisano");
                pl.SendChatMessage("~g~6 ~b~ SWAT");
                pl.SendChatMessage("~g~7 ~b~ TEDAX");
                return;
            }     
            
             EV_RecTaquillaPD(pl, 0, Uniformidad);
            
            
        }


        [RemoteEvent("RecTaquillaPD")]
        public async Task EV_RecTaquillaPD(Player player, int Categoria, int Item)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if(Categoria == -1)
            {
                Utilities.Notifications.SendNotificationINFO(player, $"Taquilla cerrada con {Item} items");
            }else if (Categoria == 0)
            {
                player.SetClothes(1, 0, 0);
                player.SetClothes(9, 0, 0);
                if (Item == 0)
                {
                    if (user.hombre == true)
                    {
                        player.SetClothes(6, 10, 0);
                        player.SetClothes(4, 35, 0);
                        player.SetClothes(8, 58, 0);
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
                                player.SetClothes(10, 9, 0);
                                player.SetClothes(5, 9, 2);
                                break;

                            case 12: //Cap1
                                player.SetClothes(10, 9, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 13: //Cap2
                                player.SetClothes(10, 9, 1);
                                player.SetClothes(5, 9, 3);
                                break;

                            case 14: //Cap3
                                player.SetClothes(10, 9, 1);
                                player.SetClothes(5, 9, 3);
                                break;
                            case 15: //Cmdte
                                player.SetClothes(10, 9, 2);
                                player.SetClothes(5, 9, 5);
                                break;
                            case 16: //JA
                                player.SetClothes(10, 9, 3);
                                player.SetClothes(5, 9, 6);
                                break;

                            case 17: //J
                                player.SetClothes(10, 9, 4);
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

                else if (Item == 1)  //B NORMAL
                {
                    if (user.hombre)
                    {
                        player.SetClothes(11, 17, 0);
                        player.SetClothes(3, 12, 0);
                        player.SetClothes(6, 10, 0);
                        player.SetClothes(4, 35, 0);
                        player.SetClothes(8, 58, 0);
                        switch (user.rank)
                        {
                            case 1: break; //CADETE
                            case 2: player.SetClothes(5, 9, 0); break; //PO I 
                            case 3: player.SetClothes(5, 9, 0); break; //PO II
                            case 4: player.SetClothes(5, 9, 0); player.SetClothes(10,10,0); break; //PO III 
                            case 5: player.SetClothes(5, 9, 0); player.SetClothes(10, 10, 1); break; //PO III+ 
                            case 6: player.SetClothes(5, 9, 4); player.SetClothes(10, 10, 4); break; //Det I
                            case 7: player.SetClothes(5, 9, 4); player.SetClothes(10, 10, 5); break; //Det II
                            case 8: player.SetClothes(5, 9, 4); player.SetClothes(10, 10, 6); break; //Det III
                            case 9: player.SetClothes(5, 9, 1); player.SetClothes(10, 10, 2); break; //Sgt. I
                            case 10: player.SetClothes(5, 9, 1); player.SetClothes(10, 10, 3); break; //Sgt. II
                            case 11: player.SetClothes(5, 9, 2); player.SetClothes(10, 7, 0); break; //Lt
                            case 12: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt I
                            case 13: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt II
                            case 14: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt III
                            case 15: player.SetClothes(5, 9, 5); player.SetClothes(10, 7, 2); break; //Cmdte
                            case 16: player.SetClothes(5, 9, 7); player.SetClothes(10, 7, 2); break; //JA
                            case 17: player.SetClothes(5, 9, 8); player.SetClothes(10, 7, 4); break; //J

                        }
                    }
                    else
                    {
                        player.SetClothes(11, 17, 0);
                        player.SetClothes(3, 3, 0);
                        player.SetClothes(6, 29, 0);
                        player.SetClothes(4, 34, 0);
                        player.SetClothes(8, 35, 0);

                        switch (user.rank)
                        {
                            case 1: break; //CADETE
                            case 2: player.SetClothes(5, 9, 0); break; //PO I 
                            case 3: player.SetClothes(5, 9, 0); break; //PO II
                            case 4: player.SetClothes(5, 9, 0); player.SetClothes(10, 9, 0); break; //PO III 
                            case 5: player.SetClothes(5, 9, 0); player.SetClothes(10, 9, 1); break; //PO III+ 
                            case 6: player.SetClothes(5, 9, 4); player.SetClothes(10, 9, 4); break; //Det I
                            case 7: player.SetClothes(5, 9, 4); player.SetClothes(10, 9, 5); break; //Det II
                            case 8: player.SetClothes(5, 9, 4); player.SetClothes(10, 9, 6); break; //Det III
                            case 9: player.SetClothes(5, 9, 1); player.SetClothes(10, 9, 2); break; //Sgt. I
                            case 10: player.SetClothes(5, 9, 1); player.SetClothes(10, 9, 3); break; //Sgt. II
                            case 11: player.SetClothes(5, 9, 2); player.SetClothes(10, 7, 0); break; //Lt
                            case 12: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt I
                            case 13: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt II
                            case 14: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt III
                            case 15: player.SetClothes(5, 9, 5); player.SetClothes(10, 7, 2); break; //Cmdte
                            case 16: player.SetClothes(5, 9, 7); player.SetClothes(10, 7, 2); break; //JA
                            case 17: player.SetClothes(5, 9, 8); player.SetClothes(10, 7, 4); break; //J

                        }
                    }
                }

                else if (Item == 2)  //B MOTORISTA
                {
                    if (user.hombre)
                    {
                        player.SetClothes(11, 17, 1);
                        player.SetClothes(3, 12, 0);
                        player.SetClothes(6, 10, 0);
                        player.SetClothes(4, 35, 0);
                        player.SetClothes(8, 58, 0);
                        switch (user.rank)
                        {
                            case 1: break; //CADETE
                            case 2: player.SetClothes(5, 9, 0); break; //PO I 
                            case 3: player.SetClothes(5, 9, 0); break; //PO II
                            case 4: player.SetClothes(5, 9, 0); player.SetClothes(10, 10, 0); break; //PO III 
                            case 5: player.SetClothes(5, 9, 0); player.SetClothes(10, 10, 1); break; //PO III+ 
                            case 6: player.SetClothes(5, 9, 4); player.SetClothes(10, 10, 4); break; //Det I
                            case 7: player.SetClothes(5, 9, 4); player.SetClothes(10, 10, 5); break; //Det II
                            case 8: player.SetClothes(5, 9, 4); player.SetClothes(10, 10, 6); break; //Det III
                            case 9: player.SetClothes(5, 9, 1); player.SetClothes(10, 10, 2); break; //Sgt. I
                            case 10: player.SetClothes(5, 9, 1); player.SetClothes(10, 10, 3); break; //Sgt. II
                            case 11: player.SetClothes(5, 9, 2); player.SetClothes(10, 7, 0); break; //Lt
                            case 12: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt I
                            case 13: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt II
                            case 14: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt III
                            case 15: player.SetClothes(5, 9, 5); player.SetClothes(10, 7, 2); break; //Cmdte
                            case 16: player.SetClothes(5, 9, 7); player.SetClothes(10, 7, 2); break; //JA
                            case 17: player.SetClothes(5, 9, 8); player.SetClothes(10, 7, 4); break; //J

                        }
                    }
                    else
                    {
                        player.SetClothes(11, 17, 1);
                        player.SetClothes(3, 3, 0);
                        player.SetClothes(6, 29, 0);
                        player.SetClothes(4, 34, 0);
                        player.SetClothes(8, 35, 0);

                        switch (user.rank)
                        {
                            case 1: break; //CADETE
                            case 2: player.SetClothes(5, 9, 0); break; //PO I 
                            case 3: player.SetClothes(5, 9, 0); break; //PO II
                            case 4: player.SetClothes(5, 9, 0); player.SetClothes(10, 9, 0); break; //PO III 
                            case 5: player.SetClothes(5, 9, 0); player.SetClothes(10, 9, 1); break; //PO III+ 
                            case 6: player.SetClothes(5, 9, 4); player.SetClothes(10, 9, 4); break; //Det I
                            case 7: player.SetClothes(5, 9, 4); player.SetClothes(10, 9, 5); break; //Det II
                            case 8: player.SetClothes(5, 9, 4); player.SetClothes(10, 9, 6); break; //Det III
                            case 9: player.SetClothes(5, 9, 1); player.SetClothes(10, 9, 2); break; //Sgt. I
                            case 10: player.SetClothes(5, 9, 1); player.SetClothes(10, 9, 3); break; //Sgt. II
                            case 11: player.SetClothes(5, 9, 2); player.SetClothes(10, 7, 0); break; //Lt
                            case 12: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt I
                            case 13: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt II
                            case 14: player.SetClothes(5, 9, 3); player.SetClothes(10, 7, 1); break; //Cpt III
                            case 15: player.SetClothes(5, 9, 5); player.SetClothes(10, 7, 2); break; //Cmdte
                            case 16: player.SetClothes(5, 9, 7); player.SetClothes(10, 7, 2); break; //JA
                            case 17: player.SetClothes(5, 9, 8); player.SetClothes(10, 7, 4); break; //J

                        }
                    }
                }

                else if (Item == 3)
                {
                    if (user.hombre == true)
                    {
                        player.SetClothes(6, 10, 0);
                        player.SetClothes(4, 35, 0);
                        player.SetClothes(8, 58, 0);
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
                        player.SetClothes(6, 10, 0);
                        player.SetClothes(4, 35, 0);
                        player.SetClothes(8, 58, 0);
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
                        player.SetClothes(6, 29, 0);
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
                }else if (Item == 5)    //PAISANO
                {
                    Utilities.Clothes.ReturnUserClothes(user);
                    if (user.hombre) {
                        player.SetClothes(8, 130, 0);
                    }
                    else
                    {
                        player.SetClothes(8, 160, 0);
                    }
                }else if (Item == 6)    //METRO SWAT
                {
                    player.SetClothes(10, 0, 0);
                    if (user.hombre)
                    {

                        player.SetClothes(9, 10, 0);
                        player.SetClothes(4, 34, 0);
                        player.SetClothes(6, 54, 0);
                        player.SetClothes(8, 58, 0);
                        player.SetClothes(11, 17, 0);
                        player.SetClothes(3, 12, 0);


                    }
                    else
                    {
                        player.SetClothes(4, 33, 0);
                        player.SetClothes(6, 25, 0);
                        player.SetClothes(9, 8, 0);
                        player.SetClothes(8, 159, 0);
                        player.SetClothes(11, 17, 0);
                        player.SetClothes(3, 3, 0);


                    }
                    player.SetClothes(1, 52, 0);
                    player.Armor = 75;
                }
                else if (Item == 7)    //METRO TEDAX
                {
                    if (user.hombre)
                    {

                        player.SetClothes(9, 10, 0);
                        player.SetClothes(4, 34, 0);
                        player.SetClothes(6, 54, 0);
                        player.SetClothes(8, 97, 0);
                        player.SetClothes(11, 17, 0);
                        player.SetAccessories(1, 16, 0);

                    }
                    else
                    {
                        player.SetClothes(4, 33, 0);
                        player.SetClothes(6, 25, 0);
                        player.SetClothes(9, 10, 0);
                        player.SetClothes(8, 105, 0);
                        player.SetClothes(11, 17, 0);

                    }
                    player.SetClothes(1, 52, 0);
                    player.Armor = 75;
                }
            }
            else if (Categoria == 1)
            {
                if(Item == 0)
                {
                    Main.DarArma(player, WeaponHash.Pistol, 36);
                }else if(Item == 1)
                {
                    if(Utilities.PlayerId.FindUserById(player.Value).rank >= 4)
                    {
                        Main.DarArma(player, WeaponHash.Combatpistol, 36);
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No tienes rango para cojer esta arma");
                    }
                }
                else if (Item == 2)
                {
                    if (Utilities.PlayerId.FindUserById(player.Value).rank >= 6)
                    {
                        Main.DarArma(player, WeaponHash.Snspistol, 18);
                    }
                    else
                    {
                        Utilities.Notifications.SendNotificationERROR(player, "No tienes rango para cojer esta arma");
                    }
                }
                else if (Item == 3)
                {
                    Main.DarArma(player, WeaponHash.Stungun);
                }
                else if (Item == 4)
                {
                    Main.DarArma(player, WeaponHash.Nightstick);
                }
                else if (Item == 5)
                {
                    Main.DarArma(player, WeaponHash.Flashlight);
                }
                
            }
        }
    }
}

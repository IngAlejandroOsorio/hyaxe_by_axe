using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DowntownRP.Data.Entities;
using MySql.Data.MySqlClient;

namespace DowntownRP.World.Factions.PD
{
    public class Main : Script
    {
        [Command("controlpd")]
        public void CMD_controlpd(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1 || user.adminLv > 0)
            {
                if (user.factionDuty || user.adminLv > 0)
                {
                    if (!player.HasData("LSPD_PROP_PANEL"))
                    {
                        player.SetData("LSPD_PROP_PANEL", true);
                        player.TriggerEvent("open_proplspd_panel");
                        return;
                    }
                    else
                    {
                        player.ResetData("LSPD_PROP_PANEL");
                        player.TriggerEvent("close_proplspd_panel");
                        return;
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres miembro de LSPD");
        }

        [RemoteEvent("S_PropsPolicia")]
        public void S_PropsPolicia(Player player, int type)
        {
            GTANetworkAPI.Object objecto;
            switch (type)
            {
                case 1:
                    objecto = NAPI.Object.CreateObject(2707666095, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 2:
                    objecto = NAPI.Object.CreateObject(939377219, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 3:
                    objecto = NAPI.Object.CreateObject(93871477, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 4:
                    objecto = NAPI.Object.CreateObject(24969275, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 5:
                    objecto = NAPI.Object.CreateObject(4089655941, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 6:
                    objecto = NAPI.Object.CreateObject(1867879106, player.Position.Subtract(new Vector3(0, 0, 1)), player.Rotation);
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    objecto.SetData("LSPD_PROP", true);
                    break;

                case 1000:
                    player.ResetData("LSPD_PROP_PANEL");
                    player.TriggerEvent("close_proplspd_panel");
                    break;
            }
        }

        [Command("proppd")]
        public void CMD_propPD (Player player, int tipo)
        {
            S_PropsPolicia(player, tipo);
        }

        [Command("borrarcontrol")]
        public void CMD_borrarcontrol(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1 || user.adminLv > 0)
            {
                if (user.factionDuty || user.adminLv > 0)
                {
                    foreach (var prop in NAPI.Pools.GetAllObjects())
                    {
                        if (prop.HasData("LSPD_PROP"))
                        {
                            if ((prop.Position.DistanceTo2D(player.Position) <= 0.5f))
                            {
                                prop.Delete();
                            }
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("ref")]
        public void CMD_ref(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    if (!player.HasData("LSPD_REF_ON"))
                    {
                        player.SetData("LSPD_REF_ON", true);
                        foreach (var lspd in NAPI.Pools.GetAllPlayers())
                        {
                            if (!lspd.HasData("USER_CLASS")) return;
                            Data.Entities.User lsspd = player.GetData<Data.Entities.User>("USER_CLASS");

                            if (lsspd.faction == 1 && lsspd.factionDuty)
                            {
                                lspd.TriggerEvent("rosco_blip", player.Name.ToString());
                            }
                        }
                    }
                    else
                    {
                        player.ResetData("LSPD_REF_ON");
                        foreach (var lspd in NAPI.Pools.GetAllPlayers())
                        {
                            if (!lspd.HasData("USER_CLASS")) return;
                            Data.Entities.User lsspd = player.GetData<Data.Entities.User>("USER_CLASS");

                            if (lsspd.factionDuty)
                            {
                                lspd.TriggerEvent("rosco_blip_destroy");
                            }
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("borrarcontroltodo")]
        public void CMD_borrarcontroltodo(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1 || user.adminLv > 0)
            {
                if (user.factionDuty || user.adminLv > 0)
                {
                    foreach (var prop in NAPI.Pools.GetAllObjects())
                    {
                        if (prop.HasData("LSPD_PROP"))
                        {
                            prop.Delete();
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("multar")]
        public async Task CMD_multar(Player player, int id, int dinero, string razon)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    Player target = Utilities.PlayerId.FindPlayerById(id);
                    if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                    else
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User targ = player.GetData<Data.Entities.User>("USER_CLASS");

                        Data.Entities.FineLSPD multa = new Data.Entities.FineLSPD()
                        {
                            userid = targ.idpj,
                            reason = razon,
                            price = dinero
                        };

                        multa.IdDatabase = await RegistrarMulta(multa);

                        Data.Lists.finesPD.Add(multa);

                        Utilities.Notifications.SendNotificationOK(player, $"Has multado al usuario ID {id}");
                        target.SendChatMessage($"~r~[MULTA {multa.IdDatabase}]~w~ {razon} | ${dinero}");
                        Utilities.Notifications.SendNotificationINFO(target, $"Has recibido una multa, dirígete a comisaría para pagarla");
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        public async Task<int> RegistrarMulta(Data.Entities.FineLSPD multa)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO multas (razon, precio, pagada, PjID) VALUES (@raz, @pre, @pag, @idPj)";
                command.Parameters.AddWithValue("@raz", multa.reason);
                command.Parameters.AddWithValue("@pre", multa.price);
                command.Parameters.AddWithValue("@pag", 0);
                command.Parameters.AddWithValue("@idPj", multa.userid);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                MySqlCommand command1 = connection.CreateCommand();
                command1.CommandText = "SELECT MAX(id) AS id FROM multas";

                System.Data.Common.DbDataReader reader = await command1.ExecuteReaderAsync().ConfigureAwait(false);

                if (reader.HasRows)
                {
                    await reader.ReadAsync().ConfigureAwait(false);
                    {
                        return reader.GetInt32(reader.GetOrdinal("id"));

                    }
                }
            }


            return -1;
        }
        public static async Task RegistrarPagoMulta(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Data.DatabaseHandler.connectionHandle))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE multas SET pagada = 1 WHERE(Id = @id)";
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                connection.Close();

            }

        }

        
        [Command("esposar")]
        public async Task CMD_esposar(Player player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1)
            {
                if (user.factionDuty)
                {
                    Player target = Utilities.PlayerId.FindPlayerById(id);
                    if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
                    else
                    {
                        if(player.Position.DistanceTo(target.Position) < 3f)
                        {
                            if (!target.HasData("USER_CLASS")) return;
                            Data.Entities.User trg = target.GetData<Data.Entities.User>("USER_CLASS");
                            if (!trg.estaEsposado)
                            {
                                target.PlayAnimation("mp_arresting", "idle", (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl));
                                trg.estaEsposado = true;
                                return;
                            }
                            target.StopAnimation();
                            trg.estaEsposado = false;
                        }
                    }
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No estás duty");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        [Command("cachear")]
        public void CMD_cachear(Player player, int target)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction == 1)
            {
                if (!user.factionDuty)
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");

                }
                else
                {
                    var t = Utilities.PlayerId.FindPlayerById(target);
                    Data.Entities.User tdata = t.GetData<Data.Entities.User>("USER_CLASS");
                    player.SendChatMessage($"--------Cacheo a {t.Name}--------");

                    sendMessageSiEsAlgo(player, tdata.inventory.slot1);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot2);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot3);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot4);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot5);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot6);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot7);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot8);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot9);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot10);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot11);
                    sendMessageSiEsAlgo(player, tdata.inventory.slot12);

                    sendMessageSiEsAlgo(player, tdata.inventory.pjSlot1);
                    sendMessageSiEsAlgo(player, tdata.inventory.pjSlot2);
                    sendMessageSiEsAlgo(player, tdata.inventory.pjSlot3);
                    sendMessageSiEsAlgo(player, tdata.inventory.pjSlot4);
                    sendMessageSiEsAlgo(player, tdata.inventory.pjSlot5);
                    sendMessageSiEsAlgo(player, tdata.inventory.pjSlot6);

                    WeaponHash[] playerWeapons = NAPI.Player.GetPlayerWeapons(player);

                    if (playerWeapons.Length != 0)
                    {

                        foreach (WeaponHash weapon in playerWeapons)
                        {
                            player.SendChatMessage($"· {Utilities.Weapon.GetWeaponNameByHash(weapon)}");
                            // will loop through all player's weapons.
                        }
                    }

                    player.SendChatMessage($"--------------------------");


                }

            }
            else Utilities.Notifications.SendNotificationERROR(player, "No formas parte de LSPD");
        }

        public void sendMessageSiEsAlgo (Player Player, Item i)
        {
            if (i.name != "NO")
            {
                Player.SendChatMessage($"· x{i.quantity}   {i.name} ");
            }
        }

        [RemoteEvent("RecArmeriaPD")]
        public async Task EV_RecArmeriaPD(Player player, int Categoria, int Item)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (await Game.Inventory.Inventory.CheckIfPlayerHasSlot(user))
            {
                if (Categoria == 0)                 //Armas Cortas
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, WeaponHash.Pistol, 32);
                            break;
                        case 1:
                            DarArma(player, (WeaponHash)0x5EF9FEC4, 32);
                            break;
                        case 2:
                            DarArma(player, (WeaponHash)0xBFD21232, 16);
                            break;
                        case 3:
                            DarArma(player, (WeaponHash)0x3656C8C1);
                            break;
                        case 4:
                            DarArma(player, (WeaponHash)0x22D8FE39, 54);
                            break;
                        case 5:
                            DarArma(player, (WeaponHash)0x99AEEB3B, 36);
                            break;
                        case 6:
                            DarArma(player, (WeaponHash)0xD205520E, 54);
                            break;

                        case 7:
                            DarArma(player, (WeaponHash)0x83839C4, 54);
                            break;
                        case 8:
                            DarArma(player, (WeaponHash)0x47757124, 8);
                            break;

                        case 9:
                            DarArma(player, (WeaponHash)0xDC4DB296, 36);
                            break;
                        case 10:
                            DarArma(player, (WeaponHash)0xC1B3C3D1, 24);
                            break;


                    }
                }
                else if (Categoria == 1) //Objetos Miscelanea
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, WeaponHash.Nightstick);
                            break;
                        case 1:
                            DarArma(player, WeaponHash.Flashlight);
                            break;
                        case 2:
                            DarArma(player, (WeaponHash)0x060EC506);
                            break;
                        case 3:
                            DarArma(player, WeaponHash.Crowbar);
                            break;
                        case 5:
                            DarArma(player, WeaponHash.Parachute);
                            break;
                    }
                }
                else if (Categoria == 2)
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, (WeaponHash)0x2BE6766B, 120);
                            break;

                        case 1:
                            DarArma(player, (WeaponHash)0xEFE7E2DF, 120);
                            break;

                        case 2:
                            DarArma(player, (WeaponHash)0x0A3D4D34, 120);
                            break;
                    }
                }
                else if (Categoria == 3)
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, (WeaponHash)0xE284C527, 16);
                            break;
                        case 1:
                            DarArma(player, (WeaponHash)0x7846A318, 16);
                            break;
                    }
                }
                else if (Categoria == 4)
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, WeaponHash.Carbinerifle, 120);
                            break;
                        case 1:
                            DarArma(player, WeaponHash.Specialcarbine, 120);
                            break;
                    }

                }
                else if (Categoria == 5)
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, WeaponHash.Marksmanrifle, 24);
                            break;
                    }
                }
                else if (Categoria == 6)
                {
                    switch (Item)
                    {
                        case 0:
                            DarArma(player, WeaponHash.Smokegrenade, 5);
                            break;
                        case 1:
                            DarArma(player, WeaponHash.Grenadelauncher, 10);
                            break;
                        case 2:
                            DarArma(player, WeaponHash.Grenade, 5);
                            break;
                    }
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes espacio en tu inventario");
        }

        public static void DarArma (Player player, WeaponHash arma, int municion = 1)
        {
            Data.Entities.Item Item = new Data.Entities.Item(0, Utilities.Weapon.GetWeaponNameByHash(arma), 1, 1);
            Item.isAWeapon = true;
            Item.bullets = municion;
            Item.weaponHash = arma;
            Game.Inventory.DatabaseFunctions.SetNewItemInventory(player, Item, true, municion);
            Utilities.Notifications.SendNotificationOK(player, $"Has cogido un arma");
        }

       [Command ("placa")]
        public void CMD_Placa (Player player, int tipo=0)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if(!user.LlevaPara)
            {
                user.LlevaPara = true;
                if (user.faction == 2)
                {
                    if (user.rank > 7) player.SetClothes(5, 9, 9);
                    else if (user.rank >= 3) player.SetClothes(5, 9, 10);
                    else Utilities.Notifications.SendNotificationERROR(player, "Hasta que te gradues no tienes placa");
                    user.LlevaPara = true;
                    return;
                }

                if (user.faction == 1)
                {
                    switch (user.rank)
                    {
                        case 1: player.SetClothes(5, 10, 0); player.SendChatMessage("Si no tienes autorización expresa, quítatela"); break; //CADETE
                        case 2: player.SetClothes(5, 10, 0); break; //POI
                        case 3: player.SetClothes(5, 10, 0); break; //POII
                        case 4: player.SetClothes(5, 10, 0); break; //POIII
                        case 5: player.SetClothes(5, 10, 0); break; //POIII+
                        case 6: player.SetClothes(5, 10, 1); break; //DetI
                        case 7: player.SetClothes(5, 10, 1); break; //DetII
                        case 8: player.SetClothes(5, 10, 1); break; //DetIII
                        case 9: player.SetClothes(5, 10, 2); break; //Sgt I
                        case 10: player.SetClothes(5, 10, 2); break; //Sgt II
                        case 11: player.SetClothes(5, 10, 3); break; //Lt
                        case 12: player.SetClothes(5, 10, 4); break; //Cpt I
                        case 13: player.SetClothes(5, 10, 4); break; //Cpt II
                        case 14: player.SetClothes(5, 10, 4); break; //Cpt III
                        case 15: player.SendChatMessage("No hay placa de este tipo para Comandante"); break;
                        case 16: player.SendChatMessage("No hay placa de este tipo para Jefe Adjunto"); break;
                        case 17: player.SendChatMessage("No hay placa de este tipo para Jefe"); break;
                    }
                    user.LlevaPara = true;
                    return;

                }
                Utilities.Notifications.SendNotificationERROR(player, "Tu facción no tiene placa o no tienes facción");
            }
            else
            {
                player.SetClothes(5, user.paracaidas, 0);
                user.LlevaPara = false;
            }
        }

    }
}

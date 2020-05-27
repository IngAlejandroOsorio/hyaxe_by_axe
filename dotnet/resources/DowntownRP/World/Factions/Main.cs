using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DowntownRP.Utilities;

namespace DowntownRP.World.Factions
{
    public class Main : Script
    {
        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape_FAC(ColShape shape, Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (shape.HasData("FACTION_CLASS"))
            {
                user.ilegalFactionShape = shape.GetData<Data.Entities.Faction>("FACTION_CLASS");
            }

            if (shape.HasData("SALIDA_FACTION"))
            {
                player.SetData("SALIDA_FACTION", shape.GetData<Vector3>("SALIDA_FACTION"));
            }


        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void OnPlayerExitColshape_FAC(ColShape shape, Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (shape.HasData("FACTION_CLASS"))
            {
                user.ilegalFactionShape = null;
            }

            if (shape.HasData("SALIDA_FACTION"))
            {
                player.ResetData("SALIDA_FACTION");
            }
        }

        [Command("faccionOOC", GreedyArg = true, Alias = "f")]
        public void CMD_f(Player player, string mensaje = "")
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction != 0)
            {
                string faction = GetFactionName(user.faction);
                string rank = GetFactionRankName(user.faction, user.rank);
                if (mensaje == "")
                {
                    user.CanalChatF = !(user.CanalChatF);
                    if (user.CanalChatF == true)
                    {
                        user.entity.SendChatMessage($"~b~[{faction}]~w~ Se ha habilitado la lectura del canal de chat de la facción.");

                    }
                    else
                    {
                        user.entity.SendChatMessage($"~b~[{faction}]~w~ Se ha deshabilitado la lectura del canal de chat de la facción.");
                    }


                }
                else
                {
                    if (user.faction < 10)
                    {
                        foreach (var Player in Data.Lists.playersConnected)
                        {
                            if (Player.faction == user.faction & Player.CanalChatF == true)
                            {
                                Player.entity.SendChatMessage($"~b~[{faction}]~w~ ({player.Value}) {player.Name} ({rank}): {mensaje}");
                            }
                        }
                        Utilities.Webhooks.sendFacWebHook(user.faction, mensaje, player.Name + " (" + GetFactionRankName(user.faction, user.rank) + ")");
                    }
                    else
                    {
                        string message = "";
                        switch (user.rank)
                        {
                            case 1:
                                message = $"~b~[{user.ilegalFaction.name}]~w~ {player.Name} ({user.ilegalFaction.rank1}): {mensaje}";
                                break;

                            case 2:
                                message = $"~b~[{user.ilegalFaction.name}]~w~ {player.Name} ({user.ilegalFaction.rank2}): {mensaje}";
                                break;

                            case 3:
                                message = $"~b~[{user.ilegalFaction.name}]~w~ {player.Name} ({user.ilegalFaction.rank3}): {mensaje}";
                                break;

                            case 4:
                                message = $"~b~[{user.ilegalFaction.name}]~w~ {player.Name} ({user.ilegalFaction.rank4}): {mensaje}";
                                break;

                            case 5:
                                message = $"~b~[{user.ilegalFaction.name}]~w~ {player.Name} ({user.ilegalFaction.rank5}): {mensaje}";
                                break;

                            case 6:
                                message = $"~b~[{user.ilegalFaction.name}]~w~ {player.Name} ({user.ilegalFaction.rank6}): {mensaje}";
                                break;
                        }

                        foreach (var Player in Data.Lists.playersConnected)
                        {
                            if (Player.faction == user.faction & Player.CanalChatF == true)
                            {
                                Player.entity.SendChatMessage(message);
                            }
                        }
                    }
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No perteneces a ninguna facción");
        }

        [Command("radiofaccion", GreedyArg = true, Alias = "r")]
        public void CMD_radiofaccion(Player player, string mensaje = "")
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction != 0)
            {
                if (user.factionDuty)
                {
                    string faction = GetFactionName(user.faction);
                    string rank = GetFactionRankName(user.faction, user.rank);
                    if (mensaje == "")
                    {
                        user.CanalChatR = !(user.CanalChatR);
                        if (user.CanalChatR == true)
                        {
                            user.entity.SendChatMessage($"~b~[{faction}]~w~ Se ha habilitado la lectura del canal de radio de la facción.");

                        }
                        else
                        {
                            user.entity.SendChatMessage($"~b~[{faction}]~w~ Se ha deshabilitado la lectura del canal de radio de la facción.");
                        }


                    }
                    else
                    {
                        if (user.faction < 10)
                        {
                            foreach (var Player in Data.Lists.playersConnected)
                            {
                                if (Player.faction == user.faction & Player.CanalChatF == true)
                                {
                                    if (Player.factionDuty)
                                    {
                                        Player.entity.SendChatMessage($"~y~[RADIO] ({player.Value}): {mensaje}");
                                        Player.entity.TriggerEvent("WalkiePdSound");
                                    }
                                }
                            }
                        }

                    }
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No perteneces a ninguna facción");
        }

        [Command("miembros")]
        public void CMD_miembros(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction != 0)
            {
                player.SendChatMessage($"~b~ Miembros en linea [{GetFactionName(user.faction)}]");
                foreach(var u in Data.Lists.playersConnected)
                {
                    if(user.faction == u.faction)
                    {
                        if (u.factionDuty) player.SendChatMessage($"({u.entity.Value}) {u.entity.Name} | {GetFactionRankName(u.faction, u.rank)} | ~g~EN SERVICIO");
                        player.SendChatMessage($"({u.entity.Value}) {u.entity.Name} | {GetFactionRankName(u.faction, u.rank)} | ~r~FUERA DE SERVICIO");
                    }
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No eres miembro de una facción");
        }

        [Command("contratar")]
        public void CMD_contratar(Player player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction != 0)
            {
                if ((user.faction == 1 & user.rank >= 9)|| (user.faction == 2 && user.rank >= 15))
                {
                    Player target = Utilities.PlayerId.FindPlayerById(id);
                    if(target != null)
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User Player = target.GetData<Data.Entities.User>("USER_CLASS");

                        string fName = GetFactionName(user.faction);

                        target.SetData("FACTION_PETICION", user.faction);

                        target.SendChatMessage($"~p~[!]~w~ Has recibido una invitación para unirte a {fName}. Usa /aceptarfaccion");
                        Utilities.Notifications.SendNotificationINFO(target, $"Has recibido una invitación para unirte a {fName}. Usa /aceptarfaccion");

                        Utilities.Notifications.SendNotificationOK(player, "Has enviado la invitación a tu facción correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No se ha encontrado ningún jugador con esa ID");
                }
                else if(user.faction >=10 && user.rank == 6)
                {
                    Player target = Utilities.PlayerId.FindPlayerById(id);
                    if (target != null)
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User Player = target.GetData<Data.Entities.User>("USER_CLASS");

                        string fName = user.ilegalFaction.name;

                        target.SetData("FACTION_PETICION", user.faction);

                        target.SendChatMessage($"~p~[!]~w~ Has recibido una invitación para unirte a {fName}. Usa /aceptarfaccion");
                        Utilities.Notifications.SendNotificationINFO(target, $"Has recibido una invitación para unirte a {fName}. Usa /aceptarfaccion");

                        Utilities.Notifications.SendNotificationOK(player, "Has enviado la invitación a tu facción correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No se ha encontrado ningún jugador con esa ID");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres el lider de tu facción");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No perteneces a ninguna facción");
        }

        [Command("aceptarfaccion")]
        public async Task CMD_aceptarfaccion(Player player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (player.HasData("FACTION_PETICION"))
            {
                int faction = player.GetData<int>("FACTION_PETICION");
                string fName = GetFactionName(faction);

                if (faction == 2)
                {
                    player.TriggerEvent("selectorLSFD", 0);
                } else
                {
                    await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(user.idpj, faction);
                    await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(user.idpj, 1);

                    user.faction = faction;
                    user.rank = 1;
                    Utilities.Notifications.SendNotificationOK(player, $"Has aceptado la invitación para unirte a {fName}. ¡Bienvenido!");
                    player.ResetData("FACTION_PETICION");
                }
                

                
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes ninguna invitación a facción activa");
        }

        [Command("setfac")]
        public async Task CMD_SetFc(Player player, int idtarget, int faction, int factionrank = 1)
        {
                Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

                if (user.adminLv != 0) {
                    Player target = Utilities.PlayerId.FindPlayerById(idtarget);
                    Data.Entities.User t = target.GetData<Data.Entities.User>("USER_CLASS");
                    if (target == null) { Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id"); return; }
                    string fName = GetFactionName(faction);
                    if (fName == "N/A" & faction != 0 ) { Utilities.Notifications.SendNotificationERROR(player, "No existe niguna facción con este ID"); return; }
                    string fRank = GetFactionRankName(faction, factionrank);
                    if (fRank == "N/A" & faction != 0) { Utilities.Notifications.SendNotificationERROR(player, "No existe ningun rango con este id en la facción: "+fName); return; }
                    await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(t.idpj, faction);
                    await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(t.idpj, factionrank);

                    t.faction = faction;
                    t.rank = factionrank;

                    Utilities.Notifications.SendNotificationOK(target, $"Un administrador te ha unido a {fName} con rango {fRank}");
                    Utilities.Notifications.SendNotificationOK(player, $"Has unido a {target.Name} a la facción {fName} con rango {fRank}");
                }
                else
                {
                    Utilities.Notifications.SendNotificationOK(player, $"No eres administrador");
                }
        }

        [Command("verranks")]
        public async Task CMD_VerRanks(Player player, int id = -1)
        {

            if(id == -1)
            {
                Data.Entities.User cl = player.GetData<Data.Entities.User>("USER_CLASS");
                id = cl.faction;
            }

            player.SendChatMessage($"Los rangos de la facción {GetFactionName(id)} son");
            string name;
            for(var i = 1; i <= 17;)
            {
                name = GetFactionRankName(id, i);
                if(name != "N/A")
                {
                    player.SendChatMessage($"{i} => {name}");
                }

                i++;
            }
        }


        [Command("miembros")]
        public async Task CMD_miembros(Player player, int id = -1)
        {

            if (id == -1)
            {
                Data.Entities.User cl = player.GetData<Data.Entities.User>("USER_CLASS");
                id = cl.faction;
            }

            player.SendChatMessage($"Los usuarios conectados de la facción {GetFactionName(id)} son");

            if(id == 1 | id == 2)
            {
                foreach (Data.Entities.User u in Data.Lists.playersConnected)
                {
                    if(u.faction == id)
                    {
                        if (u.factionDuty)
                        {
                            player.SendChatMessage($"~g~{GetFactionRankName(id, u.rank)} => {u.entity.Name}");
                        }
                        else
                        {
                            player.SendChatMessage($"~b~{GetFactionRankName(id, u.rank)} => {u.entity.Name}");
                        }
                    }
                }
            }
            else
            {
                foreach (Data.Entities.User u in Data.Lists.playersConnected)
                {
                    if (u.faction == id)
                    {

                       player.SendChatMessage($"~b~{GetFactionRankName(id, u.rank)} => {u.entity.Name}");

                    }
                }
            }

        }

        [Command("despedir")]
        public async Task CMD_despedir(Player player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.faction != 0)
            {
                if (user.faction == 1 || user.faction == 2 && user.rank == 15)
                {
                    Player target = Utilities.PlayerId.FindPlayerById(id);
                    if (target != null)
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User Player = target.GetData<Data.Entities.User>("USER_CLASS");

                        await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(Player.idpj, 0);
                        await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(Player.idpj, 1);

                        Player.faction = 0;
                        Player.rank = 1;

                        Utilities.Notifications.SendNotificationINFO(target, "Has sido expulsado de tu facción");
                        Utilities.Notifications.SendNotificationOK(player, "Has expulsado a un miembro de tu facción correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No se ha encontrado ningún jugador con esa ID");
                }
                else if(user.faction >= 10 && user.rank == 6)
                {
                    Player target = Utilities.PlayerId.FindPlayerById(id);
                    if (target != null)
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User Player = target.GetData<Data.Entities.User>("USER_CLASS");

                        await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(Player.idpj, 0);
                        await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(Player.idpj, 1);

                        Player.faction = 0;
                        Player.rank = 1;

                        Utilities.Notifications.SendNotificationINFO(target, "Has sido expulsado de tu facción");
                        Utilities.Notifications.SendNotificationOK(player, "Has expulsado a un miembro de tu facción correctamente");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No se ha encontrado ningún jugador con esa ID");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No eres el lider de tu facción");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No perteneces a ninguna facción");
        }

        public static string GetFactionName(int faction)
        {
            switch (faction)
            {
                case 0:
                    return "Sin faccion";

                case 1:
                    return "LSPD";

                case 2:
                    return "LSMD";

                default:
                    return Data.Lists.factions.Find(x => x.id == faction).name;
            }

        }

        public static string GetFactionRankName(int faction, int rank)
        {
            if(faction == 1)
            {
                switch (rank)
                {
                    case 1:
                        return "Cadete";

                    case 2:
                        return "Oficial I";

                    case 3:
                        return "Oficial II";

                    case 4:
                        return "Oficial III";

                    case 5:
                        return "Oficial III+";

                    case 6:
                        return "Detective I";

                    case 7:
                        return "Detective II";

                    case 8:
                        return "Detective III";

                    case 9:
                        return "Sargento I";

                    case 10:
                        return "Sargento II";

                    case 11:
                        return "Teniente";

                    case 12:
                        return "Capitán I";

                    case 13:
                        return "Capitán II";

                    case 14:
                        return "Capitán III";

                    case 15:
                        return "Comandante";

                    case 16:
                        return "Jefe Adjunto";

                    case 17:
                        return "Jefe de Policia";

                    default:
                        return "N/A";
                }
            }

            if(faction == 2)
            {
                switch (rank)
                {
                    case 1:
                        return "Aspirante Bombero";

                    case 2:
                        return "Aspirante Médico";

                    case 3:
                        return "Bombero I";

                    case 4:
                        return "Bombero II";

                    case 5:
                        return "Bombero III";

                    case 6:
                        return "Ingeniero I";

                    case 7:
                        return "Ingeniero II";

                    case 8:
                        return "Médico I";

                    case 9:
                        return "Médico II";

                    case 10:
                        return "Médico III";

                    case 11:
                        return "Especialista I";

                    case 12:
                        return "Especialista II";

                    case 13:
                        return "Supervisor";

                    case 14:
                        return "Sargento";

                    case 15:
                        return "Teniente";

                    case 16:
                        return "Capitán";

                    case 17:
                        return "Jefe de Departamento";

                    default:
                        return "N/A";
                }
            }
           switch (rank)
            {
                case 1:
                    return Data.Lists.factions.Find(x => x.id == faction).rank1;
                case 2:
                    return Data.Lists.factions.Find(x => x.id == faction).rank2;
                case 3:
                    return Data.Lists.factions.Find(x => x.id == faction).rank3;
                case 4:
                    return Data.Lists.factions.Find(x => x.id == faction).rank4;
                case 5:
                    return Data.Lists.factions.Find(x => x.id == faction).rank5;
                case 6:
                    return Data.Lists.factions.Find(x => x.id == faction).rank6;
                default:
                    return "N/A";
            }
        }

        [Command("ascender")]
        public async Task CMD_ascender(Player player, int target)
        {
            Data.Entities.User t = Utilities.PlayerId.FindUserById(target);
            Data.Entities.User p = player.GetData<Data.Entities.User>("USER_CLASS");

            if (p.faction == 0)
            {
                Utilities.Notifications.SendNotificationERROR(player, "No estás en ninguna facción");
                return;
            }
            if (t == null || t.faction != p.faction)
            {
                Utilities.Notifications.SendNotificationERROR(player, "Este usuario o no existe o no está en tu facción");
                return;
            }

            if (p.faction == 1)
            {
                if (p.rank >= 8 && p.rank > t.rank + 1)
                {
                    await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(t.idpj, t.rank + 1);
                    t.rank++;
                    Utilities.Notifications.SendNotificationOK(player, $"Has ascendido a {t.entity.Name} a {GetFactionRankName(1, t.rank)}");
                    Utilities.Notifications.SendNotificationOK(t.entity, $"El {GetFactionRankName(1, p.rank)} {player.Name} te ha ascendido a {GetFactionRankName(1, t.rank)}. ¡Felicidades! ");
                }
                else
                {
                    Utilities.Notifications.SendNotificationERROR(player, "No tienes permisos para ascender a esta persona");
                }
            }
            else if (p.faction == 2)
            {
                if ((p.rank >= 13 && p.rank > t.rank + 1)| Utilities.AdminLVL.PuedeUsarComando(player, 1))
                {
                    int newRank;
                    if (t.rank == 0)
                    {
                        newRank = 3;
                    }
                    else if (t.rank == 1)
                    {
                        newRank = 8;
                    }
                    else if (t.rank == 7 | t.rank == 12)
                    {
                        newRank = 13;
                    }
                    else
                    {
                        newRank = t.rank + 1;
                    }

                    await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(t.idpj, newRank);
                    t.rank = newRank;
                    Utilities.Notifications.SendNotificationOK(player, $"Has ascendido a {t.entity.Name} a {GetFactionRankName(2, t.rank)}");
                    Utilities.Notifications.SendNotificationOK(t.entity, $"El {GetFactionRankName(2, p.rank)} {player.Name} te ha ascendido a {GetFactionRankName(1, t.rank)}. ¡Felicidades! ");
                }
                else
                {

                }
            }
        }

        [Command("straficosfaccion")]
        public async Task CMD_straficosfaccion(Player player, int id, int aC, int aL)
        {
            if (Utilities.AdminLVL.PuedeUsarComando(player, 4))
            {
                DbFunctions.UpdateFactionTrafico(id, aC, aL);
                Data.Entities.Faction fac = Data.Lists.factions.Find(x => x.id == id);
                if (aC == 1)
                {
                    fac.armasCortas = true;
                }
                else
                {
                    fac.armasCortas = false;
                }
                if (aL == 1)
                {
                    fac.armasLargas = true;
                }
                else
                {
                    fac.armasLargas = false;
                }

            }
        }

        [Command ("callsign")]
        public void CMD_callsign (Player player, string callsign)
        {
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            if(user.faction == 1 |user.faction == 2)
            {
                user.Callsign = callsign;
            }
        }

        [Command ("dejarfaccion")]
        public async Task CMD_dejarFaccion (Player player)
        {
            Data.Entities.User usr = player.getClass();

            await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(usr.idpj, 0);
            await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(usr.idpj, 1);

            usr.faction = 0;
            usr.rank = 1;

        }
    }
}

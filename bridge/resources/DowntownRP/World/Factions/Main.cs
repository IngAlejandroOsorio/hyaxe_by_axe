using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Factions
{
    public class Main : Script
    {
        [Command("f", GreedyArg = true)]
        public void CMD_f(Client player, string mensaje)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction != 0)
            {
                string faction = GetFactionName(user.faction);
                string rank = GetFactionRankName(user.faction, user.rank);

                foreach(var client in Data.Lists.playersConnected)
                {
                    if(client.faction == user.faction)
                    {
                        client.entity.SendChatMessage($"<font color='#4169e1'>[{faction}]</font> {player.Name} ({rank}): {mensaje}");
                    }
                }
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No perteneces a ninguna facción");
        }

        [Command("contratar")]
        public void CMD_contratar(Client player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction != 0)
            {
                if (user.rank == 15)
                {
                    Client target = Utilities.PlayerId.FindPlayerById(id);
                    if(target != null)
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User client = target.GetData("USER_CLASS");

                        string fName = GetFactionName(user.faction);

                        target.SetData("FACTION_PETICION", user.faction);

                        target.SendChatMessage($"<font color='purple'>[!]</font> Has recibido una invitación para unirte a {fName}. Usa /aceptarfaccion");
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
        public async Task CMD_aceptarfaccion(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (player.HasData("FACTION_PETICION"))
            {
                int faction = player.GetData("FACTION_PETICION");
                string fName = GetFactionName(faction);
                await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(user.idpj, faction);
                await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(user.idpj, 1);

                user.faction = faction;
                user.rank = 1;

                Utilities.Notifications.SendNotificationOK(player, $"Has aceptado la invitación para unirte a {fName}. ¡Bienvenido!");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes ninguna invitación a facción activa");
        }

        [Command("despedir")]
        public async Task CMD_despedir(Client player, int id)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.faction != 0)
            {
                if (user.rank == 15)
                {
                    Client target = Utilities.PlayerId.FindPlayerById(id);
                    if (target != null)
                    {
                        if (!target.HasData("USER_CLASS")) return;
                        Data.Entities.User client = target.GetData("USER_CLASS");

                        await Game.CharacterSelector.CharacterSelector.UpdateUserFaction(client.idpj, 0);
                        await Game.CharacterSelector.CharacterSelector.UpdateUserFactionRank(client.idpj, 1);

                        client.faction = 0;
                        client.rank = 1;

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
                case 1:
                    return "LSPD";

                case 2:
                    return "LSMD";

                default:
                    return "N/A";
            }
        }

        public static string GetFactionRankName(int faction, int rank)
        {
            if(faction == 1)
            {
                switch (rank)
                {
                    case 1:
                        return "Aspirante";

                    case 2:
                        return "Cadete";

                    case 3:
                        return "Agente";

                    case 4:
                        return "Agente+";

                    case 5:
                        return "Sargento";

                    case 6:
                        return "Teniente";

                    case 7:
                        return "Capitán";

                    case 8:
                        return "Comandante";

                    case 9:
                        return "Ayudante del jefe";

                    case 10:
                        return "Jefe";

                    case 11:
                        return "NOOB";

                    case 12:
                        return "NOOB";

                    case 13:
                        return "NOOB";

                    case 14:
                        return "NOOB";

                    case 15:
                        return "NOOB";

                    default:
                        return "N/A";
                }
            }

            if(faction == 2)
            {
                switch (rank)
                {
                    case 1:
                        return "NOOB";

                    case 2:
                        return "NOOB";

                    case 3:
                        return "NOOB";

                    case 4:
                        return "NOOB";

                    case 5:
                        return "NOOB";

                    case 6:
                        return "NOOB";

                    case 7:
                        return "NOOB";

                    case 8:
                        return "NOOB";

                    case 9:
                        return "NOOB";

                    case 10:
                        return "NOOB";

                    case 11:
                        return "NOOB";

                    case 12:
                        return "NOOB";

                    case 13:
                        return "NOOB";

                    case 14:
                        return "NOOB";

                    case 15:
                        return "NOOB";

                    default:
                        return "N/A";
                }
            }
            return "N/A";
        }
    }
}

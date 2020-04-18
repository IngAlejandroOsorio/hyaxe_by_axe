using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Companies.Trucker
{
    public class Main : Script
    {
        [RemoteEvent("TruckerStartRace")]
        public async Task RE_TruckerStartRace(Client player, int type)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            List<double> list = await DbFunctions.GetVector3OfSupermarket();
            Vector3 vector3 = new Vector3(list[1], list[2], list[3]);
            if (list == null) return;
            int ganancies = await GetGananciesOfRace(player, vector3);

            if (!player.IsInVehicle) return;
            ColShape shape = NAPI.ColShape.CreateCylinderColShape(player.Vehicle.Position, 2, 2);
            Data.Entities.Race race = new Data.Entities.Race() { step = 0, ganancies = ganancies, position = vector3, vehicle = player.Vehicle, businessId = (int)list[0], shape = shape, startPosition = player.Position };
            player.SetData("COMPANY_RACE", race);

            Vector3 carguero = await DbFunctions.GetTruckerStart(user.companyMember.id);
            player.TriggerEvent("TruckerStartBlip", carguero, 1);
            player.TriggerEvent("chat_notification", "Dirígete al ~r~área de carga ~w~para cargar la mercancía");
        }

        [RemoteEvent("ActionCargueroInteract")]
        public async Task RE_ActionCargueroInteract(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (!player.HasData("COMPANY_RACE")) return;
            Data.Entities.Race race = player.GetData("COMPANY_RACE");
            if (user.companyMember.id == player.GetData("COMPANY_ID"))
            {
                if (race.step == 0)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Carga la caja~w~ en el camión presionando N");
                    race.step = 1;
                    return;
                }
                if (race.step == 2)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Carga la caja~w~ en el camión presionando N");
                    race.step = 3;
                    return;
                }
                if (race.step == 4)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Carga la caja~w~ en el camión presionando N");
                    race.step = 5;
                    return;
                }
                if (race.step == 6)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Carga la caja~w~ en el camión presionando N");
                    race.step = 7;
                    return;
                }
                if (race.step == 8)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Carga la caja~w~ en el camión presionando N");
                    race.step = 9;
                    return;
                }
            }

            if (user.isInBusiness)
            {
                if(user.business.id == race.businessId)
                {
                    if(race.step == 12)
                    {
                        player.TriggerEvent("TruckerStartBlip", race.position, 2);
                        player.StopAnimation();
                        NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                        player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja ~w~en el camión presionando la tecla N");
                        race.step = 13; // SIGUIENTE STEP EN COLSHAPES COMPANYS MAIN
                        return;
                    }
                    if (race.step == 14)
                    {
                        player.TriggerEvent("TruckerStartBlip", race.position, 2);
                        player.StopAnimation();
                        NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                        player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja ~w~en el camión presionando la tecla N");
                        race.step = 15; // SIGUIENTE STEP EN COLSHAPES COMPANYS MAIN
                        return;
                    }
                    if (race.step == 16)
                    {
                        player.TriggerEvent("TruckerStartBlip", race.position, 2);
                        player.StopAnimation();
                        NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                        player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja ~w~en el camión presionando la tecla N");
                        race.step = 17; // SIGUIENTE STEP EN COLSHAPES COMPANYS MAIN
                        return;
                    }
                    if (race.step == 18)
                    {
                        player.TriggerEvent("TruckerStartBlip", race.position, 2);
                        player.StopAnimation();
                        NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                        player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja ~w~en el camión presionando la tecla N");
                        race.step = 19; // SIGUIENTE STEP EN COLSHAPES COMPANYS MAIN
                        return;
                    }
                    if (race.step == 20)
                    {
                        player.TriggerEvent("TruckerStartBlip", race.position, 2);
                        player.StopAnimation();
                        NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                        player.TriggerEvent("TruckerStartBlip", race.startPosition, 1);
                        player.TriggerEvent("chat_notification", "Dirígete ~r~a dejar el camión ~w~para recibir tu pago");
                        race.step = 21; // SIGUIENTE STEP EN COLSHAPES COMPANYS MAIN
                        return;
                    }
                }
            }
        }

        [RemoteEvent("BoxOnTruck")]
        public void RE_BoxOnTruck(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (!player.HasData("COMPANY_RACE")) return;
            Data.Entities.Race race = player.GetData("COMPANY_RACE");

            if (player.Position.DistanceTo(race.vehicle.Position) < 5f)
            {
                if (race.step == 1)
                {
                    player.StopAnimation();
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                    player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja");
                    race.step = 2;
                    return;
                }
                if (race.step == 3)
                {
                    player.StopAnimation();
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                    player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja");
                    race.step = 4;
                    return;
                }
                if (race.step == 5)
                {
                    player.StopAnimation();
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                    player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja");
                    race.step = 6;
                    return;
                }
                if (race.step == 7)
                {
                    player.StopAnimation();
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                    player.TriggerEvent("chat_notification", "Vuelve a ~r~coger otra caja");
                    race.step = 8;
                    return;
                }
                if (race.step == 9)
                {
                    player.TriggerEvent("TruckerStartBlip", race.position, 2);
                    player.StopAnimation();
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.Delete", player.Value);
                    player.TriggerEvent("chat_notification", "~r~Entrega la mercancía ~w~en la ruta marcada en el GPS");
                    race.step = 10; // SIGUIENTE STEP EN COLSHAPES COMPANYS MAIN
                    return;
                }
                if (race.step == 11)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Deja la caja en la tienda~w~ presionando Y");
                    race.step = 12;
                    return;
                }
                if (race.step == 13)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Deja la caja en la tienda~w~ presionando Y");
                    race.step = 14;
                    return;
                }
                if (race.step == 15)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Deja la caja en la tienda~w~ presionando Y");
                    race.step = 16;
                    return;
                }
                if (race.step == 17)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Deja la caja en la tienda~w~ presionando Y");
                    race.step = 18;
                    return;
                }
                if (race.step == 19)
                {
                    NAPI.Player.PlayPlayerAnimation(player, (int)(Utilities.AnimationFlags.Loop | Utilities.AnimationFlags.OnlyAnimateUpperBody | Utilities.AnimationFlags.AllowPlayerControl), "anim@heists@box_carry@", "idle");
                    NAPI.ClientEvent.TriggerClientEventForAll("Object.AttachTo", "prop_cs_cardbox_01", player.Value, 18905);
                    player.TriggerEvent("chat_notification", "~r~Deja la caja en la tienda~w~ presionando Y");
                    race.step = 20;
                    return;
                }

            }
        }

        private async static Task<int> GetGananciesOfRace(Client player, Vector3 position)
        {
            return (int)Math.Floor(player.Position.DistanceTo(position)) / 5;
        }
    }
}

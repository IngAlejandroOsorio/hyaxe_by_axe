using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Houses
{
    public class Main : Script
    {
        [ServerEvent(Event.PlayerEnterColshape)]
        public void SE_PlayerEnterColShapeHouse(ColShape shape, Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (!shape.HasData("HOUSE_CLASS")) return;
            Data.Entities.House house = shape.GetData("HOUSE_CLASS");

            if(house != null)
            {
                if (house.owner == 0) player.TriggerEvent("tipComprarCasa");
                user.house = house;
                user.isInHouse = true;
            }
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void SE_PlayerExitColShapeHouse(ColShape shape, Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (!shape.HasData("HOUSE_CLASS")) return;
            Data.Entities.House house = shape.GetData("HOUSE_CLASS");

            if (house != null)
            {
                user.house = null;
                user.isInHouse = false;
            }
        }

        [RemoteEvent("ActionHouse")]
        public void RE_ActionHouse(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.isInHouse)
            {
                Data.Entities.House house = user.house;
                // if(house.owner == user.idpj) 
                if(house.owner == 0)
                {
                    if (!user.isHouseCefOpen)
                    {
                        player.TriggerEvent("OpenBuyHouseUI", GetHouseTypeName(house.type), house.price, $"{house.area}, {house.number}");
                        user.isHouseCefOpen = true;
                        return;
                    }
                    else
                    {
                        player.TriggerEvent("CloseBuyMenuUI");
                        user.isHouseCefOpen = false;
                    }
                }
                else
                {
                    //if(!house.isOpen)
                }
            }
        }

        [RemoteEvent("SS_VisitHouseMenu")]
        public async Task SS_VisitHouseMenu(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            player.TriggerEvent("CloseBuyMenuUI");
            user.isHouseCefOpen = false;

            Interiors.EnterHouse(user.house, user);
            Utilities.Notifications.SendNotificationINFO(player, "En 30 segundos serás teleportado automáticamente");

            await Task.Delay(30000);
            Interiors.ExitHouse(user.house, user);
        }

        [RemoteEvent("SS_BuyHouse")]
        public async Task SS_BuyHouse(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            player.TriggerEvent("CloseBuyMenuUI");
            user.isHouseCefOpen = false;

            if (await Game.Money.MoneyModel.SubMoney(player, user.house.price))
            {
                await World.Houses.DbFunctions.UpdateHouseOwner(user.house.id, user.idpj);
                user.house.owner = user.idpj;
                user.house.entityOwner = player;

                player.TriggerEvent("chat_goal", "¡Felicidades!", "Ahora eres propietario de una casa");
                Interiors.EnterHouse(user.house, user);
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero en mano para comprarte esta propiedad");
        }

        [RemoteEvent("SS_CloseBuyHouseMenu")]
        public void SS_CloseBuyHouseMenu(Client player)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            player.TriggerEvent("CloseBuyMenuUI");
            user.isHouseCefOpen = false;
        }

        [RemoteEvent("HouseFinishCreation")]
        public async Task RE_HouseFinishCreation(Client player, string name)
        {
            //var user = player.GetExternalData<Data.Entities.User>(0);
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData("USER_CLASS");

            if (user.adminLv == 5)
            {
                if (player.HasData("CreateHouseType"))
                {
                    if (player.HasData("CreateHousePrice"))
                    {
                        int streetid = await DbFunctions.GetLastStreetNumberFromHouse(name) + 1;

                        int type = player.GetData("CreateHouseType");
                        int price = player.GetData("CreateHousePrice");

                        int idempresa = await World.Houses.DbFunctions.CreateHouse(player, type, price, name, streetid);
                        Data.Entities.House house = new Data.Entities.House()
                        {
                            id = idempresa,
                            owner = 0,
                            type = type,
                            price = price,
                            number = streetid,
                            area = name,
                            safeBox = 0,
                            position = player.Position
                        };

                        house.shape = NAPI.ColShape.CreateCylinderColShape(player.Position, 2, 2);
                        house.marker = NAPI.Marker.CreateMarker(0, player.Position, new Vector3(), new Vector3(), 1, new Color(248, 218, 79));
                        house.label = NAPI.TextLabel.CreateTextLabel($"~b~Propiedad en venta~n~~w~Precio: ~g~${house.price}~w~~n~Pulsa ~b~F5 ~w~para interactuar~n~~p~{house.area}, {house.number}", player.Position, 5, 1, 0, new Color(255, 255, 255));
                        house.blip = NAPI.Blip.CreateBlip(374, house.position, 1, 2, "Propiedad en venta", 255, 0, true);

                        house.shape.SetData("HOUSE_CLASS", house);
                        Data.Lists.houses.Add(house);
                        Data.Info.housesSpawned++;
                    }
                }
            }
        }

        public static string GetHouseTypeName(int type)
        {
            switch (type)
            {
                case 1:
                    return "PUTO";

                case 2:
                    return "PUTO";

                default:
                    return "N/A";
            }
        }
    }
}

using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.World.Business.Ads
{
    public class Main : Script
    {
        [RemoteEvent("AdsSendToServer")]
        public async Task RE_AdsSendToServer(Player player, string mensaje)
        {
            if (!player.HasData("USER_CLASS")) return;
            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");

            if (user.phone != 0)
            {
                if (mensaje.Length < 100)
                {
                    if (await Game.Money.MoneyModel.SubMoney(player, 100))
                    {
                        player.TriggerEvent("CloseAdsBrowser");
                        foreach (var client in Data.Lists.playersConnected)
                            client.entity.SendChatMessage($"~g~[ANUNCIOS] ~w~{mensaje} | Contacto: {user.phone}");

                        Utilities.Discord.Webhooks.Webhooks.SendWebhook(mensaje, user.phone.ToString());
                        Utilities.Notifications.SendNotificationOK(player, "Has enviado un anuncio por $100");
                    }
                    else Utilities.Notifications.SendNotificationERROR(player, "No tienes suficiente dinero");
                }
                else Utilities.Notifications.SendNotificationERROR(player, "No se pueden enviar mas de 100 caracteres");
            }
            else Utilities.Notifications.SendNotificationERROR(player, "No tienes teléfono movil");
        }
    }
}

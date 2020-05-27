using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Utilities.Discord.Webhooks
{
    public class Webhooks
    {
        public static async void SendWebhook(string mensaje, string phone)
        {
            Webhook webhook = new Webhook("https://discordapp.com/api/webhooks/707671520742211705/fs4Nr2ePbqg_l8dp43o0PwbC39P_IHgiHUIwZbhTE1Pg6_jr-PWlV89BICYIkX3fPiWa")
            {
                AvatarUrl = "https://i.imgur.com/OgvNTcF.png",
                Content = "",
                Embeds = new List<Embed>()
                {
                    new Embed()
                    {
                        Author = new EmbedAuthor(){ Name = mensaje, IconUrl = "https://i.imgur.com/OgvNTcF.png"},
                        Color = 4,
                        Description = $"Teléfono de contacto: {phone}",
                        Footer = new EmbedFooter(){ Text = $"{DateTime.Now.ToString()} — v.hyaxe.com"},
                    }
                }
            };
            await webhook.Send();
        }


        //REPORTES

        public static async void SendReporte (string ureportante, string ureportado, string msg)
        {
            Webhook webhook = new Webhook("https://discordapp.com/api/webhooks/710765027203874886/3YNCK4OuJz0JjoMwc3ppmKkhK6sphWL-VrrU3BvNyU6m70I8LzLj513SldaF_5axYnOX")
            {
                AvatarUrl = "https://i.imgur.com/OgvNTcF.png",
                Content = "",
                Embeds = new List<Embed>()
                {
                    new Embed()
                    {
                        Author = new EmbedAuthor(){ Name = "Reportes", IconUrl = "https://i.imgur.com/OgvNTcF.png"},
                        Color = 4,
                        Description = $"{ureportante} reporta a {ureportado} por {msg}",
                        Footer = new EmbedFooter(){ Text = $"{DateTime.Now.ToString()} — v.hyaxe.com"},
                    }
                }
            };
            await webhook.Send();
        }
    }
}

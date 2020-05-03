using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace DowntownRP.Utilities
{
    public class Webhooks
    {
        public static byte[] Post(string url, NameValueCollection pairs)
        {
            using (WebClient webPlayer = new WebClient())
                return webPlayer.UploadValues(url, pairs);
        }

        // Función para enviar el webhook
        public static void sendWebHook(int type, string message, string username = "")
        {
            string url = "";
            switch (type)
            {
                case 1:
                    url = "https://discordapp.com/api/webhooks/640573064098349062/jGJn-hZqvZ6LTvQ94b6mlVBDZB9xU99G4ShAXg23SJKIvCtJdE70rx4vjT4awcBimFDB"; //Se tiene que cambiar pero serviría para #estado
                    username = "Downtown Logs";
                    break;
                case 2:
                    url = "https://discordapp.com/api/webhooks/700688662094020638/N-387Iygxf3HyukkQx6bTAjWva14UiqQt9jddv9o15h3HFIj0_YxquiGoFh0Au-hh3cx"; // #general (staff)
                    break;
            }

            Webhooks.Post(url, new NameValueCollection()
            {
                {
                    "username",
                    username
                },
                {
                    "content",
                    message
                }
            });
        }


        public static void sendFacWebHook(int facnum, string message, string username)
        {
            string url = "";
            switch (facnum)
            {
                //Los webhooks no son correctos
                case 1:
                    url = "https://discordapp.com/api/webhooks/704638732543852585/LeJiQ8OFjiokPbnLfFcxz2dVayZnYvTpKgOlg1RjtB8Zy0zcUOHzstX_2TIZoDz6n3yS"; //LSPD Cafeteria
                    break;
                case 2:
                    //url = "https://discordapp.com/api/webhooks/700688662094020638/N-387Iygxf3HyukkQx6bTAjWva14UiqQt9jddv9o15h3HFIj0_YxquiGoFh0Au-hh3cx"; // #general (staff)
                    break;
            }

            Webhooks.Post(url, new NameValueCollection()
            {
                {
                    "username",
                    username
                },
                {
                    "content",
                    message
                }
            });
        }
    }
}

using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DowntownRP.Utilities.Discord
{
    public class TcpPlayer
    {
        public static IPHostEntry host;
        public static IPAddress ipAddress;
        public static IPEndPoint remoteEP;
        public static Socket sender;

        public static void SendSocketTcp()
        {
            byte[] bytes = new byte[1024];

            host = Dns.GetHostEntry("54.39.66.143");
            ipAddress = host.AddressList[0];
            remoteEP = new IPEndPoint(ipAddress, 8888);

            try
            {
                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(remoteEP);

                byte[] msg = Encoding.ASCII.GetBytes($"UPDATESTATUS,{Data.Info.playersConnected},{DateTime.Now},{Data.Info.serverVersion}");

                int bytesSent = sender.Send(msg);

                while (true)
                {
                    int bytesRec = sender.Receive(bytes);
                    string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    List<string> lroozm = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split(new char[] { ',' }).ToList();
                    if(lroozm[0] == "AMESSAGE")
                    {
                        foreach (var Player in Data.Lists.playersConnected)
                        {
                            if (Player.adminLv != 0 & Player.CanalChatA == true)
                            {
                                Player.entity.SendChatMessage($"<font color='#FF0000'>[ADMIN] [DISCORD]</font> {lroozm[1]}): {lroozm[2]}");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

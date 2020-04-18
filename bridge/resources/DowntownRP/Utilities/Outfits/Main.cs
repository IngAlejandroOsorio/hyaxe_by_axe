using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DowntownRP.Utilities.Outfits
{
    public class Outfit
    {
        public Tuple<int, int>[] Components { get; set; }
        public Tuple<int, int>[] Props { get; set; }
    }

    public class Main : Script
    {
        public static int MaxProp = 9;
        public static int MaxComponent = 12;
        public static List<Outfit> MaleOutfits = new List<Outfit>();
        public static List<Outfit> FemaleOutfits = new List<Outfit>();

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            if (!System.IO.File.Exists(NAPI.Resource.GetResourceFolder(this) + "/scriptmetadata.meta"))
            {
                NAPI.Util.ConsoleOutput("Los uniformes no funcionan sin el archivo scriptmetadata.meta");
                NAPI.Util.ConsoleOutput("Exportar desde \"update\\update.rpf\\common\\data\" usando OpenIV.");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(NAPI.Resource.GetResourceFolder(this) + "/scriptmetadata.meta");

            // 200IQ code incoming
            foreach (XmlNode node in doc.SelectNodes("/CScriptMetadata/MPOutfits/*/MPOutfitsData/Item"))
            {
                Outfit newOutfit = new Outfit
                {
                    Components = new Tuple<int, int>[MaxComponent],
                    Props = new Tuple<int, int>[MaxProp]
                };

                // Load components
                XmlNode components = node.SelectSingleNode("ComponentDrawables");
                XmlNode componentTextures = node.SelectSingleNode("ComponentTextures");

                for (int compID = 0; compID < MaxComponent; compID++)
                {
                    newOutfit.Components[compID] = new Tuple<int, int>(Convert.ToInt32(components.ChildNodes[compID].Attributes["value"].Value), Convert.ToInt32(componentTextures.ChildNodes[compID].Attributes["value"].Value));
                }

                // Load props
                XmlNode props = node.SelectSingleNode("PropIndices");
                XmlNode propTextures = node.SelectSingleNode("PropTextures");

                for (int propID = 0; propID < MaxProp; propID++)
                {
                    newOutfit.Props[propID] = new Tuple<int, int>(Convert.ToInt32(props.ChildNodes[propID].Attributes["value"].Value), Convert.ToInt32(propTextures.ChildNodes[propID].Attributes["value"].Value));
                }

                switch (node.ParentNode.ParentNode.Name)
                {
                    case "MPOutfitsDataMale":
                        MaleOutfits.Add(newOutfit);
                        break;

                    case "MPOutfitsDataFemale":
                        FemaleOutfits.Add(newOutfit);
                        break;

                    default:
                        NAPI.Util.ConsoleOutput("WTF?");
                        break;
                }
            }

            NAPI.Util.ConsoleOutput("Loaded {0} outfits for FreemodeMale01.", MaleOutfits.Count);
            NAPI.Util.ConsoleOutput("Loaded {0} outfits for FreemodeFemale01.", FemaleOutfits.Count);
        }

        [Command("outfit")]
        public void CMD_Outfit(Client player, int idOrName, int ID)
        {
            Client target = Utilities.PlayerId.FindPlayerById(idOrName);
            if (target == null) Utilities.Notifications.SendNotificationERROR(player, "No hay ningún jugador conectado con esta id");
            else
            {
                switch ((PedHash)target.Model)
                {
                    case PedHash.FreemodeMale01:
                        if (ID < 0 || ID >= MaleOutfits.Count) player.SendChatMessage($"~r~ERROR: ~w~ID invalido. (0 - {MaleOutfits.Count - 1})");
                        else target.SetOutfit(ID);
                        break;

                    case PedHash.FreemodeFemale01:
                        if (ID < 0 || ID >= FemaleOutfits.Count) player.SendChatMessage($"~r~ERROR: ~w~ID invalido. (0 - {FemaleOutfits.Count - 1})");
                        else target.SetOutfit(ID);
                        break;

                    default:
                        player.SendChatMessage("~r~ERROR: ~w~Este comando solo funciona con FreemodeMale01 y FreemodeFemale01.");
                        break;
                }
            }
        }
    }
}

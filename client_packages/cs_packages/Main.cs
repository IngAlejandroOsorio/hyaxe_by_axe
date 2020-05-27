using RAGE;
using System;
using System.Collections.Generic;
using System.Text;
using RAGE.Elements;
using Newtonsoft.Json;

namespace DowntownRP_cs
{
    public class Main : Events.Script
    {
        private bool isFreezed = false;
        public Main()
        {
            Events.OnGuiReady += OnGuiReadyEvent;
            Events.OnEntityStreamIn += OnEntityStreamIn;
            Events.Add("freeze_player", FreezePlayer);
            Events.Add("unfreeze_player", UnFreezePlayer);
            Events.Add("AdjuntarAME", AdjuntarAme);
            Events.Add("SetFaceSettingsSpawn", SetFaceSettingsSpawn);
            Events.Add("LoadAllIps", LoadAllIps);
            Events.Add("LocalizarVehBlip", LocalizarVehBlip);
            Events.Add("LocalizarTrafBlip", LocalizarTrafBlip);
            Events.Add("playerLoadPj", playerLoadPj);
            RAGE.Discord.Update("rage.hyaxe.com", "Jugando en Hyaxe V");
        }

        private void UnFreezePlayer(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
        }

        private void playerLoadPj(object[] args)
        {
            Events.CallRemote("playerLoadCharacter");
        }

        private void LocalizarVehBlip(object[] args)
        {
            RAGE.Elements.Blip blip = new RAGE.Elements.Blip(225, (Vector3)args[0], "LOCALIZADOR COCHE");
            blip.SetAsShortRange(true);
        }

        private void LocalizarTrafBlip(object[] args)
        {
            RAGE.Elements.Blip blip = new RAGE.Elements.Blip(225, (Vector3)args[0], "TRAFICO DE "+args[1],color: 18);
            blip.SetAsShortRange(true);
        }

        private void LoadAllIps(object[] args)
        {
            RAGE.Game.Streaming.RequestIpl("ex_dt1_02_office_02b");
            RAGE.Game.Streaming.RequestIpl("vw_casino_garage");
        }

        private void SetFaceSettingsSpawn(object[] args)
        {
            foreach (var player in RAGE.Elements.Entities.Players.All)
            {
                Events.CallLocal("SetFaceSettings", player.RemoteId, player.GetSharedData("SetFaceSettings"));
            }
        }

        private void FreezePlayer(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition(true);
        }

        private void OnEntityStreamIn(Entity entity)
        {
            /*if((int)entity.GetSharedData("FACE_SETTINGS") != 0)
            {
                Events.CallRemote("SetFaceSettings", entity, entity.GetSharedData("FACE_SETTINGS"));
            }*/

            object objeto = entity.GetSharedData("BUSINESS_VEHICLE_SHARED");

            if(objeto != null)
            {
                RAGE.Elements.Vehicle veh = (RAGE.Elements.Vehicle)objeto;
                veh.FreezePosition(true);
            }
        }

        private void OnGuiReadyEvent()
        {
            RAGE.Game.Streaming.RequestIpl("ex_dt1_02_office_02b");
            RAGE.Game.Streaming.RequestIpl("vw_casino_garage");

            Player.LocalPlayer.SetConfigFlag(429, true);
        }

        private void AdjuntarAme(object [] args)
        {
            Player pl = (Player)args[0];
            TextLabel tl = (TextLabel)args[1];
            RAGE.Game.Entity.AttachEntityToEntity(pl.Id, tl.Id, 12844, 0, 0, 1, 0, 0, 0, false, true, false, false, 1, false);
        }
        

    }
}

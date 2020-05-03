using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.World.Companies
{
    public class CompanyVehicles : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void Companies_PlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatID)
        {
            if (!vehicle.HasData("VEHICLE_COMPANY_DATA")) return;
            if (!player.HasData("USER_CLASS")) return;
            if (player.HasData("COMPANY_RACE")) return;

            Data.Entities.User user = player.GetData<Data.Entities.User>("USER_CLASS");
            Data.Entities.VehicleCompany veh = vehicle.GetData<Data.Entities.VehicleCompany>("VEHICLE_COMPANY_DATA");
            if(user.companyMember == veh.company || user.companyProperty == veh.company)
            {
                if (user.isCompanyDuty || user.companyProperty == veh.company)
                {
                    switch (veh.company.type)
                    {
                        case 1:
                            break;

                        case 2:
                            player.TriggerEvent("StartTruckerJob", veh.company.name);
                            break;
                    }
                }
                else
                {
                    player.WarpOutOfVehicle();
                    Utilities.Notifications.SendNotificationERROR(player, "No estás en servicio");
                }
            }
            else
            {
                player.WarpOutOfVehicle();
                Utilities.Notifications.SendNotificationERROR(player, "No perteneces a la empresa propietaria del vehículo");
            }
        }
    }
}

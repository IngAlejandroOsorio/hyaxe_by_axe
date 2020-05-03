using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Data.Entities
{
    public class User
    {
        // General variables
        public int id { get; set; }
        public string name { get; set; }
        public int edad { get; set; }
        public int idpj { get; set; }
        public int idIg { get; set; }
        public string dni { get; set; }
        public Player entity { get; set; }
        public int enableMicrophone { get; set; }
        public int mpStatus { get; set; }
        public int bankAccount { get; set; }
        public string IBAN { get; set; }
        public double money { get; set; }
        public double bank { get; set; }
        public int hycoin { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int adminLv { get; set; }
        public bool chatStatus { get; set; } = false;
        public bool isFlying { get; set; } = false;
        public Vector3 lastPositionInterior { get; set; }
        public int job { get; set; }
        public int faction { get; set; }
        public int rank { get; set; }
        public bool factionDuty { get; set; } = false;
        public bool seguroMedico { get; set; } = false;
        public bool isDeath { get; set; } = false;
        public bool adviceLSMD { get; set; } = false;
        public bool acceptDeath { get; set; } = false;
        public TextLabel ame { get; set; } = null;
        public bool hombre { get; set; } = false;


        public bool isAnyCefOpen = false;
     
        // Inventory
        public Inventory inventory { get; set; } = new Inventory();
        public Item slot1 { get; set; }
        public Item slot2 { get; set; }
        public Item slot3 { get; set; }
        public Item slot4 { get; set; }
        public Item slot5 { get; set; }
        public Item slot6 { get; set; }
        public bool isInventoryOpen { get; set; } = false;

        // Bank variables
        public bool isInBank { get; set; } = false;
        public Bank bankEntity { get; set; }
        public bool isBankCefOpen { get; set; } = false;
        public bool bankDelay { get; set; } = false;

        // Company variables
        public bool isInCompany { get; set; } = false;
        public bool isCompanyCefOpen { get; set; } = false;
        public bool isInCompanyInterior { get; set; } = false;
        public bool isInCompanyExitInterior { get; set; } = false;
        public bool isInCompanyContract { get; set; } = false;
        public bool isInCompanyDuty { get; set; } = false;
        public int companyDutyId { get; set; }
        public bool isCompanyDuty { get; set; } = false;
        public Company company { get; set; }
        public Company companyInterior { get; set; }
        public Company companyProperty { get; set; } = null;
        public Company companyMember { get; set; } = null;
        public bool isMining { get; set; } = false;

        // Business variables
        public bool isInBusiness { get; set; } = false;
        public bool isBusinessCefOpen { get; set; } = false;
        public Business business { get; set; }

        // House variables
        public bool isInHouse { get; set; } = false;
        public House house { get; set; }
        public House houseInterior { get; set; }
        public bool isHouseCefOpen { get; set; } = false;
        public bool isInHouseInterior { get; set; } = false;
        public bool isInHouseExitInterior { get; set; } = false;
        public bool isInHouseMirilla { get; set; } = false;


        // Vehicle variables
        public bool isMenuVehicleCefOpen { get; set; } = false;
        public List<VehicleCharacter> vehicles { get; set; } = new List<VehicleCharacter>();
        public bool isTruckerOpen { get; set; } = false;
        public int typeVehInv { get; set; }
        public VehicleCharacter vehicleActualInv { get; set; }
        public VehicleFaction vehicleActualInvFac { get; set; }

        //Variables permisos
        public int CarLicense { get; set; } = 0;
        public int MotorbikeLicense { get; set; } = 0;
        public int TruckLicense { get; set; } = 0;

        //Variables chat (no es necesario pasarlas a database)
        public bool CanalChatA { get; set; } = true;
        public bool CanalChatF { get; set; } = true;
        public bool CanalAyudasADM { get; set; } = false;
        public bool CanalAyuda { get; set; } = false;
        public bool CanalChatR { get; set; } = true;
        public Vector3 Marca { get; set; } = new Vector3();

        //Variables facciones
        public Faction ilegalFaction { get; set; } = null;
        public bool isInFactionInterior { get; set; } = false;
        public Faction ilegalFactionShape { get; set; } = null;
        public Faction ilegalFactionInterior { get; set; } = null;
        public Policia madero { get; set; }
    }
}

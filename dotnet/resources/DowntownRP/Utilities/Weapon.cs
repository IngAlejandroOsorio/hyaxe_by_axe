using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP.Utilities
{
    public class Weapon : Script
    {
        public static string GetWeaponNameByHash(WeaponHash hash)
        {
            switch (hash)
            {
                case (WeaponHash)2460120199:
                    return "Daga";


                case (WeaponHash)2508868239:
                    return "Bate";


                case (WeaponHash)4192643659:
                    return "BotellaRota";


                case (WeaponHash)2227010557:
                    return "Palanca";


                case (WeaponHash)2725352035:
                    return "Desarmado";


                case (WeaponHash)2343591895:
                    return "Linterna";


                case (WeaponHash)1141786504:
                    return "PalodeGolf";


                case (WeaponHash)1317494643:
                    return "Martillo";


                case (WeaponHash)4191993645:
                    return "Hacha";


                case (WeaponHash)3638508604:
                    return "PuñoAmericano";


                case (WeaponHash)2578778090:
                    return "Cuchillo";


                case (WeaponHash)3713923289:
                    return "Machete";


                case (WeaponHash)3756226112:
                    return "Navaja";


                case (WeaponHash)1737195953:
                    return "Porra";


                case (WeaponHash)419712736:
                    return "LlaveInglesa";


                case (WeaponHash)3441901897:
                    return "HachadeBatalla";


                case (WeaponHash)2484171525:
                    return "TacodeBillar";


                case (WeaponHash)940833800:
                    return "HachadePiedra";






                case (WeaponHash)453432689:
                    return "Pistola9mm";


                case (WeaponHash)1593441988:
                    return "PistoladeCombate";


                case (WeaponHash)584646201:
                    return "PistolaPerforante";


                case (WeaponHash)911657153:
                    return "Taser";


                case (WeaponHash)2578377531:
                    return "Pistola.50";


                case (WeaponHash)3218215474:
                    return "PistolaCompacta";


                case (WeaponHash)3523564046:
                    return "PistolaPesada";


                case (WeaponHash)137902532:
                    return "PistolaVintage";


                case (WeaponHash)1198879012:
                    return "PistoladeBengalas";


                case (WeaponHash)3696079510:
                    return "PistoladeTirador";


                case (WeaponHash)3249783761:
                    return "RevolverPesado";


                case (WeaponHash)2548703416:
                    return "RevolverDobleAccion";


                case (WeaponHash)2939590305:
                    return "PistoladeRayos";


                case (WeaponHash)727643628:
                    return "PistolaCeramica";


                case (WeaponHash)2441047180:
                    return "RevolverLargo";


                case (WeaponHash)324215364:
                    return "Uzi";


                case (WeaponHash)736523883:
                    return "MP5";


                case (WeaponHash)4024951519:
                    return "SubfusildeAsalto";


                case (WeaponHash)171789620:
                    return "ADPdeCombate";


                case (WeaponHash)3675956304:
                    return "TEC9";


                case (WeaponHash)3173288789:
                    return "Skorpion";


                case (WeaponHash)1198256469:
                    return "Hellbringer";







                case (WeaponHash)487013001:
                    return "EscopetaCorredera";


                case (WeaponHash)2017895192:
                    return "EscopetaRecortada";


                case (WeaponHash)3800352039:
                    return "EscopetadeAsalto";


                case (WeaponHash)2640438543:
                    return "EscopetaBullpup";


                case (WeaponHash)2828843422:
                    return "Mosquete";


                case (WeaponHash)984333226:
                    return "EscopetaPesada";


                case (WeaponHash)4019527611:
                    return "EscopetaDobleCañon";


                case (WeaponHash)317205821:
                    return "EscopetaSweeper";


                case (WeaponHash)3220176749:
                    return "AK-47";


                case (WeaponHash)2210333304:
                    return "M4";


                case (WeaponHash)2937143193:
                    return "FusilAvanzado";


                case (WeaponHash)3231910285:
                    return "CarabinaEspecial";


                case (WeaponHash)2132975508:
                    return "FusilBullpup";


                case (WeaponHash)1649403952:
                    return "FusilCompacto";



                case (WeaponHash)2634544996:
                    return "Ametralladora";


                case (WeaponHash)2144741730:
                    return "AmetralladoradeCombate";


                case (WeaponHash)1627465347:
                    return "Gusenberg";


                case (WeaponHash)100416529:
                    return "RifledeFrancotirador";


                case (WeaponHash)205991906:
                    return "RifledeFrancotiradorPesado";


                case (WeaponHash)3342088282:
                    return "FusildeFrancotirador";


                case (WeaponHash)2982836145:
                    return "RPG";


                case (WeaponHash)2726580491:
                    return "Lanzagranadas";


                case (WeaponHash)1305664598:
                    return "LanzagranadasPolicial";


                case (WeaponHash)1119849093:
                    return "Minigun";


                case (WeaponHash)2138347493:
                    return "FuegosArtificiales";


                case (WeaponHash)1834241177:
                    return "Railgun";


                case (WeaponHash)1672152130:
                    return "LanzamisilesGuiado";


                case (WeaponHash)125959754:
                    return "LanzagranadasCompacto";


                case (WeaponHash)3056410471:
                    return "RayMinigun";

                case (WeaponHash)2481070269:
                    return "Granada";


                case (WeaponHash)2694266206:
                    return "GasLacrimogeno";


                case (WeaponHash)615608432:
                    return "CoctelMolotov";


                case (WeaponHash)741814745:
                    return "C4";


                case (WeaponHash)2874559379:
                    return "MinadeProximidad";


                case (WeaponHash)126349499:
                    return "BoladeNieve";


                case (WeaponHash)3125143736:
                    return "ExplosivoImprovisado";


                case (WeaponHash)600439132:
                    return "Pelota";


                case (WeaponHash)4256991824:
                    return "GranadadeHumo";


                case (WeaponHash)1233104067:
                    return "SeñalLuminosa";


                case (WeaponHash)883325847:
                    return "LatadeGasolina";


                case (WeaponHash)4222310262:
                    return "Paracaidas";



                case (WeaponHash)101631238:
                    return "Extintor";


                case (WeaponHash)3126027122:
                    return "LatadeGasolina2";


                default:
                    return "Arma";
            }
        }

        public static WeaponHash GetWeaponHashByName(string name)
        {
            switch (name)
            {
                case "Daga":
                    return (WeaponHash)2460120199;

                case "Bate":
                    return (WeaponHash)2508868239;

                case "BotellaRota":
                    return (WeaponHash)4192643659;

                case "Palanca":
                    return (WeaponHash)2227010557;

                case "Desarmado":
                    return (WeaponHash)2725352035;

                case "Linterna":
                    return (WeaponHash)2343591895;

                case "PalodeGolf":
                    return (WeaponHash)1141786504;

                case "Martillo":
                    return (WeaponHash)1317494643;

                case "Hacha":
                    return (WeaponHash)4191993645;


                case "PuñoAmericano":
                    return (WeaponHash)3638508604;


                case "Cuchillo":
                    return (WeaponHash)2578778090;


                case "Machete":
                    return (WeaponHash)3713923289;


                case "Navaja":
                    return (WeaponHash)3756226112;


                case "Porra":
                    return (WeaponHash)1737195953;


                case "LlaveInglesa":
                    return (WeaponHash)419712736;

                case "HachadeBatalla":
                    return (WeaponHash)3441901897;


                case "TacodeBillar":
                    return (WeaponHash)2484171525;


                case "HachadePiedra":
                    return (WeaponHash)940833800;






                case "Pistola9mm":
                    return (WeaponHash)453432689;


                case "PistoladeCombate":
                    return (WeaponHash)1593441988;


                case "PistolaPerforante":
                    return (WeaponHash)584646201;


                case "Taser":
                    return (WeaponHash)911657153;


                case "Pistola.50":
                    return (WeaponHash)2578377531;


                case "PistolaCompacta":
                    return (WeaponHash)3218215474;


                case "PistolaPesada":
                    return (WeaponHash)3523564046;


                case "PistolaVintage":
                    return (WeaponHash)137902532;


                case "PistoladeBengalas":
                    return (WeaponHash)1198879012;


                case "PistoladeTirador":
                    return (WeaponHash)3696079510;


                case "RevolverPesado":
                    return (WeaponHash)3249783761;


                case "RevolverDobleAccion":
                    return (WeaponHash)2548703416;


                case "PistoladeRayos":
                    return (WeaponHash)2939590305;


                case "PistolaCeramica":
                    return (WeaponHash)727643628;


                case "RevolverLargo":
                    return (WeaponHash)2441047180;


                case "Uzi":
                    return (WeaponHash)324215364;


                case "MP5":
                    return (WeaponHash)736523883;


                case "SubfusildeAsalto":
                    return (WeaponHash)4024951519;


                case "ADPdeCombate":
                    return (WeaponHash)171789620;


                case "TEC9":
                    return (WeaponHash)3675956304;


                case "Skorpion":
                    return (WeaponHash)3173288789;


                case "Hellbringer":
                    return (WeaponHash)1198256469;







                case "EscopetaCorredera":
                    return (WeaponHash)487013001;


                case "EscopetaRecortada":
                    return (WeaponHash)2017895192;


                case "EscopetadeAsalto":
                    return (WeaponHash)3800352039;


                case "EscopetaBullpup":
                    return (WeaponHash)2640438543;


                case "Mosquete":
                    return (WeaponHash)2828843422;


                case "EscopetaPesada":
                    return (WeaponHash)984333226;


                case "EscopetaDobleCañon":
                    return (WeaponHash)4019527611;


                case "EscopetaSweeper":
                    return (WeaponHash)317205821;


                case "AK-47":
                    return (WeaponHash)3220176749;


                case "M4":
                    return (WeaponHash)2210333304;


                case "FusilAvanzado":
                    return (WeaponHash)2937143193;


                case "CarabinaEspecial":
                    return (WeaponHash)3231910285;


                case "FusilBullpup":
                    return (WeaponHash)2132975508;


                case "FusilCompacto":
                    return (WeaponHash)1649403952;



                case "Ametralladora":
                    return (WeaponHash)2634544996;


                case "AmetralladoradeCombate":
                    return (WeaponHash)2144741730;


                case "Gusenberg":
                    return (WeaponHash)1627465347;


                case "RifledeFrancotirador":
                    return (WeaponHash)100416529;


                case "RifledeFrancotiradorPesado":
                    return (WeaponHash)205991906;


                case "FusildeFrancotirador":
                    return (WeaponHash)3342088282;


                case "RPG":
                    return (WeaponHash)2982836145;


                case "Lanzagranadas":
                    return (WeaponHash)2726580491;


                case "LanzagranadasPolicial":
                    return (WeaponHash)1305664598;


                case "Minigun":
                    return (WeaponHash)1119849093;


                case "FuegosArtificiales":
                    return (WeaponHash)2138347493;


                case "Railgun":
                    return (WeaponHash)1834241177;


                case "LanzamisilesGuiado":
                    return (WeaponHash)1672152130;


                case "LanzagranadasCompacto":
                    return (WeaponHash)125959754;


                case "RayMinigun":
                    return (WeaponHash)3056410471;

                case "Granada":
                    return (WeaponHash)2481070269;


                case "GasLacrimogeno":
                    return (WeaponHash)2694266206;


                case "CoctelMolotov":
                    return (WeaponHash)615608432;


                case "C4":
                    return (WeaponHash)741814745;


                case "MinadeProximidad":
                    return (WeaponHash)2874559379;


                case "BoladeNieve":
                    return (WeaponHash)126349499;


                case "ExplosivoImprovisado":
                    return (WeaponHash)3125143736;


                case "Pelota":
                    return (WeaponHash)600439132;


                case "GranadadeHumo":
                    return (WeaponHash)4256991824;


                case "SeñalLuminosa":
                    return (WeaponHash)1233104067;


                case "LatadeGasolina":
                    return (WeaponHash)883325847;


                case "Paracaidas":
                    return (WeaponHash)4222310262;



                case "Extintor":
                    return (WeaponHash)101631238;


                case "LatadeGasolina2":
                    return (WeaponHash)3126027122;


                default:
                    return WeaponHash.Ball;
            }
        }
    }
}

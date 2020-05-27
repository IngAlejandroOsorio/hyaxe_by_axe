using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;

namespace DowntownRP_cs.game.company
{
    public class CompanyMain : Events.Script
    {
        private static Ped Secretary = null;
        private static HtmlWindow contractBrowser;
        public CompanyMain()
        {
            Events.Add("GenerateSecretaryPedCompany", GenerateSecretaryPedCompany);
            Events.Add("CreateContractCompanyBrowser", CreateContractCompanyBrowser);
            Events.Add("PayContractCompanyBrowser", PayContractCompanyBrowser);
            Events.Add("CobrarContractCompany", CobrarContractCompany);
            Events.Add("DestroyContractCompanyBrowser", DestroyContractCompanyBrowser);
            Events.Add("SignContractCompanySS", SignContractCompanySS);
            Events.Add("CloseContractCompanySS", CloseContractCompanySS);
        }

        private void CloseContractCompanySS(object[] args)
        {
            Events.CallRemote("CloseContractCompany");
        }

        private void SignContractCompanySS(object[] args)
        {
            Events.CallRemote("SignContractCompany");
        }

        private void CreateContractCompanyBrowser(object[] args)
        {
            contractBrowser = new HtmlWindow("package://statics/company/contrato.html");
            Cursor.Visible = true;
        }

        private void CobrarContractCompany(object[] args)
        {
            contractBrowser.Destroy();
            Cursor.Visible = false;
            Events.CallRemote("CobrarContractCompany");
        }

            private void PayContractCompanyBrowser(object[] args)
        {
            contractBrowser = new HtmlWindow("package://statics/company/pago.html");
            //contractBrowser.ExecuteJs("pago(" + args[0] + ");");
            Cursor.Visible = true;
        }

        private void DestroyContractCompanyBrowser(object[] args)
        {
            contractBrowser.Destroy();
            Cursor.Visible = false;
        }

        private void GenerateSecretaryPedCompany(object[] args)
        {
            int dimension = (int)args[0];

            Vector3 SecretaryPos = new Vector3(-139.0331f, -633.9948f, 168.8205f);
            if (Secretary != null) Secretary.Destroy();
            Secretary = new Ped(0x50610C43, SecretaryPos, 0, (uint)dimension);
        }
    }
}

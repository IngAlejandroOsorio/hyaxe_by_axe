using Newtonsoft.Json;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntownRP_cs.game.phone
{
    public class PhoneBrowser : Events.Script
    {
        public static RAGE.Ui.HtmlWindow window;
        public static List<data.PhoneContact> phoneContacts = null;
        public PhoneBrowser()
        {
            Events.Add("OpenPhoneCef", OpenPhoneCef);
            Events.Add("ClosePhoneCef", ClosePhoneCef);
            Events.Add("loadContacts", loadContacts);
        }

        private void loadContacts(object[] args)
        {
            if(phoneContacts != null)
            {
                if(phoneContacts.Count != 0)
                {
                    foreach (var contact in phoneContacts)
                        window.ExecuteJs($"document.getElementById('phone').contentWindow.addNewContact({contact.name}, {contact.phone});");
                }
            }
        }

        private void ClosePhoneCef(object[] args)
        {
            window.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        private void OpenPhoneCef(object[] args)
        {
            phoneContacts = JsonConvert.DeserializeObject<List<data.PhoneContact>>(args[0].ToString());
            window = new RAGE.Ui.HtmlWindow("package://statics/phone/view.html");
            RAGE.Ui.Cursor.Visible = true;
        }
    }
}

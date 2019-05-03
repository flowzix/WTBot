using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    [Serializable]
    public class ProfileInfo : NameIdentifiedInfo
    {
        public static int id;
        public ProfileInfo()
        {

        }
        public const String UTF8 = "%E2%9C%93";
        public const String en_commit = "process+payment";
        public string FullName;
        public string Email;
        public string TelNr;
        public string Address1;
        public string Address2;
        public string City;
        public string Postcode;
        public string Country;
        public string CardType;
        public string CardNr;
        public string ExpMonth;
        public string ExpYear;
        public string CVV;
        public override string ToString()
        {
            return name;
        }

        public override LvItem GetLvRepresentation()
        {
            return new LvProfileItem(this);
        }
    }
}

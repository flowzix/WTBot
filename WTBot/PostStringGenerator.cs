using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    static class PostStringGenerator
    {
        public static string generateCheckoutPostString(ProfileInfo info, String authenticity_token, String reCaptcha)
        {
            StringBuilder postString = new StringBuilder();
            postString.Append("utf8=");
            postString.Append(ProfileInfo.UTF8);
            postString.Append("&authenticity_token=");
            postString.Append(Uri.EscapeDataString(authenticity_token));
            postString.Append("&order%5Bbilling_name%5D=");
            postString.Append(info.FullName.Replace(" ", "+"));
            postString.Append("&order%5Bemail%5D=");
            postString.Append(info.Email.Replace("@", "%40"));
            postString.Append("&order%5Btel%5D=");
            postString.Append(info.TelNr.Replace("+", "%2B"));
            postString.Append("&order%5Bbilling_address%5D=");
            postString.Append(info.Address1.Replace(" ", "+"));
            postString.Append("&order%5Bbilling_address_2%5D=");
            postString.Append(info.Address2.Replace(" ", "+"));
            postString.Append("&order%5Bbilling_address_3%5D=");
            postString.Append("");    // address3 will be null - this line has no effect at all
            postString.Append("&order%5Bbilling_city%5D=");
            postString.Append(info.City.Replace(" ", "+"));
            postString.Append("&order%5Bbilling_zip%5D=");
            postString.Append(info.Postcode);
            postString.Append("&order%5Bbilling_country%5D=");
            postString.Append(CountryConverter.GetCountryCode(info.Country));
            postString.Append("&same_as_billing_address=1&store_credit_id=");
            postString.Append("&credit_card%5Btype%5D=");
            postString.Append(info.CardType.ToLower());
            postString.Append("&credit_card%5Bcnb%5D=");
            string cardno = info.CardNr;
            cardno = cardno.Trim();
            cardno = cardno.Substring(0, 4) + "+" + cardno.Substring(4, 4) + "+" + cardno.Substring(8, 4) + "+" + cardno.Substring(12, 4);
            postString.Append(cardno);
            postString.Append("&credit_card%5Bmonth%5D=");
            postString.Append(info.ExpMonth);
            postString.Append("&credit_card%5Byear%5D=");
            postString.Append(info.ExpYear);
            postString.Append("&credit_card%5Bvval%5D=");
            postString.Append(info.CVV);
            postString.Append("&order%5Bterms%5D=0&order%5Bterms%5D=1");
            postString.Append("&g-recaptcha-response=");
            postString.Append(reCaptcha);
            postString.Append("&hpcvv=");
            return postString.ToString();

        }
        public static string generateAddCartPostString(string size, string style)
        {
            return "utf8=%E2%9C%93&style=" + style + "&size=" + size + "&commit=add+to+basket";
        }
    }
}

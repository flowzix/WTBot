using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    public static class CountryConverter
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>()
        {
                        {"UK","GB"},
                        {"UK (N. IRELAND)","NB"},
                        {"AT","AUSTRIA"},
                        {"BELARUS","BY"},
                        {"BELGIUM","BE"},
                        {"BULGARIA","BG"},
                        {"CROATIA","HR"},
                        {"CZECH REPUBLIC","CZ"},
                        {"DENMARK","DK"},
                        {"ESTONIA","EE"},
                        {"FINLAND","FI"},
                        {"FRANCE","FR"},
                        {"GERMANY","DE"},
                        {"GREECE","GR"},
                        {"HUNGARY","HU"},
                        {"ICELAND","IS"},
                        {"IRELAND","IE"},
                        {"ITALY","IT"},
                        {"LATVIA","LV"},
                        {"LITHUANIA","LT"},
                        {"LUXEMBOURG","LU"},
                        {"MONACO","MC"},
                        {"NETHERLANDS","NL"},
                        {"NORWAY","NO"},
                        {"POLAND","PL"},
                        {"PORTUGAL","PT"},
                        {"ROMANIA","RO"},
                        {"RUSSIA","RU"},
                        {"SLOVAKIA","SK"},
                        {"SLOVENIA","SI"},
                        {"SPAIN","ES"},
                        {"SWEDEN","SE"},
                        {"SWITZERLAND","CH"},
                        {"TURKEY","TR"}
        };
        // POLAND -> PL etc.
       public static string GetCountryCode(string country)
        {
            return dict[country];
        }
    }
}

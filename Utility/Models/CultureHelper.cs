using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.EnumData;

namespace Utility.Models
{
    public class CultureHelper
    {
        public class Culture
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
        public static String GetCulture(String culture)
        {
            bool isImplement = false;

            if (!String.IsNullOrEmpty(culture))
            {
                isImplement = (GetAllCultures().Where(x => x.Value.Equals(culture)).Count() > 0) ? true : false;
            }
            if (!isImplement)
            {
                return getDefaultCulture();
            }
            else
            {
                return culture;
            }
        }

        public static IEnumerable<Culture> GetAllCultures()
        {
            List<Culture> cultureList = new List<Culture>();

            foreach(CultureEnum cl in Enum.GetValues(typeof(CultureEnum)))
            {
                cultureList.Add(new Culture
                {
                    Key = cl.ToIntValue(),
                    Value = cl.GetDescription()
                });
            }
            return cultureList.AsEnumerable<Culture>();
        }

        private static string getDefaultCulture()
        {
            return CultureEnum.enUS.GetDescription();
        }

    }   
}

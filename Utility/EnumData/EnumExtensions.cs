using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.EnumData
{
    public static class EnumExtensions
    {
        public static int ToIntValue(this Enum self)
        {
            return Convert.ToInt16(self);
        }

        public static string GetDescription (this Enum self)
        {
            FieldInfo fi = self.GetType().GetField(self.ToString());
            DescriptionAttribute[] attributes = null;
            if (fi != null)
            {
                attributes =
                    (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes!=null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            return self.ToString();
        }
    }
}

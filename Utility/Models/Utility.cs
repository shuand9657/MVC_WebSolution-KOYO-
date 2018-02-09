using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityModelClass.Models
{
    public partial class Utility
    {
        private static string skipItem = "ChangeTracker";

        public static T CopyShadow<T>(object src) where T : new()
        {
            T returnObject = new T();

            var srcT = src.GetType();
            var dstT = returnObject.GetType();
            foreach (var f in srcT.GetFields())
            {
                var dstF = dstT.GetField(f.Name);
                if (dstF == null)
                    continue;

                if (f.FieldType == typeof(System.String))
                {
                    string temp = (String)f.GetValue(src);
                    if (!string.IsNullOrWhiteSpace(temp))
                        dstF.SetValue(returnObject, temp.Trim());
                }
                else
                    dstF.SetValue(returnObject, f.GetValue(src));
            }
            foreach (var f in srcT.GetProperties())
            {
                if (f.Name != skipItem && f.CanWrite)
                {
                    var dstF = dstT.GetProperty(f.Name);
                    if (dstF == null)
                        continue;
                    if (f.PropertyType == typeof(System.String))
                    {
                        string temp = (String)f.GetValue(src, null);
                        if (!string.IsNullOrWhiteSpace(temp))
                            dstF.SetValue(returnObject, temp.Trim(), null);
                    }
                    else
                        dstF.SetValue(returnObject, f.GetValue(src, null), null);
                }
            }
            return returnObject;
        }
    }
}
using DBModelClass.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Utility.EnumData
{
    /// <summary>
    /// IEnumerable Extensions
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string JoinString(this IEnumerable<string> values)
        {
            return JoinString(values, ",");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="values"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string JoinString(this IEnumerable<string> values, string split)
        {
            var result = values.Aggregate(string.Empty, (current, value) => current + (split + value));
            result = result.TrimStart(split.ToCharArray());
            return result;
        }

        /// <summary>
        /// Do somthing for each item in the IEnumerable
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">Source IEnumerable</param>
        /// <param name="action">Action for doing something</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
            return source;
        }

        /// <summary>
        /// Convert IEnumerable to IList<SelectListItem>
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">IEnumerable</param>
        /// <param name="text">Func for display text of IEnumerable</param>
        /// <param name="value">Func for value of IEnumerable</param>
        /// <returns>IList of SelectListItem</returns>
        public static IList<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source, Func<T, object> text, Func<T, object> value, bool isOrder = false)
        {
            return source.ToSelectList(text, value, null, null);
        }

        /// <summary>
        /// Convert IEnumerable to IList<SelectListItem>
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">IEnumerable</param>
        /// <param name="text">Func for display text of IEnumerable</param>
        /// <param name="value">Func for value of IEnumerabl</param>
        /// <param name="optionalText">Extra option</param>
        /// <returns>IList of SelectListItem</returns>
        public static IList<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source, Func<T, object> text, Func<T, object> value,
            string optionalText, bool isOrder = false)
        {
            return source.ToSelectList(text, value, null, optionalText, isOrder);
        }

        /// <summary>
        /// Convert IEnumerable to IList<SelectListItem>
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">IEnumerable</param>
        /// <param name="text">Func for display text of IEnumerable</param>
        /// <param name="value">Func for value of IEnumerabl</param>
        /// <param name="selected">Func for Seleced item</param>
        /// <returns>IList of SelectListItem</returns>
        public static IList<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source, Func<T, object> text, Func<T, object> value,
            Func<T, bool> selected, bool isOrder = false)
        {
            return source.ToSelectList(text, value, selected, null);
        }


        /// <summary>
        /// Convert IEnumerable to IList<SelectListItem>
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">IEnumerable</param>
        /// <param name="text">Func for display text of IEnumerable</param>
        /// <param name="value">Func for value of IEnumerabl</param>
        /// <param name="selected">Func for Seleced item</param>
        /// <param name="optionalText">Extra option</param>
        /// <returns>IList of SelectListItem</returns>
        public static IList<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source, Func<T, object> text, Func<T, object> value,
            Func<T, bool> selected, string optionalText, bool isOrder = false)
        {
            return source.ToSelectList(text, value, selected, optionalText, string.Empty, isOrder);
        }


        /// <summary>
        /// Core function for EnumerableExtension.cs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="selected"></param>
        /// <param name="optionalText"></param>
        /// <param name="optionalValue"></param>
        /// <returns></returns>
        private static IList<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> source, Func<T, object> text, Func<T, object> value,
            Func<T, bool> selected, string optionalText, string optionalValue, bool isOrder = false)
        {
            var items = new List<SelectListItem>();

            //預設會先加入optionalText
            if (!string.IsNullOrEmpty(optionalText))
            {
                items.Insert(0, new SelectListItem() { Text = optionalText, Value = optionalValue });
            }

            if (source == null)
            {
                return items;
            }


            foreach (var entity in source)
            {
                var item = new SelectListItem();
                item.Text = text(entity).ToString();
                item.Value = value(entity).ToString();
                if (selected != null)
                {
                    item.Selected = selected(entity);
                }

                if (item.Value != optionalValue)
                {
                    items.Add(item);
                }
            }

            if (isOrder)
            {
                items = items.OrderBy(d => d.Text).ToList();
            }

            return items;
        }
    }

    public static class StringExtensions
    {
        public static string ToAutoMuiltiLang(this string source)
        {
            return POEntityResources.ResourceManager.GetString(source);
        }
    }
}

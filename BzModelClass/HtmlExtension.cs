using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace BzModelClass.Helpers
{
    public static class HtmlExtension
    {
        public static MvcHtmlString ValidateMessageTooltipFor<TModel,TValue> 
            (this HtmlHelper<TModel>html,Expression<Func<TModel,TValue>> expression,
            object httpAttributes)
        {
            //            return ValidateMessageFor(html, expression, new RouteValueDictionary(htmlAttribute));
            return null;
        }
    }
}

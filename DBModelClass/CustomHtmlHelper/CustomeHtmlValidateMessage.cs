using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Properties;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc.Html;

namespace DBModelClass.CustomHtmlHelper
{
    public static class CustomeHtmlValidateMessage
    {   
        
        public static MvcHtmlString ValidationMessageTolltipFor<TModel,TProperty>
            (this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IDictionary<string,object> htmlAttribute)
        {
            TagBuilder containerBuilder = new TagBuilder("div");
            containerBuilder.AddCssClass("tip_trigger");
            containerBuilder.InnerHtml = "*";

            TagBuilder midBuilder = new TagBuilder("div");
            //midBuilder.AddCssClass("classic");
            midBuilder.AddCssClass("tip");
            midBuilder.InnerHtml = htmlHelper.ValidationMessageFor(expression).ToString();

            htmlAttribute.Add("kendo-tooltip", "");
            htmlAttribute.Add("k-content", midBuilder.InnerHtml);

            containerBuilder.InnerHtml += midBuilder.ToString(TagRenderMode.Normal);
            var msg = MvcHtmlString.Create(containerBuilder.InnerHtml.ToString());

            return htmlHelper.ValidationMessageFor(expression, "*", htmlAttribute);
        }
        public static MvcHtmlString ValidationMessageTolltipFor<TModel, TProperty>
             (this HtmlHelper<TModel> htmlHelper,
             Expression<Func<TModel, TProperty>> expression)
        {
            TagBuilder containerBuilder = new TagBuilder("div");
            containerBuilder.Attributes.Add("kendo-tooltip title", htmlHelper.ValidationMessageFor(expression).ToString()) ;
            containerBuilder.AddCssClass("tip_trigger");
            containerBuilder.InnerHtml = "*";

            TagBuilder midBuilder = new TagBuilder("div");
            midBuilder.AddCssClass("tip");
            midBuilder.InnerHtml = htmlHelper.ValidationMessageFor(expression).ToString();

            containerBuilder.InnerHtml += midBuilder.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(containerBuilder.ToString(TagRenderMode.Normal));
        }

        //public static MvcHtmlString ValidationMessageWithTolltipFor<TModel,TProperty> (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        //{
        //    var htmlAttribute = new Dictionary<string, object>();
        //    return htmlHelper.ValidationMessageWithTolltipFor(expression, htmlHelper);
        //}

        public static MvcHtmlString ValidationMessageWithTooltipFor<TModel, TProperty>
                (this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel,TProperty>> expression, 
                    IDictionary<string,object> htmlAttribute)
        {
            ModelMetadata modelMetaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            TagBuilder containerBuilder = new TagBuilder("div");
            containerBuilder.AddCssClass("tip_trigger");
            containerBuilder.InnerHtml = "*";

            TagBuilder midBuilder = new TagBuilder("div");
            //midBuilder.AddCssClass("classic");
            midBuilder.AddCssClass("tip");
            midBuilder.InnerHtml = htmlHelper.ValidationMessageFor(expression).ToString();

            htmlAttribute.Add("kendo-tooltip", "");
            htmlAttribute.Add("k-content", midBuilder.InnerHtml);

            containerBuilder.InnerHtml += midBuilder.ToString(TagRenderMode.Normal);

            if (htmlAttribute == null)
            {
                htmlAttribute = new Dictionary<string, object>();
            }
            else
            {
                return MvcHtmlString.Empty;
            }

            return htmlHelper.ValidationMessageFor(expression, containerBuilder.ToString(TagRenderMode.Normal), htmlAttribute);
        }
    }

}

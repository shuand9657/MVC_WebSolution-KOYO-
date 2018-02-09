using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;

namespace NgVal
{
    public static class NgValExtensions
    {
        public static MvcHtmlString NgValFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return NgValFor(htmlHelper, expression, null /* validationMessage */, new RouteValueDictionary());
        }

        public static MvcHtmlString NgValidationFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return NgValidationFor(htmlHelper, expression, null /* validationMessage */, new RouteValueDictionary());
        }

        public static MvcHtmlString ValFor<TModel, TProerty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,TProerty>> expression)
        {
            return ValFor(htmlHelper, expression, null, new RouteValueDictionary());
        }

        public static MvcHtmlString NgValFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage)
        {
            return NgValFor(htmlHelper, expression, validationMessage, new RouteValueDictionary());
        }

        public static MvcHtmlString NgValFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, object htmlAttributes)
        {
            return NgValFor(htmlHelper, expression, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        public static MvcHtmlString NgValFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            var name = ExpressionHelper.GetExpressionText(expression);
            var validations = ModelValidatorProviders.Providers.GetValidators(
                    metadata ?? ModelMetadata.FromStringExpression(name, new ViewDataDictionary()),
                    new ControllerContext())
                    .SelectMany(v => v.GetClientValidationRules()).ToArray();

            List<ValidatorMessage> validatorMessages = new List<ValidatorMessage>();
            ValidatorMessage vM = new ValidatorMessage();

            foreach(var validation in validations)
            {
                var dictionaryType = DictionaryValidationType.Where(x => x.Key == validation.ValidationType).ToArray();

                if (dictionaryType.Any())
                {
                    validatorMessages.AddRange(dictionaryType.Select(type => new ValidatorMessage()
                    {
                        Type = type.Value,
                        Message = validation.ErrorMessage.Replace("'", "")
                    }));
                }
                else
                {
                    ValidatorMessage validatorMessage = new ValidatorMessage()
                    {
                        Type = validation.ValidationType,
                        Message = validation.ErrorMessage.Replace("'", "")
                    };

                    validatorMessages.Add(validatorMessage);
                }

            }
            //TagBuilder tag = new TagBuilder("ngVal");
            //tag.Attributes.Add("validation", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name));

            //if (validations.Any())
            //{
            //    foreach (var val in validations)
            //    {
            //        string valResult = GetNgValDirectiveString(validatorMessages);
            //        tag.Attributes.Add("kendo-tooltip k-content", "'" + valResult + "'");
            //    }
            //};  

            //var validatorMessages = validations.ToDictionary(k => k.ValidationType, v => v.ErrorMessage);

            string result = "";

            result += GetNgValDirectiveString(validatorMessages);
            result += GetValidatorDirectivesString(validations);

            //string validatorMessagesStr = "{";
            //foreach (var validatorMessage in validatorMessages)
            //{
            //    validatorMessagesStr += validatorMessage.Key + ":'" + validatorMessage.Value + "',";
            //}
            //validatorMessagesStr += "}";

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString NgValidationFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            var name = ExpressionHelper.GetExpressionText(expression);
            var validations = ModelValidatorProviders.Providers.GetValidators(
                    metadata ?? ModelMetadata.FromStringExpression(name, new ViewDataDictionary()),
                    new ControllerContext())
                    .SelectMany(v => v.GetClientValidationRules()).ToArray();

            List<ValidatorMessage> validatorMessages = new List<ValidatorMessage>();
            ValidatorMessage vM = new ValidatorMessage();

            foreach (var validation in validations)
            {
                var dictionaryType = DictionaryValidationType.Where(x => x.Key == validation.ValidationType).ToArray();

                if (dictionaryType.Any())
                {
                    validatorMessages.AddRange(dictionaryType.Select(type => new ValidatorMessage()
                    {
                        Type = type.Value,
                        Message = validation.ErrorMessage.Replace("'", "")
                    }));
                }
                else
                {
                    ValidatorMessage validatorMessage = new ValidatorMessage()
                    {
                        Type = validation.ValidationType,
                        Message = validation.ErrorMessage.Replace("'", "")
                    };

                    validatorMessages.Add(validatorMessage);
                }

            }
         
            string result = "";
            result += setNGValDirectiveString();
            result += GetValidatorDirectivesString(validations);
           
            return MvcHtmlString.Create(result);
            //return new MvcHtmlString(result);
        }

        public static MvcHtmlString ValFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            var name = ExpressionHelper.GetExpressionText(expression);
            var validations = ModelValidatorProviders.Providers.GetValidators(
                    metadata ?? ModelMetadata.FromStringExpression(name, new ViewDataDictionary()),
                    new ControllerContext())
                    .SelectMany(v => v.GetClientValidationRules()).ToArray();

            List<ValidatorMessage> validatorMessages = new List<ValidatorMessage>();
            List<ValidatorMessage> listQ = new List<ValidatorMessage>();

            foreach (var validation in validations)
            {
                var dictionaryType = DictionaryValidationType.Where(x => x.Key == validation.ValidationType).ToArray();

                if (dictionaryType.Any())
                {
                    validatorMessages.AddRange(dictionaryType.Select(type => new ValidatorMessage()
                    {
                        Type = type.Value,
                        Message = validation.ErrorMessage.Replace("'", "")
                    }));
                }
                else
                {
                    ValidatorMessage validatorMessage = new ValidatorMessage()
                    {
                        Type = validation.ValidationType,
                        Message = validation.ErrorMessage.Replace("'", "")
                    };

                    validatorMessages.Add(validatorMessage);
                }
            }
            
            Console.WriteLine(listQ);

            string result = "";
            //result += GetNgValDirectiveString(validatorMessages);
            result += GetValidatorDirectivesString(validations);

            return MvcHtmlString.Create(result);
            //return new MvcHtmlString(result);
        }
        private static string GetValidatorDirectivesString(IEnumerable<ModelClientValidationRule> validations)
        {
            var result = "";
            foreach (var val in validations)
            {
                result += " " + ConvertMvcClientValidatorToAngularValidatorString(val);
            }
            return result;
        }

        private static string ConvertMvcClientValidatorToAngularValidatorString(ModelClientValidationRule val)
        {
            switch (val.ValidationType.ToLower())
            {
                case "required":
                    return "required";
                case "range":
                    return string.Format("min=\"{0}\" max=\"{1}\"", val.ValidationParameters["min"], val.ValidationParameters["max"]);
                case "daterange":
                    return string.Format("min=\"{0}\" max=\"{1}\"" , val.ValidationParameters["MinDate"], val.ValidationParameters["MaxDate"]);
                case "regex":
                    return string.Format("ng-pattern=\"{0}\"", val.ValidationParameters["pattern"]);
                case "length":
                    string lengthRes = "";
                    if (val.ValidationParameters.ContainsKey("min"))
                        lengthRes += string.Format("ng-minlength=\"{0}\" ", val.ValidationParameters["min"]);
                    if (val.ValidationParameters.ContainsKey("max"))
                        lengthRes += string.Format("ng-maxlength=\"{0}\"", val.ValidationParameters["max"]);
                    return lengthRes.TrimEnd();
                default:
                    return string.Format("{0}=\"{1}\"", val.ValidationType, Json.Encode(val.ValidationParameters));
            }
        }

        private static string GetNgValDirectiveString(Dictionary<string, string> validatorMessages)
        {
            return string.Format("ngval='{0}'", Json.Encode(validatorMessages));
        }
        private static string setNGValDirectiveString()
        {
            return string.Format("ngval-Koyo");
        }

        private static string GetNgValDirectiveString(IEnumerable<ValidatorMessage> validatorMessages)
        {
            return string.Format("ngval='{0}'", Json.Encode(validatorMessages));
        }

        private static readonly List<DictionaryType> DictionaryValidationType = new List<DictionaryType>()
        {
            new DictionaryType(){Key = "regex", Value = "pattern"},
            new DictionaryType(){Key = "range", Value = "min"},
            new DictionaryType(){Key = "range", Value = "max"},
            new DictionaryType() {Key="daterange",Value="min" },
            new DictionaryType() {Key="daterange", Value="max" }
        };
    }

    public class ValidatorMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    class DictionaryType
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

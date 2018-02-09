using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DBModelClass
{
    public class DateRangeAttribute : ValidationAttribute ,  IClientValidatable
    {
        private const string DateFormat = "{0:yyyy-MM-dd}";
        private const string DefaultErrorMessage = "'{0}' must be a date between {1} and {2}";

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string StrMinDate { get; set; }
        public string StrMaxDate { get; set; }

        public DateRangeAttribute(string minDate, string maxDate) : base(DefaultErrorMessage)
        {
            MinDate = ParseDate(minDate); 
            MaxDate = ParseDate(maxDate);
        }

        public DateRangeAttribute(int minMonth, int maxMonth) : base(DefaultErrorMessage)
        {
            StrMinDate = string.Format(DateFormat, DateTime.Now.AddMonths(minMonth));
            StrMaxDate = string.Format(DateFormat, DateTime.Now.AddMonths(maxMonth));
        }

        public DateRangeAttribute(enumSetDateRangeType type , int minRange, int maxRange)
        {
            switch ((enumSetDateRangeType)type)
            {
                case enumSetDateRangeType.setYear:
                    StrMinDate = string.Format(DateFormat, DateTime.Now.AddYears(minRange));
                    StrMaxDate = string.Format(DateFormat, DateTime.Now.AddYears(maxRange));
                    break;
                case enumSetDateRangeType.setMonth:
                    StrMinDate = string.Format(DateFormat, DateTime.Now.AddMonths(minRange));
                    StrMaxDate = string.Format(DateFormat, DateTime.Now.AddMonths(maxRange));
                    break;
                case enumSetDateRangeType.setDay:
                    StrMinDate = string.Format(DateFormat, DateTime.Now.AddDays(minRange));
                    StrMaxDate = string.Format(DateFormat, DateTime.Now.AddDays(maxRange));
                    break;
            }
        }

        
        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }
            DateTime dateValue = (DateTime)value;
            return MinDate <= dateValue && dateValue <= MaxDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinDate, MaxDate);
        }

        private DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("min", StrMinDate);
            rule.ValidationParameters.Add("max", StrMaxDate);
            rule.ValidationType = "range";

            yield return rule;
        }

        public enum enumSetDateRangeType : short
        {
            setYear = 0,
            setMonth = 1,
            setDay = 2,
            setDacade = 3
        }
    }
}

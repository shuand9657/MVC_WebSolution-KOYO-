using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DBModelClass.App_GlobalResources;
using System.Web.Mvc;

namespace DBModelClass.DBModel
{
    public class POEntityViewModel
    {
        [Key]
        public int POID { get; set; }

        [Display(Name = "PONo")]
        [Required]
        [StringLength(10,MinimumLength = 3)]
        public string PONo { get; set; }                                           

        [Display(Name = "MasterID")]
        [Required]
        [Range(minimum: 1, maximum: 6)]
        public int MasterID { get; set; }

        [Display(Name = "ProjectID")]
        [Required]
        public int ProjectID { get; set; }

        [Display(Name ="Price Range")]
        [Range(minimum:20,maximum:100)]
        public int PriceRange { get; set; }

        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="{0} field required valid email format!")]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [RegularExpression(@"/^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).{8,}$/", ErrorMessage ="Error on {0} field, required at lease 1 upper case , 1 lower case, 1 digit, at lease 8 digit length ")]
        public string Password { get; set; }

        [Display(Name ="Create Date")]
        [Required()]
        [DataType(DataType.Date)]
        [DateRange(DateRangeAttribute.enumSetDateRangeType.setYear,minRange: -2, maxRange: 5)]
        public DateTime CreateDate { get; set; }

        [Display(Name ="Month Range")]
        [DateRange(DateRangeAttribute.enumSetDateRangeType.setMonth, minRange: -2, maxRange: 2)]
        public DateTime MonthRange { get; set; }

        [Display(Name = "Day Range")]
        [DateRange(DateRangeAttribute.enumSetDateRangeType.setDay, minRange: -3, maxRange: 5)]
        public DateTime DayRange { get; set; }

        [Display(Name = "Year Range")]
        [DateRange(DateRangeAttribute.enumSetDateRangeType.setYear, minRange: -1, maxRange: 2)]
        public DateTime YearRange { get; set; }

        [Display(Name ="Term of Use")]
        [Required]
        public bool TermofUse { get; set; }

        [Display(Name ="Org Master")]
        [Required]
        public SelectList MasterList { get; set; }

        [Display(Name ="Gender :")]
        [Required()]
        public List<Gender> GenderList { get; set; }
    }
}

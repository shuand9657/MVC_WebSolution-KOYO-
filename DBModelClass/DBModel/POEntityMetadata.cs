using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBModelClass.DBModel
{
    internal sealed class POEntityMetadata
    {
        [Key]
        public int POID { get; set; }

        [Display(Name = "PO No.", Description = "Purchase Order Number")]
        [Required(ErrorMessage = "PO No. is required!")]
        public string PONo { get; set; }

        [Display(Name = "Master Name", Description = "Master Name")]
        [Required(ErrorMessage = "Master is required!")]
        public int MasterID { get; set; }
        [Display(Name = "Project Name", Description = "Project Name")]
        [Required(ErrorMessage = "Project Name is required!")]
        public int ProjectID { get; set; }
        [Required(ErrorMessage ="Supplier is required!")]
        public int SupplierID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please pick the date!")]
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public Nullable<System.DateTime> TranDate { get; set; }
        public string Memo { get; set; }
    }
}

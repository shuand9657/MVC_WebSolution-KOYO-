using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DBModelClass.DBModel
{
    public class POOrgMasterViewModel
    {
        public int MasterID { get; set; }
        
        public int DepartID { get; set; }

        public int UserID { get; set; }

        public string OrgSn { get; set; }
        public string DepartSN { get; set; }
        public string UserName { get; set; }
        [Display(Name ="Company Name")]
        public SelectList MasterList { get; set; }
        [Display(Name ="Department Name")]
        public SelectList DepartList { get; set; }
        [Display(Name = "User Name")]
        public SelectList UserList { get; set; }
    }
}

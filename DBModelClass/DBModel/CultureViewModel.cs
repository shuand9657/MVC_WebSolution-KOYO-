using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModelClass.App_GlobalResources;

namespace DBModelClass.DBModel
{
    public class CultureViewModel
    {
        [Display(Name = "CultureName", ResourceType = typeof(CultureVMResource))]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.ViewModels
{
    public class AssetTypeViewModel
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Asset Type")]
        public string TypeName { get; set; }

        [MaxLength(255)]
        public string Comments { get; set; }
    }
}

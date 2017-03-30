using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Models
{
    public class AssetType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Asset Type")]
        public string TypeName { get; set; }

        public IEnumerable<AccountType> AccountTypes { get; set; }
    }
}

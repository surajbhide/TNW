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

        public string Comments { get; set; }

        public ApplicationUser Owner { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public ICollection<PortfolioAccount> PortfolioAccounts { get; set; }
    }
}

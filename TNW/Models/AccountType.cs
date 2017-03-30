using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Models
{
    public class AccountType
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        [MaxLength(255)]
        public string Name { get; set; }

        public AssetType AssetType { get; set; }

        public int AssetTypeId { get; set; }
    }
}

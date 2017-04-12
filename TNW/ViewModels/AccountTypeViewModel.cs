using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.ViewModels
{
    public class AccountTypeViewModel
    {
        [Required]
        [Display(Name = "Account Type")]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Comments { get; set; }
    }
}

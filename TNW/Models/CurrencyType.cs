using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Models
{
    public class CurrencyType
    {
        public int Id { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }
        public ICollection<PortfolioAccount> PortfolioAccounts { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNW.Models;

namespace TNW.ViewModels
{
    public class PortfolioAccountViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Account Holder Name")]
        public string AccountHolder { get; set; }

        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Financial Institute")]
        public string FinancialInstitution { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }
        public int AccountTypeId { get; set; }

        [Required]
        public AssetType AssetType { get; set; }
        public int AssetTypeId { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Models
{
    public class PortfolioAccount
    {
        public int Id { get; set; }

        public ApplicationUser Owner { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Display(Name = "Account Holder Name")]
        public string AccountHolder { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Financial Institute")]
        public string FinancialInstitution { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        public ICollection<AccountValue> MonthlyBalances { get; set; }

        public AccountType AccountType { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

        public AssetType AssetType { get; set; }

        [Required]
        public int AssetTypeId { get; set; }

        [Display(Name = "Currency")]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        public int CurrencyTypeId { get; set; }
    }
}

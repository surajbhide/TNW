﻿using System;
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

        [Required]
        public ApplicationUser Owner { get; set; }
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

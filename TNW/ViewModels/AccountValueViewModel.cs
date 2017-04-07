using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNW.Models;

namespace TNW.ViewModels
{
    public class AccountValueViewModel
    {
        public int Id { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name = "Account Value")]
        public decimal MonthlyBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Month and Year")]
        public DateTime MonthYear { get; set; }

        public PortfolioAccount PortfolioAccount { get; set; }

        [Display(Name = "Portfolio Account")]
        public int PortfolioAccountId { get; set; }
    }
}

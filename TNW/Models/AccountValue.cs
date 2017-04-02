using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Models
{
    public class AccountValue
    {
        public int Id { get; set; }

        [Column(TypeName = "Money")]
        public decimal MonthlyBalance { get; set; }

        public DateTime MonthYear { get; set; }

        [Required]
        public PortfolioAccount PortfolioAccount { get; set; }
        public int PortfolioAccountId { get; set; }
    }
}

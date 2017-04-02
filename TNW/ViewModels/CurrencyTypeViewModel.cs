using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.ViewModels
{
    public class CurrencyTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Model
{
    public class Currency
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
        public double RateFormated { get; set; }
        public double Diffformated { get; set; }
        public double Rate { get; set; }
        public string Name { get; set; }
        public double Diff { get; set; }
        public DateTime Date { get; set; }
        public DateTime ValidFromDate { get; set; }
    }
}

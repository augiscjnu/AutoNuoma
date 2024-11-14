using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Models
{
    public class NaftosAutomobilis : Automobilis
    {
        public decimal VariklioTuris { get; set; }
        public string DegaluTipas { get; set; }
        public decimal CO2Ismetimas { get; set; }
    }
}

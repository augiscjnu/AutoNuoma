using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Models
{
    public class ElektrinisAutomobilis : Automobilis
    {
        public decimal BaterijosTalpa { get; set; }
        public int MaxNuvaziuojamasAtstumas { get; set; }
        public decimal IkrovimoLaikas { get; set; }
    }
}

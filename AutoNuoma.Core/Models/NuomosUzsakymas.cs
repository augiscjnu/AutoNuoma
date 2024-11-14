using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Models
{
    public class NuomosUzsakymas
    {
        public int Id { get; set; }
        public int KlientasId { get; set; }
        public int DarbuotojasId { get; set; }
        public int AutomobilisId { get; set; }
        public DateTime PradziosData { get; set; }
        public DateTime PabaigosData { get; set; }
        public decimal Kaina { get; set; }
    }
}

using AutoNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Contracts
{
    public interface INuomosUzsakymasRepository
    {
        List<NuomosUzsakymas> GetAllNuomosUzsakymai(); 
        NuomosUzsakymas GetNuomosUzsakymasById(int id); 
        void AddNuomosUzsakymas(NuomosUzsakymas nuomosUzsakymas); 
        void UpdateNuomosUzsakymas(NuomosUzsakymas nuomosUzsakymas); 
        void DeleteNuomosUzsakymas(int id); 
        List<NuomosUzsakymas> GetNuomosUzsakymaiByAutomobilisId(int automobilisId); 
        List<NuomosUzsakymas> GetNuomosUzsakymaiByKlientasId(int klientasId); 
        List<NuomosUzsakymas> GetNuomosUzsakymaiByDarbuotojasId(int darbuotojasId);
    }
}





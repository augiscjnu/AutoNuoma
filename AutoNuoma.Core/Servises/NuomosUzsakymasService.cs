using AutoNuoma.Core.Models;
using AutoNuoma.Core.Repo;
using System.Collections.Generic;

namespace AutoNuoma.Core.Services
{
    public class NuomosUzsakymasService
    {
        private readonly NuomosUzsakymasRepository _nuomosUzsakymasRepository;

        // Konstruktorius, kuriame injekuojame repository
        public NuomosUzsakymasService(NuomosUzsakymasRepository nuomosUzsakymasRepository)
        {
            _nuomosUzsakymasRepository = nuomosUzsakymasRepository;
        }

        // Gauti visus nuomos užsakymus
        public List<NuomosUzsakymas> GetAllNuomosUzsakymai()
        {
            return _nuomosUzsakymasRepository.GetAllNuomosUzsakymai();
        }

        // Gauti nuomos užsakymą pagal ID
        public NuomosUzsakymas GetNuomosUzsakymasById(int id)
        {
            return _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
        }

        // Pridėti naują nuomos užsakymą
        public void AddNuomosUzsakymas(NuomosUzsakymas nuomosUzsakymas)
        {
            _nuomosUzsakymasRepository.AddNuomosUzsakymas(nuomosUzsakymas);
        }

        // Atnaujinti nuomos užsakymą
        public void UpdateNuomosUzsakymas(NuomosUzsakymas nuomosUzsakymas)
        {
            _nuomosUzsakymasRepository.UpdateNuomosUzsakymas(nuomosUzsakymas);
        }

        // Ištrinti nuomos užsakymą pagal ID
        public void DeleteNuomosUzsakymas(int id)
        {
            _nuomosUzsakymasRepository.DeleteNuomosUzsakymas(id);
        }

        // Gauti nuomos užsakymus pagal automobilio ID
        public List<NuomosUzsakymas> GetNuomosUzsakymaiByAutomobilisId(int automobilisId)
        {
            return _nuomosUzsakymasRepository.GetNuomosUzsakymaiByAutomobilisId(automobilisId);
        }

        // Gauti nuomos užsakymus pagal kliento ID
        public List<NuomosUzsakymas> GetNuomosUzsakymaiByKlientasId(int klientasId)
        {
            return _nuomosUzsakymasRepository.GetNuomosUzsakymaiByKlientasId(klientasId);
        }

        // Gauti nuomos užsakymus pagal darbuotojo ID
        public List<NuomosUzsakymas> GetNuomosUzsakymaiByDarbuotojasId(int darbuotojasId)
        {
            return _nuomosUzsakymasRepository.GetNuomosUzsakymaiByDarbuotojasId(darbuotojasId);
        }
    }
}

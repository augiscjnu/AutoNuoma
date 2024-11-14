using AutoNuoma.Core.Models;
using AutoNuoma.Core.Repo;
using System.Collections.Generic;

namespace AutoNuoma.Core.Services
{
    public class KlientasService
    {
        private readonly KlientasRepository _klientasRepository;

        public KlientasService(KlientasRepository klientasRepository)
        {
            _klientasRepository = klientasRepository;
        }

        // Gauti visus klientus
        public List<Klientas> GetAllKlientai()
        {
            return _klientasRepository.GetAllKlientai();
        }

        // Gauti klientą pagal ID
        public Klientas GetKlientasById(int id)
        {
            return _klientasRepository.GetKlientasById(id);
        }

        // Pridėti naują klientą
        public void AddKlientas(Klientas klientas)
        {
            _klientasRepository.AddKlientas(klientas);
        }

      

        // Ištrinti klientą pagal ID
        public void RemoveKlientasById(int id)
        {
            _klientasRepository.RemoveKlientasById(id);
        }
    }
}

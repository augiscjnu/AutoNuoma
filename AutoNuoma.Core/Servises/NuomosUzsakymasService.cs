using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Services
{
    public class NuomosUzsakymasService
    {
        private readonly INuomosUzsakymasRepository _nuomosUzsakymasRepository;

        // Konstruktorinis injektavimas
        public NuomosUzsakymasService(INuomosUzsakymasRepository nuomosUzsakymasRepository)
        {
            _nuomosUzsakymasRepository = nuomosUzsakymasRepository;
        }

        // Gauti visus nuomos užsakymus
        public async Task<List<NuomosUzkasymas>> GetAllAsync()
        {
            return await _nuomosUzsakymasRepository.GetAllAsync();
        }

        // Gauti nuomos užsakymą pagal vardą
        public async Task<NuomosUzkasymas> GetByVardasAsync(string vardas)
        {
            return await _nuomosUzsakymasRepository.GetByVardasAsync(vardas);
        }

        // Gauti nuomos užsakymą pagal automobilio pavadinimą
        public async Task<NuomosUzkasymas> GetByPavadinimasAsync(string pavadinimas)
        {
            return await _nuomosUzsakymasRepository.GetByPavadinimasAsync(pavadinimas);
        }

        // Sukurti naują nuomos užsakymą
        public async Task CreateAsync(NuomosUzkasymas nuomosUzkasymas)
        {
            await _nuomosUzsakymasRepository.CreateAsync(nuomosUzkasymas);
        }

        // Atnaujinti esamą nuomos užsakymą pagal pavadinimą
        public async Task UpdateAsync(string pavadinimas, NuomosUzkasymas nuomosUzkasymas)
        {
            await _nuomosUzsakymasRepository.UpdateAsync(pavadinimas, nuomosUzkasymas);
        }

        // Ištrinti nuomos užsakymą pagal pavadinimą
        public async Task DeleteAsync(string pavadinimas)
        {
            await _nuomosUzsakymasRepository.DeleteAsync(pavadinimas);
        }
    }
}

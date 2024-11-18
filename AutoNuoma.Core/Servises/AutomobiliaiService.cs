using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Services
{
    public class AutomobiliaiService
    {
        private readonly IAutomobilisRepository _automobilisRepository;

        public AutomobiliaiService(IAutomobilisRepository automobilisRepository)
        {
            _automobilisRepository = automobilisRepository;
        }

        public async Task<List<Automobilis>> GetAllAutomobiliaiAsync()
        {
            return await _automobilisRepository.GetAllAutomobiliaiAsync();
        }

        public async Task<Automobilis> GetAutomobilisByIdAsync(int id)
        {
            return await _automobilisRepository.GetAutomobilisByIdAsync(id);
        }

        public async Task AddAutomobilisAsync(Automobilis automobilis)
        {
            await _automobilisRepository.AddAutomobilisAsync(automobilis);
        }

        public async Task UpdateAutomobilisAsync(Automobilis automobilis)
        {
            await _automobilisRepository.UpdateAutomobilisAsync(automobilis);
        }

        public async Task DeleteAutomobilisAsync(int id)
        {
            await _automobilisRepository.DeleteAutomobilisAsync(id);
        }
    }
}

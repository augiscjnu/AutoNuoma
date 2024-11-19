using AutoNuoma.Core.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Repositories
{
    public interface IAutomobilisRepository
    {
        Task<Automobilis> GetByIdAsync(ObjectId id);
        Task<List<Automobilis>> GetAllAsync();
        Task CreateAsync(Automobilis automobilis);
        Task UpdateAsync(ObjectId id, Automobilis automobilis);
        Task DeleteAsync(ObjectId id);
    }
}

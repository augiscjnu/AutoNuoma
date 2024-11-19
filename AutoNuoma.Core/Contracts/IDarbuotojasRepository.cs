using AutoNuoma.Core.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Contracts
{
    public interface IDarbuotojasRepository
    {
        Task<List<Darbuotojas>> GetAllAsync();
        Task<Darbuotojas> GetByIdAsync(ObjectId id);
        Task CreateAsync(Darbuotojas darbuotojas);
        Task UpdateAsync(ObjectId id, Darbuotojas darbuotojas);
        Task DeleteAsync(ObjectId id);
    }
}

using AutoNuoma.Core.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Contracts
{
    public interface IKlientasRepository
    {
        Task<List<Klientas>> GetAllAsync();
        Task<Klientas> GetByIdAsync(ObjectId id);
        Task CreateAsync(Klientas klientas);
        Task UpdateAsync(ObjectId id, Klientas klientas);
        Task DeleteAsync(ObjectId id);
    }
}

using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Repositories
{
    public class DarbuotojasRepository : IDarbuotojasRepository
    {
        private readonly IMongoCollection<Darbuotojas> _darbuotojai;

        // Constructor where MongoDB client is injected
        public DarbuotojasRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("AutoNuomaDb");  // Name of the database
            _darbuotojai = database.GetCollection<Darbuotojas>("Darbuotojai");  // Collection name
        }

        // Get all darbuotojai (employees)
        public async Task<List<Darbuotojas>> GetAllAsync()
        {
            return await _darbuotojai.Find(d => true).ToListAsync();
        }

        // Get a single darbuotojas by ID
        public async Task<Darbuotojas> GetByIdAsync(ObjectId id)
        {
            return await _darbuotojai.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        // Create a new darbuotojas
        public async Task CreateAsync(Darbuotojas darbuotojas)
        {
            await _darbuotojai.InsertOneAsync(darbuotojas);
        }

        // Update an existing darbuotojas by ID
        public async Task UpdateAsync(ObjectId id, Darbuotojas darbuotojas)
        {
            var filter = Builders<Darbuotojas>.Filter.Eq(d => d.Id, id);
            await _darbuotojai.ReplaceOneAsync(filter, darbuotojas);
        }

        // Delete a darbuotojas by ID
        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<Darbuotojas>.Filter.Eq(d => d.Id, id);
            await _darbuotojai.DeleteOneAsync(filter);
        }
    }
}

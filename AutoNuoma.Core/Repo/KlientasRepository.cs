using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Repositories
{
    public class KlientasRepository : IKlientasRepository
    {
        private readonly IMongoCollection<Klientas> _klientai;

        // Constructor where MongoDB client is injected
        public KlientasRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("AutoNuomaDb");  // Name of the database
            _klientai = database.GetCollection<Klientas>("Klientai");  // Collection name
        }

        // Get all klientai (clients)
        public async Task<List<Klientas>> GetAllAsync()
        {
            return await _klientai.Find(k => true).ToListAsync();
        }

        // Get a single klientas by ID
        public async Task<Klientas> GetByIdAsync(ObjectId id)
        {
            return await _klientai.Find(k => k.Id == id).FirstOrDefaultAsync();
        }

        // Create a new klientas
        public async Task CreateAsync(Klientas klientas)
        {
            await _klientai.InsertOneAsync(klientas);
        }

        // Update an existing klientas by ID
        public async Task UpdateAsync(ObjectId id, Klientas klientas)
        {
            var filter = Builders<Klientas>.Filter.Eq(k => k.Id, id);
            await _klientai.ReplaceOneAsync(filter, klientas);
        }

        // Delete a klientas by ID
        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<Klientas>.Filter.Eq(k => k.Id, id);
            await _klientai.DeleteOneAsync(filter);
        }
    }
}

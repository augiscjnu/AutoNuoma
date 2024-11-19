using AutoNuoma.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Repositories
{
    public class AutomobilisRepository : IAutomobilisRepository
    {
        private readonly IMongoCollection<Automobilis> _automobiliai;

        public AutomobilisRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("AutoNuomaDb");
            _automobiliai = database.GetCollection<Automobilis>("Automobiliai");
        }

        public async Task<Automobilis> GetByIdAsync(ObjectId id)
        {
            return await _automobiliai.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Automobilis>> GetAllAsync()
        {
            return await _automobiliai.Find(a => true).ToListAsync();
        }

        public async Task CreateAsync(Automobilis automobilis)
        {
            await _automobiliai.InsertOneAsync(automobilis);
        }

        public async Task UpdateAsync(ObjectId id, Automobilis automobilis)
        {
            var filter = Builders<Automobilis>.Filter.Eq(a => a.Id, id);
            await _automobiliai.ReplaceOneAsync(filter, automobilis);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var filter = Builders<Automobilis>.Filter.Eq(a => a.Id, id);
            await _automobiliai.DeleteOneAsync(filter);
        }
    }
}

using MongoDB.Driver;
using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AutoNuoma.Core.Repositories
{
    public class NuomosUzsakymasRepository : INuomosUzsakymasRepository
    {
        private readonly IMongoCollection<NuomosUzkasymas> _nuomosUzkasymasCollection;

        public NuomosUzsakymasRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("AutoNuomaDb");
            _nuomosUzkasymasCollection = database.GetCollection<NuomosUzkasymas>("NuomosUzkasymas");
        }

        public async Task<List<NuomosUzkasymas>> GetAllAsync()
        {
            return await _nuomosUzkasymasCollection.Find(_ => true).ToListAsync();
        }

        public async Task<NuomosUzkasymas> GetByVardasAsync(string vardas)
        {
            return await _nuomosUzkasymasCollection.Find(n => n.Vardas == vardas).FirstOrDefaultAsync();
        }

        public async Task<NuomosUzkasymas> GetByPavadinimasAsync(string pavadinimas)
        {
            return await _nuomosUzkasymasCollection.Find(n => n.Pavadinimas == pavadinimas).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(NuomosUzkasymas nuomosUzkasymas)
        {
            await _nuomosUzkasymasCollection.InsertOneAsync(nuomosUzkasymas);
        }

        public async Task UpdateAsync(string pavadinimas, NuomosUzkasymas nuomosUzkasymas)
        {
            var filter = Builders<NuomosUzkasymas>.Filter.Eq(n => n.Pavadinimas, pavadinimas);
            await _nuomosUzkasymasCollection.ReplaceOneAsync(filter, nuomosUzkasymas);
        }

        public async Task DeleteAsync(string pavadinimas)
        {
            var filter = Builders<NuomosUzkasymas>.Filter.Eq(n => n.Pavadinimas, pavadinimas);
            await _nuomosUzkasymasCollection.DeleteOneAsync(filter);
        }
    }
}

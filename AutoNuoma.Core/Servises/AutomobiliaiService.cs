using AutoNuoma.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class AutomobilisService
{
    private readonly IMongoCollection<Automobilis> _automobiliaiCollection;

    // Konstruktoras, priimantis IMongoDatabase kaip parametrą
    public AutomobilisService(IMongoDatabase database)
    {
        _automobiliaiCollection = database.GetCollection<Automobilis>("Automobiliai");
    }

    public async Task<List<Automobilis>> GetAllAutomobiliaiAsync()
    {
        return await _automobiliaiCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Automobilis> GetAutomobilisByIdAsync(ObjectId id)
    {
        return await _automobiliaiCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAutomobilisAsync(Automobilis automobilis)
    {
        await _automobiliaiCollection.InsertOneAsync(automobilis);
    }

    public async Task UpdateAutomobilisAsync(ObjectId id, Automobilis automobilis)
    {
        await _automobiliaiCollection.ReplaceOneAsync(a => a.Id == id, automobilis);
    }

    public async Task DeleteAutomobilisAsync(ObjectId id)
    {
        await _automobiliaiCollection.DeleteOneAsync(a => a.Id == id);
    }
}

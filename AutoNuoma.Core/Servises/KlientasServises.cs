using AutoNuoma.Core.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.API.Services
{
    public class KlientasService
    {
        private readonly IMongoCollection<Klientas> _klientaiCollection;
        private readonly ILogger<KlientasService> _logger;

        public KlientasService(IMongoClient mongoClient, ILogger<KlientasService> logger)
        {
            var database = mongoClient.GetDatabase("AutoNuomaDb");
            _klientaiCollection = database.GetCollection<Klientas>("Klientai");
            _logger = logger;
        }

        // Get all Klientas
        public async Task<List<Klientas>> GetAllKlientaiAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all klientai from the database.");
                return await _klientaiCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching all klientai: {ex.Message}");
                throw new Exception($"Error occurred while fetching klientai: {ex.Message}");
            }
        }

        // Get Klientas by name (Vardas)
        public async Task<Klientas> GetKlientasByNameAsync(string vardas)
        {
            try
            {
                _logger.LogInformation($"Searching for klientas with name: {vardas}");
                return await _klientaiCollection
                    .Find(k => k.Vardas.Equals(vardas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while searching for klientas by name '{vardas}': {ex.Message}");
                throw new Exception($"Error occurred while searching for klientas by name '{vardas}': {ex.Message}");
            }
        }

        // Insert new Klientas
        public async Task InsertKlientasAsync(Klientas klientas)
        {
            try
            {
                _logger.LogInformation($"Inserting new klientas with name: {klientas.Vardas} {klientas.Pavarde}");
                await _klientaiCollection.InsertOneAsync(klientas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while inserting klientas: {ex.Message}");
                throw new Exception($"Error occurred while inserting klientas: {ex.Message}");
            }
        }

        // Update Klientas by name (Vardas)
        public async Task UpdateKlientasAsync(string vardas, Klientas klientas)
        {
            try
            {
                _logger.LogInformation($"Updating klientas with name: {vardas}");

                var existingKlientas = await GetKlientasByNameAsync(vardas);
                if (existingKlientas == null)
                {
                    throw new Exception($"Klientas with name '{vardas}' not found.");
                }

                klientas.Vardas = vardas;  // Ensure the first name stays the same
                await _klientaiCollection.ReplaceOneAsync(k => k.Vardas.Equals(vardas, StringComparison.OrdinalIgnoreCase), klientas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating klientas: {ex.Message}");
                throw new Exception($"Error occurred while updating klientas: {ex.Message}");
            }
        }

        // Delete Klientas by name (Vardas)
        public async Task DeleteKlientasAsync(string vardas)
        {
            try
            {
                _logger.LogInformation($"Deleting klientas with name: {vardas}");

                var klientas = await GetKlientasByNameAsync(vardas);
                if (klientas == null)
                {
                    throw new Exception($"Klientas with name '{vardas}' not found.");
                }

                await _klientaiCollection.DeleteOneAsync(k => k.Vardas.Equals(vardas, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting klientas: {ex.Message}");
                throw new Exception($"Error occurred while deleting klientas: {ex.Message}");
            }
        }
    }
}

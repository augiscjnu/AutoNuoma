using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlientasController : ControllerBase
    {
        private readonly IMongoCollection<Klientas> _klientaiCollection;
        private readonly ILogger<KlientasController> _logger;

        // Constructor with dependency injection of MongoClient and Logger
        public KlientasController(IMongoClient mongoClient, ILogger<KlientasController> logger)
        {
            var database = mongoClient.GetDatabase("AutoNuomaDb");
            _klientaiCollection = database.GetCollection<Klientas>("Klientai");
            _logger = logger;
        }

        // GET: api/Klientas
        [HttpGet]
        public async Task<ActionResult<List<Klientas>>> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all klientai from the database.");
                var klientai = await _klientaiCollection.Find(_ => true).ToListAsync();
                return Ok(klientai);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching klientai: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Klientas/name/{Vardas}
        [HttpGet("name/{Vardas}")]
        public async Task<ActionResult<Klientas>> GetByName(string Vardas)
        {
            try
            {
                _logger.LogInformation($"Searching for klientas with name: {Vardas}");
                var klientas = await _klientaiCollection
                    .Find(k => k.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (klientas == null)
                {
                    _logger.LogWarning($"Klientas with name '{Vardas}' not found.");
                    return NotFound($"Klientas with name '{Vardas}' not found.");
                }

                _logger.LogInformation($"Klientas with name '{Vardas}' found.");
                return Ok(klientas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while searching for klientas by name '{Vardas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Klientas
        [HttpPost]
        public async Task<ActionResult<Klientas>> Post([FromBody] Klientas klientas)
        {
            if (string.IsNullOrWhiteSpace(klientas.Vardas) || string.IsNullOrWhiteSpace(klientas.Pavarde))
            {
                return BadRequest("Klientas must have both a first name (Vardas) and last name (Pavarde).");
            }

            try
            {
                _logger.LogInformation($"Inserting a new klientas with name: {klientas.Vardas} {klientas.Pavarde}");
                await _klientaiCollection.InsertOneAsync(klientas);
                return CreatedAtAction(nameof(GetByName), new { Vardas = klientas.Vardas }, klientas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while inserting klientas: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Klientas/name/{Vardas}
        [HttpPut("name/{Vardas}")]
        public async Task<IActionResult> Put(string Vardas, [FromBody] Klientas klientas)
        {
            if (klientas == null || string.IsNullOrWhiteSpace(klientas.Vardas) || string.IsNullOrWhiteSpace(klientas.Pavarde))
            {
                return BadRequest("Klientas must have both a first name (Vardas) and last name (Pavarde).");
            }

            try
            {
                _logger.LogInformation($"Updating klientas with name: {Vardas}");

                var existingKlientas = await _klientaiCollection
                    .Find(k => k.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (existingKlientas == null)
                {
                    _logger.LogWarning($"Klientas with name '{Vardas}' not found.");
                    return NotFound($"Klientas with name '{Vardas}' not found.");
                }

                klientas.Vardas = Vardas;  // Ensure the first name stays the same
                await _klientaiCollection.ReplaceOneAsync(k => k.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase), klientas);

                _logger.LogInformation($"Successfully updated klientas with name: {Vardas}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating klientas: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Klientas/name/{Vardas}
        [HttpDelete("name/{Vardas}")]
        public async Task<IActionResult> Delete(string Vardas)
        {
            try
            {
                _logger.LogInformation($"Deleting klientas with name: {Vardas}");

                var klientas = await _klientaiCollection
                    .Find(k => k.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (klientas == null)
                {
                    _logger.LogWarning($"Klientas with name '{Vardas}' not found.");
                    return NotFound($"Klientas with name '{Vardas}' not found.");
                }

                await _klientaiCollection.DeleteOneAsync(k => k.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase));

                _logger.LogInformation($"Successfully deleted klientas with name: {Vardas}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting klientas: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add this namespace
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoNuoma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DarbuotojasController : ControllerBase
    {
        private readonly IMongoCollection<Darbuotojas> _darbuotojaiCollection;
        private readonly ILogger<DarbuotojasController> _logger;  // Declare logger

        // Inject ILogger and MongoDB client into the constructor
        public DarbuotojasController(IMongoClient mongoClient, ILogger<DarbuotojasController> logger)
        {
            // MongoDB collection setup
            var database = mongoClient.GetDatabase("AutoNuomaDb");
            _darbuotojaiCollection = database.GetCollection<Darbuotojas>("Darbuotojai");

            // Assign the logger
            _logger = logger;
        }

        // GET: api/Darbuotojas
        [HttpGet]
        public async Task<ActionResult<List<Darbuotojas>>> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all darbuotojai from the database.");

                var darbuotojai = await _darbuotojaiCollection.Find(_ => true).ToListAsync();

                _logger.LogInformation($"Successfully fetched {darbuotojai.Count} darbuotojai.");
                return Ok(darbuotojai);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching darbuotojai: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Darbuotojas/name/{Vardas}
        [HttpGet("name/{Vardas}")]
        public async Task<ActionResult<Darbuotojas>> GetByName(string Vardas)
        {
            try
            {
                _logger.LogInformation($"Searching for darbuotojas with name: {Vardas}");

                var darbuotojas = await _darbuotojaiCollection
                    .Find(d => d.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase)) // Case-insensitive search
                    .FirstOrDefaultAsync();

                if (darbuotojas == null)
                {
                    _logger.LogWarning($"Darbuotojas with name '{Vardas}' not found.");
                    return NotFound($"Darbuotojas with name '{Vardas}' not found.");
                }

                _logger.LogInformation($"Darbuotojas with name '{Vardas}' found.");
                return Ok(darbuotojas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while searching for darbuotojas by name '{Vardas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Darbuotojas
        [HttpPost]
        public async Task<ActionResult<Darbuotojas>> Post([FromBody] Darbuotojas darbuotojas)
        {
            if (string.IsNullOrWhiteSpace(darbuotojas.Vardas) || string.IsNullOrWhiteSpace(darbuotojas.Pavarde))
            {
                return BadRequest("Darbuotojas must have both a first name (Vardas) and last name (Pavarde).");
            }

            try
            {
                _logger.LogInformation("Inserting a new darbuotojas into the database.");

                await _darbuotojaiCollection.InsertOneAsync(darbuotojas);

                _logger.LogInformation($"Successfully inserted darbuotojas with name: {darbuotojas.Vardas} {darbuotojas.Pavarde}");

                return CreatedAtAction(nameof(GetByName), new { Vardas = darbuotojas.Vardas }, darbuotojas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while inserting darbuotojas: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Darbuotojas/name/{Vardas}
        [HttpPut("name/{Vardas}")]
        public async Task<IActionResult> Put(string Vardas, [FromBody] Darbuotojas darbuotojas)
        {
            if (darbuotojas == null || string.IsNullOrWhiteSpace(darbuotojas.Vardas) || string.IsNullOrWhiteSpace(darbuotojas.Pavarde))
            {
                return BadRequest("Darbuotojas must have both a first name (Vardas) and last name (Pavarde).");
            }

            try
            {
                _logger.LogInformation($"Updating darbuotojas with name: {Vardas}");

                var existingDarbuotojas = await _darbuotojaiCollection
                    .Find(d => d.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (existingDarbuotojas == null)
                {
                    _logger.LogWarning($"Darbuotojas with name '{Vardas}' not found.");
                    return NotFound($"Darbuotojas with name '{Vardas}' not found.");
                }

                darbuotojas.Vardas = Vardas;  // Ensure the first name stays the same

                await _darbuotojaiCollection.ReplaceOneAsync(d => d.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase), darbuotojas);

                _logger.LogInformation($"Successfully updated darbuotojas with name: {Vardas}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating darbuotojas with name '{Vardas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Darbuotojas/name/{Vardas}
        [HttpDelete("name/{Vardas}")]
        public async Task<IActionResult> Delete(string Vardas)
        {
            try
            {
                _logger.LogInformation($"Deleting darbuotojas with name: {Vardas}");

                var darbuotojas = await _darbuotojaiCollection
                    .Find(d => d.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (darbuotojas == null)
                {
                    _logger.LogWarning($"Darbuotojas with name '{Vardas}' not found.");
                    return NotFound($"Darbuotojas with name '{Vardas}' not found.");
                }

                await _darbuotojaiCollection.DeleteOneAsync(d => d.Vardas.Equals(Vardas, StringComparison.OrdinalIgnoreCase));

                _logger.LogInformation($"Successfully deleted darbuotojas with name: {Vardas}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting darbuotojas with name '{Vardas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

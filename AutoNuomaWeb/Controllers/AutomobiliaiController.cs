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
    public class AutomobiliaiController : ControllerBase
    {
        private readonly IMongoCollection<Automobilis> _automobiliaiCollection;
        private readonly ILogger<AutomobiliaiController> _logger;  // Declare logger

        // Inject ILogger into the constructor
        public AutomobiliaiController(IMongoClient mongoClient, ILogger<AutomobiliaiController> logger)
        {
            // MongoDB collection setup
            var database = mongoClient.GetDatabase("AutoNuomaDb");
            _automobiliaiCollection = database.GetCollection<Automobilis>("Automobiliai");

            // Assign the logger
            _logger = logger;
        }

        // GET: api/Automobiliai
        [HttpGet]
        public async Task<ActionResult<List<Automobilis>>> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all automobiliai from the database.");

                var automobiliai = await _automobiliaiCollection.Find(_ => true).ToListAsync();

                _logger.LogInformation($"Successfully fetched {automobiliai.Count} automobiliai.");
                return Ok(automobiliai);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching automobiliai: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Automobiliai/name/{Pavadinimas}
        // GET: api/Automobiliai/name/{Pavadinimas}
        [HttpGet("name/{Pavadinimas}")]
        public async Task<ActionResult<Automobilis>> GetByName(string Pavadinimas)
        {
            try
            {
                _logger.LogInformation($"Searching for automobilis with name: {Pavadinimas}");

                var automobilis = await _automobiliaiCollection
                    .Find(a => a.Pavadinimas.Equals(Pavadinimas, StringComparison.OrdinalIgnoreCase)) // Case-insensitive search
                    .FirstOrDefaultAsync();

                if (automobilis == null)
                {
                    _logger.LogWarning($"Automobilis with name '{Pavadinimas}' not found.");
                    return NotFound($"Automobilis su pavadinimu '{Pavadinimas}' nerastas.");
                }

                _logger.LogInformation($"Automobilis with name '{Pavadinimas}' found.");
                return Ok(automobilis);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while searching for automobilis by name '{Pavadinimas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // POST: api/Automobiliai
        [HttpPost]
        public async Task<ActionResult<Automobilis>> Post([FromBody] Automobilis automobilis)
        {
            if (string.IsNullOrWhiteSpace(automobilis.Pavadinimas))
            {
                return BadRequest("Automobilis privalo turėti pavadinimą.");
            }

            try
            {
                _logger.LogInformation("Inserting a new automobilis into the database.");

                await _automobiliaiCollection.InsertOneAsync(automobilis);

                _logger.LogInformation($"Successfully inserted automobilis with name: {automobilis.Pavadinimas}");

                return CreatedAtAction(nameof(GetByName), new { Pavadinimas = automobilis.Pavadinimas }, automobilis);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while inserting automobilis: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Automobiliai/name/{Pavadinimas}
        [HttpPut("name/{Pavadinimas}")]
        public async Task<IActionResult> Put(string Pavadinimas, [FromBody] Automobilis automobilis)
        {
            if (automobilis == null || string.IsNullOrWhiteSpace(automobilis.Pavadinimas))
            {
                return BadRequest("Automobilis turi turėti pavadinimą.");
            }

            try
            {
                _logger.LogInformation($"Updating automobilis with name: {Pavadinimas}");

                var existingAutomobilis = await _automobiliaiCollection
                    .Find(a => a.Pavadinimas.Equals(Pavadinimas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (existingAutomobilis == null)
                {
                    _logger.LogWarning($"Automobilis with name '{Pavadinimas}' not found.");
                    return NotFound("Automobilis nerastas.");
                }

                automobilis.Pavadinimas = Pavadinimas;  // Ensure the name stays the same

                await _automobiliaiCollection.ReplaceOneAsync(a => a.Pavadinimas.Equals(Pavadinimas, StringComparison.OrdinalIgnoreCase), automobilis);

                _logger.LogInformation($"Successfully updated automobilis with name: {Pavadinimas}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating automobilis with name '{Pavadinimas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Automobiliai/name/{Pavadinimas}
        [HttpDelete("name/{Pavadinimas}")]
        public async Task<IActionResult> Delete(string Pavadinimas)
        {
            try
            {
                _logger.LogInformation($"Deleting automobilis with name: {Pavadinimas}");

                var automobilis = await _automobiliaiCollection
                    .Find(a => a.Pavadinimas.Equals(Pavadinimas, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (automobilis == null)
                {
                    _logger.LogWarning($"Automobilis with name '{Pavadinimas}' not found.");
                    return NotFound("Automobilis nerastas.");
                }

                await _automobiliaiCollection.DeleteOneAsync(a => a.Pavadinimas.Equals(Pavadinimas, StringComparison.OrdinalIgnoreCase));

                _logger.LogInformation($"Successfully deleted automobilis with name: {Pavadinimas}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting automobilis with name '{Pavadinimas}': {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

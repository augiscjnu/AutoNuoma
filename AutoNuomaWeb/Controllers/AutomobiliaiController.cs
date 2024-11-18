using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

namespace AutoNuomaWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomobiliaiController : ControllerBase
    {
        private readonly IAutomobilisRepository _automobilisRepository;

        // Inject Serilog into the controller
        public AutomobiliaiController(IAutomobilisRepository automobilisRepository)
        {
            _automobilisRepository = automobilisRepository;
        }

        // GET api/automobiliai
        [HttpGet]
        public IActionResult GetAllAutomobiliai()
        {
            Log.Information("Fetching all automobiliai.");
            try
            {
                var automobiliai = _automobilisRepository.GetAllAutomobiliai();
                Log.Information("{Count} automobiliai fetched successfully.", automobiliai.Count);
                return Ok(automobiliai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching automobiliai.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/automobiliai/{id}
        [HttpGet("{id}")]
        public IActionResult GetAutomobilis(int id)
        {
            Log.Information("Fetching automobilis with ID {Id}.", id);
            try
            {
                var automobilis = _automobilisRepository.GetAutomobilisById(id);
                if (automobilis == null)
                {
                    Log.Warning("Automobilis with ID {Id} not found.", id);
                    return NotFound();
                }
                Log.Information("Automobilis with ID {Id} fetched successfully.", id);
                return Ok(automobilis);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching automobilis with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/automobiliai
        [HttpPost]
        public IActionResult AddAutomobilis([FromBody] Automobilis automobilis)
        {
            Log.Information("Attempting to add a new automobilis with ID {Id}.", automobilis.Id);
            try
            {
                _automobilisRepository.AddAutomobilis(automobilis);
                Log.Information("Automobilis with ID {Id} added successfully.", automobilis.Id);
                return CreatedAtAction(nameof(GetAutomobilis), new { id = automobilis.Id }, automobilis);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding automobilis with ID {Id}.", automobilis.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/automobiliai/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAutomobilis(int id, [FromBody] Automobilis automobilis)
        {
            automobilis.Id = id;
            Log.Information("Attempting to update automobilis with ID {Id}.", id);
            try
            {
                _automobilisRepository.UpdateAutomobilis(automobilis);
                Log.Information("Automobilis with ID {Id} updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating automobilis with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/automobiliai/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAutomobilis(int id)
        {
            Log.Information("Attempting to delete automobilis with ID {Id}.", id);
            try
            {
                _automobilisRepository.DeleteAutomobilis(id);
                Log.Information("Automobilis with ID {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting automobilis with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/automobiliai/laisvi
        [HttpGet("laisvi")]
        public IActionResult GetLaisviAutomobiliai(DateTime pradziosData, DateTime pabaigosData)
        {
            Log.Information("Fetching available automobiliai for the period {PradziosData} - {PabaigosData}.", pradziosData, pabaigosData);
            try
            {
                var automobiliai = _automobilisRepository.GetLaisviAutomobiliai(pradziosData, pabaigosData);
                Log.Information("{Count} available automobiliai fetched for the period {PradziosData} - {PabaigosData}.", automobiliai.Count, pradziosData, pabaigosData);
                return Ok(automobiliai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching available automobiliai for the period {PradziosData} - {PabaigosData}.", pradziosData, pabaigosData);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

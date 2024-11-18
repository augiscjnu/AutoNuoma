using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

namespace AutoNuoma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlientasController : ControllerBase
    {
        private readonly IKlientasRepository _klientasRepository;

        // Konstruktorinė priklausomybė nuo IKlientasRepository
        public KlientasController(IKlientasRepository klientasRepository)
        {
            _klientasRepository = klientasRepository;
        }

        // Gauti visus klientus
        [HttpGet]
        public IActionResult GetKlientai()
        {
            Log.Information("Fetching all klientai.");
            try
            {
                var klientai = _klientasRepository.GetAllKlientai();
                if (klientai == null || klientai.Count == 0)
                {
                    Log.Warning("No klientai found.");
                    return NotFound("Klientai nerasti.");
                }
                Log.Information("{Count} klientai fetched successfully.", klientai.Count);
                return Ok(klientai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching klientai.");
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Gauti klientą pagal ID
        [HttpGet("{id}")]
        public IActionResult GetKlientas(int id)
        {
            Log.Information("Fetching klientas with ID {Id}.", id);
            try
            {
                var klientas = _klientasRepository.GetKlientasById(id);
                if (klientas == null)
                {
                    Log.Warning("Klientas with ID {Id} not found.", id);
                    return NotFound($"Klientas su ID {id} nerastas.");
                }
                Log.Information("Klientas with ID {Id} fetched successfully.", id);
                return Ok(klientas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching klientas with ID {Id}.", id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Pridėti naują klientą
        [HttpPost]
        public IActionResult AddKlientas([FromBody] Klientas klientas)
        {
            Log.Information("Attempting to add a new klientas with ID {Id}.", klientas?.Id);
            try
            {
                if (klientas == null)
                {
                    Log.Warning("Invalid klientas data.");
                    return BadRequest("Kliento duomenys yra neteisingi.");
                }

                _klientasRepository.AddKlientas(klientas);
                Log.Information("Klientas with ID {Id} added successfully.", klientas.Id);
                return CreatedAtAction(nameof(GetKlientas), new { id = klientas.Id }, klientas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding klientas with ID {Id}.", klientas?.Id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Ištrinti klientą pagal ID
        [HttpDelete("{id}")]
        public IActionResult DeleteKlientas(int id)
        {
            Log.Information("Attempting to delete klientas with ID {Id}.", id);
            try
            {
                var klientas = _klientasRepository.GetKlientasById(id);
                if (klientas == null)
                {
                    Log.Warning("Klientas with ID {Id} not found.", id);
                    return NotFound($"Klientas su ID {id} nerastas.");
                }

                _klientasRepository.RemoveKlientasById(id);
                Log.Information("Klientas with ID {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting klientas with ID {Id}.", id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }
    }
}

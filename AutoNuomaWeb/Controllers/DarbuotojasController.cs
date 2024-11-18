using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;

namespace AutoNuoma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DarbuotojasController : ControllerBase
    {
        private readonly IDarbuotojasRepository _darbuotojasRepository;

        public DarbuotojasController(IDarbuotojasRepository darbuotojasRepository)
        {
            _darbuotojasRepository = darbuotojasRepository;
        }

        // Gauti visus darbuotojus
        [HttpGet]
        public IActionResult GetDarbuotojai()
        {
            Log.Information("Fetching all darbuotojai.");
            try
            {
                var darbuotojai = _darbuotojasRepository.GetAllDarbuotojai();
                if (darbuotojai == null || darbuotojai.Count == 0)
                {
                    Log.Warning("No darbuotojai found.");
                    return NotFound("Darbuotojai nerasti.");
                }
                Log.Information("{Count} darbuotojai fetched successfully.", darbuotojai.Count);
                return Ok(darbuotojai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching darbuotojai.");
                return StatusCode(500, "Internal server error");
            }
        }

        // Gauti darbuotoją pagal ID
        [HttpGet("{id}")]
        public IActionResult GetDarbuotojas(int id)
        {
            Log.Information("Fetching darbuotojas with ID {Id}.", id);
            try
            {
                var darbuotojas = _darbuotojasRepository.GetDarbuotojasById(id);
                if (darbuotojas == null)
                {
                    Log.Warning("Darbuotojas with ID {Id} not found.", id);
                    return NotFound($"Darbuotojas su ID {id} nerastas.");
                }
                Log.Information("Darbuotojas with ID {Id} fetched successfully.", id);
                return Ok(darbuotojas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching darbuotojas with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // Pridėti naują darbuotoją
        [HttpPost]
        public IActionResult AddDarbuotojas([FromBody] Darbuotojas darbuotojas)
        {
            Log.Information("Attempting to add a new darbuotojas with ID {Id}.", darbuotojas?.Id);
            try
            {
                if (darbuotojas == null)
                {
                    Log.Warning("Invalid darbuotojas data.");
                    return BadRequest("Darbuotojo duomenys neteisingi.");
                }

                _darbuotojasRepository.AddDarbuotojas(darbuotojas);
                Log.Information("Darbuotojas with ID {Id} added successfully.", darbuotojas.Id);
                return CreatedAtAction(nameof(GetDarbuotojas), new { id = darbuotojas.Id }, darbuotojas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding darbuotojas with ID {Id}.", darbuotojas?.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        // Atnaujinti darbuotoją
        [HttpPut("{id}")]
        public IActionResult UpdateDarbuotojas(int id, [FromBody] Darbuotojas darbuotojas)
        {
            Log.Information("Attempting to update darbuotojas with ID {Id}.", id);
            try
            {
                if (darbuotojas == null || darbuotojas.Id != id)
                {
                    Log.Warning("Invalid darbuotojas data or mismatched ID. Provided ID: {ProvidedId}, expected ID: {Id}.", darbuotojas?.Id, id);
                    return BadRequest("Netinkami darbuotojo duomenys.");
                }

                _darbuotojasRepository.UpdateDarbuotojas(darbuotojas);
                Log.Information("Darbuotojas with ID {Id} updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating darbuotojas with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // Ištrinti darbuotoją pagal ID
        [HttpDelete("{id}")]
        public IActionResult DeleteDarbuotojas(int id)
        {
            Log.Information("Attempting to delete darbuotojas with ID {Id}.", id);
            try
            {
                var darbuotojas = _darbuotojasRepository.GetDarbuotojasById(id);
                if (darbuotojas == null)
                {
                    Log.Warning("Darbuotojas with ID {Id} not found.", id);
                    return NotFound($"Darbuotojas su ID {id} nerastas.");
                }

                _darbuotojasRepository.RemoveDarbuotojasById(id);
                Log.Information("Darbuotojas with ID {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting darbuotojas with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

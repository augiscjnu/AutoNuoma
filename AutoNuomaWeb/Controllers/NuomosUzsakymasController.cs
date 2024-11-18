using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

namespace AutoNuoma.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NuomosUzsakymasController : ControllerBase
    {
        private readonly INuomosUzsakymasRepository _nuomosUzsakymasRepository;
        private readonly IRecieptRepository _recieptRepository;

        public NuomosUzsakymasController(INuomosUzsakymasRepository nuomosUzsakymasRepository, IRecieptRepository recieptRepository)
        {
            _nuomosUzsakymasRepository = nuomosUzsakymasRepository;
            _recieptRepository = recieptRepository;
        }

        // Gauti visus nuomos užsakymus
        [HttpGet]
        public ActionResult<List<NuomosUzsakymas>> GetAllNuomosUzsakymai()
        {
            Log.Information("Fetching all nuomos užsakymai.");
            try
            {
                var nuomosUzsakymai = _nuomosUzsakymasRepository.GetAllNuomosUzsakymai();
                if (nuomosUzsakymai == null || nuomosUzsakymai.Count == 0)
                {
                    Log.Warning("No nuomos užsakymai found.");
                    return NotFound("Nuomos užsakymai nerasti.");
                }
                Log.Information("{Count} nuomos užsakymai fetched successfully.", nuomosUzsakymai.Count);
                return Ok(nuomosUzsakymai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching nuomos užsakymai.");
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Gauti nuomos užsakymą pagal ID
        [HttpGet("{id}")]
        public ActionResult<NuomosUzsakymas> GetNuomosUzsakymasById(int id)
        {
            Log.Information("Fetching nuomos užsakymas with ID {Id}.", id);
            try
            {
                var nuomosUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
                if (nuomosUzsakymas == null)
                {
                    Log.Warning("Nuomos užsakymas with ID {Id} not found.", id);
                    return NotFound($"Nuomos užsakymas su ID {id} nerastas.");
                }
                Log.Information("Nuomos užsakymas with ID {Id} fetched successfully.", id);
                return Ok(nuomosUzsakymas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching nuomos užsakymas with ID {Id}.", id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Pridėti naują nuomos užsakymą
        // Pridėti naują nuomos užsakymą
        [HttpPost]
        public ActionResult AddNuomosUzsakymas([FromBody] NuomosUzsakymas nuomosUzsakymas)
        {
            Log.Information("Attempting to add a new nuomos užsakymas with ID {Id}.", nuomosUzsakymas?.Id);
            try
            {
                if (nuomosUzsakymas == null)
                {
                    Log.Warning("Invalid nuomos užsakymas data.");
                    return BadRequest("Neišsamūs duomenys apie nuomos užsakymą.");
                }

                // Pridėti nuomos užsakymą į duomenų bazę
                _nuomosUzsakymasRepository.AddNuomosUzsakymas(nuomosUzsakymas);
                Log.Information("Nuomos užsakymas with ID {Id} added successfully.", nuomosUzsakymas.Id);

                // Sugeneruoti kvitą po užsakymo pridėjimo
                var automobilisId = nuomosUzsakymas.AutomobilisId;
                var pradziosData = nuomosUzsakymas.PradziosData;
                var pabaigosData = nuomosUzsakymas.PabaigosData;
                var bendraKaina = nuomosUzsakymas.Kaina;

                // Kvito generavimas
                _recieptRepository.GenerateReceipt(nuomosUzsakymas.Id, automobilisId, pradziosData, pabaigosData, bendraKaina);

                Log.Information("Receipt for rental order with ID {Id} generated successfully.", nuomosUzsakymas.Id);

                // Grąžinti sėkmingą atsakymą su nauju užsakymu
                return CreatedAtAction(nameof(GetNuomosUzsakymasById), new { id = nuomosUzsakymas.Id }, nuomosUzsakymas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding nuomos užsakymas with ID {Id}.", nuomosUzsakymas?.Id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Atnaujinti nuomos užsakymą
        [HttpPut("{id}")]
        public ActionResult UpdateNuomosUzsakymas(int id, [FromBody] NuomosUzsakymas nuomosUzsakymas)
        {
            Log.Information("Attempting to update nuomos užsakymas with ID {Id}.", id);
            try
            {
                if (nuomosUzsakymas == null || nuomosUzsakymas.Id != id)
                {
                    Log.Warning("Invalid nuomos užsakymas data or mismatched ID. Provided ID: {ProvidedId}, expected ID: {Id}.", nuomosUzsakymas?.Id, id);
                    return BadRequest("Neteisingas užsakymo ID.");
                }

                var existingUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
                if (existingUzsakymas == null)
                {
                    Log.Warning("Nuomos užsakymas with ID {Id} not found for update.", id);
                    return NotFound($"Nuomos užsakymas su ID {id} nerastas.");
                }

                _nuomosUzsakymasRepository.UpdateNuomosUzsakymas(nuomosUzsakymas);
                Log.Information("Nuomos užsakymas with ID {Id} updated successfully.", id);
                return NoContent(); // 204 OK be turinio
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating nuomos užsakymas with ID {Id}.", id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Ištrinti nuomos užsakymą pagal ID
        [HttpDelete("{id}")]
        public ActionResult DeleteNuomosUzsakymas(int id)
        {
            Log.Information("Attempting to delete nuomos užsakymas with ID {Id}.", id);
            try
            {
                var nuomosUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
                if (nuomosUzsakymas == null)
                {
                    Log.Warning("Nuomos užsakymas with ID {Id} not found for deletion.", id);
                    return NotFound($"Nuomos užsakymas su ID {id} nerastas.");
                }

                _nuomosUzsakymasRepository.DeleteNuomosUzsakymas(id);
                Log.Information("Nuomos užsakymas with ID {Id} deleted successfully.", id);
                return NoContent(); // 204 OK be turinio
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting nuomos užsakymas with ID {Id}.", id);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Generuoti kvitą pagal nuomos užsakymą
        [HttpPost("generate-receipt/{uzsakymoId}")]
        public ActionResult GenerateReceiptForRentalOrder(int uzsakymoId)
        {
            Log.Information("Attempting to generate receipt for rental order with ID {UzsakymoId}.", uzsakymoId);
            try
            {
                // Gauti nuomos užsakymą pagal ID
                var nuomosUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(uzsakymoId);
                if (nuomosUzsakymas == null)
                {
                    Log.Warning("Nuomos užsakymas with ID {UzsakymoId} not found.", uzsakymoId);
                    return NotFound($"Nuomos užsakymas su ID {uzsakymoId} nerastas.");
                }

                // Paimti automobilio ID ir datą iš užsakymo
                var automobilisId = nuomosUzsakymas.AutomobilisId;
                var pradziosData = nuomosUzsakymas.PradziosData;
                var pabaigosData = nuomosUzsakymas.PabaigosData;
                var bendraKaina = nuomosUzsakymas.Kaina;

                // Generuoti kvitą
                _recieptRepository.GenerateReceipt(uzsakymoId, automobilisId, pradziosData, pabaigosData, bendraKaina);

                Log.Information("Receipt for rental order with ID {UzsakymoId} generated successfully.", uzsakymoId);
                return Ok("Kvitas buvo sėkmingai sugeneruotas.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating receipt for rental order with ID {UzsakymoId}.", uzsakymoId);
                return StatusCode(500, $"Klaida generuojant kvitą: {ex.Message}");
            }
        }

        // Gauti nuomos užsakymus pagal automobilio ID
        [HttpGet("automobilis/{automobilisId}")]
        public ActionResult<List<NuomosUzsakymas>> GetNuomosUzsakymaiByAutomobilisId(int automobilisId)
        {
            Log.Information("Fetching nuomos užsakymai by automobilis ID {AutomobilisId}.", automobilisId);
            try
            {
                var nuomosUzsakymai = _nuomosUzsakymasRepository.GetNuomosUzsakymaiByAutomobilisId(automobilisId);
                if (nuomosUzsakymai == null || nuomosUzsakymai.Count == 0)
                {
                    Log.Warning("No nuomos užsakymai found for automobilis ID {AutomobilisId}.", automobilisId);
                    return NotFound($"Nuomos užsakymai su automobilio ID {automobilisId} nerasti.");
                }
                Log.Information("{Count} nuomos užsakymai fetched successfully for automobilis ID {AutomobilisId}.", nuomosUzsakymai.Count, automobilisId);
                return Ok(nuomosUzsakymai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching nuomos užsakymai for automobilis ID {AutomobilisId}.", automobilisId);
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }
    }
}

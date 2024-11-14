using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var klientai = _klientasRepository.GetAllKlientai();
                if (klientai == null || klientai.Count == 0)
                {
                    return NotFound("Klientai nerasti.");
                }
                return Ok(klientai);
            }
            catch (Exception ex)
            {
                // Galima užfiksuoti klaidą, jei reikia
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Gauti klientą pagal ID
        [HttpGet("{id}")]
        public IActionResult GetKlientas(int id)
        {
            try
            {
                var klientas = _klientasRepository.GetKlientasById(id);
                if (klientas == null)
                {
                    return NotFound($"Klientas su ID {id} nerastas.");
                }
                return Ok(klientas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Pridėti naują klientą
        [HttpPost]
        public IActionResult AddKlientas([FromBody] Klientas klientas)
        {
            try
            {
                if (klientas == null)
                {
                    return BadRequest("Kliento duomenys yra neteisingi.");
                }

                _klientasRepository.AddKlientas(klientas);
                return CreatedAtAction(nameof(GetKlientas), new { id = klientas.Id }, klientas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }

        // Ištrinti klientą pagal ID
        [HttpDelete("{id}")]
        public IActionResult DeleteKlientas(int id)
        {
            try
            {
                var klientas = _klientasRepository.GetKlientasById(id);
                if (klientas == null)
                {
                    return NotFound($"Klientas su ID {id} nerastas.");
                }

                _klientasRepository.RemoveKlientasById(id);
                return NoContent(); // Tai žymės, kad ištrynimas pavyko
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Klaida: {ex.Message}");
            }
        }
    }
}

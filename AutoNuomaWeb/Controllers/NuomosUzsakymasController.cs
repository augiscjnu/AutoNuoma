using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoNuoma.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NuomosUzsakymasController : ControllerBase
    {
        private readonly INuomosUzsakymasRepository _nuomosUzsakymasRepository;

        public NuomosUzsakymasController(INuomosUzsakymasRepository nuomosUzsakymasRepository)
        {
            _nuomosUzsakymasRepository = nuomosUzsakymasRepository;
        }

        // Gauti visus nuomos užsakymus
        [HttpGet]
        public ActionResult<List<NuomosUzsakymas>> GetAllNuomosUzsakymai()
        {
            var nuomosUzsakymai = _nuomosUzsakymasRepository.GetAllNuomosUzsakymai();
            if (nuomosUzsakymai == null || nuomosUzsakymai.Count == 0)
            {
                return NotFound("Nuomos užsakymai nerasti.");
            }
            return Ok(nuomosUzsakymai);
        }

        // Gauti nuomos užsakymą pagal ID
        [HttpGet("{id}")]
        public ActionResult<NuomosUzsakymas> GetNuomosUzsakymasById(int id)
        {
            var nuomosUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
            if (nuomosUzsakymas == null)
            {
                return NotFound($"Nuomos užsakymas su ID {id} nerastas.");
            }
            return Ok(nuomosUzsakymas);
        }

        // Pridėti naują nuomos užsakymą
        [HttpPost]
        public ActionResult AddNuomosUzsakymas([FromBody] NuomosUzsakymas nuomosUzsakymas)
        {
            if (nuomosUzsakymas == null)
            {
                return BadRequest("Neišsamūs duomenys apie nuomos užsakymą.");
            }

            _nuomosUzsakymasRepository.AddNuomosUzsakymas(nuomosUzsakymas);
            return CreatedAtAction(nameof(GetNuomosUzsakymasById), new { id = nuomosUzsakymas.Id }, nuomosUzsakymas);
        }

        // Atnaujinti nuomos užsakymą
        [HttpPut("{id}")]
        public ActionResult UpdateNuomosUzsakymas(int id, [FromBody] NuomosUzsakymas nuomosUzsakymas)
        {
            if (nuomosUzsakymas == null || nuomosUzsakymas.Id != id)
            {
                return BadRequest("Neteisingas užsakymo ID.");
            }

            var existingUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
            if (existingUzsakymas == null)
            {
                return NotFound($"Nuomos užsakymas su ID {id} nerastas.");
            }

            _nuomosUzsakymasRepository.UpdateNuomosUzsakymas(nuomosUzsakymas);
            return NoContent(); // 204 OK be turinio
        }

        // Ištrinti nuomos užsakymą pagal ID
        [HttpDelete("{id}")]
        public ActionResult DeleteNuomosUzsakymas(int id)
        {
            var nuomosUzsakymas = _nuomosUzsakymasRepository.GetNuomosUzsakymasById(id);
            if (nuomosUzsakymas == null)
            {
                return NotFound($"Nuomos užsakymas su ID {id} nerastas.");
            }

            _nuomosUzsakymasRepository.DeleteNuomosUzsakymas(id);
            return NoContent(); // 204 OK be turinio
        }

        // Gauti nuomos užsakymus pagal automobilio ID
        [HttpGet("automobilis/{automobilisId}")]
        public ActionResult<List<NuomosUzsakymas>> GetNuomosUzsakymaiByAutomobilisId(int automobilisId)
        {
            var nuomosUzsakymai = _nuomosUzsakymasRepository.GetNuomosUzsakymaiByAutomobilisId(automobilisId);
            if (nuomosUzsakymai == null || nuomosUzsakymai.Count == 0)
            {
                return NotFound($"Nuomos užsakymai pagal automobilio ID {automobilisId} nerasti.");
            }
            return Ok(nuomosUzsakymai);
        }

        // Gauti nuomos užsakymus pagal kliento ID
        [HttpGet("klientas/{klientasId}")]
        public ActionResult<List<NuomosUzsakymas>> GetNuomosUzsakymaiByKlientasId(int klientasId)
        {
            var nuomosUzsakymai = _nuomosUzsakymasRepository.GetNuomosUzsakymaiByKlientasId(klientasId);
            if (nuomosUzsakymai == null || nuomosUzsakymai.Count == 0)
            {
                return NotFound($"Nuomos užsakymai pagal kliento ID {klientasId} nerasti.");
            }
            return Ok(nuomosUzsakymai);
        }

        // Gauti nuomos užsakymus pagal darbuotojo ID
        [HttpGet("darbuotojas/{darbuotojasId}")]
        public ActionResult<List<NuomosUzsakymas>> GetNuomosUzsakymaiByDarbuotojasId(int darbuotojasId)
        {
            var nuomosUzsakymai = _nuomosUzsakymasRepository.GetNuomosUzsakymaiByDarbuotojasId(darbuotojasId);
            if (nuomosUzsakymai == null || nuomosUzsakymai.Count == 0)
            {
                return NotFound($"Nuomos užsakymai pagal darbuotojo ID {darbuotojasId} nerasti.");
            }
            return Ok(nuomosUzsakymai);
        }
    }
}

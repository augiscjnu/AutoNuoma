using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
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
            var darbuotojai = _darbuotojasRepository.GetAllDarbuotojai();
            if (darbuotojai == null || darbuotojai.Count == 0)
            {
                return NotFound("Darbuotojai nerasti.");
            }
            return Ok(darbuotojai);
        }

        // Gauti darbuotoją pagal ID
        [HttpGet("{id}")]
        public IActionResult GetDarbuotojas(int id)
        {
            var darbuotojas = _darbuotojasRepository.GetDarbuotojasById(id);
            if (darbuotojas == null)
            {
                return NotFound($"Darbuotojas su ID {id} nerastas.");
            }
            return Ok(darbuotojas);
        }

        // Pridėti naują darbuotoją
        [HttpPost]
        public IActionResult AddDarbuotojas([FromBody] Darbuotojas darbuotojas)
        {
            if (darbuotojas == null)
            {
                return BadRequest("Darbuotojo duomenys neteisingi.");
            }

            _darbuotojasRepository.AddDarbuotojas(darbuotojas);
            return CreatedAtAction(nameof(GetDarbuotojas), new { id = darbuotojas.Id }, darbuotojas);
        }

        // Atnaujinti darbuotoją
        [HttpPut("{id}")]
        public IActionResult UpdateDarbuotojas(int id, [FromBody] Darbuotojas darbuotojas)
        {
            if (darbuotojas == null || darbuotojas.Id != id)
            {
                return BadRequest("Netinkami darbuotojo duomenys.");
            }

            _darbuotojasRepository.UpdateDarbuotojas(darbuotojas);
            return NoContent();
        }

        // Ištrinti darbuotoją pagal ID
        [HttpDelete("{id}")]
        public IActionResult DeleteDarbuotojas(int id)
        {
            var darbuotojas = _darbuotojasRepository.GetDarbuotojasById(id);
            if (darbuotojas == null)
            {
                return NotFound($"Darbuotojas su ID {id} nerastas.");
            }

            _darbuotojasRepository.RemoveDarbuotojasById(id);
            return NoContent();
        }
    }
}

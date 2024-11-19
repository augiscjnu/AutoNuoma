using AutoNuoma.Core.Models;
using AutoNuoma.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoNuoma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NuomosUzkasymasController : ControllerBase
    {
        private readonly NuomosUzsakymasService _nuomosUzsakymasService;

        public NuomosUzkasymasController(NuomosUzsakymasService nuomosUzsakymasService)
        {
            _nuomosUzsakymasService = nuomosUzsakymasService;
        }

        // GET: api/NuomosUzkasymas
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var nuomosUzkasymas = await _nuomosUzsakymasService.GetAllAsync();
            return Ok(nuomosUzkasymas);
        }

        // GET: api/NuomosUzkasymas/{vardas}
        [HttpGet("{vardas}")]
        public async Task<IActionResult> GetByVardasAsync(string vardas)
        {
            var nuomosUzkasymas = await _nuomosUzsakymasService.GetByVardasAsync(vardas);
            if (nuomosUzkasymas == null)
            {
                return NotFound();
            }
            return Ok(nuomosUzkasymas);
        }

        // POST: api/NuomosUzkasymas
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NuomosUzkasymas nuomosUzkasymas)
        {
            await _nuomosUzsakymasService.CreateAsync(nuomosUzkasymas);
            return CreatedAtAction(nameof(GetByVardasAsync), new { vardas = nuomosUzkasymas.Vardas }, nuomosUzkasymas);
        }

        // PUT: api/NuomosUzkasymas/{pavadinimas}
        [HttpPut("{pavadinimas}")]
        public async Task<IActionResult> UpdateAsync(string pavadinimas, [FromBody] NuomosUzkasymas nuomosUzkasymas)
        {
            await _nuomosUzsakymasService.UpdateAsync(pavadinimas, nuomosUzkasymas);
            return NoContent();
        }

        // DELETE: api/NuomosUzkasymas/{pavadinimas}
        [HttpDelete("{pavadinimas}")]
        public async Task<IActionResult> DeleteAsync(string pavadinimas)
        {
            await _nuomosUzsakymasService.DeleteAsync(pavadinimas);
            return NoContent();
        }
    }
}

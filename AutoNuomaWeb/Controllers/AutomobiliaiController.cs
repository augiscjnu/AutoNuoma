using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuomaWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomobiliaiController : ControllerBase
    {
        private readonly IAutomobilisRepository _automobilisRepository;

        public AutomobiliaiController(IAutomobilisRepository automobilisRepository)
        {
            _automobilisRepository = automobilisRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAutomobiliai()
        {
            var automobiliai = await _automobilisRepository.GetAllAutomobiliai();
            return Ok(automobiliai);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutomobilis(int id)
        {
            var automobilis = await _automobilisRepository.GetAutomobilisById(id);
            if (automobilis == null)
            {
                return NotFound();
            }
            return Ok(automobilis);
        }

        [HttpPost]
        public async Task<IActionResult> AddAutomobilis([FromBody] Automobilis automobilis)
        {
            await _automobilisRepository.AddAutomobilis(automobilis);
            return CreatedAtAction(nameof(GetAutomobilis), new { id = automobilis.Id }, automobilis);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAutomobilis(int id, [FromBody] Automobilis automobilis)
        {
            automobilis.Id = id;
            await _automobilisRepository.UpdateAutomobilis(automobilis);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutomobilis(int id)
        {
            await _automobilisRepository.DeleteAutomobilis(id);
            return NoContent();
        }
    }

}

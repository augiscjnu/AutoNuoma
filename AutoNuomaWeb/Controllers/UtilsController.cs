using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/*namespace AutoNuoma.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private readonly IAutomobilisRepository _automobiliaiRepo;
        private readonly IKlientasRepository _klientaiRepo;
        private readonly IDarbuotojasRepository _darbuotojaiRepo;
        private readonly DataBackupService _backupService;

        // Konstruktoras, kuriame inicijuojamos priklausomybės
        public UtilsController(
            IAutomobilisRepository automobiliaiRepo,
            IKlientasRepository klientaiRepo,
            IDarbuotojasRepository darbuotojaiRepo,
            INuomosUzsakymasRepository nuomosUzsakymaiRepo,
            DataBackupService backupService)
        {
            _automobiliaiRepo = automobiliaiRepo;
            _klientaiRepo = klientaiRepo;
            _darbuotojaiRepo = darbuotojaiRepo;
            _backupService = backupService;
        }

        // POST metodas, skirtas atsarginių kopijų kūrimui
        [HttpPost("CreateBackup")]
        public IActionResult CreateBackup()
        {
            Log.Information("Starting backup process...");

            try
            {
                // Gauti visus duomenis iš repo
                var automobiliai = _automobiliaiRepo.GetAllAutomobiliai(); // Neikite tiesiogiai į ToList()
                var klientai = _klientaiRepo.GetAllKlientai(); // Neikite tiesiogiai į ToList()
                var darbuotojai = _darbuotojaiRepo.GetAllDarbuotojai(); // Neikite tiesiogiai į ToList()

                // Logging the count of entities being backed up
                Log.Information("Fetched {AutomobiliaiCount} automobiliai, {KlientaiCount} klientai, and {DarbuotojaiCount} darbuotojai for backup.",
                    automobiliai.Count, klientai.Count, darbuotojai.Count);

                // Sukuriame atsargines kopijas sinchroniniu būdu
                Task.Run(() => _backupService.BackupAutomobiliai(automobiliai));
                Task.Run(() => _backupService.BackupKlientai(klientai));
                Task.Run(() => _backupService.BackupDarbuotojai(darbuotojai));

                Log.Information("Backup process initiated for automobiliai, klientai, and darbuotojai.");

                return Ok(new { message = "Backup initiated successfully!" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Backup failed due to an error.");
                return StatusCode(500, new { message = "Backup failed", error = ex.Message });
            }
        }
    }
}

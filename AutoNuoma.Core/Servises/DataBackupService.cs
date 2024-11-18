using AutoNuoma.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DataBackupService
{
    private readonly string _backupDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Backups");

    public DataBackupService()
    {
        // Sukuriame backup katalogą, jei jis neegzistuoja
        if (!Directory.Exists(_backupDirectory))
        {
            Directory.CreateDirectory(_backupDirectory);
        }
    }

    private string GetBackupFilePath(string dataType, string subfolder = "")
    {
        string folder = string.IsNullOrEmpty(subfolder) ? _backupDirectory : Path.Combine(_backupDirectory, subfolder);

        // Sukuriame subkatalogą, jei jo nėra
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Sukuriame failo pavadinimą su laiko žyma
        return Path.Combine(folder, $"{dataType}_{DateTime.Now:yyyyMMdd_HHmmss}.json");
    }

    // Backup funkcijos

    public void BackupAutomobiliai(List<Automobilis> automobiliai)
    {
        // Filtruojame visus automobilius, nes dabar "Automobilis" yra bendra bazinė klasė
        string filePath = GetBackupFilePath("Automobiliai", "Automobiliai");
        File.WriteAllText(filePath, JsonConvert.SerializeObject(automobiliai, Formatting.Indented));
    }

    public void BackupKlientai(List<Klientas> klientai)
    {
        string filePath = GetBackupFilePath("Klientai");
        File.WriteAllText(filePath, JsonConvert.SerializeObject(klientai, Formatting.Indented));
    }

    public void BackupDarbuotojai(List<Darbuotojas> darbuotojai)
    {
        string filePath = GetBackupFilePath("Darbuotojai");
        File.WriteAllText(filePath, JsonConvert.SerializeObject(darbuotojai, Formatting.Indented));
    }

    

    
}

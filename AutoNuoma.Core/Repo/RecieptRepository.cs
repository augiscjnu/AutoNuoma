using System;
using System.IO;
using AutoNuoma.Core.Models;
using AutoNuoma.Core.Contracts;

namespace AutoNuoma.Core.Repo
{
    public class RecieptRepository : IRecieptRepository
    {
        private readonly string _receiptDirectory;
        private readonly IAutomobilisRepository _automobilisRepository;

        // Constructor to receive the path for storing receipts and the AutomobilisRepository
        public RecieptRepository(string receiptDirectory, IAutomobilisRepository automobilisRepository)
        {
            _receiptDirectory = receiptDirectory;
            _automobilisRepository = automobilisRepository;
        }

        // Method to generate a receipt for a rental order
        public void GenerateReceipt(int uzsakymoId, int automobilisId, DateTime pradziosData, DateTime pabaigosData, decimal bendraKaina)
        {
            // Fetch car details
            var automobilis = _automobilisRepository.GetAutomobilisById(automobilisId);
            if (automobilis == null)
            {
                throw new ArgumentException($"Automobilis su ID {automobilisId} nerastas.");
            }

            // Prepare the receipt content
            var receiptContent = CreateReceiptContent(uzsakymoId, automobilis, pradziosData, pabaigosData, bendraKaina);

            // Define the file path
            var filePath = Path.Combine(_receiptDirectory, $"Receipt_{uzsakymoId}.txt");

            // Ensure directory exists
            Directory.CreateDirectory(_receiptDirectory);

            // Write the content to the file
            File.WriteAllText(filePath, receiptContent);
        }

        // Helper method to create the content of the receipt
        private string CreateReceiptContent(int uzsakymoId, Automobilis automobilis, DateTime pradziosData, DateTime pabaigosData, decimal bendraKaina)
        {
            var type = automobilis.Pavadinimas.Contains("elek") ? "Elektrinis" : "Naftos";

            return $@"
            Nuomos Užsakymas ID: {uzsakymoId}
            Automobilis: {automobilis.Pavadinimas}
            Metai: {automobilis.Metai}
            Tipas: {type}
            Paros Kaina: {automobilis.NuomosKaina} EUR
            Laikotarpis:
                Nuo: {pradziosData.ToString("yyyy-MM-dd")}
                Iki: {pabaigosData.ToString("yyyy-MM-dd")}
            Bendra Kaina: {bendraKaina} EUR
            ";
        }
    }
}

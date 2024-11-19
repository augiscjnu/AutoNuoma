using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using System;

namespace AutoNuoma.Core.Services
{
   /* public class RentalService
    {
        private readonly INuomosUzsakymasRepository _nuomosUzsakymasRepository;
        private readonly IAutomobilisRepository _automobilisRepository;
        private readonly IRecieptRepository _recieptRepository;

        public RentalService(INuomosUzsakymasRepository nuomosUzsakymasRepository, IAutomobilisRepository automobilisRepository, IRecieptRepository recieptRepository)
        {
            _nuomosUzsakymasRepository = nuomosUzsakymasRepository;
            _automobilisRepository = automobilisRepository;
            _recieptRepository = recieptRepository;
        }

        // Method to create a rental order and generate a receipt
        public void CreateRentalOrder(int automobilisId, DateTime pradziosData, DateTime pabaigosData)
        {
            // Fetch car data
            var automobilis = _automobilisRepository.GetAutomobilisById(automobilisId);
            if (automobilis == null)
            {
                throw new ArgumentException($"Automobilis su ID {automobilisId} nerastas.");
            }

            // Calculate the total rental cost
            var days = (pabaigosData - pradziosData).Days;
            var bendraKaina = automobilis.NuomosKaina * days;

            // Create a rental order (this would be saved to the database in a real-world scenario)
            var uzsakymas = new NuomosUzsakymas
            {
                AutomobilisId = automobilisId,
                PradziosData = pradziosData,
                PabaigosData = pabaigosData,
                Kaina = bendraKaina
            };

            _nuomosUzsakymasRepository.AddNuomosUzsakymas(uzsakymas);

            // Generate a receipt
            _recieptRepository.GenerateReceipt(uzsakymas.Id, automobilisId, pradziosData, pabaigosData, bendraKaina);
        }
    }*/
}

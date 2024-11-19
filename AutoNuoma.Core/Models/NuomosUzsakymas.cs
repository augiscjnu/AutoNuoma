using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AutoNuoma.Core.Models
{
    public class NuomosUzkasymas
    {
        [BsonId]
        public ObjectId Id { get; set; }  // MongoDB ObjectId as the unique identifier

        public string Vardas { get; set; }  // Kliento vardas (pakeistas iš KlientasId į Vardas)
        public string Pavadinimas { get; set; }  // Automobilio pavadinimas (pakeistas iš AutomobilisId į Pavadinimas)

        public DateTime UzkasymoData { get; set; }  // Date and time of the rental order
        public DateTime PradziosData { get; set; }  // Start date of the rental period
        public DateTime PabaigosData { get; set; }  // End date of the rental period

        public decimal Kaina { get; set; }  // Total price for the rental period

       
    }
}

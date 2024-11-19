using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Models
{
    public class Automobilis
    {
        [BsonId]
        public ObjectId Id { get; set; }
      
        public string Pavadinimas { get; set; }
        public int Metai { get; set; }
        public decimal NuomosKaina { get; set; }
    }
}
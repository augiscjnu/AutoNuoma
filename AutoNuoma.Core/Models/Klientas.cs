using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Models
{
    public class Klientas
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public string ElPastas { get; set; }
        public string Telefonas { get; set; }
    }
}

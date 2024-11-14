using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoNuoma.Core.Repo
{
    public class NuomosUzsakymasRepository : INuomosUzsakymasRepository
    {
        private readonly string _connectionString;

        public NuomosUzsakymasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Gauti visus nuomos užsakymus
        public List<NuomosUzsakymas> GetAllNuomosUzsakymai()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai").ToList();
        }

        // Gauti nuomos užsakymą pagal ID
        public NuomosUzsakymas GetNuomosUzsakymasById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai WHERE Id = @Id", new { Id = id }); 
        }

        // Pridėti naują nuomos užsakymą
        public void AddNuomosUzsakymas(NuomosUzsakymas nuomosUzsakymas)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "INSERT INTO NuomosUzsakymai (AutomobilisId, KlientasId, DarbuotojasId, PradziosData, PabaigosData, Kaina) VALUES (@AutomobilisId, @KlientasId, @DarbuotojasId, @PradziosData, @PabaigosData, @Kaina)";
            connection.Execute(query, nuomosUzsakymas);
        }
        

        // Atnaujinti nuomos užsakymą
        public void UpdateNuomosUzsakymas(NuomosUzsakymas nuomosUzsakymas)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE NuomosUzsakymai SET AutomobilisId = @AutomobilisId, KlientasId = @KlientasId, DarbuotojasId = @DarbuotojasId, PradziosData = @PradziosData, PabaigosData = @PabaigosData, Kaina = @Kaina WHERE Id = @Id";
            connection.Execute(query, nuomosUzsakymas); 
        }

        // Ištrinti nuomos užsakymą pagal ID
        public void DeleteNuomosUzsakymas(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM NuomosUzsakymai WHERE Id = @Id";
            connection.Execute(query, new { Id = id }); 
        }

        // Gauti nuomos užsakymus pagal automobilio ID
        public List<NuomosUzsakymas> GetNuomosUzsakymaiByAutomobilisId(int automobilisId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai WHERE AutomobilisId = @AutomobilisId", new { AutomobilisId = automobilisId }).ToList(); 
        }

        // Gauti nuomos užsakymus pagal kliento ID
        public List<NuomosUzsakymas> GetNuomosUzsakymaiByKlientasId(int klientasId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai WHERE KlientasId = @KlientasId", new { KlientasId = klientasId }).ToList(); 
        }

        // Gauti nuomos užsakymus pagal darbuotojo ID
        public List<NuomosUzsakymas> GetNuomosUzsakymaiByDarbuotojasId(int darbuotojasId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<NuomosUzsakymas>("SELECT * FROM NuomosUzsakymai WHERE DarbuotojasId = @DarbuotojasId", new { DarbuotojasId = darbuotojasId }).ToList(); 
        }
    }
}

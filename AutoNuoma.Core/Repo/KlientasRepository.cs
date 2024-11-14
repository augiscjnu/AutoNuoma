using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoNuoma.Core.Repo
{
    public class KlientasRepository : IKlientasRepository
    {
        private readonly string _connectionString;

        public KlientasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

       

        // Gauti visus klientus
        public List<Klientas> GetAllKlientai()
        {

            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Klientas>("SELECT * FROM Klientai1").ToList(); 
        }

        // Gauti klientą pagal ID
        public Klientas GetKlientasById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Klientas>("SELECT * FROM Klientai1 WHERE Id = @Id", new { Id = id }); 
        }

        // Pridėti naują klientą
        public void AddKlientas(Klientas klientas)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "INSERT INTO Klientai1 (Vardas, Pavarde, ElPastas, Telefonas) VALUES (@Vardas, @Pavarde, @ElPastas, @Telefonas)";
            connection.Execute(query, klientas); 
        }

        // Ištrinti klientą pagal ID
        public void RemoveKlientasById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Klientai1 WHERE Id = @Id";
            connection.Execute(query, new { Id = id }); 
        }
    }
}

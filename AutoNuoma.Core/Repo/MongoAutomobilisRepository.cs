using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoNuoma.Core.Repo
{
    public class AutomobilisRepository : IAutomobilisRepository
    {
        private readonly string _connectionString;

        // Constructor to receive the connection string
        public AutomobilisRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Synchronously get all cars
        public List<Automobilis> GetAllAutomobiliai()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Automobilis>("SELECT * FROM Automobiliai").ToList();
        }

        // Synchronously get a car by its ID
        public Automobilis GetAutomobilisById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Automobilis>("SELECT * FROM Automobiliai WHERE Id = @Id", new { Id = id });
        }

        // Synchronously add a new car
        public void AddAutomobilis(Automobilis automobilis)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "INSERT INTO Automobiliai (Marke, Modelis, Metai, Spalva, Kaina) VALUES (@Marke, @Modelis, @Metai, @Spalva, @Kaina)";
            connection.Execute(query, automobilis);
        }

        // Synchronously update an existing car
        public void UpdateAutomobilis(Automobilis automobilis)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE Automobiliai SET Marke = @Marke, Modelis = @Modelis, Metai = @Metai, Spalva = @Spalva, Kaina = @Kaina WHERE Id = @Id";
            connection.Execute(query, automobilis);
        }

        // Synchronously delete a car by ID
        public void DeleteAutomobilis(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Automobiliai WHERE Id = @Id";
            connection.Execute(query, new { Id = id });
        }

        // Synchronously get available cars based on rental dates
        public List<Automobilis> GetLaisviAutomobiliai(DateTime pradziosData, DateTime pabaigosData)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT * 
                FROM Automobiliai a
                WHERE NOT EXISTS (
                    SELECT 1 
                    FROM NuomosUzsakymai nu 
                    WHERE nu.AutomobilisId = a.Id 
                    AND ((nu.PradziosData BETWEEN @PradziosData AND @PabaigosData) 
                    OR (nu.PabaigosData BETWEEN @PradziosData AND @PabaigosData))
                )";
            return connection.Query<Automobilis>(query, new { PradziosData = pradziosData, PabaigosData = pabaigosData }).ToList();
        }
    }
}

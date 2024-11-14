using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Repo
{
    public class AutomobilisRepository : IAutomobilisRepository
    {
        private readonly string _connectionString;

        public AutomobilisRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Automobilis>> GetAllAutomobiliai()
        {
            using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryAsync<Automobilis>("SELECT * FROM Automobiliai")).ToList();
        }

        public async Task<Automobilis> GetAutomobilisById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Automobilis>("SELECT * FROM Automobiliai WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAutomobilis(Automobilis automobilis)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("INSERT INTO Automobiliai (Pavadinimas, Metai, NuomosKaina) VALUES (@Pavadinimas, @Metai, @NuomosKaina)", automobilis);
        }

        public async Task UpdateAutomobilis(Automobilis automobilis)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("UPDATE Automobiliai SET Pavadinimas = @Pavadinimas, Metai = @Metai, NuomosKaina = @NuomosKaina WHERE Id = @Id", automobilis);
        }

        public async Task DeleteAutomobilis(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("DELETE FROM Automobiliai WHERE Id = @Id", new { Id = id });
        }

        public async Task<List<Automobilis>> GetLaisviAutomobiliai(DateTime pradziosData, DateTime pabaigosData)
        {
            using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryAsync<Automobilis>("SELECT * FROM Automobiliai WHERE Id NOT IN (SELECT AutomobilisId FROM NuomosUzsakymai WHERE PradziosData < @PabaigosData AND PabaigosData > @PradziosData)",
                new { PradziosData = pradziosData, PabaigosData = pabaigosData })).ToList();
        }
    }

}

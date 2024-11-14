using AutoNuoma.Core.Contracts;
using AutoNuoma.Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AutoNuoma.Core.Repo
{
    public class DarbuotojasRepository : IDarbuotojasRepository
    {
        private readonly string _connectionString;

        public DarbuotojasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Gauti visus darbuotojus
        public List<Darbuotojas> GetAllDarbuotojai()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Darbuotojas>("SELECT * FROM Darbuotojai").ToList();
        }

        // Gauti darbuotoją pagal ID
        public Darbuotojas GetDarbuotojasById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<Darbuotojas>("SELECT * FROM Darbuotojai WHERE Id = @Id", new { Id = id });
        }

        // Pridėti naują darbuotoją
        public void AddDarbuotojas(Darbuotojas darbuotojas)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "INSERT INTO Darbuotojai (Vardas, Pavarde, Pareigos) VALUES (@Vardas, @Pavarde, @Pareigos)";
            connection.Execute(query, darbuotojas);
        }

        // Atnaujinti darbuotoją
        public void UpdateDarbuotojas(Darbuotojas darbuotojas)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE Darbuotojai SET Vardas = @Vardas, Pavarde = @Pavarde, Pareigos = @Pareigos WHERE Id = @Id";
            connection.Execute(query, darbuotojas);
        }

        // Ištrinti darbuotoją pagal ID
        public void RemoveDarbuotojasById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Darbuotojai WHERE Id = @Id";
            connection.Execute(query, new { Id = id });
        }
    }
}

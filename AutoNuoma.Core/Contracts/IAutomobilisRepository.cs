using AutoNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Contracts
{
    public interface IAutomobilisRepository
    {
        Task<List<Automobilis>> GetAllAutomobiliai();
        Task<Automobilis> GetAutomobilisById(int id);
        Task AddAutomobilis(Automobilis automobilis);
        Task UpdateAutomobilis(Automobilis automobilis);
        Task DeleteAutomobilis(int id);
        Task<List<Automobilis>> GetLaisviAutomobiliai(DateTime pradziosData, DateTime pabaigosData);
    }

}

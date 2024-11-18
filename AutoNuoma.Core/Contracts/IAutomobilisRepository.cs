using AutoNuoma.Core.Models;
using System;
using System.Collections.Generic;

namespace AutoNuoma.Core.Contracts
{
    public interface IAutomobilisRepository
    {
        List<Automobilis> GetAllAutomobiliai(); 
        Automobilis GetAutomobilisById(int id); 
        void AddAutomobilis(Automobilis automobilis);
        void UpdateAutomobilis(Automobilis automobilis); 
        void DeleteAutomobilis(int id); 
        List<Automobilis> GetLaisviAutomobiliai(DateTime pradziosData, DateTime pabaigosData); 
    }
}

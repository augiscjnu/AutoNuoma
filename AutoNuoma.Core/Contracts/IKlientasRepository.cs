using AutoNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoNuoma.Core.Contracts
{
    public interface IKlientasRepository
    {
        List<Klientas> GetAllKlientai();             // Sinchroninis metodas, grąžinantis visus klientus
        Klientas GetKlientasById(int id);            // Sinchroninis metodas, grąžinantis klientą pagal ID
        void AddKlientas(Klientas klientas);         // Sinchroninis metodas klientui pridėti
        void RemoveKlientasById(int id);             // Sinchroninis metodas klientui ištrinti pagal ID
    }
}


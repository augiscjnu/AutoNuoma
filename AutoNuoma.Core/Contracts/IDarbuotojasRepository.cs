using AutoNuoma.Core.Models;

namespace AutoNuoma.Core.Contracts
{
    public interface IDarbuotojasRepository
    {
        List<Darbuotojas> GetAllDarbuotojai();
        Darbuotojas GetDarbuotojasById(int id);
        void AddDarbuotojas(Darbuotojas darbuotojas);
        void UpdateDarbuotojas(Darbuotojas darbuotojas);
        void RemoveDarbuotojasById(int id);
    }
}

using System;

namespace AutoNuoma.Core.Contracts
{
    public interface IRecieptRepository
    {
        void GenerateReceipt(int uzsakymoId, int automobilisId, DateTime pradziosData, DateTime pabaigosData, decimal bendraKaina);
    }
}

using AutoNuoma.Core.Models;

public interface INuomosUzsakymasRepository
{
    Task<List<NuomosUzkasymas>> GetAllAsync();
    Task<NuomosUzkasymas> GetByVardasAsync(string vardas);  // Pakeista pagal naują lauką
    Task<NuomosUzkasymas> GetByPavadinimasAsync(string pavadinimas);  // Pakeista pagal naują lauką
    Task CreateAsync(NuomosUzkasymas nuomosUzkasymas);
    Task UpdateAsync(string pavadinimas, NuomosUzkasymas nuomosUzkasymas);  // Pakeista pagal Pavadinimą
    Task DeleteAsync(string pavadinimas);  // Pakeista pagal Pavadinimą
}

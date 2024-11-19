using AutoNuoma.Core.Models;

public interface INuomosUzkasymasService
{
    Task<List<NuomosUzkasymas>> GetAllAsync();
    Task<NuomosUzkasymas> GetByIdAsync(string id);
    Task CreateAsync(NuomosUzkasymas nuomosUzkasymas);
    Task UpdateAsync(string id, NuomosUzkasymas nuomosUzkasymas);
    Task DeleteAsync(string id);
}

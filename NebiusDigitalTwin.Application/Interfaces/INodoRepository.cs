using NebiusDigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebiusDigitalTwin.Application.Interfaces
{
     public interface INodoRepository
    {
        Task<IEnumerable<Nodo>> GetAllAsync();
        Task<Nodo?> GetByIdAsync(string id);
        Task CreateAsync(Nodo nodo);

        // Nueva firma para eliminar
        Task<bool> DeleteAsync(string id);
        Task UpdateAsync(Nodo nodo);
    }
}

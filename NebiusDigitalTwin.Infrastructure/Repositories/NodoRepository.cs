using NebiusDigitalTwin.Application.Interfaces;
using NebiusDigitalTwin.Domain.Entities;
using NebiusDigitalTwin.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace NebiusDigitalTwin.Infrastructure.Repositories
{
     public class NodoRepository: INodoRepository
    {

        private readonly MongoDbContext _context;

        public NodoRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nodo>> GetAllAsync()
        {
            return await _context.Nodos.Find(_ => true).ToListAsync();
        }

        public async Task<Nodo?> GetByIdAsync(string id)
        {
            return await _context.Nodos.Find(n => n.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Nodo nodo)
        {
            await _context.Nodos.InsertOneAsync(nodo);
        }

        // Nueva implementación para eliminar un nodo de MongoDB
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Nodos.DeleteOneAsync(n => n.Id == id);

            // Retorna true si se eliminó exactamente un documento
            return result.DeletedCount == 1;
        }

        public async Task UpdateAsync(Nodo nodo)
        {
            await _context.Nodos.ReplaceOneAsync(n => n.Id == nodo.Id, nodo);
        }


    }
}

using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private readonly ICatalogContext _context;

        public BoxRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Box>> GetBoxes()
        {
            return await _context
                            .Boxes
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<Box> GetBox(string id)
        {
            return await _context
                           .Boxes
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Box>> GetBoxByName(string name)
        {
            FilterDefinition<Box> filter = Builders<Box>.Filter.Eq(p => p.Name, name);

            return await _context
                            .Boxes
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Box>> GetBoxByCategory(string categoryName)
        {
            FilterDefinition<Box> filter = Builders<Box>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Boxes
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateBox(Box box)
        {
            await _context.Boxes.InsertOneAsync(box);
        }

        public async Task<bool> UpdateBox(Box box)
        {
            var updateResult = await _context
                                        .Boxes
                                        .ReplaceOneAsync(filter: g => g.Id == box.Id, replacement: box);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteBox(string id)
        {
            FilterDefinition<Box> filter = Builders<Box>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Boxes
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}

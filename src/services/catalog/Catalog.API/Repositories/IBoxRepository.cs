using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> GetBoxes();
        Task<Box> GetBox(string id);
        Task<IEnumerable<Box>> GetBoxByName(string name);
        Task<IEnumerable<Box>> GetBoxByCategory(string categoryName);

        Task CreateBox(Box box);
        Task<bool> UpdateBox(Box box);
        Task<bool> DeleteBox(string id);
    }
}

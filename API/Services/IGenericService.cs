using AssetManager.Models;

namespace AssetManager.Services
{
    public interface IGenericService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Create(T createObj);
        Task<bool> Update(T updateObj);
        Task<bool> Delete(int id);
    }
}

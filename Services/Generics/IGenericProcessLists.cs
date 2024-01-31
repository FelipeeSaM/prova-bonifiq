using ProvaPub.Models.Base;

namespace ProvaPub.Services.Generics {
    public interface IGenericProcessLists<T> where T : BaseModel {
        Task<List<T>> ListEntitiesAsync(int page);
    }
}
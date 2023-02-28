namespace PetShopProject.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetByID(int id);
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        void Save();

    }
}

using Microsoft.EntityFrameworkCore;
using PetShopProject.Models;

namespace PetShopProject.DAL
{
    public class CategoryRepository : IRepository<Category>
    {
        private AnimalContext context;

        public CategoryRepository(AnimalContext context)
        {
            this.context = context;
        }


        public Category? GetByID(int id)
        {
            Category? Category = GetAll().FirstOrDefault(x => x.Id == id);

            return Category;

        }

        public IEnumerable<Category> GetAll() => context.Categories.Include(x => x.Animals);

        public void Delete(int id)
        {
            Category? Category = GetByID(id);
            if (Category != null)
            {
                context.Categories.Remove(Category);
                Save();
            }
        }

        public void Insert(Category Category)
        {
            context.Categories.Add(Category);
            Save();
        }

        public void Update(Category Category)
        {
            var CategoryInDb = GetByID(Category.Id);
            if (CategoryInDb != null)
            {

                CategoryInDb.CategoryName = Category.CategoryName;
                Save();
            }

        }

        public void Save() => context.SaveChanges();

    }
}

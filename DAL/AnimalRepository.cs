using PetShopProject.Models;
using Microsoft.EntityFrameworkCore;

namespace PetShopProject.DAL
{
    public class AnimalRepository : IRepository<Animal>
    {
        private AnimalContext context;
        public AnimalRepository(AnimalContext context)
        {
            this.context = context;
        }


        public Animal? GetByID(int animalId)
        {
            Animal? animal = GetAll().SingleOrDefault(x => x.Id == animalId);

            return animal;
        }

        public IEnumerable<Animal> GetAll() => context.Animals.Include(x => x.Comments);

        public void Delete(int animalId)
        {
            Animal? animal = GetByID(animalId);
            if (animal != null)
            {
                context.Animals.Remove(animal);
                Save();
            }
        }

        public void Insert(Animal animal)
        {
            context.Animals.Add(animal);
            Save();
        }

        public void Update(Animal animal)
        {
            var animalInDb = GetByID(animal.Id);
            if (animalInDb != null)
            {

                animalInDb.Name = animal.Name;
                animalInDb.ImagePath = animal.ImagePath;
                animalInDb.Description = animal.Description;
                Save();
            }
        }

        public void Save() => context.SaveChanges();


    }
}

using PetShopProject.DAL;
using PetShopProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace PetShopProject.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Animal> _animalRepository { get; set; }
        private IRepository<Comment> _Commentcontext { get; set; }
        private IRepository<Category> _categoryRepository { get; set; }
        public HomeController(IRepository<Animal> context, IRepository<Comment> commentcontext, IRepository<Category> categoryRepository)
        {
            _animalRepository = context;
            _Commentcontext = commentcontext;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(_animalRepository.GetAll().OrderByDescending(x => x.Comments?.Count).Take(2));
        }
        public IActionResult Catalog()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(_animalRepository.GetAll());
        }

        public IActionResult ShowAnimalByCategory(int id)
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            var list = _animalRepository.GetAll().Where(x => x.CategoryId == id);
            return View("Catalog", list);
        }
        [HttpGet]
        public IActionResult ShowAnimal(int Id)

        {
            var animal = _animalRepository.GetByID(Id);
            if (animal == null) return NotFound();
            ViewBag.Category = _categoryRepository.GetByID(animal.CategoryId);
            return View(animal);
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            comment.UserName = User?.Identity?.Name;
            comment.UserIcon = User?.GetIconPath();
            _Commentcontext.Insert(comment);
            _Commentcontext.Save();
            var animal = _animalRepository.GetByID(comment.AnimalId);
            if (animal == null) return NotFound();
            ViewBag.Category = _categoryRepository.GetByID(animal.CategoryId);
            return View("ShowAnimal", animal);
        }
    }
}

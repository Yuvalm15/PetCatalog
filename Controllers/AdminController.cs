using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShopProject.DAL;
using PetShopProject.Models;
using PetShopProject.Services;

namespace PetShopProject.Controllers
{
    public class AdminController : Controller
    {
        private IRepository<Animal> _animalRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Comment> _commentRepository;
        private IPictureService _pictureService;

        public AdminController(IRepository<Animal> animalRepository, IRepository<Category> categoryRepository, IRepository<Comment> commentRepository, IPictureService pictureService)
        {
            _animalRepository = animalRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _pictureService = pictureService;
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult AdminView()
        {
            return View(_animalRepository.GetAll());
        }


        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            var animal = _animalRepository.GetByID(id);
            if (animal == null) return NotFound();

            ViewBag.Category = _categoryRepository.GetByID(animal.CategoryId);
            ViewBag.PicPath = animal.ImagePath;
            return View(animal);

        }

        [Authorize(Roles = "Administrators")]
        public IActionResult DetailsChanged(Animal animal)
        {
            animal.ImagePath = _pictureService.BrowsePicture(animal.Picture!).Result;
            _animalRepository.Update(animal);

            return RedirectToAction("EditAnimal", new { id = animal.Id });
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = _animalRepository.GetByID(id);
            if (animal == null) return BadRequest();
            _animalRepository.Delete(animal.Id);
            return RedirectToAction("AdminView");
        }

        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public IActionResult CreateAnimal()
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            return View();
        }
        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public IActionResult CreateAnimal(Animal animal)
        {
            animal.ImagePath = _pictureService.BrowsePicture(animal.Picture!).Result;
            _animalRepository.Insert(animal);
            return RedirectToAction("AdminView");
        }
        [Authorize(Roles = "Administrators")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _commentRepository.GetByID(id);
            if (comment == null) return BadRequest();
            _commentRepository.Delete(id);
            return RedirectToAction(nameof(EditAnimal), new { id = comment.AnimalId } );
        }
    }
    }
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShopProject.DAL;
using PetShopProject.Models;

namespace AmirFinalProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private IRepository<Comment> _commentRepository;
        private IRepository<Animal> _animalRepository;
        public UserController(UserManager<UserModel> userManager, IRepository<Comment> commentRepository, IRepository<Animal> animalRepository)
        {
            _userManager = userManager;
            _commentRepository = commentRepository;
            _animalRepository = animalRepository;
        }
        [Authorize]
        public IActionResult Profile(string Name)
        {
            ViewBag.Animals = _animalRepository.GetAll();
            ViewBag.Comments = _commentRepository.GetAll().Where(x => x.UserName == Name);
            UserModel? user = _userManager.FindByNameAsync(Name).Result;
            return View(user);
        }
    }
}

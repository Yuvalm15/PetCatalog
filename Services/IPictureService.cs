namespace PetShopProject.Services
{
    public interface IPictureService
    {
        public Task<string> BrowsePicture(IFormFile userfile);
    }
}

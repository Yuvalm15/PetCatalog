namespace PetShopProject.Services
{
    public class PictureService : IPictureService
    {
        public async Task<string> BrowsePicture(IFormFile userfile)
        {
            string filename = userfile.FileName;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Assets\\AnimalPictures", filename);
            var stream = new FileStream(uploadpath, FileMode.Create);
            await userfile.CopyToAsync(stream);
            stream.Close();

            return filename;
        }
    }
}

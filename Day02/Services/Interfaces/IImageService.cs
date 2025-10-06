namespace Day02.Services.Interfaces
{
    public interface IImageService
    {
       
        Task<string> AddImageAsync(IFormFile file);
        void DeleteImageAsync(string src);
    }
}

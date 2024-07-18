using Movies.Api.Interfaces;

namespace Movies.Api.Repositories;

public class StorageService : IFileStorageService
{
    private readonly IWebHostEnvironment environment;
    private readonly IHttpContextAccessor contextAccessor;

    public StorageService(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
    {
        this.environment = environment;
        this.contextAccessor = contextAccessor;
    }

    public Task DeleteFile(string fileRoute, string containerName)
    {
        if (String.IsNullOrEmpty(fileRoute))
        {
            throw new ArgumentNullException(nameof(fileRoute)); 
        }

        var fileName = Path.GetFileName(fileRoute);
        var folder = Path.Combine(environment.WebRootPath, containerName, fileName);

        if (!Directory.Exists(folder)) 
        {   
            return Task.CompletedTask;
        }

        File.Delete(folder);
        return Task.CompletedTask;
    }

    public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
    {
        await DeleteFile(fileRoute, containerName);
        return await SaveFile(containerName, file);
    }

    public async Task<string> SaveFile(string containerName, IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var folder = Path.Combine(environment.WebRootPath, containerName);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        if (!File.Exists(fileName)) 
        { 
            var route = Path.Combine(folder, fileName);
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            await File.WriteAllBytesAsync(route, memoryStream.ToArray());
            var currentUrl = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}";
            var path = Path.Combine(containerName, fileName).Replace("\\", "/");
            return Path.Combine(currentUrl, path).Replace("\\", "/");
            //return path;
        }
        else
        {
            throw new Exception("File already exists");
        }
    }
}

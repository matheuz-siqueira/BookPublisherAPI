using System.Reflection.Metadata;
using BookPublisher.Application.Dtos.File;
using BookPublisher.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BookPublisher.Application.Services;

public class FileService : IFileService
{
    private readonly string _basePath; 
    private readonly IHttpContextAccessor _httpContextAccessor;
    public FileService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor; 
        _basePath = Directory.GetCurrentDirectory() + "/UploadDir/";
    }

    public byte[] GetFile(string filename)
    {
        var filePath = _basePath + filename;
        return File.ReadAllBytes(filePath);
    }

    public async Task<FileDetailJson> SaveFile(IFormFile file)
    {
        var fileDetail = new FileDetailJson(); 
        var fileType = Path.GetExtension(file.FileName);
        var baseUrl = _httpContextAccessor.HttpContext.Request.Host;

        if(fileType.ToLower() == ".pdf" || fileType.ToLower() == ".docx" )
        {
            var docName = Path.GetFileName(file.FileName);
            if(file is not null && file.Length > 0)
            {
                var destination = Path.Combine(_basePath, "", docName);
                fileDetail.DocumentName = docName; 
                fileDetail.DocumentType = fileType; 
                fileDetail.DocumentUrl = Path.Combine(baseUrl + "/api/v1/file/" + fileDetail.DocumentName);

                using var stream = new FileStream(destination, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }

        return fileDetail; 
    }

    public async Task<List<FileDetailJson>> SaveFiles(List<IFormFile> files)
    {
        var list = new List<FileDetailJson>();
        foreach(var file in files)
        {
            list.Add(await SaveFile(file));
        }
        return list;
    }
}

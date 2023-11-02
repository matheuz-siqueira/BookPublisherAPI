using BookPublisher.Application.Dtos.File;
using Microsoft.AspNetCore.Http;

namespace BookPublisher.Application.Interfaces;

public interface IFileService
{
    byte[] GetFile(string filename);
    Task<FileDetailJson> SaveFile(IFormFile file);
    Task<List<FileDetailJson>> SaveFiles(List<IFormFile> files);
}

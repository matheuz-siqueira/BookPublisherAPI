using BookPublisher.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookPublisher.WebUI.Controllers;

[Route("api/v{version:apiVersion}/file")]
public class FileController : BookPublisherController
{
    private readonly IFileService _service;
    public FileController(IFileService service)
    {
        _service = service; 
    }

    [HttpPost("upload-file")]
    public async Task<ActionResult> UploadFile([FromForm] IFormFile file)
    {
        var response = await _service.SaveFile(file); 
        return Ok(response); 
    }

    [HttpPost("upload-files")]
    public async Task<ActionResult> UploadFiles([FromForm] List<IFormFile> files)
    {
        var response = await _service.SaveFiles(files); 
        return Ok(response); 
    }

    [HttpGet("download-file/{filename}")]
    public async Task<ActionResult> DownloadFile(string filename)
    {
        byte[] buffer = _service.GetFile(filename);
        if(buffer is not null)
        {
            HttpContext.Response.ContentType = 
                $"application/{Path.GetExtension(filename).Replace(".", "")}"; 
            
            HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
            await HttpContext.Response.Body.WriteAsync(buffer);
        }
        return new ContentResult();
    }
}

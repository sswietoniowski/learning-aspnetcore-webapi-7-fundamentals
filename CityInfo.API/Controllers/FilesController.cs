﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
    private readonly ILogger<FilesController> _logger;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider, ILogger<FilesController> logger)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                                            ?? throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("{fileid}")]
    public ActionResult GetFile(string fileId)
    {
        _logger.LogInformation($"Called: {nameof(GetFile)}");

        //FileContentResult result = new FileContentResult(new byte[0], "application/octet-stream");
        //FileStreamResult result = new FileStreamResult(new MemoryStream(new byte[0]), "application/octet-stream");

        var pathToFile = "test.txt";

        if (!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }

        if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var bytes = System.IO.File.ReadAllBytes(pathToFile);

        return File(bytes, contentType, Path.GetFileName(pathToFile));
    }
}
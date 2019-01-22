﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Courses.WebAPI.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile uploadedFile)
        {
            var files = new List<IFormFile> {uploadedFile};
            var filesLength = await _fileService.SaveFiles(files);
            return Ok(filesLength);
        }
    }
}
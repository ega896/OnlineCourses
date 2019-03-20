using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Infrastructure;
using Courses.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Courses.Application.Courses.Commands.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public CreateCourseCommandHandler(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Course
            {
                Name = request.Name,
                Description = request.Description,
                //UserId = request.UserId
            };

            if (request.Avatar != null)
            {
                entity.File = new AppFile
                {
                    Caption = request.Avatar.FileName,
                    Course = entity,
                    Name =  await _fileService.SaveFile(request.Avatar)
                };
            }

            _context.Courses.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class CreateCourseCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
 
        public IFormFile Avatar { get; set; }

        public ICollection<string> Languages { get; set; }
    }
}
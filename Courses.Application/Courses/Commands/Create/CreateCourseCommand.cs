using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Courses.Application.Interfaces;
using Courses.Domain.Entities;
using Courses.Infrastructure.Extensions;
using Courses.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Courses.Application.Courses.Commands.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCourseCommandHandler(ApplicationDbContext context, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.CurrentUser() ?? throw new Exception();

            var entity = new Course
            {
                Name = request.Name,
                Description = request.Description,
                UserId = user
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
using System;
using System.Threading;
using System.Threading.Tasks;
using Courses.Persistence;
using MediatR;

namespace Courses.Application.Courses.Commands.Delete
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToDelete = _context.Courses.Find(request.Id);

            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class DeleteCourseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
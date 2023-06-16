using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
        {
            private readonly ICategoryRepository _CategoryRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCategoryCommandHandler(ICategoryRepository CategoryRepository, IUnitOfWork unitOfWork)
            {
                _CategoryRepository = CategoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
            {
                var product = await _CategoryRepository.GetByIdAsync(command.Id);
                await _CategoryRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(product.Id);
            }
        }
    }
}
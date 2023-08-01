using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetById
{
    public class GetProductPromotionByIdQuery : IRequest<Result<List<GetProductPromotionByIdResponse>>>
    {
        public int Id { get; set; }

        public class GetProductPromotionByIdQueryHandler : IRequestHandler<GetProductPromotionByIdQuery, Result<List<GetProductPromotionByIdResponse>>>
        {
            private readonly IProductPromotionRepository _repository;
            private readonly IProductPromotionFreeRepository _repositoryProductFree;
            private readonly IMapper _mapper;

            public GetProductPromotionByIdQueryHandler(IProductPromotionRepository productCache, IProductPromotionFreeRepository repositoryProductFree, IMapper mapper)
            {
                _repository = productCache;
                _repositoryProductFree = repositoryProductFree;
                _mapper = mapper;
            }

            public async Task<Result<List<GetProductPromotionByIdResponse>>> Handle(GetProductPromotionByIdQuery query, CancellationToken cancellationToken)
            {
                var res = _repository.ProductPromotions.Where(x => x.ProductId == query.Id).Select(x => new GetProductPromotionByIdResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Percent = x.Percent
                }).ToList();

                foreach (var item in res)
                {
                    var productFrees = _repositoryProductFree.ProductPromotionFrees.Where(x => x.ProductPromotionId == item.Id).Select(x => new GetProductPromotionFreeResponse
                    {
                        Id = x.ProductId,
                        Name = x.Product.Name,
                        Quantity = x.Quantity,
                    }).ToList();

                    item.product = productFrees;
                }


                return Result<List<GetProductPromotionByIdResponse>>.Success(res);
            }
        }
    }
}
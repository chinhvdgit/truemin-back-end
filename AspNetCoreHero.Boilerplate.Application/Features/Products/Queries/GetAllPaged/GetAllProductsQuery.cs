﻿using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged
{
    public class GetAllProductsQuery : IRequest<PaginatedResult<GetAllProductsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool? IsHighlight { get; set; }
        public bool? IsSale { get; set; }

        public GetAllProductsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResult<GetAllProductsResponse>>
    {
        private readonly IProductRepository _repository;

        public GGetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllProductsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetAllProductsResponse>> expression = e => new GetAllProductsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Rate = e.Rate,
                Barcode = e.Barcode,
                SalePrice = e.SalePrice,
                Price = e.Price,
                Image = e.Image,
                CategoryId = e.ProductCategories.Select(x => x.CategoryId).ToList(),
                Sale = e.Sale == null ? null : new ProductSaleResponse { Type = e.Sale.Type, Percent = e.Sale.Percent, Amount = e.Sale.Amount },
                IsSale = e.Promotions == null || e.Promotions.Count == 0 ? false : true,
                IsHighlight = e.IsHighlight
            };
            var paginatedList = await _repository.Products
                .Where(x => (request.IsHighlight == null || x.IsHighlight == request.IsHighlight)
                            && (request.IsSale == null || (request.IsSale.Value && x.Promotions.Count > 0)))
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
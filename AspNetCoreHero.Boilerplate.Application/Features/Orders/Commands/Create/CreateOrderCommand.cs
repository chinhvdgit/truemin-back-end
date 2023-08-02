using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Orders.Commands.Create
{
    public partial class CreateOrderDetailModel
    {
        public int ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? OrderPrice { get; set; }
        public decimal? Amount { get; set; }
        public bool IsFree { get; set; }
    }

    public partial class CreateOrderCommand : IRequest<Result<int>>
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<CreateOrderDetailModel> OrderDetails { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderDetailRepository _OrderDetailRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateOrderCommandHandler(IOrderRepository OrderRepository, IOrderDetailRepository OrderDetailRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _OrderRepository = OrderRepository;
            _OrderDetailRepository = OrderDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var Order = _mapper.Map<Order>(request);
            var orderId = await _OrderRepository.InsertAsync(Order);
            //var orderDetails = _mapper.Map<List<OrderDetail>>(request.OrderDetails);
            //foreach (var item in orderDetails)
            //{
            //    await _OrderDetailRepository.InsertAsync(item);
            //}
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(Order.Id);
        }
    }
}
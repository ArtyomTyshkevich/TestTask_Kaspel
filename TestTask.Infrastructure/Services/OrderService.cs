using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(cancellationToken);
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
            var orderDTO = _mapper.Map<OrderDTO>(order);
            return orderDTO;
        }

        public async Task<List<OrderDTO>> GetFilteredAsync(Guid? id, DateTime? createdDate, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetFilteredAsync(id, createdDate, cancellationToken);
            var ordersDTO =_mapper.Map<List<OrderDTO>>(orders);
            return ordersDTO;
        }

        public async Task<OrderDTO> CreateAsync(OrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            order.Books = await _unitOfWork.Books.GetByIdsAsync(request.BooksIds,cancellationToken);
            order.TotalPrice = order.Books.Sum(b => b.Price);
            await _unitOfWork.Orders.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.Orders.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

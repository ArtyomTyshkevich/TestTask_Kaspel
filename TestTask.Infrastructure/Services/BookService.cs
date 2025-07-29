using AutoMapper;
using TestTask.Application.DTOs;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.GetAllAsync(cancellationToken);
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
            return book is null ? null : _mapper.Map<BookDTO>(book);
        }

        public async Task AddAsync(BookDTO bookDTO, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(bookDTO);
            await _unitOfWork.Books.AddAsync(book, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(BookDTO bookDTO, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(bookDTO);
            await _unitOfWork.Books.UpdateAsync(book, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<BookDTO>> GetFilteredAsync(string? name, DateTime? releaseDate, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.GetFilteredAsync(name, releaseDate, cancellationToken);
            var booksDTO = _mapper.Map<List<BookDTO>>(books);
            return booksDTO;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _unitOfWork.Books.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

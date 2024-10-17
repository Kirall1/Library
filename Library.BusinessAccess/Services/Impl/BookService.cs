using AutoMapper;
using Library.DataAccess;
using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;
using Library.BusinessObject;

namespace Library.BusinessAccess.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookService(IUnitOfWork unitOfWork, IFileService fileService,
            IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BookBaseResponseDto>> GetBooksAsync(CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookBaseResponseDto>>(books);
        }

        public async Task<IEnumerable<BookBaseResponseDto>> GetBooksByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.Books.GetByPageAsync(page, pageSize, cancellationToken);
            return _mapper.Map<IEnumerable<BookBaseResponseDto>>(books);
        }

        public async Task<BookDetailedResponseDto> GetBookByIdAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
            if (book == null)
                throw new NotFoundException("Book not found");
            return _mapper.Map<BookDetailedResponseDto>(book);
        }

        public async Task<BookBaseResponseDto> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIsbnAsync(isbn, cancellationToken);
            if (book == null)
                throw new NotFoundException("Book not found");
            return _mapper.Map<BookDetailedResponseDto>(book);
        }

        public async Task<BookCreateResponseDto> AddBookAsync(BookCreateDto bookToCreate,
            CancellationToken cancellationToken)
        {
            if ((await _unitOfWork.Books.GetByIsbnAsync(bookToCreate.Isbn, cancellationToken)) != null)
            {
                throw new ConflictException("Book with this ISBN already exists");
            }
            var imagePath = string.Empty;
            if (bookToCreate.ImageFile != null)
                imagePath = await _fileService.SaveFileAsync(bookToCreate.ImageFile, ["png", "jpg", "jpeg"], cancellationToken);

            var book = _mapper.Map<Book>(bookToCreate);

            if (imagePath != string.Empty)
                book.Image = imagePath;
            book = await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<BookCreateResponseDto>(book);
        }

        public async Task<BaseResponseDto> EditBookAsync(BookUpdateDto bookToUpdate,
            CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookToUpdate.Id, cancellationToken);
            if (book == null)
                throw new NotFoundException("Book not found");

            _mapper.Map(bookToUpdate, book);


            if (bookToUpdate.ImageFile != null)
            {
                var imagePath = await _fileService.SaveFileAsync(bookToUpdate.ImageFile,
                    ["png", "jpg", "jpeg"], cancellationToken);

                if (book.Image != null)
                    _fileService.DeleteFile(book.Image);
                book.Image = imagePath;
            }
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = book.Id };
        }

        public async Task<BaseResponseDto> DeleteBookAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
            if (book == null)
                throw new NotFoundException("Book not found");
            if (book.Image != null)
                _fileService.DeleteFile(book.Image);
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }

        public async Task<BaseResponseDto> TakeBookAsync(BookTakeReturnDto bookToTake,
            CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookToTake.Id, cancellationToken);
            book.TakeBook(bookToTake.UserId);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = book.Id };
        }

        public async Task<BaseResponseDto> ReturnBookAsync(BookTakeReturnDto bookToReturn,
            CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookToReturn.Id, cancellationToken);
            book.ReturnBook();
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = book.Id };
        }

        public async Task<IEnumerable<BookBaseResponseDto>> GetBooksByAuthorAsync(int authorId,
            CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken);
            var books = author.Books;
            return _mapper.Map<IEnumerable<BookBaseResponseDto>>(books);
        }
    }
}

using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;
using Library.BusinessAccess.UseCases.Authors;
using Library.BusinessAccess.UseCases.Authors.Impl;
using Library.Domain;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Library.Tests.AuthorTests
{
    [TestFixture]
    public class AuthorServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private IUnitOfWork _unitOfWork;
        private DatabaseContext _context;
        private ICreateAuthorUseCase _createAuthorUseCase;
        private IDeleteAuthorUseCase _deleteAuthorUseCase;
        private IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private IGetAuthorsByPageUseCase _getAuthorsByPageUseCase;
        private IGetAuthorsUseCase _getAuthorsUseCase;
        private IUpdateAuthorUseCase _updateAuthorUseCase;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDatabase")
                .Options;
            _context = new DatabaseContext(options);
            _unitOfWork = new UnitOfWork(_context);

            _mapperMock = new Mock<IMapper>();
            
            _createAuthorUseCase = new CreateAuthorUseCase(_mapperMock.Object, _unitOfWork);
            _deleteAuthorUseCase = new DeleteAuthorUseCase(_unitOfWork);
            _getAuthorByIdUseCase = new GetAuthorByIdUseCase(_mapperMock.Object, _unitOfWork);
            _getAuthorsByPageUseCase = new GetAuthorsByPageUseCase(_mapperMock.Object, _unitOfWork);
            _getAuthorsUseCase = new GetAuthorsUseCase(_mapperMock.Object, _unitOfWork);
            _updateAuthorUseCase = new UpdateAuthorUseCase(_mapperMock.Object, _unitOfWork);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _unitOfWork.Dispose();
        }

        [Test]
        public async Task GetAuthorsAsync_ShouldReturnAllAuthors()
        {
            // Arrange
            _context.Authors.AddRange
            (
                new Author { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" },
                new Author { Id = 2, Name = "Author 2", Surname = "Surname 2", Country = "Country 2" }
            );
            await _context.SaveChangesAsync();

            var expectedAuthors = new List<AuthorResponseDto>
            {
                new AuthorResponseDto { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1"},
                new AuthorResponseDto { Id = 2, Name = "Author 2", Surname = "Surname 2", Country = "Country 2"}
            };
            _mapperMock.Setup(m => m.Map<IEnumerable<AuthorResponseDto>>(It.IsAny<IEnumerable<Author>>()))
                .Returns(expectedAuthors);

            // Act
            var authors = await _getAuthorsUseCase.ExecuteAsync(CancellationToken.None);

            // Assert
            Assert.AreEqual(2, authors.Count());
        }

        [Test]
        public async Task GetAuthorByIdAsync_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            var author = new Author { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            var expectedAuthor = new AuthorResponseDto 
                { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" };
            _mapperMock.Setup(m => m.Map<AuthorResponseDto>(author)).Returns(expectedAuthor);

            // Act
            var result = await _getAuthorByIdUseCase.ExecuteAsync(1, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async Task CreateAuthorAsync_ShouldCreateAuthor()
        {
            // Arrange
            var authorCreateDto = new AuthorCreateDto { Name = "Author 1", Surname = "Surname 1", Country = "Country 1" };
            var author = new Author { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" };
            var authorCreateResponseDto = new AuthorCreateResponseDto { Id = 1 };

            _mapperMock.Setup(m => m.Map<Author>(authorCreateDto)).Returns(author);
            _mapperMock.Setup(m => m.Map<AuthorCreateResponseDto>(author)).Returns(authorCreateResponseDto);

            // Act
            var result = await _createAuthorUseCase.ExecuteAsync(authorCreateDto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(authorCreateResponseDto.Id, result.Id);
        }

        [Test]
        public void GetAuthorByIdAsync_ShouldThrowNotFoundException_WhenAuthorDoesNotExist()
        {
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _getAuthorByIdUseCase.ExecuteAsync(99, CancellationToken.None));
        }

        [Test]
        public async Task UpdateAuthorAsync_ShouldUpdateAuthor()
        {
            // Arrange
            var existingAuthor = new Author { Id = 1, Name = "Old Name", Surname = "Surname 1", Country = "Country 1" };
            _context.Authors.Add(existingAuthor);
            await _context.SaveChangesAsync();

            var authorUpdateDto = new AuthorUpdateDto { Id = 1, Name = "New Name", Surname = "Surname 1", Country = "Country 1" };
            _mapperMock.Setup(m => m.Map(authorUpdateDto, existingAuthor))
                       .Callback<AuthorUpdateDto, Author>((src, dest) =>
                       {
                           dest.Name = src.Name;
                           dest.Surname = src.Surname;
                           dest.Country = src.Country;
                       });
            

            // Act
            var result = await _updateAuthorUseCase.ExecuteAsync(authorUpdateDto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("New Name", _context.Authors.First().Name);
        }

        [Test]
        public async Task DeleteAuthorAsync_ShouldDeleteAuthor()
        {
            // Arrange
            var author = new Author { Name = "Author 1", Surname = "Surname 1", Country = "Country 1" };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // Act
            var result = await _deleteAuthorUseCase.ExecuteAsync(1, CancellationToken.None);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.IsNull(await _context.Authors.FindAsync(1));
        }
        
        [Test]
        public async  Task GetAuthorsByPageAsync_ShouldReturnAuthorsByPage()
        {
            // Arrange
            _context.Authors.AddRange
            (
                new Author { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" },
                new Author { Id = 2, Name = "Author 2", Surname = "Surname 2", Country = "Country 2" },
                new Author { Id = 3, Name = "Author 3", Surname = "Surname 3", Country = "Country 3" }
            );
            await _context.SaveChangesAsync();

            var expectedAuthors = new List<AuthorResponseDto>
            {
                new AuthorResponseDto { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" },
                new AuthorResponseDto { Id = 2, Name = "Author 2", Surname = "Surname 2", Country = "Country 2" },
                new AuthorResponseDto { Id = 3, Name = "Author 3", Surname = "Surname 3", Country = "Country 3" }
            };
            _mapperMock.Setup(m => m.Map<IEnumerable<AuthorResponseDto>>(It.IsAny<IEnumerable<Author>>()))
                .Returns(expectedAuthors);

            // Act
            var authors = await _getAuthorsByPageUseCase.ExecuteAsync(1, 3, CancellationToken.None);

            // Assert
            Assert.AreEqual(3, authors.Count());
        }
    }
}

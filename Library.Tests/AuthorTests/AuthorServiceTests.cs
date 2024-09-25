using AutoMapper;
using FluentValidation;
using Library.Business.Exceptions;
using Library.Business.Models.Author;
using Library.Business.Services.Impl;
using Library.Business.Services;
using Library.DataAccess;
using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Library.Tests.AuthorTests
{
    [TestFixture]
    public class AuthorServiceTests
    {
        private AuthorService _authorService;
        private Mock<IMapper> _mapperMock;
        private Mock<IValidationService> _validationServiceMock;
        private IUnitOfWork _unitOfWork;
        private DatabaseContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDatabase")
                .Options;
            _context = new DatabaseContext(options);
            _unitOfWork = new UnitOfWork(_context);

            _mapperMock = new Mock<IMapper>();
            _validationServiceMock = new Mock<IValidationService>();

            _authorService = new AuthorService(_unitOfWork, _mapperMock.Object, _validationServiceMock.Object);
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
            var authors = await _authorService.GetAuthorsAsync(CancellationToken.None);

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

            var expectedAuthor = new AuthorResponseDto { Id = 1, Name = "Author 1", Surname = "Surname 1", Country = "Country 1" };
            _mapperMock.Setup(m => m.Map<AuthorResponseDto>(author)).Returns(expectedAuthor);

            // Act
            var result = await _authorService.GetAuthorByIdAsync(1, CancellationToken.None);

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
            _validationServiceMock.Setup(v => v.ValidateAsync(authorCreateDto, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            var result = await _authorService.CreateAuthorAsync(authorCreateDto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(authorCreateResponseDto.Id, result.Id);
        }

        [Test]
        public void GetAuthorByIdAsync_ShouldThrowNotFoundException_WhenAuthorDoesNotExist()
        {
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _authorService.GetAuthorByIdAsync(99, CancellationToken.None));
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

            _validationServiceMock.Setup(v => v.ValidateAsync(authorUpdateDto, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            var result = await _authorService.UpdateAuthorAsync(authorUpdateDto, CancellationToken.None);

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
            var result = await _authorService.DeleteAuthorAsync(1, CancellationToken.None);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.IsNull(await _context.Authors.FindAsync(1));
        }
    }
}

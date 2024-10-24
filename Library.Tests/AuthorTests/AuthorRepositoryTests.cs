using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess.Repositories.Impl;
using Library.DataAccess;


namespace Library.Tests.AuthorTests
{
    [TestFixture]
    public class AuthorRepositoryTests
    {
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;
        private AuthorRepository _authorRepository;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDatabase")
                .Options;

            _context = new DatabaseContext(_options);
            _authorRepository = new AuthorRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _authorRepository.Dispose();
        }

        [Test]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var newAuthor = new Author { Id = 1, Name = "New Author", Surname = "Surname", Country = "Country" };

            // Act
            var addedAuthor = await _authorRepository.AddAsync(newAuthor, CancellationToken.None);
            await _context.SaveChangesAsync();

            // Assert
            var authorInDb = await _context.Authors.FindAsync(1);
            Assert.IsNotNull(authorInDb);
            Assert.AreEqual("New Author", authorInDb.Name);
            Assert.AreEqual(1, _context.Authors.Count());
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Author 1", Surname = "Surname", Country = "Country" },
                new Author { Id = 2, Name = "Author 2", Surname = "Surname", Country = "Country" }
            };

            _context.Authors.AddRange(authors);
            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(a => a.Name == "Author 1"));
            Assert.IsTrue(result.Any(a => a.Name == "Author 2"));
        }
    }
}

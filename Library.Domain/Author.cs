namespace Library.Domain
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Country { get; set; }
        public virtual IEnumerable<Book>? Books { get; set; }
    }
}

namespace Library.DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public virtual Author? Author { get; set; }
        public DateTime? BookTakenTime { get; set; }
        public DateTime? BookReturnTime { get; set; }
        public virtual ApplicationUser? CurrentUser { get; set; }
        public string? Image { get; set; }
    }
}

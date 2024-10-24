namespace Library.Domain
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
        public virtual int? CurrentUserId { get; set; }
        public string? Image { get; set; }

        public void TakeBook(int userId)
        {
            CurrentUserId = userId;
            BookTakenTime = DateTime.Now;
            BookReturnTime = DateTime.Now.AddDays(7);
        }
        
        public void ReturnBook()
        {
            CurrentUserId = null;
            BookTakenTime = null;
            BookReturnTime = null;
        }
    }
}

namespace Library.DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public DateTime BookTakenTime { get; set; }
        public DateTime BookReturnTime { get; set; }
        public User? CurrentUser { get; set; }  
        public string Image {  get; set; }
    }
}

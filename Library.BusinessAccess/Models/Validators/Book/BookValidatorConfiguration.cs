namespace Library.BusinessAccess.Models.Validators.Book
{
    public static class BookValidatorConfiguration
    {
        public const int IsbnMinLength = 10;
        public const int IsbnMaxLength = 13;
        public const int TitleMaxLength = 250;
        public const int AuthorMaxLength = 100;
        public const int DescriptionMaxLength = 1000;
        public const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
    }
}

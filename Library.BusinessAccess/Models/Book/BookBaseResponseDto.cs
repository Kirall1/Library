﻿namespace Library.BusinessAccess.Models.Book
{
    public class BookBaseResponseDto : BaseResponseDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string? AuthorName { get; set; }
        public string? Image { get; set; }
    }
}

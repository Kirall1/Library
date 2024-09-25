﻿using Microsoft.AspNetCore.Http;

namespace Library.Business.Models.Book
{
    public class BookUpdateDto : BaseResponseDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

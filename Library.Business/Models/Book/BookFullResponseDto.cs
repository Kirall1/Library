﻿namespace Library.Business.Models.Book
{
    public class BookFullResponseDto : BookDetailedResponseDto
    {
        public DateTime BookTakenTime { get; set; }
        public DateTime BookReturnTime { get; set; }
    }
}
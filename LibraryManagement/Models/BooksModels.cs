using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BooksModels
    {
        public int Id { get; set; }

        [Display(Name = "Book Id")]
        [Required(ErrorMessage = "You need to enter the book id.")]
        public string BookId { get; set; }
        
        [Display(Name = "Book Name")]
        [Required(ErrorMessage = "You need to enter the name of the book.")]
        public string BookName { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "You need to enter the name of the author.")]
        public string BookAuthor { get; set; }

        [Display(Name = "Description")]
        public string BookDescription  { get; set; }
       
        [Range(0,9999,ErrorMessage = "Enter valid in-stock books number")]
        [Display(Name = "Book Stock")]
        [Required(ErrorMessage = "Enter the number of books available.")]
        public int BookStock { get; set; }

        [Display(Name = "Book Genre")]
        [Required(ErrorMessage = "Select Genre of the book.")]
        public string Genre { get; set; }

        [Display(Name = "Book Language")]
        [Required(ErrorMessage = "Select language of the book.")]
        public string Language { get; set; }
   
        [Range(0, 10000, ErrorMessage = "Enter valid book cost")]
        [Display(Name = "Book Cost")]
        [Required(ErrorMessage = "Enter the price of book.")]
        public int Cost { get; set; }

        [Display(Name = "Book Edition")]
        [Required(ErrorMessage = "Enter the edition of the book.")]
        public string Edition { get; set; }

        public string BookImage { get; set; } = "Unknown";
    
    }
    public enum Languagelist
    {
        English,
        Hindi,
        Marathi,
        Spanish
    }
    public enum Genres
    {
        Fiction, Novel, Narrative, Science_Fiction, Romance, History, Fantacy, Horror, CookBook, Autobiograhy
    }
}
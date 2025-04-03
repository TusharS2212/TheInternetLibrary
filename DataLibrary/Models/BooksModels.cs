using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BooksModels
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookDescription { get; set; }
        public int BookStock { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public int Cost { get; set; }
        public string Edition { get; set; }
        public string BookImage { get; set; }
    }
}

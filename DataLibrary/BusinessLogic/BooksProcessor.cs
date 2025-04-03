using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class BooksProcessor
    {
        public static int CreateBook(string bookId, string bookName, string bookAuthor, string bookDescription, int bookStock, string genre, string language, int cost, string edition, string bookImage)
        {
            BooksModels data = new BooksModels
            {
                BookId = bookId,
                BookName = bookName,
                BookAuthor = bookAuthor,
                BookDescription = bookDescription,
                BookStock = bookStock,
                Genre = genre,
                Language = language,
                Cost = cost,
                Edition = edition,
                BookImage = bookImage
            };
            string sql = @"INSERT INTO dbo.Books (BookId, BookName, BookAuthor, BookDescription, BookStock, Genre, Language, Cost, Edition, BookImage) VALUES (@BookId, @BookName, @BookAuthor, @BookDescription, @BookStock, @Genre, @Language, @Cost, @Edition, @BookImage);";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int EditBook(int id, string bookId, string bookName, string bookAuthor, string bookDescription, int bookStock, string genre, string language, int cost, string edition, string bookImage )
        {
            BooksModels data = new BooksModels
            {
                Id = id,
                BookId = bookId,
                BookName = bookName,
                BookAuthor = bookAuthor,
                BookDescription = bookDescription,
                BookStock = bookStock,
                Genre = genre,
                Language = language,
                Cost = cost,
                Edition = edition,
                BookImage = bookImage
            };
            string sql = @"UPDATE dbo.Books SET BookId = @BookId, BookName = @BookName, BookAuthor = @BookAuthor, BookDescription = @BookDescription, BookStock = @BookStock, Genre = @Genre, Language = @Language, Cost = @Cost, Edition = @Edition, BookImage = @BookImage WHERE Id = @Id;";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int DeleteBook(int id)
        {
            BooksModels data = new BooksModels
            {
               Id = id,
            };
            string sql = @"DELETE FROM dbo.books WHERE Id = @Id;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<BooksModels> LoadOneBooks(int bookId)
        {

            BooksModels data = new BooksModels
            {
                Id = bookId
            };
            string sql = @"SELECT Id, BookId, BookName, BookAuthor, BookDescription, BookStock, Genre, Language, Cost, Edition, BookImage FROM dbo.Books WHERE Id = @Id;";

            return SqlDataAccess.LoadOneData<BooksModels>(sql, data);
        }
        public static List<BooksModels> LoadBooks()
        {
            string sql = @"SELECT Id, BookId, BookName, BookAuthor, BookDescription, BookStock, Genre, Language, Cost, Edition, BookImage FROM dbo.Books;";

            return SqlDataAccess.LoadData<BooksModels>(sql);
        }
    }
}

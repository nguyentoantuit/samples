using MongoDataInteraction.Model;
using MongoDB.Driver;
using System;
using System.Linq;

namespace MongoDataInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("LibraryDB");
            var collection = db.GetCollection<BookStore>("BookStore");
            //BookStore bookStore = new BookStore
            //{
            //    BookTitle = "MongoDB Basics",
            //    ISBN = "876889785213yu",
            //    Auther = "Tanya",
            //    Category = "NoSQL DBMS",
            //    TotalPages = 376
            //};

            //collection.InsertOne(bookStore);



            var bookCount = collection.AsQueryable().Where(b => b.TotalPages > 200).ToList();

            var mongoBooks = collection.AsQueryable().Where(b => b.BookTitle.StartsWith("Mongo")).ToList();

            var sortedBooks = collection.AsQueryable().OrderBy(b => b.BookTitle).ToList();

            var specificBook = collection.AsQueryable().FirstOrDefault<BookStore>(b => b.ISBN == "876889785213");

            Console.Read();

        }
    }
}

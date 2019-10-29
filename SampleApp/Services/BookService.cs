#region snippet_BookServiceClass
using BooksApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using System;

namespace BooksApi.Services
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _books;
        private readonly IMongoContext _MongoContext;

        #region snippet_BookServiceConstructor
        public BookService(IOptions<BookstoreDatabaseSettings> settings, IMongoContext mongoContext)
        {
            //var client = new MongoClient(settings.Value.ConnectionString);
            ////mongoContext.
            //var database = client.GetDatabase(settings.Value.DatabaseName);

            //_books = database.GetCollection<Book>(settings.Value.BooksCollectionName);

            _MongoContext = mongoContext;
            _books = mongoContext.GetCollection<Book>(settings.Value.BooksCollectionName);

        }
        #endregion

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public async Task<List<Book>> GetBooksAsync()
        {
            var filter = FilterDefinition<Book>.Empty;
            var data = await _books.FindAsync(FilterDefinition<Book>.Empty, null,CancellationToken.None);

            return data.ToList();
        }
        public Book Get(string id)
        {
            //var filterExpression = (Expression<Func<Book, bool>>)(x => x.Id == id);

            Expression<Func<Book, bool>> predicate = x => x.BookName == "sadasd";
            var data = _books.FindSync<Book>(predicate, null, CancellationToken.None);
            return data.FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

      
    }

  
        public interface IMongoContext 
        {
         
            IMongoCollection<Book> GetCollection<T>(string name);
           void Create(Book obj);
        }
    

    public interface IBookService
    {
        public List<Book> Get();

        public Task<List<Book>> GetBooksAsync();

        public Book Get(string id);

        public Book Create(Book book);

        public void Update(string id, Book bookIn);

        public void Remove(Book bookIn);

        public void Remove(string id);


    }



    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }

        public  IMongoCollection<Book> _books;
        public MongoContext(IConfiguration configuration)
        {

            // Configure mongo (You can inject the config, just to simplify)
           MongoClient = new MongoClient(configuration.GetSection("MongoSettings").GetSection("Connection").Value);

           Database = MongoClient.GetDatabase(configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
        }

        public IMongoCollection<Book> GetCollection<T>(string name)
        {

            _books = Database.GetCollection<Book>(name);
            return _books;
        }

        public void Create(Book obj)
        {
            _books.InsertOneAsync(obj);
        }
    }
}
#endregion

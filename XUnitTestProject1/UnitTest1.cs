using BooksApi.Models;
using BooksApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {

          //  _fakeMongoDatabase = new Mock<IMongoDatabase>();
          //  _collection = db.GetCollection<Book>(DriverTestConfiguration.CollectionNamespace.CollectionName);
          //  _fakeMongoColeection = new Mock<IMongoCollection<Book>>();
          //  var bppk = new Book { Author = "adasd", Category = "asdasa", BookName = "sadasa" };
          //  var books = new List<Book>();
          //  books.Add(bppk);
          //  _fakeCollectionResult = new Mock<IFindFluent<BsonDocument, BsonDocument>>();
      
          //_fakeMongoDatabase = new Mock<IMongoDatabase>();
          //  //_fakeCollectionResult = (BsonDocument)bppk;
          //  //_fakeMongoDatabase
          //  //    .Setup(_ => _.GetCollection<BsonDocument>("Books", It.IsAny<MongoCollectionSettings>()))
          //  //    .Returns(_fakeMongoCollection.Object);


          //  // Database.GetCollection<Book>(name);

          //  _fakeMongoCollection = new Mock<IFakeMongoCollection>();

          

          //  //_fakeMongoCollection.Setup(x=>x.InsertMany())

          //  _collection.InsertMany(new[]
          //  {
          //      new Book { Author = "adasd", Category = "asdasa", BookName = "sadasa" }
          //  });


          //  //_fakeMongoColeection = _collection;
          //  //_fakeMongoColeection.Setup(x=>x.FindAsync(It.IsAny<Expression<Func<Book, bool>>>())).Returns(bppk);
          //  _fakeMongoDatabase.Setup(_ => _.GetCollection<Book>("Books", It.IsAny<MongoCollectionSettings>()))
          //      .Returns(_collection);

          //  //var mockIDBsettings = new Mock<IBookstoreDatabaseSettings>();

          //  var configuration = new ConfigurationBuilder()

          //    .SetBasePath(Directory.GetCurrentDirectory())
          //    .AddJsonFile("appsettings.json", false)
          //    .Build();

          //  _fakeMongoContext = new Mock<IMongoContext>();
          //  _fakeMongoContext.Setup(_ => _.GetConnection()).Returns(_fakeMongoDatabase.Object);

          //  _fakeMongoContext.Setup(_ => _.GetConnection()).Returns(_fakeMongoDatabase.Object);

          //  _mongoSettings = Options.Create(configuration.GetSection("BookstoreDatabaseSettings").Get<BookstoreDatabaseSettings>());

        

          //  BookService service = new BookService(_mongoSettings, _fakeMongoContext.Object);


          //  service.Get("sadasa");

        }


        [Fact]
        public void Test2()
        {
            var mockClient = new Mock<IMongoClient>();
            var mockContext = new Mock<IMongoContext>();
            var client = mockClient.Object;
            var mockCollection = CreateMockCollection();
            var collection = mockCollection.Object;
            var session = new Mock<IClientSessionHandle>().Object;

            var options = new ChangeStreamOptions();
            var cancellationToken = new CancellationTokenSource().Token;

            Book document = new Book();

            document.Author = "asdas";
            document.BookName = "sadasd";

            document.Id = ObjectId.GenerateNewId().ToString();

            collection.InsertOne(document);

            mockContext.Setup(x => x.GetCollection<Book>("Book")).Returns(collection);
           

            BookstoreDatabaseSettings setting = new BookstoreDatabaseSettings();
            setting.BooksCollectionName = "Book";
            var mockIDBsettings = new Mock<IOptions<BookstoreDatabaseSettings>>();
            mockIDBsettings.Setup(x => x.Value).Returns(setting);

            BookService service = new BookService(mockIDBsettings.Object, mockContext.Object);
            var result = service.Create(document);
            
            mockContext.Verify(x => x.GetCollection<Book>("Book"), Times.Once);


        }


        [Fact]
        public void Test3()
        {
            var mockClient = new Mock<IMongoClient>();
            var mockContext = new Mock<IMongoContext>();
            var client = mockClient.Object;
            var mockCollection = CreateMockCollection();
            var collection = mockCollection.Object;
            var session = new Mock<IClientSessionHandle>().Object;

            var options = new ChangeStreamOptions();
            var cancellationToken = new CancellationTokenSource().Token;

            Book document = new Book();

            document.Author = "asdas";
            document.BookName = "sadasd";

            document.Id = ObjectId.GenerateNewId().ToString();

            collection.InsertOne(document);

            mockContext.Setup(x => x.GetCollection<Book>("Book")).Returns(collection);


            BookstoreDatabaseSettings setting = new BookstoreDatabaseSettings();
            setting.BooksCollectionName = "Book";
            var mockIDBsettings = new Mock<IOptions<BookstoreDatabaseSettings>>();
            mockIDBsettings.Setup(x => x.Value).Returns(setting);

            BookService service = new BookService(mockIDBsettings.Object, mockContext.Object);
            var result = service.Create(document);

            mockContext.Verify(x => x.GetCollection<Book>("Book"), Times.Once);

            var filterDefinition = Builders<Book>.Filter.Eq("BookName", "sadasd");
            var options1 = new FindOptions<Book>(); // no projection


            var filterExpression = (Expression<Func<Book, bool>>)(x => x.BookName == "sadasd");
            //FindFluent<Book, Book> fluent;

            //mockCollection.Verify(x=>x.Find(filterExpression, null), Times.Once);

            mockCollection.Verify(m => m.FindAsync<Book>(It.IsAny<ExpressionFilterDefinition<Book>>(), options1, cancellationToken), Times.Once);


        }

        [Fact]
        public async void Test4GetAsync()
        {
            var mockClient = new Mock<IMongoClient>();
            var mockContext = new Mock<IMongoContext>();
            var client = mockClient.Object;
            var mockCollection = CreateMockCollection();
            var collection = mockCollection.Object;
            var session = new Mock<IClientSessionHandle>().Object;

            var options = new ChangeStreamOptions();
            var cancellationToken = new CancellationTokenSource().Token;

            Book document = new Book();

            document.Author = "asdas";
            document.BookName = "sadasd";

            document.Id = ObjectId.GenerateNewId().ToString();

            collection.InsertOne(document);
            var bookMock = new Mock<IFindFluent<Book, Book>>();

            mockContext.Setup(x => x.GetCollection<Book>("Book")).Returns(mockCollection.Object);


            BookstoreDatabaseSettings setting = new BookstoreDatabaseSettings();
            setting.BooksCollectionName = "Book";
            var mockIDBsettings = new Mock<IOptions<BookstoreDatabaseSettings>>();
            mockIDBsettings.Setup(x => x.Value).Returns(setting);

            //mockCollection.Setup(x => x.FindAsync(It.IsAny<ExpressionFilterDefinition<Book>>(), null, cancellationToken))
            //    .Returns(bookMock.Object);
           
            BookService service = new BookService(mockIDBsettings.Object, mockContext.Object);
            //service.Create(document);
            var result = await service.GetBooksAsync();

            mockContext.Verify(x => x.GetCollection<Book>("Book"), Times.Once);
    


            var options1 = new FindOptions<Book>(); // no projection

            // var filterExpression = (Expression<Func<Book, bool>>)(x => x.BookName == "sadasd");

            var filter = It.IsAny<ExpressionFilterDefinition<Book>>();
            var filter1 = It.IsAny<FilterDefinition<Book>>();
            //var filter2 = It.IsAny<EmptyFilterDefinition<Book>>();
            //FindFluent<Book, Book> fluent;
            //
            //mockCollection.Verify(x=>x.Find(filterExpression, null), Times.Once);
            // 
            //var filterDefinition = Builders<Book>.Filter.Eq("BookName", "sadasd");


            var filterExpression = (Expression<Func<Book, bool>>)(x => x.BookName == "sadasd");
            IMongoCollectionExtensions.FindAsync(collection, session, filterExpression, options1, cancellationToken);
            mockCollection.Verify(m => m.FindAsync<Book>(session, It.IsAny<ExpressionFilterDefinition<Book>>(), options1, cancellationToken), Times.Once);
            mockCollection.Verify(m => m.FindAsync<Book>(FilterDefinition<Book>.Empty, null, CancellationToken.None), Times.Once);
            //
            //
            //
            //
            //

        }

        private Mock<IMongoCollection<Book>> CreateMockCollection()
        {
            var settings = new MongoCollectionSettings();
            var mockCollection = new Mock<IMongoCollection<Book>> { DefaultValue = DefaultValue.Mock };
            mockCollection.SetupGet(s => s.DocumentSerializer).Returns(settings.SerializerRegistry.GetSerializer<Book>());
            mockCollection.SetupGet(s => s.Settings).Returns(settings);
            return mockCollection;
        }


        [Fact]
        public async void Test_GetBooksAsync_Verify()
        {
            //Arrange
            var mockContext = new Mock<IMongoContext>();
            var mockCollection = CreateMockCollection();
            var collection = mockCollection.Object;

            Book document = new Book();
            document.Author = "asdas";
            document.BookName = "sadasd";
            document.Id = ObjectId.GenerateNewId().ToString();
            mockCollection.Object.InsertOne(document);

            BookstoreDatabaseSettings setting = new BookstoreDatabaseSettings();
            setting.BooksCollectionName = "Book";
            var mockIDBsettings = new Mock<IOptions<BookstoreDatabaseSettings>>();

            mockContext.Setup(x => x.GetCollection<Book>("Book")).Returns(mockCollection.Object);
            mockIDBsettings.Setup(x => x.Value).Returns(setting);

            //Act

            BookService service = new BookService(mockIDBsettings.Object, mockContext.Object);
            var result = await service.GetBooksAsync();

            //ASSERT
            mockContext.Verify(x => x.GetCollection<Book>("Book"), Times.Once);
            mockCollection.Verify(m => m.FindAsync<Book>(FilterDefinition<Book>.Empty, null, CancellationToken.None), Times.Once);
        }

        [Fact]
        public  void Test_GetBooks_Verify()
        {
            //Arrange
            var mockContext = new Mock<IMongoContext>();
            var mockCollection = CreateMockCollection();
            Book document = new Book();
            document.Author = "asdas";
            document.BookName = "sadasd";
            document.Id = ObjectId.GenerateNewId().ToString();
            mockCollection.Object.InsertOne(document);

            BookstoreDatabaseSettings setting = new BookstoreDatabaseSettings();
            setting.BooksCollectionName = "Book";
            var mockIDBsettings = new Mock<IOptions<BookstoreDatabaseSettings>>();

            mockContext.Setup(x => x.GetCollection<Book>("Book")).Returns(mockCollection.Object);
            mockIDBsettings.Setup(x => x.Value).Returns(setting);
            var filterExpression1 = (Expression<Func<Book, bool>>)(x => x.Id == document.Id);


    //        var expectedEntities = new List<Book>();
    //        expectedEntities.Add(document);
    //        var mockCursor = new Mock<IAsyncCursor<Book>>();
    //        mockCursor.Setup(_ => _.Current).Returns(expectedEntities); //<-- Note the entities here
    //        mockCursor
    //            .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
    //            .Returns(true)
    //            .Returns(false);
    //        mockCursor
    //            .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
    //            .Returns(Task.FromResult(true))
    //            .Returns(Task.FromResult(false));

    //        mockCollection.Setup(x => x.FindSync<Book>(
    //        filterExpression1,
    //        null,
    //        CancellationToken.None
    //    ))
    //.Returns(mockCursor.Object);

            //Act

            BookService service = new BookService(mockIDBsettings.Object, mockContext.Object);
            var result = service.Get(document.Id);

            //ASSERT
            mockContext.Verify(x => x.GetCollection<Book>("Book"), Times.Once);

            var bookMock = new Mock<IFindFluent<Book, Book>>();
            //mockCollection.Verify(m => m.Find<Book>(FilterDefinition<Book>.Empty, null, CancellationToken.None), Times.Once);
            var options = new FindOptions<Book>();
            var cancellationToken = new CancellationTokenSource().Token;
            var filterDefinition = Builders<Book>.Filter.Eq("BookName", "sadasd");

            var filters = FilterDefinition<Book>.Equals("BookName", "sadasd");
         
            //mockCollection.Verify(m => m.FindSync<Book>(filterDefinition, options, cancellationToken));

            var filterExpression = (Expression<Func<Book, bool>>)(x => x.Id == document.Id);



      
              Expression<Func<Book, bool>> predicate = x => x.BookName == "sadasd";
           // Expression<Func<Person, bool>> predicate = x => x.Name == captured[0];

            mockCollection.Verify(m => m.FindSync<Book>(predicate, null, CancellationToken.None), Times.Once);



        }
    }
}

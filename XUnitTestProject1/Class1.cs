using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject1
{


    public class IFindFluentExtensionsTests
    {
        //private IFindFluent<Person, Person> CreateSubject()
        //{
        //    var settings = new MongoCollectionSettings();
        //    var mockCollection = new Mock<IMongoCollection<Person>>();
        //    mockCollection.SetupGet(c => c.Settings).Returns(settings);
        //    var options = new FindOptions<Person, Person>();
        //    return new IFindFluent<Person, Person>(session: null, collection: mockCollection.Object, filter: new BsonDocument(), options: options);
        //}

        public class Person
        {
            public string FirstName;
            public string LastName;
            public int Age;
        }
    }
}

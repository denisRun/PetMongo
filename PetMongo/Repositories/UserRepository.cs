using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using PetMongo.Extensions;
using PetMongo.Models;
using PetMongo.Requests;
using PetMongo.Responses;

namespace PetMongo.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _collectionName = "users";
    private readonly string _connecitonString = "mongodb://localhost:27017/workout";
    
    public UserRepository()
    {
        var mongoUrl = new MongoUrl(_connecitonString);
        var dbname = mongoUrl.DatabaseName;
        var db = new MongoClient(mongoUrl).GetDatabase(dbname);
        _userCollection = db.GetCollection<User>(_collectionName);
    }
    
    private IMongoCollection<User> _userCollection;

    public async Task CreateUser(User newItem)
    {
        await _userCollection.InsertOneAsync(newItem);
    }

    public async Task UpdateUser(User updateItem)
    {
        await _userCollection.ReplaceOneAsync(u => u.Id == updateItem.Id, updateItem);
    }
    
    public User GetUserById(ObjectId id)
    {
        return _userCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public PagedResultSet<User> SearchUsers(UsertSearchRequest request)
    {
        var filter = BuildFitler(request);
        var sorting = BuildSorting(request);
        int skip = (request.PageNumber - 1) * request.PageSize;
        int limit = request.PageSize;

        var totalItemsCount = _userCollection.CountDocuments(filter);
        var totalPageCount = (long)Math.Ceiling((double)totalItemsCount / request.PageSize);

        var items =_userCollection
            .Find(filter)
            .Sort(sorting)
            .Skip(skip)
            .Limit(limit)
            .ToList();

        return new PagedResultSet<User>(
            request.PageNumber,
            request.PageSize,
            totalPageCount,
            totalItemsCount,
            items);
    }

    private FilterDefinition<User> BuildFitler(UsertSearchRequest request)
    {
        var builder = Builders<User>.Filter;

        var filter = builder.Where(x =>  
            request.Query.IsNullOrEmpty() ||
            x.Email.ToLower().Contains(request.Query.ToLower()) ||
            x.FullName.ToLower().Contains(request.Query.ToLower()));

        return filter;
    }
    
    private SortDefinition<User> BuildSorting(UsertSearchRequest request)
    {
        var builder = Builders<User>.Sort;
        SortDefinition<User> sorting = null;
        string sortingField = request.OrderByField == UsertSearchRequest.OrderBy.Email
            ? UsertSearchRequest.OrderBy.Email.GetEnumDescription()
            : UsertSearchRequest.OrderBy.FullName.GetEnumDescription();
        
        switch (request.Order)
        {
            case UsertSearchRequest.SortOrder.Ascending:
                sorting = builder.Ascending(sortingField);
                break;
            case UsertSearchRequest.SortOrder.Descending:
                sorting = builder.Descending(sortingField);
                break;
        }

        return sorting;
    }
}
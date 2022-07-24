using MongoDB.Bson;
using PetMongo.Models;
using PetMongo.Requests;
using PetMongo.Responses;

namespace PetMongo.Repositories;

public interface IUserRepository
{
    Task CreateUser(User newItem);
    Task UpdateUser(User updateItem);
    User GetUserById(ObjectId id);
    PagedResultSet<User> SearchUsers(UsertSearchRequest request);
}
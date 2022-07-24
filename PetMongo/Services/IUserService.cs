using MongoDB.Bson;
using PetMongo.Models;
using PetMongo.Requests;
using PetMongo.Resources;
using PetMongo.Responses;

namespace PetMongo.Services;

public interface IUserService
{
    Task CreateUser(User newItem);
    Task UpdateUser(User userToUpdate, User newItem);
    User GetUserById(ObjectId id);
    PagedResultSet<User> SearchUsers(UsertSearchRequest request);
}
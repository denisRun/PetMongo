using AutoMapper;
using MongoDB.Bson;
using PetMongo.Enumerations;
using PetMongo.Models;
using PetMongo.Repositories;
using PetMongo.Requests;
using PetMongo.Resources;
using PetMongo.Responses;

namespace PetMongo.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task CreateUser(User newItem)
    {
        await _userRepository.CreateUser(newItem);
    }

    public async Task UpdateUser(User userToUpdate, User newUser)
    {
        _mapper.Map(newUser, userToUpdate);
        await _userRepository.UpdateUser(userToUpdate);
    }
    
    public User GetUserById(ObjectId id)
    {
        return _userRepository.GetUserById(id);
    }

    public PagedResultSet<User> SearchUsers(UsertSearchRequest request)
    {
        return _userRepository.SearchUsers(request);
    }
}
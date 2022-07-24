using AutoMapper;
using PetMongo.Models;
using PetMongo.Resources;
using PetMongo.Responses;

namespace PetMongo.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, User>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<User, UserResource>();
        CreateMap<UserSaveResource, User>();
        CreateMap<PagedResultSet<User>, PagedResultSet<UserResource>>();
    }
}
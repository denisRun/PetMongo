using PetMongo.Models;

namespace PetMongo.Resources;

public class UserResource
{
    public UserResource(){}
    
    public string Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
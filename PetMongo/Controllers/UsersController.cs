using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PetMongo.Enumerations;
using PetMongo.Models;
using PetMongo.Requests;
using PetMongo.Resources;
using PetMongo.Responses;
using PetMongo.Services;

namespace PetMongo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserResource),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResource>> CreateUser(UserSaveResource newUser)
    {
        var model = _mapper.Map<User>(newUser);
        await _userService.CreateUser(model);
        
        var resource = _mapper.Map<UserResource>(model);
        return CreatedAtAction(nameof(GetById), new { id = resource.Id }, resource);;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResource),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(UserResource),StatusCodes.Status404NotFound)]
    public ActionResult<UserResource> GetById(string id)
    {
        var model = _userService.GetUserById(new ObjectId(id));

        if (model == null)
        {
            return BadRequest();
        }
        
        var resource = _mapper.Map<UserResource>(model);
        return Ok(resource);
    }
    
    [HttpPost("search")]
    [ProducesResponseType(typeof(UserResource),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(UserResource),StatusCodes.Status404NotFound)]
    public ActionResult<PagedResultSet<UserResource>> Search(UsertSearchRequest request)
    {
        var model = _userService.SearchUsers(request);
        
        var resources = _mapper.Map<PagedResultSet<UserResource>>(model);
        return Ok(resources);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResource),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResource>> UpdateUser(string id, [FromBody] UserSaveResource newUser)
    {
        var modeltoUpdate = _userService.GetUserById(ObjectId.Parse(id));
        var newmModel = _mapper.Map<User>(newUser);
        await _userService.UpdateUser(modeltoUpdate, newmModel);
        
        var resource = _mapper.Map<UserResource>(modeltoUpdate);
        return Ok(resource);
    }
}
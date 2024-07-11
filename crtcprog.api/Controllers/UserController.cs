using crtcprog.api.Controllers;
using crtcprog.api.DTO;
using crtcprog.api.Models;
using crtcprog.api.Services;
using ctrcprog.api.Data;
using ctrcprog.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crtcprog.api.Controllers;


[ApiController]
[Route("api/Users")]
public class UserController : GenericController<User>
{
    private readonly IUserService _userService; // Assuming you have an IUserService      
    private readonly IRepository<User> _repository;


    public UserController(IRepository<User> repository, IUserService userService)
            : base(repository)
    {
        _userService = userService;
        _repository = repository;
    }

    [HttpPost("sendata")]
    public  void   SendData(string username, string email)
        {

    }

    [HttpGet("getByEmail/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Email is required" });


        var user = (await _repository.FindAsync(user => user.Email.ToLower() == email.ToLower())).FirstOrDefault();

        if (user?.Email == null)
            return Ok(new { message = "User not found" });

        return Ok(user);

    }


    [HttpGet("userByEmail")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Email is required" });


        var user = (await _repository.FindAsync(user => user.Email.ToLower() == email.ToLower())).FirstOrDefault();

        if (user?.Email == null)
            return Ok(new { message = "User not found" });

        return Ok(user);

    }


    [HttpGet("checkemail")]
    public async Task<IActionResult> CheckEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Email is required" });


        var exists = await _userService.CheckEmailExistsAsync(email);


        return Ok(new { exists });
    }

    //[HttpPost("login")]
    //public async Task<IActionResult> Login([FromBody] LoginModel model)

    //{
    //    // var user = await _userService.AuthenticateAsync(model.Email, model.Password);
    //    string? _email = model.Email.ToLower();
    //    string? _password = model.Password;

    //    var user = await _repository.FindAsync(
    //        user => user.Email.ToLower() == _email);

    //    //  if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))

    //    var foundUser = user.FirstOrDefault();
    //    if (foundUser == null || foundUser.Password != _password)
    //        return BadRequest(new { message = "Email or password is incorrect" });



    //    return Ok(foundUser);
    //}

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userModel)
    {
        var registeredUser = await _userService.RegisterAsync(userModel);

        if (registeredUser == null)
            return BadRequest(new { message = "Registration failed" });

        return Ok(registeredUser);
    }

   
     

}
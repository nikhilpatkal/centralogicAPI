using AuthenticationApp.Business.User;
using AuthenticationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Controllers.User
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUser _userBusiness;
    public UserController(IUser userBusiness) {
      _userBusiness = userBusiness;
    }

    // ✅ This is public (no token needed)
    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult LoginUser([FromBody] LoginReq request)
    {
      try
      {
        String userName=request.UserName;
        String password=request.Password;
        var data = _userBusiness.Login(userName,password);

        return Ok(data);
      }
      catch (Exception ex)
      {
        return Unauthorized(ex.Message);
      }
    }
    [HttpGet("GetProfile")]
    public IActionResult GetProfile()
    {
      return Ok("You are an authorized user! 👍");
    }
  }
}

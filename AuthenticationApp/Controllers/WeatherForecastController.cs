  using AuthenticationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

  namespace AuthenticationApp.Controllers
  {
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      //getting call from the data for two parameters
      [HttpPost("LoginReq")]
      public IActionResult loginReq(string name,long password)
      {
        string UserName = null;
        try
        {
          UserName=name.ToLower();
        }
        catch (Exception ex)
        {
          BadRequest(ex.Message);
        }

        return Ok(UserName);
      }

      //getting data in specific format on that only this endpoint call
      [HttpPost("SignUp")]
      public IActionResult signUp([FromBody] UserModels userData)
      {
        try
        {
          return Ok("Done SignUp"+userData.UserName);
        }
        catch (Exception ex)
        {
          return BadRequest("Not Abel to SignUp");
        }
      }

      //getting the req data from the url 
      [HttpPost("userRew/{id}")]
      public IActionResult logout(int id)
      {
        return Ok($"User with ID {id} logged out successfully.");
      }

      [HttpPost("search")]
      public IActionResult Search([FromQuery] string keyword)
      {
        return Ok($"Searching for {keyword}");
      }

    [HttpPost("Login")]
    public IActionResult Login()
    {
      return Ok("You Have Been ");
    }
  }
  }

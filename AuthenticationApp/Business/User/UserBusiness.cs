using AuthenticationApp.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Data.SqlClient;
using AuthenticationApp.Helpers;

namespace AuthenticationApp.Business.User
{
  public class UserBusiness : IUser
  {
    private readonly IConfiguration _config;
    private readonly DataBaseHelper _dbhelper;
    public UserBusiness(IConfiguration config, DataBaseHelper dbhelper)
    {
      _config = config;
      _dbhelper = dbhelper;
    }
    public ResposeResult Login(string userName,string password)
    {

      ResposeResult Result = new ResposeResult();
      if (userName == null || password == null)
      {
        throw new ArgumentNullException("Value not Provided");
      }
      if((userName=="Nikhil" && password == "123") || (userName == "Aniket" && password == "123"))
      {
        dynamic parameters = new SqlParameter[]
           {
                  new SqlParameter("@Email","nikhilpatkal9552@gmail.com")
           };
        List<dynamic> users = _dbhelper.ExecuteStoredProcedureDynamic<dynamic>("GetUserByEmailAsync", parameters);


        Result.Success = true;
        Result.Message = $"{userName} You Have Sucessfully Login";
        Result.ResultData = GenerateJwtToken(userName); ;



        return Result;
      }
      Result.Success = false;
      Result.Message = "User Not Found";
      return Result;

    }
    private string GenerateJwtToken(string username)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: _config["Jwt:Issuer"],
          audience: _config["Jwt:Audience"],
          claims: new[] { new Claim(ClaimTypes.Name, username) },
          expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["Jwt:ExpireMinutes"])),
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}

using AuthenticationApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Business.User
{
  public interface IUser
  {
    public ResposeResult Login(string userName,string password);
  }
}

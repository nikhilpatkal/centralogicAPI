namespace AuthenticationApp.Models
{
  public class UserModels
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

  }
  public class LoginReq
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
  public class ResposeResult
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public string ResultData { get; set; }
  }
}

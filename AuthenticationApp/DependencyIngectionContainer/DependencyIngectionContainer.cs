using AuthenticationApp.Business.User;

namespace AuthenticationApp.DependencyIngectionContainer
{
  public class DependencyIngectionContainer
  {
    public static void Injector(IServiceCollection services)
    {
      services.AddScoped<IUser, UserBusiness>();

    }
  }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebApplication1.Models;

namespace WebApplication1.Models
{
   

    public class ApplicationUserManager :  UserManager<UserModel>
    {
        public ApplicationUserManager(IUserStore<UserModel> store)
        : base(store)
        {
        }

        public UserModel Id => throw new System.NotImplementedException();

        public string UserName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
        IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<UserModel>(db));
            return manager;
        }
    }
}
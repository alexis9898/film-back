using BLL.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUp(SignUpModel signUpModel);
        Task<UserModel> LoginAsync(SignInModel signInModel);
    }
}

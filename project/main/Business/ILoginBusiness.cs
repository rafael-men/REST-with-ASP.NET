using main.Models;
using main.VO;

namespace main.Business
{
    public interface ILoginBusiness
    {
        string LoginUser(LoginVO login);
        User RegisterUser(UserVO userCredentials);
    }
}

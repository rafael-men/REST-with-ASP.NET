using main.Models;
using main.VO;

namespace main.Business
{
    public interface ILoginBusiness
    {
        string ValidateCredentials(LoginVO user);
        User RegisterUser(UserVO userCredentials);
    }
}

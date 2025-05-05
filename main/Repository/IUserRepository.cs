using main.Models;
using main.VO;

namespace main.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User RefreshUserInfo(User user);
        bool ExistsByUserName(string userName);
        User Create(User user);

    }
}

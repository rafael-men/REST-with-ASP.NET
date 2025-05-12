using main.Models;
using main.VO;

namespace main.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        bool ExistsByUserName(string userName);
        User Create(User user);
        User FindByUserName(string userName);

    }
}

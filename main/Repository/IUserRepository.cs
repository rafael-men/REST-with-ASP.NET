using main.Models;
using main.VO;

namespace main.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
    }
}

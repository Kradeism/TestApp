using System.Collections.Generic;
using System.Threading.Tasks;
using UserTestApplication.Models;

namespace UserTestApplication.Services
{
    public interface IUserServices
    {
        Task<List<UserModel>> GetUsers();
    }
}

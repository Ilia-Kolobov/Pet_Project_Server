using BSA_EF.DAL.Interfaces;
using BSA_EF.DAL.Models;
using BSA_EF.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Services
{
    public class UsersService : IUsersService
    {
        private IUnitOfWork unitOfWork;

        public UsersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetUsers()
        {
            var result = await unitOfWork.Users.GetAsync();
            return result.ToList();
        }

        public Task<User> GetUser(int id)
        {
            return unitOfWork.Users.GetAsync(id);
        }

        public async Task PostUser(User user)
        {
            await unitOfWork.Users.CreateAsync(user);
            await unitOfWork.Commit();
        }

        public async Task PutUser(User user)
        {
            await unitOfWork.Users.UpdateAsync(user);
            await unitOfWork.Commit();
        }

        public async Task DeleteUser(int id)
        {
            await unitOfWork.Users.DeleteAsync(id);
            await unitOfWork.Commit();
        }
    }
}

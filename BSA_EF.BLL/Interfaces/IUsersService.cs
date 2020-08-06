using BSA_EF.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Interfaces
{
    public interface IUsersService
    {
        public Task<List<User>> GetUsers();
        public Task<User> GetUser(int id);
        public Task PostUser(User user);
        public Task PutUser(User user);
        public Task DeleteUser(int id);
    }
}

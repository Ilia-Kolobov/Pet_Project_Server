
using System.Collections.Generic;


namespace Common.DTOs.LinqDTOs
{
    public class TeamsUsersDTO
    {
        public TeamDTO Team { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}

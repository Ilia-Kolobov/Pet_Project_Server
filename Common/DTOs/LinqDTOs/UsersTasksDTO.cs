
using System.Collections.Generic;


namespace Common.DTOs.LinqDTOs
{
    public class UsersTasksDTO
    {
        public UserDTO User { get; set; }
        public IEnumerable<ProjectTaskDTO> Tasks { get; set; }
    }
}

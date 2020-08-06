
using System.Collections.Generic;


namespace Common.DTOs.LinqDTOs
{
    public class UserInfoDTO
    {
        public UserDTO User { get; set; }
        public ProjectDTO LastProject { get; set; }
        public int TasksCount { get; set; }
        public int UnfinishedAndCanceledTasksCount { get; set; }
        public ProjectTaskDTO LongestTask { get; set; }
    }
}

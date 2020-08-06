

namespace Common.DTOs.LinqDTOs
{
    public class ProjectInfoDTO
    {
        public ProjectDTO Project { get; set; }
        public ProjectTaskDTO LongestTaskByDicription { get; set; }
        public ProjectTaskDTO LessTaskByName { get; set; }
        public int UserCounWhereDescriptionMore20orTaskCounLess3 { get; set; }
    }
}

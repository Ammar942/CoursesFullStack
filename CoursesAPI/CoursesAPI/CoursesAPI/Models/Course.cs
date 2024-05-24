namespace CoursesAPI.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public int ClassNo { get; set; }
        public string Code { get; set; }
    }
}

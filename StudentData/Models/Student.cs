using System.ComponentModel.DataAnnotations;

namespace StudentData.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Standard { get; set; }
        public string? Stream { get; set; }
    }
}

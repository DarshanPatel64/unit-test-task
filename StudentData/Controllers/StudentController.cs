using Microsoft.AspNetCore.Mvc;
using StudentData.Models;

namespace StudentData.Controllers
{
    public class StudentController : Controller
    {
        private AppDbContext context;
        public StudentController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IEnumerable<Student> GetStudents()
        {
            var data = context.students;
            return data.ToList();
        }
        
        public ActionResult GetStudentWithId(int id)
        {

            var students = context.students.FirstOrDefault(_ => _.Id == id);

            return (students == null) ? NotFound() : Ok(students);

        }

        public ActionResult AddStudent(Student student)
        {

            var students = context.students.Add(student);

            return Ok();

        }
        
        public ActionResult deleteStudent(int id)
        {
            var student = context.students.FirstOrDefault(_ => _.Id == id);
            if(student == null) return NotFound();
            context.students.Remove(student);
            return Ok();
        }

        public ActionResult editStudent(Student student)
        {
            var rslt = context.students.Where(x => x.Id == student.Id);
            if(rslt == null) return NotFound();
            context.students.Update(student);
            return Ok();
        }


    }
}

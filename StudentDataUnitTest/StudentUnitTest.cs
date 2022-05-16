using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StudentData.Controllers;
using StudentData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;



namespace StudentDataUnitTest
{
    [TestFixture]
    public class Tests
    {
        public AppDbContext context { get; set; }

        public StudentController studentController;
        [SetUp]
        public void Setup()
        {
           
            context = GetContext();
            studentController = new StudentController(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            studentController.Dispose();
        }
        
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseInMemoryDatabase(databaseName: "students");

            var context = new AppDbContext(options.Options);
            context.students.Add(new Student { Id = 1, Name = "darshan", Age = 16, Standard = 11, Stream = "Science" });
            context.students.Add(new Student { Id = 2, Name = "Dhaval", Age = 17, Standard = 12, Stream = "Arts" });
            context.SaveChanges();
            //

            return context;
        }

        [Test]
        public void GetStudents_Should_Return_two_records()
        {
           
            var result = studentController.GetStudents();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetStudentsById_WhenValidIdPassed_ShouldReturnOnerecords()
        {
            var result = studentController.GetStudentWithId(1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetStudentsById_WhenInValidIdPassed_ShouldReturnOnerecords()
        {
            var result = studentController.GetStudentWithId(9);

           

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public void AddStudent_WhenAdd_shouldReturnOk()
        {
            Student student = new Student()
            {
                Id = 5,
                Name = "aadarsh",
                Age = 16,
                Standard = 11,
                Stream = "commerse"
            };
            var result = studentController.AddStudent(student);
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public void deleteStudent_whenValidIdPassed_ShouldDeleteStudent()
        {
            var result = studentController.deleteStudent(1);
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public void deleteStudent_whenInValidIdPassed_ShouldDeleteStudent()
        {
            var result = studentController.deleteStudent(111);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public void editStudent_shouldEditStudent()
        {
            Student student = new Student()
            {
                Id = 5,
                Name = "aadarsh",
                Age = 19,
                Standard = 11,
                Stream = "commerse"
            };
            var result = studentController.editStudent(student);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
using Microsoft.Extensions.Logging;
using StudentManagementDataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementDataAccess.Repository
{
    public class MockStudentRepository : IStudentRepository
    {
        private ILogger<MockStudentRepository> logger;
        private List<Student> _studentList;


        public MockStudentRepository(ILogger<MockStudentRepository> logger)
        {
            this.logger = logger;
            _studentList = new List<Student>()
            {
            new Student() { Id = 1, Name = "張三", Major = MajorEnum.FirstGrade, Email = "Tony-zhang@52abp.com" },
            new Student() { Id = 2, Name = "李四", Major = MajorEnum.SecondGrade, Email = "lisi@52abp.com" },
            new Student() { Id = 3, Name = "王二麻子", Major = MajorEnum.GradeThree, Email = "wang@52abp.com" },
            };

        }

        public Student Add(Student student)
        {
            student.Id = _studentList.Max(s => s.Id) + 1;
            _studentList.Add(student);
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            logger.LogTrace("Students Trace(跟踪) Log");
            logger.LogDebug("Students Debug(偵錯) Log");
            logger.LogInformation("Students 信息(Information) Log");
            logger.LogWarning("Students 警告(Warning) Log");
            logger.LogError("Students 錯誤(Error) Log");
            logger.LogCritical("Students 嚴重(Critical) Log");

            return _studentList;

        }
        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(a => a.Id == id);
        }
        public Student Delete(int id)
        {
            Student student = _studentList.FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                _studentList.Remove(student);

            }
            return student;

        }
        public Student Update(Student updateStudent)
        {
            Student student = _studentList.FirstOrDefault(s => s.Id == updateStudent.Id);

            if (student != null)
            {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.Major = updateStudent.Major;
            }
            return student;
        }
    }
}

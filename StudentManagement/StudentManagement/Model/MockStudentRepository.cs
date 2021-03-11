using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Model
{
    public class MockStudentRepository : IStudentRepository
    {
        private readonly List<Student> _studentList;

        public MockStudentRepository()
        {
            _studentList = new List<Student>()
            {
            new Student() { Id = 1, Name = "張三", Major = MajorEnum.FirstGrade, Email = "Tony-zhang@52abp.com" },
            new Student() { Id = 2, Name = "李四", Major = MajorEnum.SecondGrade, Email = "lisi@52abp.com" },
            new Student() { Id = 3, Name = "王二麻子", Major = MajorEnum.GradeThree, Email = "wang@52abp.com" },
            };
        }


        public Student GetStudent(int id)
        {
            // 返回學生名字
            return _studentList.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentList;
        }
    }
}

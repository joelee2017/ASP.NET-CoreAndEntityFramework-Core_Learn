using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Model
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList;

        public MockStudentRepository()
        {
            _studentList = new List<Student>()
            {
            new Student() { Id = 1, Name = "張三", Major = "一年级", Email = "Tony-zhang@52abp.com" },
            new Student() { Id = 2, Name = "李四", Major = "二年级", Email = "lisi@52abp.com" },
            new Student() { Id = 3, Name = "王二麻子", Major = "二年级", Email = "wang@52abp.com" },
            };
        }


        public Student GetStudent(int id)
        {
            // 返回學生名字
            return _studentList.FirstOrDefault(a => a.Id == id);
        }
    }
}

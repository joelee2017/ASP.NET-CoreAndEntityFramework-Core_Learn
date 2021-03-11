using System.Collections.Generic;

namespace StudentManagement.Model
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();

        Student Add(Student student);
    }
}

using StudentManagementDataAccess.Models;
using System.Collections.Generic;

namespace StudentManagementDataAccess.Repository
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
        Student Add(Student student);
        Student Update(Student updateStudent);
        Student Delete(int id);
    }
}

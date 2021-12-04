using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPl.Repositories
{
      public interface IStudentRepository
    {
        Task<IEnumerable<Student>> Search(string name);
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<Student> AddStudents(Student student);
        Task<Student> UpdateStudents(Student student);
        Task <Student> DeleteStudents(int id);

    }
}

using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPl.DataContext;

namespace WebApplicationAPl.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbcontext _Context;

        public StudentRepository(ApplicationDbcontext Context)
        {
            _Context = Context;
        }
        public async Task<Student> AddStudents(Student student)
        {
            var stu = await _Context.students.AddAsync(student);
             await _Context.SaveChangesAsync();
            return stu.Entity;

        }

        public async Task<Student> DeleteStudents(int id)
        {
            var stu = await _Context.students.Where(a => a.id == id).FirstOrDefaultAsync();
            if (stu != null)
            {
                _Context.students.Remove(stu);
                await _Context.SaveChangesAsync();
                return stu;
            }
            return null;
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _Context.students.
                 FirstOrDefaultAsync(a => a.id == id);
        }
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _Context.students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> Search(string name)
        {
            IQueryable<Student> query = _Context.students;
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Student> UpdateStudents(Student student)
        {
            var stu = await _Context.students.
                FirstOrDefaultAsync(a => a.id == student.id);
            if (stu != null)
            {
                stu.Name = student.Name;
                stu.City = student.City;
                await _Context.SaveChangesAsync();
                return stu;
            }
            return null;
        }
    }

       
    }


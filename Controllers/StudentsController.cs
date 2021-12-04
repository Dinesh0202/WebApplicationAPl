using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPl.Repositories;

namespace WebApplicationAPl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentsController (IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                return Ok(await _studentRepository.GetStudents());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

         }

        [HttpGet ("{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var stu = await _studentRepository.GetStudent(id);
                if (stu == null)
                {
                    return NotFound();
                }
                return stu;
            }
            catch (Exception ex)
            {
               return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
              if(student==null)
                {
                    return BadRequest();
                }
                var CreatedStudent = await _studentRepository.AddStudents(student);
                return CreatedAtAction(nameof(GetStudent), new { Id = CreatedStudent.id },CreatedStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudents(int id, Student student)
        {
            try
            {
                if(id!= student.id)
                {
                    return BadRequest("Id Mismatch");
                }
                var Stuupdate = await _studentRepository.GetStudent(id);
                if(Stuupdate==null)
                {
                    return NotFound($"Student Id ={id} Not Found");
                }
                if(student==null)
                {
                    return BadRequest();
                }
                 return  await _studentRepository.UpdateStudents(student);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Student>> DeleteStudents(int id)
        {
            try
            {
                
                var Studelete = await _studentRepository.GetStudent(id);
                if (Studelete == null)
                {
                    return NotFound($"Student Id ={id} Not Found");
                }
                
                return await _studentRepository.DeleteStudents(id);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }
        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string name)
        {
            try
            {
               var stusearch=  await _studentRepository.Search(name);
                if(stusearch.Any())
                {
                    return Ok(stusearch);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}

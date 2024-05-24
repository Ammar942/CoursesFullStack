using CoursesAPI.Data;
using CoursesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CourseDbContext _courseDbContext;
        public CoursesController(CourseDbContext courseDbContext)
        {
            _courseDbContext = courseDbContext;
        }

        public CourseDbContext CourseDbContext { get; }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseDbContext.Courses.ToListAsync();
            return Ok(courses);
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course courseRequest)
        {
            courseRequest.Id = Guid.NewGuid();
            await _courseDbContext.Courses.AddAsync(courseRequest);
            await _courseDbContext.SaveChangesAsync();
            return Ok(courseRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCourse([FromRoute] Guid id)
        {
            var Course = await _courseDbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (Course == null)
            {
                return NotFound();
            }
            return Ok(Course);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, Course updateCourse)
        {
            var Course = await _courseDbContext.Courses.FindAsync(id);
            if (Course == null)
            {
                return NotFound();
            }
            Course.Name = updateCourse.Name;
            Course.Teacher = updateCourse.Teacher;
            Course.ClassNo = updateCourse.ClassNo;
            Course.Code = updateCourse.Code;

            await _courseDbContext.SaveChangesAsync();

            return Ok(Course);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id)
        {
            var Course = await _courseDbContext.Courses.FindAsync(id);
            if (Course == null)
            {
                return NotFound();
            }
            _courseDbContext.Courses.Remove(Course);
            await _courseDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly MovieContext _studentContext;

        public StudentsController(MovieContext studentContext)
        {
            _studentContext = studentContext;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _studentContext.Students.ToListAsync();
        }
    }
}

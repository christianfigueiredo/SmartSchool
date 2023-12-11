using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly Contexto _context;
        public ProfessorController(Contexto contexto) 
        {
            _context = contexto;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
          
    }
}

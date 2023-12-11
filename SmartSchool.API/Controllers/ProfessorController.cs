using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly Contexto _contexto;
        public ProfessorController(Contexto contexto) 
        {
            _contexto = contexto;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_contexto.Professores);
        }

         [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var professor = _contexto.Professores.FirstOrDefault(a => a.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }   
        [HttpGet("byNome")]
        public IActionResult GetByNome(string nome)
        {
            var professor = _contexto.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if(professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }   

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _contexto.Add(professor);
            _contexto.SaveChanges();            
            return Ok(professor);
        }        
            
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var alu = _contexto.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("Professor não encontrado");
            _contexto.Update(professor);
            _contexto.SaveChanges();
            return Ok(professor);
        }  

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor) 
        { 
            var prof = _contexto.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado");
            _contexto.Update(professor);
            _contexto.SaveChanges();           
            return Ok(professor);
        }  

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _contexto.Professores.FirstOrDefault(a => a.Id == id);          
            if(professor == null) return BadRequest("Professor não encontrado");
              _contexto.Remove(professor);
            _contexto.SaveChanges();                        
            return Ok();
        }  
          
    }
}

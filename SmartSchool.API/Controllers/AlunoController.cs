using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly Contexto _contexto;
         public AlunoController(Contexto contexto) 
         {
             _contexto = contexto;
         }          
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contexto.Alunos);
        }

        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var aluno = _contexto.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }   
        [HttpGet("byNome")]
        public IActionResult GetByNome(string nome, string sobrenome)
        {
            var aluno = _contexto.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if(aluno == null) return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }   

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _contexto.Add(aluno);
            _contexto.SaveChanges();            
            return Ok(aluno);
        }        
            
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _contexto.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado");
            _contexto.Update(aluno);
            _contexto.SaveChanges();
            return Ok(aluno);
        }  

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) 
        { 
            var alu = _contexto.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado");
            _contexto.Update(aluno);
            _contexto.SaveChanges();           
            return Ok(aluno);
        }  

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _contexto.Alunos.FirstOrDefault(a => a.Id == id);          
            if(aluno == null) return BadRequest("Aluno não encontrado");
              _contexto.Remove(aluno);
            _contexto.SaveChanges();                        
            return Ok();
        }  
       
    }
}

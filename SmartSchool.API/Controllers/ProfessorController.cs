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
       
        private readonly IRepository _repository;
       
        public ProfessorController(IRepository repository) 
        {
           
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllProfessores(true); 
            return Ok(result);
        }

         [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }   
         

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if(_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("professor não cadastrado");
        }        
            
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var alu = _repository.GetProfessorById(id, false);
            if(alu == null) return BadRequest("Professor não encontrado");

             _repository.Update(professor);
            if(_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("professor não atualizado");            
        }  

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor) 
        { 
            var prof = _repository.GetProfessorById(id, false);
            if(prof == null) return BadRequest("Professor não encontrado");

             _repository.Update(professor);
            if(_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("professor não atualizado");
        }  

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorById(id, false);         
            if(professor == null) return BadRequest("Professor não encontrado");

             _repository.Delete(professor);
            if(_repository.SaveChanges())
            {
                return Ok("professor deletado");
            }
            return BadRequest("professor não deletado");
        }  
          
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly SmartContext _context;

        public readonly IRepository _repository;

        public AlunoController(SmartContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }
               
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O aluno não foi encontrado.");
            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome));
            if (aluno == null) return BadRequest("O aluno não foi encontrado.");
            return Ok(aluno);
        }

        [HttpPost]  
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado.");            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {   
            var alunoDB = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alunoDB == null) return BadRequest("O aluno não foi encontrado.");

             _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não Atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoDB = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alunoDB == null) return BadRequest("O aluno não foi encontrado.");    

             _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não Atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {   
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O aluno não foi encontrado.");

             _repository.Delete(aluno);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno Deletado.");
            }
            return BadRequest("Aluno não Deletado.");
        }
       
        
    }
}
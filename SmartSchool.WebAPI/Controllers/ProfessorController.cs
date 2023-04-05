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
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public IRepository _repository;

        public ProfessorController(SmartContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }       

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null) return BadRequest("O professor não foi encontrado.");
            return Ok(professor);
        }
        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));
            if (professor == null) return BadRequest("O professor não foi encontrado.");
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
           _repository.Add(professor);

            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var professorDB = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (professorDB == null) return BadRequest("O professor não foi encontrado.");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var professorDB = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (professorDB == null) return BadRequest("O professor não foi encontrado.");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null) return BadRequest("O professor não foi encontrado.");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        
        
    }
}
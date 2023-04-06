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
        public IRepository _repository;

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

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
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
            var professorDB = _repository.GetProfessorById(id);
            if (professorDB == null) return BadRequest("O professor não foi encontrado.");
            _repository.Update(professor);
            _repository.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var professorDB = _repository.GetProfessorById(id);
            if (professorDB == null) return BadRequest("O professor não foi encontrado.");
            _repository.Update(professor);
            _repository.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("O professor não foi encontrado.");

            _repository.Delete(professor);
            _repository.SaveChanges();
            return Ok(professor);
        }

        
        
    }
}
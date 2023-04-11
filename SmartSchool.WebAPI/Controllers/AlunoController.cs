using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.DTos;
using AutoMapper;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repository;
        public readonly IMapper _mapper;
        public AlunoController(IRepository repository, IMapper mapper)
        {           
            _repository = repository;
            _mapper = mapper;
        }
               
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));            
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
                    
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado.");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
            //return Ok(aluno);
        }       

        [HttpPost]  
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado.");            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {   
            var alunoDB = _repository.GetAlunoById(id);
            if (alunoDB == null) return BadRequest("O aluno não foi encontrado.");

            _mapper.Map(model, alunoDB);

             _repository.Update(alunoDB);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alunoDB));
            }
            return BadRequest("Aluno não Atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var alunoDB = _repository.GetAlunoById(id);
            if (alunoDB == null) return BadRequest("O aluno não foi encontrado.");

            _mapper.Map(model, alunoDB);    

             _repository.Update(alunoDB);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alunoDB));
            }
            return BadRequest("Aluno não Atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {   
            var aluno = _repository.GetAlunoById(id,false);
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
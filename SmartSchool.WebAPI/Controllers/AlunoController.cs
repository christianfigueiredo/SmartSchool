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
    /// <summary>
    /// Versão 1.0
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {       
        public readonly IRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Método Construtor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>        
        public AlunoController(IRepository repository, IMapper mapper)
        {           
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsável por retornar todos os alunos
        /// </summary>               
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));            
        }

        /// <summary>
        /// Método responsável por retornar todos os alunos por Id
        /// </summary>

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
                    
        }

        /// <summary>
        /// Método responsável por retornar todos os alunos por Id
        /// </summary>

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado.");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
            //return Ok(aluno);
        }    

        /// <summary>
        /// Método responsável por retornar todos os alunos por nome
        /// </summary>
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

        /// <summary>
        /// Método responsável por atualizar todos os alunos por Id
        /// </summary>
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

        /// <summary>
        /// Método responsável por atualizar parcialmente todos os alunos por Id
        /// </summary>
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

        /// <summary>
        /// Método responsável por deletar todos os alunos por Id
        /// </summary>
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
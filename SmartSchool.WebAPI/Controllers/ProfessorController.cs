using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SmartSchool.WebAPI.DTos;

namespace SmartSchool.WebAPI.Controllers
{
    /// <summary>
    /// Versão 1.0
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Método Construtor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>   

        public ProfessorController(IRepository repository, IMapper mapper)
        {            
            _repository = repository;
            _mapper = mapper;
        }       

        /// <summary>
        /// Método responsável por retornar todos os professores
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        /// <summary>
        /// Método responsável por retornar todos os professores por Id
        /// </summary>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O professor não foi encontrado.");

            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

        /// <summary>
        /// Método responsável por retornar todos os professores por nome
        /// </summary>        
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

           _repository.Add(professor);

            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado.");
        }

        /// <summary>
        /// Método responsável por atualizar todos os professores por Id
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor model)
        {
            var professorDB = _repository.GetProfessorById(id);
            if (professorDB == null) return BadRequest("O professor não foi encontrado.");

            _mapper.Map(model, professorDB);


            _repository.Update(professorDB);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professorDB));
            }
            return BadRequest("Professor não atualizado.");            
        }

        /// <summary>
        /// Método responsável por atualizar todos os professores por Id
        /// </summary>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor model)
        {
            var professorDB = _repository.GetProfessorById(id, false);
            if (professorDB == null) return BadRequest("O professor não foi encontrado.");
            _mapper.Map(model, professorDB); 

            _repository.Update(professorDB);

            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professorDB));
            }
            return BadRequest("Professor não atualizado.");
        }

        /// <summary>
        /// Método responsável por deletar todos os professores por Id
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O professor não foi encontrado.");

            _repository.Delete(professor);
            _repository.SaveChanges();
            return Ok(professor);
        }

        
        
    }
}
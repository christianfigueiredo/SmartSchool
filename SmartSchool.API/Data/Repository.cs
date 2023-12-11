using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly Contexto _contexto;

        public Repository(Contexto contexto)
        {
            _contexto = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);

        }

        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
            
        }

        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
            
        }       

        public bool SaveChanges()
        {
            return(_contexto.SaveChanges() > 0);
            
        }

        public Aluno[] GetAllAlunos( bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _contexto.Alunos;

            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _contexto.Alunos;

            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
            .OrderBy(a => a.Id)
            .Where(a => a.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
              IQueryable<Aluno> query = _contexto.Alunos;

            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
            .OrderBy(a => a.Id)
            .Where(a => a.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAluno = false)
        {
            IQueryable<Professor> query = _contexto.Professores;

            if(includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _contexto.Professores;

            if(includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);                             
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(a => a.Disciplinas.Any(ad => ad.AlunosDisciplinas.Any(
                        d => d.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _contexto.Professores;

            if(includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);                             
            }

            query = query.AsNoTracking()
            .OrderBy(a => a.Id)
            .Where(p => p.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}
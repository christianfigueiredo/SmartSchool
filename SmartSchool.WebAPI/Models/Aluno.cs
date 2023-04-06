using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Models
{
    public class Aluno
    {
        public Aluno(){}
        
        public Aluno(int id, int matricula,string nome,string sobrenome, string telefone,DateTime datanasc)
        {
            this.Id = id;
            this.Matricula= matricula;
            this.Nome = nome;
            this.DataNasc = datanasc;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;   
        }
        public int Id { get; set; }
        public int Matricula { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public IEnumerable<AlunoDisciplina> AlunoDisciplinas { get; set; }
        
    }
}
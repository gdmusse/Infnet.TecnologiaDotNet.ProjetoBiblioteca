using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model
{
    public class Emprestimo
    {
        public Usuario Usuario { get; private set; }
        public Livro Livro { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataPrevista { get; private set; }
        public DateTime? DataDevolucao { get; private set; }

        public Emprestimo(Usuario usuario, Livro livro, DateTime dataInicio, DateTime dataPrevista)
        {
            Usuario = usuario;
            Livro = livro;
            DataInicio = dataInicio;
            DataPrevista = dataPrevista;
            DataDevolucao = null;
        }

        public void FinalizarEmprestimo(DateTime dataDevolucao)
        {
            DataDevolucao = dataDevolucao;
        }
    }
}

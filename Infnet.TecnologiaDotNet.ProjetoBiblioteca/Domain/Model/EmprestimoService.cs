using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model
{
    public class EmprestimoService
    {
        public Emprestimo CriarEmprestimo(Usuario usuario, Livro livro, DateTime dataPrevista, int emprestimosAtivos)
        {
            if (emprestimosAtivos >= usuario.ObterLimiteEmprestimos())
                throw new Exception("Limite de emprestimo de usuário atingido");

            livro.Emprestar();

            return new Emprestimo(usuario, livro, DateTime.Now, dataPrevista);
        }
    }
}

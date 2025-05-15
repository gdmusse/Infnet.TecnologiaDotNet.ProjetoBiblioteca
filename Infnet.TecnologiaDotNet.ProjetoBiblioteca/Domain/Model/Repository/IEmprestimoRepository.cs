using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Repository
{
    public interface IEmprestimoRepository
    {
        void Adicionar(Emprestimo emprestimo);
        void Atualizar(Emprestimo emprestimo);
        void Remover(Emprestimo emprestimo);

        int ObterQuantidadeEmprestimosAtivosPorUsuario(Usuario usuario);
    }
}

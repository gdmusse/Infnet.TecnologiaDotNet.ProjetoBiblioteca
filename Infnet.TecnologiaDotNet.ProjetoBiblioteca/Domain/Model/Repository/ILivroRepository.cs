using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Repository
{
    public interface ILivroRepository
    {
        void Adicionar(Livro livro);
        void Atualizar(Livro livro);
        void Remover(Livro livro);
        Livro ObterPorNome(string nome);
    }
}

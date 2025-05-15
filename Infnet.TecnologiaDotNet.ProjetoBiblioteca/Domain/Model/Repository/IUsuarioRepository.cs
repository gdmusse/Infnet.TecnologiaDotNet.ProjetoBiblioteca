using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Repository
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Remover(Usuario usuario);
        Usuario ObterPorIdentificador(string identificador);
    }
}

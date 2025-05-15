using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Factory
{
    public static class UsuarioFactory
    {
        public static Usuario Usuario(string nome, string identificacao, List<Telefone> telefones)
        {
            return new Usuario(nome, identificacao, telefones); 
        }

        public static Usuario Usuario(string nome, string identificacao, List<Telefone> telefones, string matricula)
        {
            return new Professor(nome, identificacao, telefones, matricula);
        }
    }
}

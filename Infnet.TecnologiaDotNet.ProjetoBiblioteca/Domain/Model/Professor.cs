using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model
{
    public class Professor : Usuario
    {
        public string Matricula { get; private set; }

        // LSP (Liskov Substitution Principle): Professor é uma especialização de Usuario e pode ser usado onde Usuario é esperado, sem alterar o comportamento esperado.
        public Professor(string nome, string identificacao, List<Telefone> telefones, string matricula)
            : base(nome, identificacao, telefones)
        {
            Matricula = matricula;
        }

        public override int ObterLimiteEmprestimos() => 5;
    }
}

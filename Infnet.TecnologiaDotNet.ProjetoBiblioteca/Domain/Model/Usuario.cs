using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model
{
    public class Usuario
    {
        // Encapsulamento: propriedades com acesso controlado apenas por leitura pública.
        public string Nome { get; private set; }
        public string Identificacao { get; private set; }
        public List<Telefone> Telefones { get; private set; }

        //SRP - Single Responsibility Principle - a classe tem apenas uma responsabilidade: representar um usuário com seus dados básicos.
        public Usuario(string nome, string identificacao, List<Telefone> telefones)
        {
            Nome = nome;
            Identificacao = identificacao;
            Telefones = telefones;
        }

        // Método virtual para permitir polimorfismo e Open/Closed Principle
        public virtual int ObterLimiteEmprestimos() => 1;

        public void AdicionarTelefone(string numero)
        {
            Telefones.Add(new Telefone(numero));
        }
    }
}

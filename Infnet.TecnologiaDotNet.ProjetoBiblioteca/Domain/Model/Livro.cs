using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model
{
    public class Livro
    {
        // SRP (Single Responsibility Principle): Esta classe tem a responsabilidade única de representar um livro e seu estado.
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string Editora { get; private set; }
        public StatusLivro Status { get; private set; }

        public Livro(string titulo, string autor, string editora)
        {
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            Status = StatusLivro.Disponivel;
        }

        // High Cohesion: O método está diretamente relacionado à função da classe, que é controlar o status do livro.

        public void Emprestar()
        {
            if (Status != StatusLivro.Disponivel)
                throw new InvalidOperationException("Livro não está disponível para empréstimo.");

            Status = StatusLivro.Emprestado;
        }

        public void Devolver()
        {
            Status = StatusLivro.Disponivel;
        }
    }
}

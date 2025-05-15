using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model
{
    public class Telefone
    {
        public string Numero { get; private set; }

        public Telefone(string numero)
        {
            Numero = numero;
        }
    }
}

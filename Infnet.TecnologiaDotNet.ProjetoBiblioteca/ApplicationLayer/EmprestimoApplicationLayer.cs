using Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model;
using Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.ApplicationLayer
{
    public class EmprestimoApplicationLayer
    {
        private readonly EmprestimoService _emprestimoService;
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EmprestimoApplicationLayer(
            EmprestimoService emprestimoService,
            IEmprestimoRepository emprestimoRepository,
            ILivroRepository livroRepository,
            IUsuarioRepository usuarioRepository)
        {
            _emprestimoService = emprestimoService;
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
            _usuarioRepository = usuarioRepository; 
        }

        public void EmprestarLivro(string identificadorUsuario, string nomeLivro, DateTime dataPrevista)
        {
            try
            {
                Livro livro = _livroRepository.ObterPorNome(nomeLivro);

                if (livro == null)
                    throw new Exception("Livro não encontrado");

                if (livro.Status != StatusLivro.Disponivel)
                    throw new Exception("Livro indisponível");

                Usuario usuario = _usuarioRepository.ObterPorIdentificador(identificadorUsuario);

                if (usuario == null)
                    throw new Exception("Usuario não encontrado");

                var emprestimosAtivos = _emprestimoRepository.ObterQuantidadeEmprestimosAtivosPorUsuario(usuario);

                var emprestimo = _emprestimoService.CriarEmprestimo(usuario, livro, dataPrevista, emprestimosAtivos);

                _emprestimoRepository.Adicionar(emprestimo);
                _livroRepository.Atualizar(livro);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao emprestar livro", ex);
            }
        }
    }
}


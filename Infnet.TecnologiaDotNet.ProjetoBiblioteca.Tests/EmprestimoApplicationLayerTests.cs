using Infnet.TecnologiaDotNet.ProjetoBiblioteca.ApplicationLayer;
using Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Repository;
using Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model;
using NSubstitute;
using Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model.Factory;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Tests
{
    public class EmprestimoApplicationLayerTests
    {
        private readonly IEmprestimoRepository _emprestimoRepository = Substitute.For<IEmprestimoRepository>();
        private readonly ILivroRepository _livroRepository = Substitute.For<ILivroRepository>();
        private readonly IUsuarioRepository _usuarioRepository = Substitute.For<IUsuarioRepository>();
        private readonly EmprestimoService _emprestimoService;
        private readonly EmprestimoApplicationLayer _application;

        public EmprestimoApplicationLayerTests()
        {
            _emprestimoService = new EmprestimoService();
            _application = new EmprestimoApplicationLayer(
                _emprestimoService,
                _emprestimoRepository,
                _livroRepository,
                _usuarioRepository
            );
        }

        [Fact]
        public void EmprestarLivro_DeveCriarEmprestimo_QuandoDadosForemValidos()
        {
            // Arrange
            var livro = new Livro("Dom Casmurro", "Machado de Assis", "Editora A");
            var usuario = UsuarioFactory.Usuario("João", "123", new List<Telefone>());
            var dataPrevista = DateTime.Today.AddDays(7);

            _livroRepository.ObterPorNome("Dom Casmurro").Returns(livro);
            _usuarioRepository.ObterPorIdentificador("123").Returns(usuario);
            _emprestimoRepository.ObterQuantidadeEmprestimosAtivosPorUsuario(usuario).Returns(0);

            // Act
            _application.EmprestarLivro("123", "Dom Casmurro", dataPrevista);

            // Assert
            _emprestimoRepository.Received(1).Adicionar(Arg.Any<Emprestimo>());
            _livroRepository.Received(1).Atualizar(livro);
        }

        [Fact]
        public void EmprestarLivro_DeveCriarEmprestimoProfessor_QuandoDadosForemValidos()
        {
            // Arrange
            var livro = new Livro("Dom Casmurro", "Machado de Assis", "Editora A");
            var telefone = new Telefone("99999-9999");
            var listaTelefones = new List<Telefone>{ telefone };
            var usuario = UsuarioFactory.Usuario("João", "123", listaTelefones, "1");
            var dataPrevista = DateTime.Today.AddDays(7);

            _livroRepository.ObterPorNome("Dom Casmurro").Returns(livro);
            _usuarioRepository.ObterPorIdentificador("123").Returns(usuario);
            _emprestimoRepository.ObterQuantidadeEmprestimosAtivosPorUsuario(usuario).Returns(0);

            // Act
            _application.EmprestarLivro("123", "Dom Casmurro", dataPrevista);

            // Assert
            _emprestimoRepository.Received(1).Adicionar(Arg.Any<Emprestimo>());
            _livroRepository.Received(1).Atualizar(livro);
        }

        [Fact]
        public void EmprestarLivro_DeveLancarExcecao_SeLivroIndisponivel()
        {
            // Arrange
            var livro = new Livro("A Revolução dos Bichos", "George Orwell", "Companhia");
            livro.Emprestar();

            _livroRepository.ObterPorNome("A Revolução dos Bichos").Returns(livro);

            // Act + Assert
            var exception = Record.Exception(() =>
                  _application.EmprestarLivro("123", "A Revolução dos Bichos", DateTime.Now.AddDays(7)));

            Assert.NotNull(exception);
            Assert.Contains("Livro indisponível", exception.InnerException?.Message ?? exception.Message);
        }

        [Fact]
        public void EmprestarLivro_DeveLancarExcecao_SeUsuarioAtingiuLimite()
        {
            // Arrange
            var livro = new Livro("Livro C", "Autor", "Editora C");
            var usuario = UsuarioFactory.Usuario("Maria", "456", new List<Telefone>());
            var dataPrevista = DateTime.Today.AddDays(5);

            _livroRepository.ObterPorNome("Livro C").Returns(livro);
            _usuarioRepository.ObterPorIdentificador("456").Returns(usuario);
            _emprestimoRepository.ObterQuantidadeEmprestimosAtivosPorUsuario(usuario).Returns(usuario.ObterLimiteEmprestimos());

            // Act
            var exception = Record.Exception(() => _application.EmprestarLivro("456", "Livro C", dataPrevista));

            // Assert
            Assert.NotNull(exception);
            Assert.Contains("Limite de emprestimo de usuário atingido", exception.InnerException?.Message ?? exception.Message);
        }

        [Fact]
        public void EmprestarLivro_DeveLancarExcecao_SeLivroNaoForEncontrado()
        {
            // Arrange
            _livroRepository.ObterPorNome("Inexistente").Returns((Livro)null);

            // Act
            var exception = Record.Exception(() =>
                _application.EmprestarLivro("123", "Inexistente", DateTime.Today.AddDays(3))
            );

            // Assert
            Assert.NotNull(exception);
            Assert.Contains("Livro não encontrado", exception.InnerException?.Message ?? exception.Message);
        }

        [Fact]
        public void EmprestarLivro_DeveLancarExcecao_SeUsuarioNaoForEncontrado()
        {
            // Arrange
            var livro = new Livro("Livro Z", "Autor", "Editora Z");
            _livroRepository.ObterPorNome("Livro Z").Returns(livro);
            _usuarioRepository.ObterPorIdentificador("999").Returns((Usuario)null);

            // Act
            var exception = Record.Exception(() =>
                _application.EmprestarLivro("999", "Livro Z", DateTime.Today.AddDays(3))
            );

            // Assert
            Assert.NotNull(exception);
            Assert.Contains("Usuario não encontrado", exception.InnerException?.Message ?? exception.Message);
        }
    }
}

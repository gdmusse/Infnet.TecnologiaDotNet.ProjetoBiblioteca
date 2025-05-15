using Infnet.TecnologiaDotNet.ProjetoBiblioteca.Domain.Model;

namespace Infnet.TecnologiaDotNet.ProjetoBiblioteca.Tests
{
    public class EmprestimoServiceTests
    {
        [Fact]
        public void CriarEmprestimo_DeveCriarEmprestimo_SeUsuarioEstiverDentroDoLimite()
        {
            // Arrange
            var usuario = new Usuario("João", "123", new List<Telefone>());
            var livro = new Livro("Dom Casmurro", "Machado de Assis", "Editora A");
            var dataPrevista = DateTime.Today.AddDays(7);
            int emprestimosAtivos = 0;

            var service = new EmprestimoService();

            // Act
            var emprestimo = service.CriarEmprestimo(usuario, livro, dataPrevista, emprestimosAtivos);

            // Assert
            Assert.NotNull(emprestimo);
            Assert.Equal(usuario, emprestimo.Usuario);
            Assert.Equal(livro, emprestimo.Livro);
            Assert.Equal(dataPrevista, emprestimo.DataPrevista);
            Assert.Equal(StatusLivro.Emprestado, livro.Status);
        }

        [Fact]
        public void CriarEmprestimo_UsuarioComum_DeveLancarExcecao_SeLimiteExcedido()
        {
            // Arrange
            var usuario = new Usuario("Maria", "456", new List<Telefone>());
            var livro = new Livro("A Revolução dos Bichos", "George Orwell", "Companhia");
            var dataPrevista = DateTime.Today.AddDays(7);
            int emprestimosAtivos = 1; 

            var service = new EmprestimoService();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                service.CriarEmprestimo(usuario, livro, dataPrevista, emprestimosAtivos));

            Assert.Equal("Limite de emprestimo de usuário atingido", ex.Message);
        }

        [Fact]
        public void CriarEmprestimo_Professor_DeveLancarExcecao_SeLimiteExcedido()
        {
            // Arrange
            var usuario = new Usuario("Maria", "456", new List<Telefone>());
            var livro = new Livro("A Revolução dos Bichos", "George Orwell", "Companhia");
            var dataPrevista = DateTime.Today.AddDays(7);
            int emprestimosAtivos = 5;

            var service = new EmprestimoService();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                service.CriarEmprestimo(usuario, livro, dataPrevista, emprestimosAtivos));

            Assert.Equal("Limite de emprestimo de usuário atingido", ex.Message);
        }
    }
}
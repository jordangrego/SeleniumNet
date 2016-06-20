using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumLib.Pages.Tests
{
    [TestClass()]
    public class ProtocoloPagesTests
    {
        private string usuario = "admin";
        private string senha = "123456";

        [TestMethod()]
        public void SelecionandoCombosTest()
        {
            new ProtocoloPages(usuario, senha).SelecionandoCombos();
        }

        [TestMethod()]
        public void VerificarCadastramentoTest()
        {
            new ProtocoloPages(usuario, senha).VerificarCadastramento();
        }

        [TestMethod()]
        public void GravarListaTest()
        {
            new ProtocoloPages(usuario, senha).GravarLista();
        }

        [TestMethod()]
        public void SelecionarItemTabelaTest()
        {
            new ProtocoloPages(usuario, senha).SelecionarItemTabela();
        }
    }
}
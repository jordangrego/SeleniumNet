using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumLib.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace SeleniumLib.Pages
{
    public class ProtocoloPages : BasePage
    {
        protected override string radicalUrl
        {
            get
            {
                return "http://localhost/Protocolo/";
            }
        }

        public string Usuario { get; set; }
        public string Senha { get; set; }

        #region controles da tela de Login
        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_lgnAutenticaUsuario_UserName")]
        public IWebElement txtUsuario { get; set; }

        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_lgnAutenticaUsuario_Password")]
        public IWebElement txtSenha { get; set; }

        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_lgnAutenticaUsuario_Login")]
        public IWebElement btnEntrar { get; set; }
        #endregion

        #region paginas

        #endregion

        public ProtocoloPages(string usuario, string senha) : base()
        {
            this.Usuario = usuario;
            this.Senha = senha;
        }

        public void Logar()
        {
            this.NavegarPagina("Login.aspx");
            txtUsuario.SendKeys(Usuario);
            txtSenha.SendKeys(Senha);
            btnEntrar.Click();

            // Waits for browser result.
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Verifica se deterninada elemento existe, no caso para identificar que foi gravado com sucesso.
        /// </summary>
        public void VerificarCadastramento()
        {
            this.Logar();
            this.NavegarPagina("Administracao/TipoItemCadastro.aspx");

            IWebElement btnCadastrar = Driver.FindElement(By.Id("ContentPlaceHolder1_btnCadastrar"));
            btnCadastrar.Click();
            Thread.Sleep(3000);

            IWebElement txtDescricao = Driver.FindElement(By.Id("ContentPlaceHolder1_txtDescricaoTipoItem"));
            txtDescricao.SendKeys("Tipo Item - " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second);

            IWebElement chkTipoComposto = Driver.FindElement(By.Id("ContentPlaceHolder1_chkItemComposto"));
            chkTipoComposto.Click();

            Thread.Sleep(10000);

            IWebElement btnSalvar = Driver.FindElement(By.Id("ContentPlaceHolder1_btnSalvar"));
            btnSalvar.Click();

            Thread.Sleep(5000);

            try
            {
                // verifica modal de sucesso
                IWebElement modalRetorno = Driver.FindElement(By.ClassName("alert-success"));

                if (modalRetorno != null)
                {
                    Debug.WriteLine("Gravou");
                }
                else
                {
                    Debug.WriteLine("Erro");
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Erro");
            }
        }

        /// <summary>
        /// Gravar Lista.
        /// </summary>
        public void GravarLista()
        {
            this.Logar();
            List<string> lista = UtilIO.RecuperaLinhasArquivoTexto("C:\\geraimagem\\dados.txt");
            foreach(string registro in lista)
            {
                this.CadastrarClassificacao(registro);
            }
        }

        /// <summary>
        /// Cadastrar Classificacao.
        /// </summary>
        /// <param name="dado"></param>
        public void CadastrarClassificacao(string dado)
        {
            this.NavegarPagina("Administracao/ClassificacaoCadastro.aspx");

            IWebElement btnCadastrarClassificacao = Driver.FindElement(By.Id("ContentPlaceHolder1_btnCadastrar"));
            btnCadastrarClassificacao.Click();
            Thread.Sleep(1000);

            IWebElement txtDescricaoClassificacao = Driver.FindElement(By.Id("ContentPlaceHolder1_txtDescricaoClassificacao"));
            txtDescricaoClassificacao.SendKeys(dado);

            IWebElement btnSalvarClassificacao = Driver.FindElement(By.Id("ContentPlaceHolder1_btnSalvar"));
            btnSalvarClassificacao.Click();

            Thread.Sleep(3000);
        }

        /// <summary>
        /// Selecionando Combos
        /// </summary>
        public void SelecionandoCombos()
        {
            this.Logar();
            this.NavegarPagina("Itens/ItensPesquisa.aspx");
            IWebElement dropdownMenu1 = Driver.FindElement(By.Id("dropdownMenu1"));
            dropdownMenu1.Click();
            Thread.Sleep(1000);

            IWebElement cadastrarItemSimples = Driver.FindElement(By.Id("ContentPlaceHolder1_btnCadastrar"));
            cadastrarItemSimples.Click();
            Thread.Sleep(1000);

            IWebElement ddlProprietario = Driver.FindElement(By.Id("ContentPlaceHolder1_wucItemCadastro_ddlEntidade"));
            SelectElement selectDdlProprietario = new SelectElement(ddlProprietario);
            selectDdlProprietario.SelectByText("Anvisa");
            Thread.Sleep(1000);

            IWebElement ddlArea = Driver.FindElement(By.Id("ContentPlaceHolder1_wucItemCadastro_ddlArea"));
            SelectElement selectDdlArea = new SelectElement(ddlArea);
            selectDdlArea.SelectByText("GDOC");
            Thread.Sleep(1000);

            IWebElement btnSalvar = Driver.FindElement(By.Id("ContentPlaceHolder1_wucItemCadastro_btnSalvar"));
            btnSalvar.Click();

            Thread.Sleep(1000);
        }

        /// <summary>
        /// Lê dados tag <table>
        /// </summary>
        public void SelecionarItemTabela()
        {
            this.Logar();
            this.NavegarPagina("Itens/ItensPesquisa.aspx");
            IWebElement btnPesquisar = Driver.FindElement(By.Id("ContentPlaceHolder1_btnPesquisar"));
            btnPesquisar.Click();
            Thread.Sleep(3000);

            /* todas as linhas
            IWebElement grvItens = Driver.FindElement(By.Id("ContentPlaceHolder1_grvItens"));
            ReadOnlyCollection<IWebElement> registros = grvItens.FindElements(By.TagName("tr"));

            foreach(IWebElement registro in registros)
            {
                ReadOnlyCollection<IWebElement> celulas =  registro.FindElements(By.TagName("td"));

                foreach(IWebElement celula in celulas)
                {
                    Debug.WriteLine("Valor Celula: " + celula.Text);
                }
            }
            */

            IWebElement grvItens = Driver.FindElement(By.Id("ContentPlaceHolder1_grvItens"));
            ReadOnlyCollection<IWebElement> registros = grvItens.FindElements(By.TagName("tr"));
            IWebElement registro = registros[1]; //primeira linha eh TH e não TR
            ReadOnlyCollection<IWebElement> celulas = registro.FindElements(By.TagName("td"));
            Debug.WriteLine("Identificador: " + celulas[0].Text + " | Tipo: " + celulas[1].Text);
        }
    }
}

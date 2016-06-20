using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace SeleniumLib.Pages
{
    public abstract class BasePage
    {
        protected abstract string radicalUrl { get; }

        public IWebDriver Driver { get; private set; }

        /// <summary>
        /// Base URL used for the UI tests.
        /// </summary>
        protected string baseURL { get; set; }

        /// <summary>
        /// Default constructor.
        /// Initializes page objectos within DOM.
        /// </summary>
        public BasePage()
        {
            if (this.Driver == null)
            {
                this.Driver = new FirefoxDriver();
            }

            this.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            this.Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
            this.Driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));

            Driver.Manage().Window.Maximize();


            PageFactory.InitElements(this.Driver, this);

        }

        /// <summary>
        /// Runs a JS script on the current page.
        /// </summary>
        /// <param name="jsScript">The JS string that holds the script to be executed within the browser.</param>
        protected internal void ExecuteScript(string jsScript)
        {
            ((IJavaScriptExecutor)this.Driver).ExecuteScript(jsScript);
        }


        /// <summary>
        /// Verifica se tem alert aberto.
        /// </summary>
        /// <returns></returns>
        protected bool IsMostrandoAlert()
        {
            try
            {
                Driver.SwitchTo().Alert();
                return true;
            }   // try 
            catch (NoAlertPresentException Ex)
            {
                return false;
            }   // catch 
        }

        /// <summary>
        /// Recupera Texto do Alert
        /// </summary>
        /// <returns></returns>
        protected string RecuperaTextoAlert()
        {
            IAlert alert = Driver.SwitchTo().Alert();
            return alert.Text;
        }

        /// <summary>
        /// Negave para a página.
        /// </summary>
        /// <param name="urlPagina"></param>
        protected void NavegarPagina(string urlPagina)
        {
            this.baseURL = this.radicalUrl + urlPagina;
            Driver.Navigate().GoToUrl(baseURL);
        }
    }
}

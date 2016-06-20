using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SeleniumLib.Util
{
    public static class UtilIO
    {
        /// <summary>
        /// Grava Arquivo no File System.
        /// </summary>
        /// <param name="conteudo">Arquivo que será gravado.</param>
        /// <param name="pathArquivo">Caminho onde será gravado na HD.</param>
        public static void GravaArquivoFileSystem(byte[] conteudo, string pathArquivo)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(pathArquivo, FileMode.Create, FileAccess.Write);
            fileStream.Write(conteudo, 0, conteudo.Length);
            fileStream.Close();
        }

        /// <summary>
        /// Grava Arquivo Texto.
        /// </summary>
        /// <param name="pathArquivo"></param>
        /// <param name="conteudoArquivo"></param>
        /// <param name="caminhoArquivo"></param>
        public static void GravaArquivoTexto(string pathArquivo, string conteudoArquivo)
        {
            StreamWriter file = new System.IO.StreamWriter(pathArquivo);
            file.WriteLine(conteudoArquivo);
            file.Close();
        }

        /// <summary>
        /// Informa se a Pasta Existe.
        /// </summary>
        /// <param name="folder">Path da Pasta.</param>
        public static bool IsPastaExistente(string folder)
        {
            return Directory.Exists(folder);
        }

        /// <summary>
        /// Cria pasta.
        /// </summary>
        /// <param name="folder">Path da Pasta.</param>
        public static void CriarPasta(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        /// <summary>
        /// Cria pasta.
        /// </summary>
        /// <param name="folder">Path da Pasta.</param>
        public static List<string> PesquisaDiretorios(string folder, string criterioPesquisa)
        {
            List<string> arquivosPasta = null;
            if (Directory.Exists(folder))
            {
                arquivosPasta = Directory.GetDirectories(folder, criterioPesquisa + "*").ToList<string>();
            }

            return arquivosPasta;
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="folder">Caminho da Pasta.</param>
        public static void DeletaPasta(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static string RecuperaCaminhoDiretorioDoArquivo(string caminhoArquivo)
        {
            return Path.GetDirectoryName(caminhoArquivo);
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="folder">Caminho da Pasta.</param>
        public static List<string> RecuperaConteudoPasta(string folder)
        {
            List<string> arquivosPasta = null;
            if (Directory.Exists(folder))
            {
                arquivosPasta = Directory.GetFiles(folder).ToList<string>();
            }

            return arquivosPasta;
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="folder">Caminho da Pasta.</param>
        public static List<string> PesquisaArquivosSubdiretorios(string folder, string criterio)
        {
            List<string> arquivosPasta = null;
            if (Directory.Exists(folder))
            {
                try
                {
                    arquivosPasta = Directory.GetFiles(folder, criterio, System.IO.SearchOption.AllDirectories).ToList<string>();
                }
                catch (UnauthorizedAccessException ex)
                {
                }
            }

            return arquivosPasta;
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="folder">Caminho da Pasta.</param>
        public static List<string> RecuperaFolders(string folder)
        {
            List<string> folders = null;
            if (Directory.Exists(folder))
            {
                folders = Directory.GetDirectories(folder).ToList<string>();
            }

            return folders;
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do Arquivo.</param>
        public static string RecuperaNomeArquivo(string caminhoArquivo)
        {
            string nomeArquivo = null;

            if (File.Exists(caminhoArquivo))
            {
                nomeArquivo = Path.GetFileName(caminhoArquivo);
            }

            return nomeArquivo;
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do Arquivo.</param>
        public static DateTime RecuperaDataCriacaoArquivo(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                return File.GetCreationTime(caminhoArquivo);
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Deleta todo o conteúdo de uma pasta.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do Arquivo.</param>
        public static DateTime RecuperaDataUltimaAlteracaoArquivo(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                return File.GetLastWriteTime(caminhoArquivo);
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Deleta Todo Conteudo da Pasta.
        /// </summary>
        /// <param name="pathPasta"></param>
        public static void DeletarTodoConteudoPasta(string pathPasta)
        {
            DirectoryInfo di = new DirectoryInfo(pathPasta);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        /// <summary>
        /// Recupera Tamanho do Arquivo.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho do Arquivo.</param>
        /// <returns>Tamanho do Arquivo.</returns>
        public static long RecuperaTamanhoArquivo(string caminhoArquivo)
        {
            long tamanhoArquivo = 0L;
            if (File.Exists(caminhoArquivo))
            {
                tamanhoArquivo = new FileInfo(caminhoArquivo).Length;
            }

            return tamanhoArquivo;
        }

        /// <summary>
        /// Recupera a Extensão do Arquivo.
        /// </summary>
        /// <param name="nomeArquivo">Nome do Arquivo.</param>
        /// <returns>Extensão do Arquivo.</returns>
        public static string RecuperaExtensao(string nomeArquivo)
        {
            return Path.GetExtension(nomeArquivo).Replace(".", string.Empty);
        }

        /// <summary>
        /// Deleta Arquivo.
        /// </summary>
        /// <param name="caminhoPath"></param>
        public static void DeletaArquivo(string caminhoPath)
        {
            File.Delete(caminhoPath);
        }

        /// <summary>
        /// Recupera Nome Arquivo Sem Extensao.
        /// </summary>
        /// <param name="nomeArquivo">Nome do Arquivo.</param>
        /// <returns>Nome Arquivo Sem Extensao.</returns>
        public static string RecuperaNomeArquivoSemExtensao(string nomeArquivo)
        {
            return nomeArquivo.Substring(0, nomeArquivo.LastIndexOf("."));
        }

        /// <summary>
        /// Copia Arquivo.
        /// </summary>
        /// <param name="origem">Path de origem do Arquivo.</param>
        /// <param name="destino">Path de destino do Arquivo.</param>
        public static void CopiaArquivo(string origem, string destino)
        {
            File.Copy(origem, destino, true);
        }

        /// <summary>
        /// Recupera Linhas Arquivo Texto.
        /// </summary>
        /// <param name="pathArquivoTexto"></param>
        /// <returns></returns>
        public static List<string> RecuperaLinhasArquivoTexto(string pathArquivoTexto)
        {
            List<string> conteudo = new List<string>();
            if (File.Exists(pathArquivoTexto))
            {
                string[] conteudoArquivo = File.ReadAllLines(pathArquivoTexto);
                conteudo = conteudoArquivo.ToList<string>();
            }

            return conteudo;
        }

        /// <summary>
        /// Recupera Nome Diretorio.
        /// </summary>
        /// <param name="pathFolder"></param>
        /// <returns></returns>
        public static string RecuperaNomeDiretorio(string pathFolder)
        {
            string nomeFolder = null;

            if (Directory.Exists(pathFolder))
            {
                nomeFolder = new DirectoryInfo(Path.GetDirectoryName(pathFolder)).Name;
            }

            return nomeFolder;
        }
    }
}

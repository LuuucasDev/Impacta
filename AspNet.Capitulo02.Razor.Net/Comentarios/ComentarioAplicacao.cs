using AspNet.Capitulo02.Razor.Net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AspNet.Capitulo02.Razor.Net.Comentarios
{
    public class ComentarioAplicacao
    {
        private string caminho = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["caminhoArquivoComentario"];

        public void Inserir(string nome, string comentario)
        {
            File.AppendAllText(caminho, $"{DateTime.Now}|{nome}|{comentario}" + Environment.NewLine);
        }
        public List<ComentarioViewModel> Selecionar()
        {
            var comentarios = new List<ComentarioViewModel>();

            foreach (var linha in File.ReadAllLines(caminho))
            {
                if (linha.Trim() == String.Empty)
                {
                    continue;
                }

                var propriedades = linha.Split('|');

                ComentarioViewModel comentario = new ComentarioViewModel();
                comentario.Data = Convert.ToDateTime(propriedades[0]);
                comentario.Nome = propriedades[1];
                comentario.Comentario = propriedades[2];

                comentarios.Add(comentario);
            }
            return comentarios;
        }
    }
}
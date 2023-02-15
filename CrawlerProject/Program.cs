using Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;



namespace CrawlerProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var respostaService = new RespostaService(httpClient);
                    var resposta = await respostaService.ObterRespostaAsync();

                    Console.WriteLine("A Resposta é " + resposta);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }

          
        }
    }
}

using CrawlerProject.Services.Interface;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class RespostaService : IRespostaService
    {
        private readonly HttpClient _httpClient;

        public RespostaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ObterRespostaAsync()
        {
            var response = await  _httpClient.GetAsync("http://applicant-test.us-east-1.elasticbeanstalk.com/");
            var responseString = response.Content.ReadAsStringAsync().Result;

            var doc = new HtmlDocument();
            doc.LoadHtml(responseString);

            var button = doc.DocumentNode.SelectSingleNode("//input[@type='button' and @value='Descobrir resposta']");

            var buttonId = button.GetAttributeValue("id", "");

            if (string.IsNullOrEmpty(buttonId))
            {
                buttonId = button.GetAttributeValue("name", "");
            }

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("submit", "true"),
                new KeyValuePair<string, string>("entry.1725222409", "Teste")
            });

            var postResponse = await _httpClient.PostAsync($"http://applicant-test.us-east-1.elasticbeanstalk.com/?{buttonId}=Descobrir+resposta", content);
            var postResponseString = await postResponse.Content.ReadAsStringAsync();

            var resultDoc = new HtmlDocument();
            resultDoc.LoadHtml(postResponseString);

            var answer = resultDoc.DocumentNode.SelectSingleNode("//span[@id='answer']");

            if (answer == null)
            {
                return "Sem resposta";
            }
            
            return answer.InnerHtml;

        }
    }
}

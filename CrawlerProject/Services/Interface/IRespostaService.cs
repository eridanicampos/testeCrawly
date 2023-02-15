using System.Threading.Tasks;

namespace CrawlerProject.Services.Interface
{
    interface IRespostaService
    {
        Task<string> ObterRespostaAsync();
    }
}

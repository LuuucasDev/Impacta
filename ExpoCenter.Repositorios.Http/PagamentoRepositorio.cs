using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Dominio.Interfaces;
using System.Net.Http.Json;

namespace ExpoCenter.Repositorios.Http
{
    public class PagamentoRepositorio : IPagamentoRepositorio
    {
        private readonly HttpClient httpClient;
        private const string caminho = "pagamentos";

        public PagamentoRepositorio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Pagamento>> Get()
        {
            using var resposta = await httpClient.GetAsync($"{caminho}");
            return await resposta.Content.ReadFromJsonAsync<List<Pagamento>>();
        }

        public async Task<Pagamento> Get(int id)
        {
            using var resposta = await httpClient.GetAsync($"{caminho}/{id}");
            return await resposta.Content.ReadFromJsonAsync<Pagamento>();
        }

        public async Task<Pagamento> Post(Pagamento pagamento)
        {
            using var resposta = await httpClient.PostAsJsonAsync($"{caminho}", pagamento);
            return await resposta.Content.ReadFromJsonAsync<Pagamento>();
        }

        public async Task Put(Pagamento pagamento)
        {
            using var resposta = await httpClient.PutAsJsonAsync($"{caminho}/{pagamento.Id}", pagamento);
            resposta.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            using var resposta = await httpClient.DeleteAsync($"{caminho}/{id}");
            resposta.EnsureSuccessStatusCode();
        }
    }
}
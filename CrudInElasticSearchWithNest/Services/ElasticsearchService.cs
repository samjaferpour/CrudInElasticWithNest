using CrudInElasticSearchWithNest.Dtos.Settings;
using CrudInElasticSearchWithNest.Entities;
using CrudInElasticSearchWithNest.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nest;

namespace CrudInElasticSearchWithNest.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _client;
        private readonly SiteSettings _siteSettings;

        public ElasticsearchService(IOptions<SiteSettings> options)
        {
            this._siteSettings = options.Value;
            this._client = CreateInstance();
        }
        public async Task CheckIndex(string indexName)
        {
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (anyy.Exists)
                return;

            var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .ProductMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

            return;
        }

        public async Task DeleteDocument(string indexName, Product product)
        {
            var response = await _client.CreateAsync(product, q => q.Index(indexName));
            await _client.DeleteAsync(DocumentPath<Product>.Id(product.Id).Index(indexName));
        }

        public Task DeleteDocumentById(string indexName, int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteIndex(string indexName)
        {
            await _client.Indices.DeleteAsync(indexName);
        }

        public async Task<Product> GetDocumentById(string indexName, int id)
        {
            var response = await _client.GetAsync<Product>(id, q => q.Index(indexName));
            return response.Source;
        }

        public async Task<List<Product>> GetAllDocuments(string indexName)
        {
            var response = await _client.SearchAsync<Product>(s => s
                                  .Index(indexName)
                                   );
            return response.Documents.ToList();
        }

        public async Task InsertDocument(string indexName, Product product)
        {
            var response = await _client.CreateAsync(product, q => q.Index(indexName));
        }
        #region private methods
        private ElasticClient CreateInstance()
        {
            string host = _siteSettings.ElasticsearchServer.Host;
            string port = _siteSettings.ElasticsearchServer.Port;
            string username = _siteSettings.ElasticsearchServer.Username;
            string password = _siteSettings.ElasticsearchServer.Password;
            var settings = new ConnectionSettings(new Uri(host + ":" + port));
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                settings.BasicAuthentication(username, password);

            return new ElasticClient(settings);
        }
        #endregion
    }
}

using CrudInElasticSearchWithNest.Entities;

namespace CrudInElasticSearchWithNest.Services
{
    public interface IElasticsearchService
    {
        Task CheckIndex(string indexName);
        Task DeleteIndex(string indexName);
        //------------------------------------
        Task<Product> GetDocumentById(string indexName, int id);
        Task<List<Product>> GetAllDocuments(string indexName);
        Task InsertDocument(string indexName, Product product);
        Task DeleteDocument(string indexName, Product product);
        Task DeleteDocumentById(string indexName, int id);

    }
}

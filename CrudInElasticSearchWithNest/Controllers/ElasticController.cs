using CrudInElasticSearchWithNest.Entities;
using CrudInElasticSearchWithNest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudInElasticSearchWithNest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly IElasticsearchService _service;

        public ElasticController(IElasticsearchService service) 
        {
            this._service = service;
        }
        [HttpPost]
        public async Task<IActionResult> AddDocument(Product product, string indexName)
        {
            await _service.InsertDocument(indexName, product);
            return Ok();
        }
    }
}

using CrudInElasticSearchWithNest.Entities;
using Nest;

namespace CrudInElasticSearchWithNest.Helpers
{
    public static class Mapping
    {
        public  static CreateIndexDescriptor ProductMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<Product>(x => x.Properties(p => p
            .Keyword(x => x.Name(x => x.Id))
            .Text(x => x.Name(x => x.Name))
            .Text(x => x.Name(x => x.Brand))
            .Number(x => x.Name(x => x.Price))
            .Date(x => x.Name(x => x.CreatedDate)))
            );
        }
    }
}

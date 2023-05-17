namespace CrudInElasticSearchWithNest.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

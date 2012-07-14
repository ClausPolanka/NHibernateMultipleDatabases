using Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Master
{
    public class ProductsMap : ClassMap<Products>
    {
        public ProductsMap()
        {
            Id(x => x.ProductId);
            Map(x => x.Name);
        }
    }
}
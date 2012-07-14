using Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.MyGuitar
{
    public class GuitarMap : ClassMap<Guitar>
    {
        public GuitarMap()
        {
            Id(x => x.Id);
            Map(x => x.Type);
        }
    }
}
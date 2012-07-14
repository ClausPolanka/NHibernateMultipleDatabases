using Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Playground
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id);
        }
    }
}
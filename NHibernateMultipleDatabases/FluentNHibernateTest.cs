using System.Collections.Generic;
using DataAccess;
using Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NUnit.Framework;

namespace NHibernateMultipleDatabases
{
    [TestFixture]
    public class FluentNHibernateTest
    {
        [Test]
        public void GetSomeDataFromSqlServerMyGuitarDatabase()
        {
            ISessionFactory sf = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(@"Data Source=WIN7-VIAO-NB\SAGENIUZ;Initial Catalog=myGuitarStore;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<GuitarMap>())
                .BuildSessionFactory();

            using (ISession session = sf.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Guitar>();
                IList<Guitar> guitarTypes = criteria.List<Guitar>();

                Assert.That(guitarTypes.Count, Is.EqualTo(10), "number of guitar types");
            }
        }
        
        [Test]
        public void GetSomeDataFromSqlExpressPlaygroundDatabase()
        {
            ISessionFactory sf = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(@"Data Source=WIN7-VIAO-NB\SAGENIUZ;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Products>())
                .BuildSessionFactory();

            using (ISession session = sf.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Products>();
                IList<Products> products = criteria.List<Products>();

                Assert.That(products.Count, Is.EqualTo(0), "number of products");
            }
        }
    }
}
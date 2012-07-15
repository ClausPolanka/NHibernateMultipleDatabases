using System.Collections.Generic;
using DataAccess;
using DataAccess.Master;
using DataAccess.MyGuitar;
using DataAccess.Playground;
using Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
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
        public void GetSomeDataFromSqlServerMasterDatabase()
        {
            ISessionFactory sf = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(@"Data Source=WIN7-VIAO-NB\SAGENIUZ;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductsMap>())
                .BuildSessionFactory();

            using (ISession session = sf.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Products>();
                IList<Products> products = criteria.List<Products>();

                Assert.That(products.Count, Is.EqualTo(0), "number of products");
            }
        }
        
        [Test]
        public void CheckIfFluentNHibernateThrowsExceptionIfNoTablesExist()
        {
            Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(@"Data Source=WIN7-VIAO-NB\SAGENIUZ;Initial Catalog=NHibernate;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentMap>())
                .BuildSessionFactory();
        }

        [Test]
        public void GetSomeInsertedDataFromSqlServerPlaygroundDatabase()
        {
            ISessionFactory sf = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(@"Data Source=WIN7-VIAO-NB\SAGENIUZ;Initial Catalog=Playground;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();

            using (ISession session = sf.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.Save(new Student { Id = 1 });
                    session.Save(new Student { Id = 2 });
                    tx.Commit();
                }
            }

            using (ISession session = sf.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Student>();
                IList<Student> students = criteria.List<Student>();

                Assert.That(students.Count, Is.EqualTo(2), "number of students");
            }
        }

        [Test]
        public void GetSomeInsertedDataFromSqlServerPlaygroundDatabaseUsingCustomTransactionContext()
        {
            ISessionFactory sf = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(@"Data Source=WIN7-VIAO-NB\SAGENIUZ;Initial Catalog=Playground;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();

            TranactionContext.Execute(sf, session =>
            {
                session.Save(new Student { Id = 1 });
                session.Save(new Student { Id = 2 });
            });

            TranactionContext.Execute(sf, session =>
            {
                ICriteria criteria = session.CreateCriteria<Student>();
                IList<Student> students = criteria.List<Student>();

                Assert.That(students.Count, Is.EqualTo(2), "number of students");
            });
        }

        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(script: false, export: true);
        }
    }
}
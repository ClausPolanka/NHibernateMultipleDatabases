using System;
using NHibernate;

namespace DataAccess
{
    public class TranactionContext
    {
        public static void Execute(ISessionFactory sf, Action<ISession> block)
        {
            using (ISession s = sf.OpenSession())
            {
                using (ITransaction tx = s.BeginTransaction())
                {
                    try
                    {
                        block(s);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
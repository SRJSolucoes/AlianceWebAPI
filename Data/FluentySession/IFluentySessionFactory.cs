using NHibernate;
using NHibernate.Cfg;

namespace Data.FluentySession
{
    public interface IFluentySessionFactory
    {
        ISessionFactory CreateSessionFactory();

        void BuildSchema(Configuration config);
    }
}

using NHibernate;
using NHibernate.Context;
using System;
using System.Diagnostics;

namespace Data.FluentySession
{
    public class SessionFactory
    {
        public SessionFactory()
        {
        }

        public ISession GetCurrentSession()
        {
            ISession currentSession;
            var sessionFactory = SessionFact.GetSessionFact();
            currentSession = sessionFactory.OpenSession();

            //if (CurrentSessionContext.HasBind(sessionFactory))
            //{
            //    currentSession =sessionFactory.GetCurrentSession();
            //}
            //else
            //{
            //    currentSession =sessionFactory.OpenSession();
            //    CurrentSessionContext.Bind(currentSession);
            //}
            return currentSession;
        }
        public ISession GetCurrentSessionInput()
        {
            ISession currentSession;
            var sessionFactory = SessionFact.GetSessionFactInput();
            currentSession = sessionFactory.OpenSession();

            //if (CurrentSessionContext.HasBind(sessionFactory))
            //{
            //    currentSession =sessionFactory.GetCurrentSession();
            //}
            //else
            //{
            //    currentSession =sessionFactory.OpenSession();
            //    CurrentSessionContext.Bind(currentSession);
            //}
            return currentSession;
        }
        public ISession GetCurrentSessionOutPut()
        {
            ISession currentSession;
            var sessionFactory = SessionFact.GetSessionFactOutput();
            currentSession = sessionFactory.OpenSession();

            //if (CurrentSessionContext.HasBind(sessionFactory))
            //{
            //    currentSession =sessionFactory.GetCurrentSession();
            //}
            //else
            //{
            //    currentSession =sessionFactory.OpenSession();
            //    CurrentSessionContext.Bind(currentSession);
            //}
            return currentSession;
        }

        public static void DisposeCurrentSession()
        {
            var factory = SessionFact.GetSessionFact();
            var session = CurrentSessionContext.Unbind(factory);
            if (session != null && session.IsOpen)
            {
                try
                {
                    if (session.Transaction != null && session.Transaction.IsActive)
                    {
                        session.Transaction.Rollback();
                        throw new Exception("Rolling back uncommited NHibernate transaction.");
                    }
                    session.Flush();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SessionKey.EndContextSession", ex);
                    throw;
                }
                finally
                {
                    session.Close();
                    session.Dispose();
                }
            }

            var factoryInput = SessionFact.GetSessionFactInput();
            var sessionInput = CurrentSessionContext.Unbind(factoryInput);
            if (sessionInput != null && sessionInput.IsOpen)
            {
                try
                {
                    if (sessionInput.Transaction != null && sessionInput.Transaction.IsActive)
                    {
                        sessionInput.Transaction.Rollback();
                        throw new Exception("Rolling back uncommited NHibernate transaction.");
                    }
                    session.Flush();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SessionKey.EndContextSession", ex);
                    throw;
                }
                finally
                {
                    sessionInput.Close();
                    sessionInput.Dispose();
                }
            }

            var factoryOutPut = SessionFact.GetSessionFactOutput();
            var sessionOutPut = CurrentSessionContext.Unbind(factoryOutPut);
            if (sessionOutPut != null && sessionOutPut.IsOpen)
            {
                try
                {
                    if (sessionOutPut.Transaction != null && sessionOutPut.Transaction.IsActive)
                    {
                        sessionOutPut.Transaction.Rollback();
                        throw new Exception("Rolling back uncommited NHibernate transaction.");
                    }
                    sessionOutPut.Flush();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("SessionKey.EndContextSession", ex);
                    throw;
                }
                finally
                {
                    session.Close();
                    session.Dispose();
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using LoggerApi.Infrastructure;
using NHibernate;
using NHibernate.Criterion;

namespace LoggerApi.Models.Repositories
{
    public class GenericRepository : IRepository
    {
        private readonly ISession _session;

        public GenericRepository(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
        }

        public IEnumerable<T> WhereAllEq<T>(IDictionary queryFields, IDictionary<string, bool> orderByFields = null)
        {
            var criteria = _session.CreateCriteria(typeof(T));

            if (queryFields != null) criteria.Add(Restrictions.AllEq(queryFields));
            if (orderByFields != null)
            {
                foreach (var field in orderByFields)
                {
                    criteria.AddOrder(new Order(field.Key, field.Value));
                }
            }

            return criteria.List<T>();
        }

        public IEnumerable WhereAllEq(Type targetType, IDictionary queryFields)
        {
            return _session
                .CreateCriteria(targetType)
                .Add(Restrictions.AllEq(queryFields))
                .List();
        }

        public void SaveOrUpdate<T>(T obj)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(obj);
                tx.Commit();
            }
        }

        public void Update<T>(T obj)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Update(obj);
                tx.Commit();
            }
        }

        public void Delete<T>(T obj)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(obj);
                tx.Commit();
            }
        }

        public T GetById<T>(object id)
        {
            return _session.Get<T>(id);
        }

        public object GetById(Type targetType, object id)
        {
            return _session.Get(targetType, id);
        }

        public void Save<T>(T obj)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(obj);
                tx.Commit();
            }
        }
    }
}
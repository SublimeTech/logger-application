using System;
using System.Collections;
using System.Collections.Generic;

namespace LoggerApi.Models.Repositories
{
    public interface IRepository
    {
        T GetById<T>(object id);
        object GetById(Type targetType, object id);
        T Load<T>(object id);

        IList<T> GetAll<T>(IDictionary<string, bool> orderByFields = null);

        IEnumerable<T> QueryLike<T>(IDictionary<string, string> queryFields, IDictionary<string, bool> orderByFields = null);
        IEnumerable<T> QueryOr<T>(IEnumerable<Tuple<string, object>> query);

        IEnumerable<T> WhereGe<T>(string field, object value);
        IEnumerable<T> WhereAllEq<T>(IDictionary queryFields, IDictionary<string, bool> orderByFields = null);
        IEnumerable WhereAllEq(Type targetType, IDictionary queryFields);

        void Save<T>(T obj);
        void SaveOrUpdate<T>(T obj);
        void Update<T>(T obj);
        void Delete<T>(T obj);
        void Merge<T>(T obj) where T : class;
    }
}
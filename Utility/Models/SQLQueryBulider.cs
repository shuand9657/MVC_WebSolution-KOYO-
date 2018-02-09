using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UtilityModelClass.EnumData;


namespace UtilityModelClass
{
   public class SQLQueryBulider
    {
        public static  List<T> QueryData<T>(IQueryable<T> contextItem, List<QueryParameterEntity> queryParameters)
        {
            List<ParameterExpression> pars = new List<ParameterExpression>();
            string sqlStr = string.Empty;
            System.Linq.Expressions.Expression predicateBody = null;
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T));
            MethodCallExpression CallExpression = null;
            foreach (var p in queryParameters)
            {
                var property = System.Linq.Expressions.Expression.Property(parameter, p.ColumnName);
                var value = System.Linq.Expressions.Expression.Constant(p.DataValue);
                var type = Type.GetType(p.DataType);
                if (p.Comparier == StructSQLComparier.SQLComparierType.StrContains)
                {
                    var containsmethod = type.GetMethod(StructSQLComparier.GetSQLComparier(p.Comparier), new[] { type });
                    CallExpression = System.Linq.Expressions.Expression.Call(property, containsmethod, value);
                    predicateBody = System.Linq.Expressions.Expression.And(CallExpression, CallExpression);
                }
                else
                {
                    predicateBody = LambdaParse(p.Comparier, property, value);
                }
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicateBody, parameter);
                contextItem = contextItem.Where(lambda);

            }
            return contextItem.ToList<T>();

        }

        public static List<T> QueryDataAPI<T>(IQueryable<T> contextItem, List<QueryParameterEntity> queryParameters)
        {
            string queryStr = GetSQLStrQueryDetail(queryParameters);
            if (!string.IsNullOrEmpty(queryStr))
            {
                List<Object> parsmeters = GetQueryValues(queryParameters);
                LambdaExpression lambda = System.Linq.Dynamic.DynamicExpression.ParseLambda(contextItem.ElementType,
                   typeof(bool), queryStr, parsmeters.ToArray());
                IQueryable returnObject = contextItem.Provider.CreateQuery(System.Linq.Expressions.Expression.Call(typeof(Queryable), "Where",
                        new Type[] { contextItem.ElementType }, contextItem.Expression, System.Linq.Expressions.Expression.Quote(lambda)));
                return ((IQueryable<T>)returnObject).Take(20).ToList();
            }
            else
                return contextItem.Take(20).ToList();
        }
        public static List<T> QueryDataAPIModi<T>(IQueryable<T> contextItem, List<QueryParameterEntity> queryParameters, int pageIndex , int pageSize)
        {
            string queryStr = GetSQLStrQueryDetail(queryParameters);
            if (!string.IsNullOrEmpty(queryStr))
            {
                List<Object> parsmeters = GetQueryValues(queryParameters);
                LambdaExpression lambda = System.Linq.Dynamic.DynamicExpression.ParseLambda(contextItem.ElementType,
                   typeof(bool), queryStr, parsmeters.ToArray());
                IQueryable returnObject = contextItem.Provider.CreateQuery(System.Linq.Expressions.Expression.Call(typeof(Queryable), "Where",
                        new Type[] { contextItem.ElementType }, contextItem.Expression, System.Linq.Expressions.Expression.Quote(lambda)));
                return ((IQueryable<T>)returnObject).Take(pageSize).Skip(pageIndex).ToList();
            }
            else
                return contextItem.Take(pageSize).Skip(pageIndex).ToList();
        }

        private static string GetSQLStrQueryDetail(List<QueryParameterEntity> queryDetail)
        {
            StringBuilder sb = new StringBuilder(1000);
            Int16 i = 0;
            foreach (var p in queryDetail)
            {
                if (i != 0)
                    sb.Append(String.Format(" {0} ", p.OperatorType));

                if (p.Comparier == StructSQLComparier.SQLComparierType.StrContains)
                {
                    sb.Append(String.Format("{0}.{1}(@{2})", p.ColumnName, StructSQLComparier.GetSQLComparier(p.Comparier), i));
                }
                else
                {
                    sb.Append(String.Format("{0} {1} @{2}", p.ColumnName, StructSQLComparier.GetSQLComparier(p.Comparier), i));
                }

                i++;
            }
            return sb.ToString();
        }

        private static List<Object> GetQueryValues(List<QueryParameterEntity> queryDetail)
        {
            List<Object> searchParameters = new List<Object>();
            if (queryDetail != null)
            {
                foreach (var p in queryDetail)
                {
                    searchParameters.Add(p.DataValue);
                }
            }
            return searchParameters;
        }

        public static System.Linq.Expressions.Expression LambdaParse(StructSQLComparier.SQLComparierType Comparier, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
        {
            System.Linq.Expressions.Expression predicateBody = null;
            switch (Comparier)
            {
                case StructSQLComparier.SQLComparierType.EqualTo:
                    predicateBody = System.Linq.Expressions.Expression.Equal(left, right);
                    break;
                case StructSQLComparier.SQLComparierType.GreaterThan:
                    predicateBody = System.Linq.Expressions.Expression.GreaterThan(left, right);
                    break;
            }
            return predicateBody;

        }

    }
}

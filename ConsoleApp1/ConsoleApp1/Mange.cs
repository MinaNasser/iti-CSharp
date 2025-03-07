using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App

{
    public delegate bool Condtion<T>(T item);
    public delegate TOut Mapper<TOut, TIn>(TIn item);
    public static class Mange
    {
       public static IEnumerable<T>IWhere<T>(this IEnumerable<T> source, Condtion<T> condtion)
        {
            foreach (T item in source)
            {
                yield return item;
            }
        }
       public static IEnumerable<TOut> ISelect<TOut, TIn>(this IEnumerable<TIn> source, Mapper<TOut, TIn> mapper)
        {
            List<TOut> list = new List<TOut>();
            foreach (TIn t in source)
            {
                TOut item = mapper(t);
                list.Add(item);
            }
            return list;
        }
    }
}

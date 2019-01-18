using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteAsync<T>(this DataServiceQuery<T> query)
        {
            return
                await Task.Factory.FromAsync<IEnumerable<T>>(query.BeginExecute(null, null), query.EndExecute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="context"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(this DataServiceContext context, Uri requestUri)
        {
            return
                await Task.Factory.FromAsync<IEnumerable<TResult>>(context.BeginExecute<TResult>(requestUri, null, null),
                                                             executeAsyncResult =>
                                                             {
                                                                 List<TResult> executeResult = context.EndExecute<TResult>(executeAsyncResult).ToList();
                                                                 return executeResult;
                                                             });
        }
    }
}
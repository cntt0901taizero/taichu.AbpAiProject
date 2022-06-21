using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using BaseApplication.Factory;
using Volo.Abp.Application.Dtos;

namespace BaseApplication.Helper
{
    public static class DataBaseHelper
    {
        public static async Task<PagedResultDto<T>> GetPagedAsync<T>(this IQueryable<T> query, int skipCount = 0 , int maxResultCount = 10)
        {
            var result = new PagedResultDto<T>
            {
                TotalCount = await query.CountAsync()
            };

            if (result.TotalCount == 0)
            {
                return result;
            }

            result.Items = await query.Skip(skipCount).Take(maxResultCount).ToListAsync();
            return result;
        }

        //public static async Task<PagedResultDto<T>> GetPagedWithDapperAsync<T>(this IDbConnectionFactory db, string query, object input, int skipCount, int maxResultCount)
        //{
        //    var result = new PagedResultDto<T>();
        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return result;
        //    }
        //    result.TotalCount =
        //        await db.Connection.QueryFirstOrDefaultAsync<int>($"select count(1) totalCount from ({query}) A ", input);
        //    if (result.TotalCount == 0) return result;
        //    result.Items =
        //        (await db.Connection.QueryAsync<T>($"select * from ({query}) A limit {skipCount}, {maxResultCount}", input)).ToList();
        //    return result;
        //}
    }
}

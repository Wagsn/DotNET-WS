using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using PermissionCenter.Stores;

namespace PermissionCenter.Controllers
{
    /// <summary>
    /// SQL 控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/sql")]
    public class SqlController : Controller
    {
        public SqlController(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private ApplicationDbContext Context { get; }

        /// <summary>
        /// SQL 执行请求
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index([FromQuery]string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return Json(new
                {
                    Code = 400,
                    Message = "请求体不能为空"
                });
            }
            try
            {
                // TODO 执行SQL并返回DataTable
                var dataTable = GetDataTable(Context, sql);
                var count = dataTable.Rows.Count;
                return Json(new
                {
                    Code = 200,
                    Message = $"查询成功、共有【{count}】条数据",
                    Data = dataTable
                });
            }
            catch(Exception e)
            {
                return new  JsonResult(new { Code = 500, Message = e.Message });
            }
        }

        public static DataTable GetDataTable(DbContext context, string sql, params object[] parameters)
        {
            var conn = context.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            DataTable table = new DataTable();
            var pageSize = 0;
            var pageIndex = 10;
            conn.Open();
            using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                int fieldCount = reader.FieldCount;
                for (int i = 0; i < fieldCount; i++)
                {
                    table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                }

                object[] values = new object[fieldCount];
                int currentIndex = 0;
                int startIndex = pageSize * pageIndex;
                try
                {
                    table.BeginLoadData();
                    while (reader.Read())
                    {
                        if (startIndex > currentIndex++)
                            continue;

                        if (pageSize > 0 && (currentIndex - startIndex) > pageSize)
                            break;

                        reader.GetValues(values);
                        table.LoadDataRow(values, true);
                    }
                }
                finally
                {
                    table.EndLoadData();
                    try  //lgy:由于连接阿里云ADS数据库cmd.Cancel()会报错，所以把错误忽略了。
                    {
                        cmd.Cancel();
                    }
                    catch
                    {
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            return table;

            //    using (var c = context.Database.GetDbConnection())
            //{
            //    c.Open();
            //    var cmd = c.CreateCommand();
            //    cmd.CommandText = sql;
            //    //if (parameters != null)
            //    //{
            //    //    foreach (SqlParameter parameter in parameters)
            //    //    {
            //    //        if (!parameter.ParameterName.Contains("@"))
            //    //            parameter.ParameterName = $"@{parameter.ParameterName}";
            //    //        command.Parameters.Add(parameter);
            //    //    }
            //    //}
            //    System.Data.Common.DbDataReader reader = cmd.ExecuteReader();
            //    DataTable dt = new DataTable();
            //    dt.Load(reader);
            //    reader.Close();
            //    c.Close();
            //    return dt;
            //}
            //var concurrencyDetector = context.Database.GetService<IConcurrencyDetector>();
            //using (concurrencyDetector.EnterCriticalSection())
            //{
            //    var rawSqlCommand = context.Database.GetService<IRawSqlCommandBuilder>().Build(sql, parameters);

            //    RelationalDataReader query = rawSqlCommand.RelationalCommand.ExecuteReader(context.Database.GetService<IRelationalConnection>(), parameterValues: rawSqlCommand.ParameterValues);

            //    var table = new DataTable();
            //    table.Load(query.DbDataReader);

            //    return table;
            //}
        }
    }
}

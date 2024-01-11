using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project_Data_Access.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
    public class spcall : IspCall
    {
        private readonly ApplicationDbContext _context;
        private static string connectionString = "";
        public spcall(ApplicationDbContext context)
        {
            _context = context;
            connectionString = _context.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void excecute(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                sqlcon.Execute(procdureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(procdureName,param,commandType: CommandType.StoredProcedure);
            }
        }

        
            

        public T oneRecord<T>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                var value=sqlcon.Query<T>(procdureName,param,commandType: CommandType.StoredProcedure);
                return value.FirstOrDefault();
            }
        }

        public T single<T>(string procdureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return sqlcon.ExecuteScalar<T>(procdureName, param, commandType: CommandType.StoredProcedure);
            }
        }





        Tuple<IEnumerable<T1>, IEnumerable<T2>> IspCall.List<T1, T2>(string procdureName, DynamicParameters param)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                var result = SqlMapper.QueryMultiple(sqlcon, procdureName, param, commandType: CommandType.StoredProcedure);
                var item1 = result.Read<T1>();
                var item2 = result.Read<T2>();
                if (item1 != null && item2 != null)
                
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
            
                
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
            }
        }
    }
}

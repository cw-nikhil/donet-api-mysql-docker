using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace demo.Controllers
{
    public class Repo
    {
        private static readonly string server = Environment.GetEnvironmentVariable("HOST");
        private static readonly string port = Environment.GetEnvironmentVariable("PORT");
        private static readonly string password = Environment.GetEnvironmentVariable("PASSWORD");
        private static readonly string sqlConnectionString = $"SERVER={server};Port={port};UID=root;PASSWORD={password};DATABASE=sys;UseAffectedRows=True";

        public string GetValue(string key)
        {
            string sql = "SELECT V AS Value from sys.Kvp where K = @key";
            try
            {
                using (IDbConnection con = new MySqlConnection(sqlConnectionString))
                {

                    var values = con.Query<KVP>(sql, new
                    {
                        key
                    }).ToList();
                    return values.Count() == 0 ? string.Empty : values.First().Value;
                }
            }
            catch (Exception e)
            
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(e);
                Console.WriteLine("-----------------------------------------------------------------------");
                return string.Empty;
            }
        }
        public int Add(KVP kvp)
        {
            string sql = "INSERT INTO sys.Kvp(K, V) VALUES (@key, @value); SELECT LAST_INSERT_ID();";
            try
            {
                using (var con = new MySqlConnection(sqlConnectionString))
                {

                    int id = con.ExecuteScalar<int>(sql, new
                    {
                        @key = kvp.Key,
                        @value = kvp.Value
                    });
                    return id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(e);
                Console.WriteLine("-----------------------------------------------------------------------");
                return -1;
            }
        }
    }
}


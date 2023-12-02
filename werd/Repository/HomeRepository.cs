using Dapper;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Data.Common;
using werd.Model;

namespace werd.Repository
{
    public class HomeRepository : IRepository<TestData>
    {
        private readonly DBContext context;
        public HomeRepository(DBContext context)
        {
            this.context = context;
        }

        public bool Add(TestData entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int[] ids)
        {
            throw new NotImplementedException();
        }

        public void testgtp()
        {
            using (var conn = this.context.CreateConnection())
            {
                conn.Open();

                using (var command = new NpgsqlCommand("werd.all_supplier", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // 设置存储过程的输入参数（如果有的话）
                    //command.Parameters.AddWithValue("param_name", param_value);

                    //设置 refcursor 的输出参数
                    command.Parameters.Add(new NpgsqlParameter("o_supplier", NpgsqlDbType.Refcursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    using (var reader = command.ExecuteReader())
                    {
                        // 检查是否有 refcursor 输出
                        if (command.Parameters["o_supplier"].Value != DBNull.Value)
                        {
                            // 获取 refcursor 的名称
                            string refCursorName = command.Parameters["o_supplier"].Value.ToString();

                            // 关闭第一个 reader
                            reader.Close();

                            // 使用 refcursor 执行查询
                            using (var command2 = new NpgsqlCommand($"FETCH ALL IN \"{refCursorName}\"", conn))
                            using (var reader2 = command2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    // 处理结果
                                    Console.WriteLine(reader2[0]);
                                }
                            }
                        }
                        else
                        {
                            // 没有 refcursor 输出的处理逻辑
                        }
                    }
                    //using (var reader = command.ExecuteReader())
                    //{
                    //    // 检查是否有 refcursor 输出
                    //    if (command.Parameters["o_supplier"].Value != DBNull.Value)
                    //    {
                    //        // 获取 refcursor 的名称
                    //        string refCursorName = command.Parameters["o_supplier"].Value.ToString();

                    //        // 使用 refcursor 执行查询
                    //        using (var command2 = new NpgsqlCommand($"FETCH ALL IN \"{refCursorName}\"", conn))
                    //        {
                    //            using (var reader2 = command2.ExecuteReader())
                    //            {
                    //                while (reader2.Read())
                    //                {
                    //                    // 处理结果
                    //                    Console.WriteLine(reader2[0]);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // 没有 refcursor 输出的处理逻辑
                    //    }
                    //}
                }
            }

        }

        public async void test()
        {
            using (var conn = this.context.CreateConnection())
            {
                conn.Open();
                using var cmd = new NpgsqlCommand("CALL werd.all_supplier()", conn);
                using var reader = cmd.ExecuteReader();
                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                //using var command1 = new NpgsqlCommand("CALL all_supplier()", conn)
                //{
                //    CommandType = CommandType.StoredProcedure
                //};
                //command1.Parameters.Add(new NpgsqlParameter("o_supplier", NpgsqlDbType.Refcursor)
                //{
                //    Direction = ParameterDirection.Output
                //});
                //await using var reader = await command1.ExecuteReaderAsync();
            }
        }


        public async void GetAll()
        {
            //using (var conn = this.context.CreateConnection())
            //{
            //    string sql = "SELECT * FROM werd.tb_sys_is_deleted;";

            //    return conn.Query<TestData>(sql);
            //}
            //var result = conn.Query<Supplier>("werd.all_supplier", parameters, commandType: CommandType.StoredProcedure);

            //using (var conn = this.context.CreateConnection())
            //{
            //    conn.Open();
            //    using (NpgsqlCommand cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = "all_supplier";
            //        cmd.Connection = conn;
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        NpgsqlDataReader dr = cmd.ExecuteReader();
            //        while (dr.Read())
            //        {
            //            Console.WriteLine(dr);
            //        }
            //    }
            using (var conn = this.context.CreateConnection())
            {
                conn.Open();
                using var command1 = new NpgsqlCommand("werd.all_supplier", this.context.CreateConnection())
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                };
                NpgsqlParameter p = new NpgsqlParameter();
                p.ParameterName = "o_supplier";
                p.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Refcursor;
                p.Direction = ParameterDirection.InputOutput;
                p.Value = "o_supplier";
                command1.Parameters.Add(p);
                //command1.ExecuteNonQuery();
                //command1.Parameters.Add(new NpgsqlParameter("o_supplier", NpgsqlDbType.Refcursor)
                //{
                //    Direction = ParameterDirection.InputOutput,
                //    Value = "o_supplier"
                //});
                //command1.CommandText = "fetch all in \"o_supplier\"";
                //command1.CommandType = CommandType.Text;

                NpgsqlDataReader dr = command1.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine(dr[0]);
                }
                //await using var reader = await command1.ExecuteReaderAsync();
            };
            //command1.ExecuteNonQuery();

            //return new List<TestData>();

        }

        public TestData GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(TestData entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TestData> IRepository<TestData>.GetAll()
        {
            throw new NotImplementedException();
        }

        int IRepository<TestData>.Add(TestData entity)
        {
            throw new NotImplementedException();
        }

        int IRepository<TestData>.Update(TestData entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string[] ids)
        {
            throw new NotImplementedException();
        }
    }
}

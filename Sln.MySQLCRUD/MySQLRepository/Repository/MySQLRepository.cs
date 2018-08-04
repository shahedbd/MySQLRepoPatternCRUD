using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace MySQLRepository.Repository
{
    public class MySQLRepository<TEntity> : IMySQLRepository<TEntity> where TEntity : class
    {
        private static MySqlConnection conn;
        public MySQLRepository(MySqlConnection connectionString)
        {
            try
            {
                conn = MySQLConnection.MySqlConnectionApplication();
            }
            catch (Exception ex)
            {
                //Need to log write
                throw ex;
            }
        }


        public virtual async Task<IEnumerable<TEntity>> GetAll(string sqlText)
        {
            var list = new List<TEntity>();
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                var reader = cmd.ExecuteReader();
                try
                {
                    list = Helper.DataReaderMapToList<TEntity>(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return await Task.FromResult(list);
        }

        public virtual async Task<TEntity> GetByID(string sqlText)
        {
            TEntity record = null;

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                var reader = cmd.ExecuteReader();
                try
                {
                    record = Helper.DataReaderMapToEntity<TEntity>(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return await Task.FromResult(record);
        }


        public virtual async Task<DataTable> GetByIDUsingSP(long ID)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SPGetbasicinfo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cid", ID);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {                      
                        sda.Fill(dt);

                        var abc = dt.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return await Task.FromResult(dt);
        }

        public virtual async Task<bool> Execute(string sqlText)
        {
            bool result = false;

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                await Task.FromResult(cmd.ExecuteNonQuery());
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return await Task.FromResult(result);
        }
        
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MySQLRepository
{
    public static class Helper
    {
        public static List<T> DataReaderMapToList<T>(MySqlDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                int fieldCount = dr.FieldCount;
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {

                    if (ColumnExists(dr, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    else
                    {
                        prop.SetValue(obj, null, null);
                    }
                }
                list.Add(obj);
            }

            return list;
        }

        public static T DataReaderMapToEntity<T>(MySqlDataReader dr)
        {
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                int fieldCount = dr.FieldCount;
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (ColumnExists(dr, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    else
                    {
                        prop.SetValue(obj, null, null);
                    }
                }
            }

            return obj;
        }

        public static bool ColumnExists(MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))    //reader.GetName(i) == columnName
                {
                    return true;
                }
            }
            return false;
        }

        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }


        public static IEnumerable<DataRow> AsEnumerable(this DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                yield return table.Rows[i];
            }
        }


    }
}

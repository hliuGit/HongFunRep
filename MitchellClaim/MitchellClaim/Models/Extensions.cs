using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MitchellClaim.Models
{
    public static class Extensions
    {
        public static IEnumerable<T> Select<T>(this SqlDataReader reader, Func<SqlDataReader, T> projection)
        {
            while (reader.Read())
            { yield return projection(reader); }
        }
        public static T GetValue<T>(this SqlDataReader reader, string columnName)
        {
            int index = reader.GetOrdinal(columnName);

            if (index < 0)
                return default(T);

            object temp = reader.GetValue(index);

            if (temp == System.DBNull.Value)
                return default(T);

            return (T)temp;
        }

        

        public static bool TryGetValue<T>(this IDictionary<string, object> map, string key, out T value)
        {
            value = default(T);
            object temp = null;

            if (map.TryGetValue(key, out temp))
            {
                value = (T)temp;
                return true;
            }

            return false;
        }

        

        

       

        
    }
}
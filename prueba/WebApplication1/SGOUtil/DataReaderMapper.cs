using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGOUtil
{
    public class DataReaderMapper<T> where T : new()
    {
        public IEnumerable<T> MapListAll(IDataReader reader)
        {
            return MapListExcludeColumns(reader);
        }

        public IQueryable<T> MapListExcludeColumns(IDataReader reader, params string[] excludeColumns)
        {
            var listOfObjects = new List<T>();
            while (reader.Read())
            {
                listOfObjects.Add(MapRowExclude(reader, excludeColumns));
            }
            return listOfObjects.AsQueryable();
        }

        public T MapRowExclude(IDataReader reader, params string[] columns)
        {
            return MapRow(reader, false, columns);
        }

        public T MapRowInclude(IDataReader reader, params string[] columns)
        {
            return MapRow(reader, true, columns);
        }

        public T MapRowAll(IDataReader reader)
        {
            return MapRow(reader, true, null);
        }

        private T MapRow(IDataReader reader, bool includeColumns, params string[] columns)
        {
            T item = new T(); // 1. 
            var properties = GetPropertiesToMap(includeColumns, columns); // 2. 
            foreach (var property in properties)
            {
                int ordinal = reader.GetOrdinal(property.Name); // 3. 
                if (!reader.IsDBNull(ordinal)) // 4.
                {
                    // if dbnull the property will get default value, 
                    // otherwise try to read the value from reader
                    property.SetValue(item, reader[ordinal], null); // 5.
                }
            }
            return item;
        }

        public IEnumerable<System.Reflection.PropertyInfo> GetPropertiesToMap(bool includeColumns, string[] columns)
        {

            var properties = typeof(T).GetProperties().Where(y =>
                (y.PropertyType.Equals(typeof(string)) ||
                y.PropertyType.Equals(typeof(byte[])) ||
                y.PropertyType.IsValueType) &&
                (columns == null || (columns.Contains(y.Name) == includeColumns)));
            return properties;
        }
    }
}

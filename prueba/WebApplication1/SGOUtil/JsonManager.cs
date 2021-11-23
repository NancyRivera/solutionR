using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SGOUtil
{
    public class JsonManager
    {

        private List<Hashtable> _items;
        public List<Hashtable> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public JsonManager()
        {
            _items = new List<Hashtable>();
        }

        public JsonManager Serialization<T>(IList<T> oList)
        {
            JsonManager oJsonResponse = new JsonManager();

            Type elementType = typeof(T);
            foreach (T item in oList)
            {
                Hashtable ohashTable = new Hashtable();

                foreach (PropertyInfo propInfo in elementType.GetProperties())
                {
                    if (propInfo.GetValue(item, null) == null)
                    {
                        ohashTable.Add(propInfo.Name, "");
                    }
                    else
                    {
                        ohashTable.Add(propInfo.Name, propInfo.GetValue(item, null).ToString());
                    }
                }
                oJsonResponse.Items.Add(ohashTable);
            }

            return oJsonResponse;
        }

    }
}

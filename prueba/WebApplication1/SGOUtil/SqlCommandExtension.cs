using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SGOUtil
{
    public static class SqlCommandExtension
    {
        /// <summary>
        /// Genera cadena de consulta de un Store procedure, esto con el objetivo de hacer pruebas si es que no se tiene el Agente de SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="storeProcedure"></param>
        /// <returns></returns>
        public static string ToQueryString(this SqlCommand sqlCommand)
        {
            var diccionario = new Dictionary<string, object>();
            string storeProcedure = sqlCommand.CommandText;
            int i = 0;
            foreach (var item in sqlCommand.Parameters)
            {
                var key = sqlCommand.Parameters[i].ParameterName;
                var value = sqlCommand.Parameters[i].Value;
                diccionario.Add(key, value);
                i++;
            }
            var parametrosYValores = JsonConvert.SerializeObject(diccionario);
            parametrosYValores = parametrosYValores.Replace("\"", "'").Replace("{", "").Replace("}", "");
            parametrosYValores = parametrosYValores.Replace("':", "=");
            parametrosYValores = parametrosYValores.Replace("'@", "@");
            parametrosYValores = parametrosYValores.Replace("@", $"{Environment.NewLine}@");
            string uspExecute = $"USE SGO{Environment.NewLine}GO{Environment.NewLine}{storeProcedure}{Environment.NewLine}{parametrosYValores}";
            return uspExecute;
        }
    }
}

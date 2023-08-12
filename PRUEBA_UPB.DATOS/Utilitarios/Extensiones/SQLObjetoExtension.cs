using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA_UPB.DATOS.Utilitarios.Extensiones
{ 
    internal static class SQLObjetoExtension
    {
        internal static T Parse<T>(this NpgsqlDataReader item) where T : class, new()
        {
            var result = new T();
            var properties = typeof(T).GetProperties();


            for (int i = 0; i < item.FieldCount; i++)
            {
                string columnNane = item.GetName(i).ToUpper();
                var property = properties.FirstOrDefault(x => x.Name.ToUpper() == columnNane);

                if (property != null && !item.IsDBNull(i))
                {
                    object value = item.GetValue(i);
                    property.SetValue(result, value);
                }
            }

            return result;
        }
    }
}
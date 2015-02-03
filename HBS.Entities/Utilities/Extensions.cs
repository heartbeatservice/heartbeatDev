using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Entities.Utilities
{
    public static class Extensions
    {
        public static bool HasColumn(this IDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        public static List<string> ColumnList(this IDataReader dr)
        {
            List<string> columnList = new List<string>();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                columnList.Add(dr.GetName(i).ToLower());
            }
            return columnList;
        }

    }

}

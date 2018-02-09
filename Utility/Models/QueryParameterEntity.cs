using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityModelClass.EnumData;

namespace UtilityModelClass
{
    public class QueryParameterEntity
    {
        public string OperatorType { get; set; }
        public string ColumnName { get; set; }
        public StructSQLComparier.SQLComparierType Comparier { get; set; }
        public string DataType { get; set; }
        public object DataValue { get; set; }

    }
}

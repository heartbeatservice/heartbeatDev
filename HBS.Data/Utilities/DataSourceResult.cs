using System;
using System.Collections;
using System.Linq;

namespace HBS.Data.Utilities
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public int Total { get; set; }
        public string Errors { get; set; }

        public DataSourceResult(IEnumerable data, int total)
        {
            this.Data = data;
            this.Total = total;
        }

        public DataSourceResult(string errors)
        {
            this.Errors = errors;
        }

        public DataSourceResult()
        {

        }
    }
}

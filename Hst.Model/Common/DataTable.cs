//using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hst.Model.Common
{
    public class DataTableResult<T>
    {
        public DataTableResult()
        {

        }
        public DataTableResult(T _data, int _totalRecord, int _draw)
        {
            data = _data;
            recordsTotal = _totalRecord;
            recordsFiltered = _totalRecord;
            draw = _draw;
        }
        public T data { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    public class DataTableRequest
    {
        public int Identifier { get; set; }
        public int UserId { get; set; }
        public string SearchText { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }

        //public DynamicParameters GetParameters()
        //{
        //    var p = new DynamicParameters();
        //    p.Add("OffSet", start);
        //    p.Add("PageSize", length);
        //    p.Add("SortBy", order != null ? columns[order[0].column].data : "");
        //    p.Add("SortDirection", order != null ? order[0].dir.ToLower() : "");
        //    p.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    return p;
        //}
    }


    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

}

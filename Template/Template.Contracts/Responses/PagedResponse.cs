using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;

namespace Template.Model.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse() {}
        public PagedResponse(IEnumerable<T> response)
        {
            Data = response;
        }

        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
    }
}

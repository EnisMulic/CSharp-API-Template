using System;
using System.Collections.Generic;

namespace Template.Contracts.V1.Requests
{
    public class SortQuery
    {
        public string OrderBy { get; set; }

        public List<string> ordering { get; set; }
        public List<Tuple<string, int>> tpl { get; set; }
    }
}

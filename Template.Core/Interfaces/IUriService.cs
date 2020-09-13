﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.Requests;

namespace Template.Core.Interfaces
{
    public interface IUriService
    {
        public Uri GetUri(PaginationQuery pagination = null);
    }
}
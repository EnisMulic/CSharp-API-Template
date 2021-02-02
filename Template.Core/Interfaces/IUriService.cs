using System;
using Template.Contracts.V1.Requests;

namespace Template.Core.Interfaces
{
    public interface IUriService
    {
        public Uri GetUri(PaginationQuery pagination = null);
    }
}

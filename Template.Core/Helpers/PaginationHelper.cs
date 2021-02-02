using System;
using System.Collections.Generic;
using System.Linq;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces;

namespace Template.WebAPI.Helpers
{
    public class PaginationHelper
    {

        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationQuery pagination, List<T> response, int count)
        {
            int LastPageNumber = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / pagination.PageSize));

            var nextPage = pagination.PageNumber >= 1 && pagination.PageNumber < LastPageNumber
                ? uriService.GetUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
                : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService.GetUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString()
                : null;

            var firstPage = pagination.PageNumber > 1
                ? uriService.GetUri(new PaginationQuery(1, pagination.PageSize)).ToString()
                : null;

            var lastPage = pagination.PageNumber < LastPageNumber
                ? uriService.GetUri(new PaginationQuery(LastPageNumber, pagination.PageSize)).ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage,
                FirstPage = firstPage,
                LastPage = lastPage
            };
        }
    }
}

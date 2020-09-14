using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using Template.Contracts.V1.Requests;
using Template.Core.Interfaces;

namespace Template.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetUri(PaginationQuery pagination = null)
        {
            var uri = _baseUri;
            if (pagination != null)
            {
                Dictionary<string, string> queryDictionary = new Dictionary<string, string>();

                queryDictionary.Add("pageNumber", pagination.PageNumber.ToString());
                queryDictionary.Add("pageSize", pagination.PageSize.ToString());

                uri = QueryHelpers.AddQueryString(_baseUri, queryDictionary);
            }

            return new Uri(uri);
        }
    }
}

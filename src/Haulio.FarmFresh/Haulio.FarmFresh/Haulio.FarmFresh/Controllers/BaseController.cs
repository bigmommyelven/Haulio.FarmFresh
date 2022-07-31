using Haulio.FarmFresh.Domain.Common;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Specialized;
using System.Net;

namespace Haulio.FarmFresh.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult StatusCodeWithObject(
            HttpStatusCode statusCode, object obj)
        {
            var res = new Response(obj);
            return StatusCode((int)statusCode, res);
        }

        protected IActionResult StatusCodeWithPagination(HttpStatusCode statusCode, PagedResponse pagedResponse)
        {
            ProcessPagination(ref pagedResponse);
            return StatusCode((int)statusCode, pagedResponse);
        }

        private void ProcessPagination(ref PagedResponse pagedResponse)
        {
            var url = HttpContext.Request.GetEncodedUrl();
            if (pagedResponse.Paging.TotalPage > pagedResponse.Paging.Page)
            {
                var nextUrl = SetQueryValue(url, nameof(pagedResponse.Paging.Page), pagedResponse.Paging.Page + 1);
                pagedResponse.Paging.Next = nextUrl;
            }
            if (pagedResponse.Paging.Page > 1)
            {
                var prevUrl = SetQueryValue(url, nameof(pagedResponse.Paging.Page), pagedResponse.Paging.Page - 1);
                pagedResponse.Paging.Prev = prevUrl;
            }
        }

        private static string SetQueryValue(string stringUri, string name, object value)
        {
            var uri = new Uri(stringUri);
            NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(uri.Query);
            nvc[name] = value.ToString();
            return new UriBuilder(uri)
            {
                Query = nvc.ToString()
            }.Uri
            .ToString();
        }
    }
}

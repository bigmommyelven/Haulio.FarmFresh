using System;

namespace Haulio.FarmFresh.Domain.Common
{
    public class PagedResponse
    {
        public readonly bool Succeded = true;
        public object Data { get; set; }
        public PagedDetail Paging { get; set; }
        public PagedResponse(object data, Pagination pagination, int totalRecords)
        {
            Data = data;
            Paging = new PagedDetail
            {
                TotalRecords = totalRecords,
                Page = pagination.Page,
                TotalPage = (int)Math.Ceiling((double)totalRecords / pagination.Limit)
            };
        }
    }
}

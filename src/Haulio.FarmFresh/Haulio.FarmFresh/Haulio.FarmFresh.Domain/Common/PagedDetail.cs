namespace Haulio.FarmFresh.Domain.Common
{
    public class PagedDetail
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }
}

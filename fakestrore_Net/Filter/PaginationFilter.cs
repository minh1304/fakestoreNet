namespace fakestrore_Net.Filter
{
    public class PaginationFilter
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PaginationFilter()
        {
            PageNumber = null;
            PageSize = null;
        }

        public PaginationFilter(int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

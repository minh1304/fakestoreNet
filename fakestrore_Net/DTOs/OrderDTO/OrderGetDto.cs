namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderGetDto
    {
        public string SearchText { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDateFrom { get; set; }
        public DateTime OrderDateEnd { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; } 
    }
}

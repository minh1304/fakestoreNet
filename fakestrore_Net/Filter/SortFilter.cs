namespace fakestore_Net.Filter
{
    public class SortFilter
    {
        public bool? IsDescending { get; set; }

        public SortFilter()
        {
            IsDescending = null;
        }

        public SortFilter(bool? isDescending)
        {
            IsDescending = isDescending;
        }
    }
}
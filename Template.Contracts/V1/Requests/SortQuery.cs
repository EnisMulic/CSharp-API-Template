namespace Template.Contracts.V1.Requests
{
    public enum SortOrder
    {
        ASC, DESC
    }
    public class SortQuery
    {
        public string OrderBy { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}

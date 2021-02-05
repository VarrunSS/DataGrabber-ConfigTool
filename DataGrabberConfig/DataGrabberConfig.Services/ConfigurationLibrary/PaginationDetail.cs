namespace DataGrabberConfig.Services
{
    public class PaginationDetail
    {
        public string PagingType { get; set; } = "NoPaging";

        public string PaginationContainerPathType { get; set; }        
        public string PaginationContainer { get; set; }
        public bool DoesHaveNextButton { get; set; }
        public string NextButtonPathType { get; set; }
        public string NextButton { get; set; }
        public string ActiveClassForCurrentPage { get; set; }
        public string DisabledClassForLastPage { get; set; }
        public bool ShouldLimitPaging { get; set; }
        public string PagingLimit { get; set; }
    }
}


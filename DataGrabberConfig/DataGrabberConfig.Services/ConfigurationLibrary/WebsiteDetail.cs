using System.Collections.Generic;
using System.Data;

namespace DataGrabberConfig.Services
{
    public class WebsiteDetail
    {
        public WebsiteDetail()
        {
            WebsiteURLs = new List<string>();
            InputFields = new List<InputField>();
            DbInputFields = new DataTable();
        }

        public string WebsiteNamePrefix { get; set; }
        public string WebscrapeType { get; set; } = "SingleURL";
        public string WebsiteURL { get; set; }
        public List<string> WebsiteURLs { get; set; }
        public string DbWebsiteURLs { get; set; }

        public bool DoesHaveSearchButton { get; set; }
        public string SearchButtonPathType { get; set; }        
        public string SearchButton { get; set; }

        public bool DoesHaveResetButton { get; set; }
        public string ResetButtonPathType { get; set; }        
        public string ResetButton { get; set; }

        public List<InputField> InputFields { get; set; }

        public DataTable DbInputFields { get; set; }
    }
}

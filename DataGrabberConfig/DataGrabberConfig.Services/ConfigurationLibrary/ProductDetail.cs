using System.Collections.Generic;
using System.Data;

namespace DataGrabberConfig.Services
{
    public class ProductDetail
    {
        public ProductDetail()
        {
            Fields = new List<Field>();
            DbFields = new DataTable();
        }

        public string OverallContainerPathType { get; set; }        
        public string OverallContainer { get; set; }

        public string ProductContainerPathType { get; set; }
        public string ProductContainer { get; set; }

        public List<Field> Fields { get; set; }

        public DataTable DbFields { get; set; }
    }
}

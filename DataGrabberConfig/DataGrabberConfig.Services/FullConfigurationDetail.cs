using DataGrabberConfig.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services
{
    public class FullConfigurationDetail : BasicConfigurationDetail
    {
        public FullConfigurationDetail()
        {
            SiteConfiguration = new SiteConfiguration();
            WebsiteDetail = new WebsiteDetail();
            ProductDetail = new ProductDetail();
            PaginationDetail = new PaginationDetail();
            MailingInformation = new MailingInformation();
        }

        public SiteConfiguration SiteConfiguration { get; set; }

        public WebsiteDetail WebsiteDetail { get; set; }

        public ProductDetail ProductDetail { get; set; }

        public PaginationDetail PaginationDetail { get; set; }

        public MailingInformation MailingInformation { get; set; }


        public void SetDataBeforeSave()
        {

            WebsiteDetail.DbWebsiteURLs = string.Join(",", WebsiteDetail.WebsiteURLs);

            WebsiteDetail.DbInputFields = WebsiteDetail.InputFields.ToDataTable();
            
            ProductDetail.DbFields = ProductDetail.Fields.ToDataTable();
        }
    }
}

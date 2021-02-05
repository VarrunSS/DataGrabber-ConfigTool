using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services
{
    public class BasicConfigurationDetail
    {
        public BasicConfigurationDetail()
        {
            ConfigID = string.Empty;
            ConfigGUID = string.Empty;
            ConfigName = string.Empty;
            ConfigType = string.Empty;
            URL = string.Empty;
            CreatedBy = string.Empty;
            CreatedOn = string.Empty;
            UpdatedBy = string.Empty;
            UpdatedOn = string.Empty;
        }

        [JsonIgnore]
        public string ConfigID { get; set; }

        public string ConfigGUID { get; set; }

        public string ConfigName { get; set; }

        public string ConfigType { get; set; }
        
        public string URL { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public string UpdatedOn { get; set; }


    }
}

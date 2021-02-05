namespace DataGrabberConfig.Services
{
    public class SiteConfiguration
    {
        public string WebsiteConfigurationName { get; set; }
        public string ScrapingMechanism { get; set; } = "Selenium";
        public bool ShouldRotateProxy { get; set; }
        public bool RequireInputValues { get; set; }
        public bool ShouldDisableJavaScript { get; set; }
        public string WaitingTimeAfterPageLoad { get; set; }
    }
}

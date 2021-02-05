namespace DataGrabberConfig.Services
{
    public class MailingInformation
    {
        public bool ShouldSendMail { get; set; }
        public string MailToAddress { get; set; }
        public string MailCCAddress { get; set; }
        public string MailBCCAddress { get; set; }

    }
}

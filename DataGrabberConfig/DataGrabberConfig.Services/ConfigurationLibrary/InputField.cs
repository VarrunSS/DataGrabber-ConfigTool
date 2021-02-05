namespace DataGrabberConfig.Services
{
    public class InputField : BaseField
    {
        public override string FieldName { get; set; }
        public override string FieldPathType { get; set; }
        public override string FieldPath { get; set; }

        public string TargetType { get; set; }
        public bool HasPartnerElement { get; set; }
        public string WaitingTimeAfterElementChange { get; set; }
    }
}

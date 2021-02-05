namespace DataGrabberConfig.Services
{
    public class Field : BaseField
    {
        public override string FieldName { get; set; }
        public override string FieldPathType { get; set; }
        public override string FieldPath { get; set; }

        public bool ShouldCheckElementInBody { get; set; }
        public string RemoveText { get; set; }
        public string AttributeName { get; set; }
    }
}

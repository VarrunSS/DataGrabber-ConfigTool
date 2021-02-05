namespace DataGrabberConfig.Services
{
    public abstract class BaseField 
    {
        public abstract string FieldName { get; set; }
        public abstract string FieldPathType { get; set; }
        public abstract string FieldPath { get; set; }

        //public bool IsCollapsed { get; set; }
    }

}

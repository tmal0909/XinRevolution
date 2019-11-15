namespace XinRevolution.Web.Model
{
    public class ServiceResultModel<T> where T : new() 
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public ServiceResultModel()
        {
            Status = true;
            Message = string.Empty;
            Data = new T();
        }
    }
}

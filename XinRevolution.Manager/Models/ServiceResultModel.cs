using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XinRevolution.Manager.Models
{
    public class ServiceResultModel
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public ServiceResultModel()
        {
            Status = false;
            Message = $"尚未執行操作";
        }
    }

    public class ServiceResultModel<T>
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public ServiceResultModel()
        {
            Status = false;
            Message = $"尚未執行操作";
            Data = default(T);
        }
    }
}

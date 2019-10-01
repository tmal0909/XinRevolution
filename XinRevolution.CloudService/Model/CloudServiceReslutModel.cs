using System;
using System.Collections.Generic;
using System.Text;

namespace XinRevolution.CloudService.Model
{
    public class CloudServiceReslutModel
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public CloudServiceReslutModel()
        {
            Status = false;
            Message = $"尚未執行操作";
        }
    }

    public class CloudServiceReslutModel<TData>
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public TData Data { get; set; }

        public CloudServiceReslutModel()
        {
            Status = false;
            Message = $"尚未執行操作";
            Data = default(TData);
        }
    }
}


using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Database.Enum
{
    public enum ReferenceTypeEnum
    {
        [Display(Name = "文字")]
        Text,

        [Display(Name = "圖片")]
        Image,

        [Display(Name = "影片")]
        Video
    }
}

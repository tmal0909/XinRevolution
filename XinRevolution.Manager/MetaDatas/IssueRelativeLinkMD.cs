using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace XinRevolution.Manager.MetaDatas
{
    public class IssueRelativeLinkMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入相關連結")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "相關連結", Prompt = "請輸入相關連結")]
        public string LinkUrl { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請輸入資源連結")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請輸入資源連結")]
        public string ResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入備註")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "備註", Prompt = "請輸入備註")]
        public string Note { get; set; }
        
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int IssueId { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif", ErrorMessage = "檔案類型錯誤")]
        [Display(Name = "選擇資源")]
        public IFormFile ResourceFile { get; set; }

        [Display(Name = "資源名稱")]
        public string ResourceName { get { return ResourceUrl; } set { } }
    }
}

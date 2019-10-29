using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGGroupMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入群組名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "議題名稱", Prompt = "請輸入群組名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請選擇資源")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "背景資源連結", Prompt = "請選擇背景資源連結")]
        public string ResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入群組順序")]
        [Display(Name = "群組順序", Prompt = "請輸入群組順序")]
        public int Sort { get; set; }

        [Required(ErrorMessage = "請輸入備註")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "備註", Prompt = "請輸入備註")]
        public string Note { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }
    }
}

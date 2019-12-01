using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGSeasonChapterMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入章節編號")]
        [Display(Name = "章節編號", Prompt = "請輸入章節編號")]
        public int SerialNumber { get; set; }

        [Required(ErrorMessage = "請輸入章節名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "章節名稱", Prompt = "請輸入章節名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入章節簡介")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "章節簡介", Prompt = "請輸入章節簡介")]
        public string Intro { get; set; }

        [Required(ErrorMessage = "請選擇章節偏移量")]
        [Range(0, 6, ErrorMessage = "偏移量介於 0 ~ 6")]
        [Display(Name = "偏移量", Prompt = "請輸入章節偏移量")]
        public int Offset { get; set; }

        [Required(ErrorMessage = "請選擇資源")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請選擇資源連結")]
        public string ResourceUrl { get; set; }
        
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int SeasonId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }
    }
}

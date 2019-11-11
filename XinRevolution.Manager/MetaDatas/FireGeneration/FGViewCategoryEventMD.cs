using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGViewCategoryEventMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入事件名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "事件名稱", Prompt = "請輸入事件名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入事件標題")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "事件標題", Prompt = "請輸入事件標題")]
        public string Title { get; set; }

        [Required(ErrorMessage = "請上傳資源檔案")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請上傳資源檔案")]
        public string ResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入事件簡介")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "事件簡介", Prompt = "請輸入事件簡介")]
        public string Intro { get; set; }

        [Required(ErrorMessage = "請輸入事件順序")]
        [Display(Name = "事件順序", Prompt = "請輸入事件順序")]
        public int Sort { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int CategoryId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }
    }
}

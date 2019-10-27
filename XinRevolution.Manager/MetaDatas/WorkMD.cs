using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas
{
    public class WorkMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入作品名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "作品名稱", Prompt = "請輸入作品名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入作品簡介")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "作品簡介", Prompt = "請輸入作品簡介")]
        public string Intro { get; set; }

        [Required(ErrorMessage = "請選擇資源檔案")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請上傳資源檔案")]
        public string ResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入作品順序")]
        [Display(Name = "作品順序", Prompt = "請輸入作品順序")]
        public int Sort { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請選擇控制器")]
        public string Controller { get; set; }
        
        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGRoleEquipmentMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入裝備名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "裝備名稱", Prompt = "請輸入裝備名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入裝備簡介")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "裝備簡介", Prompt = "請輸入裝備簡介")]
        public string Intro { get; set; }

        [Required(ErrorMessage = "請選擇資源")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "輪播資源連結", Prompt = "請選擇資源")]
        public string SlideResourceUrl { get; set; }

        [Required(ErrorMessage = "請選擇資源")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "展示資源連結", Prompt = "請選擇資源")]
        public string MainResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入裝備順序")]
        [Display(Name = "裝備順序", Prompt = "請輸入裝備順序")]
        public int Sort { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int RoleId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile SlideResourceFile { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile MainResourceFile { get; set; }
    }
}

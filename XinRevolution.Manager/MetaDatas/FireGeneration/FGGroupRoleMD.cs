using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGGroupRoleMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "請輸入角色名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "角色名稱", Prompt = "請輸入角色名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入角色簡介")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "角色簡介", Prompt = "請輸入角色簡介")]
        public string Intro { get; set; }

        [Required(ErrorMessage = "請選擇封面圖案(主)")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "封面圖案(主)", Prompt = "請選擇封面圖案(主)連結")]
        public string CoverMainResourceUrl { get; set; }

        [Required(ErrorMessage = "請選擇封面圖案(副)")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "封面圖案(副)", Prompt = "請選擇封面圖案(副)連結")]
        public string CoverViceResourceUrl { get; set; }

        [Required(ErrorMessage = "請選擇角色圖案(主)")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "角色圖案(主)", Prompt = "請選擇角色圖案(主)連結")]
        public string CharacterMainResourceUrl { get; set; }

        [Required(ErrorMessage = "請選擇角色圖案(副)")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "角色圖案(副)", Prompt = "請選擇角色圖案(副)連結")]
        public string CharacterViceResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入相關連結")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "相關連結", Prompt = "請輸入相關連結")]
        public string RelativeLinkUrl { get; set; }

        [Required(ErrorMessage = "請輸入角色順序")]
        [Display(Name = "角色順序", Prompt = "請輸入角色順序")]
        public int Sort { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int GroupId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile CoverMainResourceFile { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile CoverViceResourceFile { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile CharacterMainResourceFile { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile CharacterViceResourceFile { get; set; }
    }
}

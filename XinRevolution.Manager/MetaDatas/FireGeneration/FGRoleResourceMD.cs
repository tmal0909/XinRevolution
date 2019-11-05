using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XinRevolution.Database.Enum.FireGeneration;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGRoleResourceMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請選擇資源類型")]
        [Display(Name = "資源類型", Prompt = "請選擇資源類型")]
        public FGRoleResourceTypeEnum Type { get; set; }

        [Required(ErrorMessage = "請選擇資源")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請選擇資源")]
        public string ResourceUrl { get; set; }

        [Required(ErrorMessage = "請輸入資源順序")]
        [Display(Name = "資源順序", Prompt = "請輸入資源順序")]
        public int Sort { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int RoleId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }

        public List<SelectListItem> TypeOptions { get; set; }
    }
}

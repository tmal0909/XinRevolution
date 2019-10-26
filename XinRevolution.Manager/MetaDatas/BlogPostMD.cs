using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XinRevolution.Database.Enum;

namespace XinRevolution.Manager.MetaDatas
{
    public class BlogPostMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請選擇資源類型")]
        [Display(Name = "資源類型", Prompt = "請選擇資源類型")]
        public ReferenceTypeEnum ReferenceType { get; set; }

        [Required(ErrorMessage = "請輸入文字內文")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源內文", Prompt = "請輸入文字內文")]
        public string TextReferenceContent { get; set; }

        [Required(ErrorMessage = "請選擇資源檔案")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源內文", Prompt = "請選擇資源檔案")]
        public string MediaReferenceContent { get; set; }

        [Required(ErrorMessage = "請輸入內文順序")]
        [Display(Name = "內文順序", Prompt = "請輸入內文順序")]
        public int Sort { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int BlogId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }

        public List<SelectListItem> ReferenceTypeOptions { get; set; }
    }
}

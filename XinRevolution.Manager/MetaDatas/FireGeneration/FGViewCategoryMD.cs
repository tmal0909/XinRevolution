using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas.FireGeneration
{
    public class FGViewCategoryMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "請輸入分類名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "分類名稱", Prompt = "請輸入分類名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入分類順序")]
        [Display(Name = "分類順序", Prompt = "請輸入分類順序")]
        public int Sort { get; set; }
    }
}

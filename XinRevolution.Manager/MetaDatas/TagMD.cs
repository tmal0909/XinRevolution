using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas
{
    public class TagMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入標籤名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "標籤名稱", Prompt = "請輸入標籤名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請選擇開啟狀態")]
        [Display(Name = "開啟狀態", Prompt = "請選擇開啟狀態")]
        public bool Status { get; set; }

        public List<SelectListItem> StatusOptions { get; set; }
    }
}

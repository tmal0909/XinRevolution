using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas
{
    public class BlogTagMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int BlogId { get; set; }

        [Required(ErrorMessage = "請選擇標籤")]
        [Display(Name = "標籤", Prompt = "請選擇標籤")]
        public int TagId { get; set; }

        public List<SelectListItem> TagOptions { get; set; }
    }
}

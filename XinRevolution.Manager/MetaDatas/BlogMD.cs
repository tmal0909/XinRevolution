using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas
{
    public class BlogMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入部落格名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "部落格名稱", Prompt = "請輸入部落格名稱")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "請選擇發行日期")]
        [Display(Name = "發行日期", Prompt = "請選擇發行日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get; set; }
    }
}

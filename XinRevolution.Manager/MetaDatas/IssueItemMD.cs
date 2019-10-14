using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas
{
    public class IssueItemMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入議題標題")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "議題標題", Prompt = "請輸入議題標題")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "請選擇發行日期")]
        [Display(Name = "發行日期", Prompt = "請選擇發行日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "請上傳資源檔案")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請上傳資源檔案")]
        public string ResourceUrl { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "請輸入議題內文")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "議題內文", Prompt = "請輸議題內文")]
        public string Content { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int IssueId { get; set; }

        [Display(Name = "瀏覽資源")]
        public IFormFile ResourceFile { get; set; }
    }
}

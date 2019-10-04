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

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "請輸入議題內文")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "議題內文", Prompt = "請輸議題內文")]
        public string Content { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請輸入資源連結")]
        [StringLength(300, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "資源連結", Prompt = "請輸入資源連結")]
        public string ResourceUrl { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "請輸入發行日期")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "發行日期", Prompt = "請輸入發行日期")]
        public DateTime ReleaseDate { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "請攜帶外部鍵值")]
        public int IssueId { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif", ErrorMessage = "檔案類型錯誤")]
        [Display(Name = "選擇資源")]
        public IFormFile ResourceFile { get; set; }

        [Display(Name = "資源名稱")]
        public string ResourceName { get { return ResourceUrl; } set { } }
    }
}

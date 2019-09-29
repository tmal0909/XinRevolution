using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Manager.MetaDatas
{
    public class IssueMD
    {
        [HiddenInput]
        [Required(ErrorMessage = "請攜帶鍵值")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "請輸入議題名稱")]
        [StringLength(50, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "議題名稱", Prompt = "請輸入議題名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入議題簡介")]
        [StringLength(500, ErrorMessage = "資料長度過長，請重新輸入")]
        [Display(Name = "議題簡介", Prompt = "請輸入議題簡介")]
        public string Intro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XinRevolution.Manager.Models
{
    public static class ValidResourceTypeModel
    {
        public static List<string> Image = new List<string> { ".jpg", ".jpeg", ".png", ".svg", ".gif" };

        public static List<string> Video = new List<string> { ".mp4" };
    }
}

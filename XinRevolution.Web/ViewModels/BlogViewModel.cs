using System;
using System.Collections.Generic;
using XinRevolution.Database.Entity;

namespace XinRevolution.Web.ViewModels
{
    public class BlogViewModel
    {
        public IEnumerable<BlogEntity> Blogs { get; set; }

        public IEnumerable<TagEntity> Tags { get; set; }
    }
}

using System.Collections.Generic;
using XinRevolution.Database.Entity.FireGeneration;

namespace XinRevolution.Web.ViewModels.FireGeneration
{
    public class FireGenerationViewCategoryViewModel
    {
        public IEnumerable<FGViewCategoryEntity> Categories { get; set; }
    }
}

using System.Collections.Generic;
using XinRevolution.Database.Entity.FireGeneration;

namespace XinRevolution.Web.ViewModels.FireGeneration
{
    public class FireGenerationStoryLineViewModel
    {
        public IEnumerable<FGSeasonEntity> Stories { get; set; }
    }
}

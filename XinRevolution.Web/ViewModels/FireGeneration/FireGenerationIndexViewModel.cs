using System.Collections.Generic;
using XinRevolution.Database.Entity.FireGeneration;

namespace XinRevolution.Web.ViewModels.FireGeneration
{
    public class FireGenerationIndexViewModel
    {
        public IEnumerable<FGGroupEntity> FireGenerationGroups { get; set; }
    }
}

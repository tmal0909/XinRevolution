using System.ComponentModel.DataAnnotations;

namespace XinRevolution.Database.Enum.FireGeneration
{
    public enum FGRoleResourceTypeEnum
    {
        [Display(Name = "封面(主)")]
        Main1,

        [Display(Name = "封面(副)")]
        Main2,

        [Display(Name = "角色(主)")]
        Character1,

        [Display(Name = "角色(副)")]
        Character2
    }
}

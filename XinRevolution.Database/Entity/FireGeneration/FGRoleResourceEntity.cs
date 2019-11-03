using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinRevolution.Database.Enum.FireGeneration;

namespace XinRevolution.Database.Entity.FireGeneration
{
    public class FGRoleResourceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column(TypeName = "int", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public FGRoleResourceTypeEnum Type { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string ResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public int Sort { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int RoleId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UtcUpdateTime { get; set; }

        public FGGroupRoleEntity Role { get; set; }
    }
}

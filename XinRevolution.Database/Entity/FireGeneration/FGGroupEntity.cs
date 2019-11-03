using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XinRevolution.Database.Entity.FireGeneration
{
    public class FGGroupEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column(TypeName = "int", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string ResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Sort { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Note { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UtcUpdateTime { get; set; }

        public List<FGGroupRoleEntity> Roles { get; set; }
    }
}

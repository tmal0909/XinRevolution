using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XinRevolution.Database.Entity.FireGeneration
{
    public class FGRoleEquipmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column(TypeName = "int", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Intro { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string SlideResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string MainResourceUrl { get; set; }

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

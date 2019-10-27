using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XinRevolution.Database.Entity.FireGeneration
{
    public class FGGroupRoleEntity
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
        [Column(TypeName = "nvarchar(300)")]
        public string CoverMainResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string CoverViceResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string CharacterMainResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string CharacterViceResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string RelativeLinkUrl { get; set; }
        
        [Required]
        [Column(TypeName = "smallint")]
        public int Sort { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int GroupId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UtcUpdateTime { get; set; }

        public FGGroupEntity Group { get; set; }

        public List<FGRoleEquipmentEntity> Equipments { get; set; }
    }
}

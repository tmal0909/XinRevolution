using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XinRevolution.Database.Entity.FireGeneration
{
    public class FGSeasonChapterEntity
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
        [Column(TypeName = "int")]
        public int Offset { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string ResourceUrl { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int SeasonId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UtcUpdateTime { get; set; }

        public FGSeasonEntity Season { get; set; }

        public List<FGChapterComicEntity> Comics { get; set; }
    }
}

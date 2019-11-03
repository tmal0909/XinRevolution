using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinRevolution.Database.Enum;

namespace XinRevolution.Database.Entity
{
    public class BlogPostEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column(TypeName = "int", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public ReferenceTypeEnum ReferenceType { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string ReferenceContent { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public int Sort { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int BlogId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UtcUpdateTime { get; set; }
        
        public BlogEntity Blog { get; set; }
    }
}

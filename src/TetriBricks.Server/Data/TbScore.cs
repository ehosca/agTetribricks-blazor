using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TetriBricks.Server.Data;

[Table("TbScores")]
public class TbScore
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    public int ScoreValue { get; set; }

    public DateTime ScoreDate { get; set; }

    [MaxLength(100)]
    public string? EmailAddress { get; set; }
}

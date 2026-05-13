using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquarelaApi.Models;

[Table("tb_usuario")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [Column("nm_usuario")]
    [MaxLength(100)]
    public string NmUsuario { get; set; } = string.Empty;

    [Required]
    [Column("ds_emaill")]
    [MaxLength(100)]
    public string DsEmail { get; set; } = string.Empty;

    [Required]
    [Column("dm_ativo")]
    public bool DmAtivo { get; set; }

    [Required]
    [Column("ds_senha")]
    [MaxLength(100)]
    public string DsSenha { get; set; } = string.Empty;
}

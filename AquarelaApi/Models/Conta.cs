using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquarelaApi.Models;

[Table("tb_conta")]
public class Conta
{
    [Key]
    [Column("id_conta")]
    public int IdConta { get; set; }

    [Required]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [Column("nm_conta")]
    [MaxLength(100)]
    public string NmConta { get; set; } = string.Empty;

    [Required]
    [Column("nr_saldo")]
    public decimal NrSaldo { get; set; }
  
    [ForeignKey(nameof(IdUsuario))]
    public Usuario? Usuario { get; set; }
}

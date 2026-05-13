using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquarelaApi.Models;

[Table("tb_divida")]
public class Divida
{
    [Key]
    [Column("id_divida")]
    public int IdDivida { get; set; }

    [Required]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [Column("nm_divida")]
    [MaxLength(100)]
    public string NmDivida { get; set; } = string.Empty;

    [Required]
    [Column("dia_vencimento")]
    public int DiaVencimento { get; set; }

    [Required]
    [Column("dt_primeiro_vencimento")]
    public DateTime DtPrimeiroVencimento { get; set; }

    [Required]
    [Column("nr_parcelas")]
    public int NrParcelas { get; set; }

    [Required]
    [Column("nr_valor")]
    public decimal NrValor { get; set; }

    [ForeignKey(nameof(IdUsuario))]
    public Usuario? Usuario { get; set; }
}

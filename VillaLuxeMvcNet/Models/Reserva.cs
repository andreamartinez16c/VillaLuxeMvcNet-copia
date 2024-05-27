using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaLuxeMvcNet.Models
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        [Column("idreserva")]
        public int IdReserva { get; set; }

        [Column("idusuario")]
        public int IdUsuario { get; set; }

        [Column("idvilla")]
        public int IdVilla { get; set; }

        [Column("fechainicio")]
        public DateTime FechaInicio { get; set; }

        [Column("fechafinal")]
        public DateTime FechaFin { get; set; }

        [Column("preciototal")]
        /*[Display(Name = "Precio Total")]*/
        public decimal PrecioTotal { get; set; }

        [Column("estado")]
        public string Estado { get; set; }
    }
}

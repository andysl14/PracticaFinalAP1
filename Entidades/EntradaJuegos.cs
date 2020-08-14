using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PracticaFinalAP1.Entidades
{
    public class EntradaJuegos
    {
        [Key]
        public int EntradaId { get; set; }
        public int JuegoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        [ForeignKey("JuegoId")]
        public virtual Juegos juegos { get; set; }
    }
}

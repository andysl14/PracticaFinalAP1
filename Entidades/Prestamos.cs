using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PracticaFinalAP1.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public int AmigoId { get; set; }
        public string Observacion { get; set; }
        public double CantidadJuegos { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        [ForeignKey("PrestamoId")]
        public virtual List<PrestamosDetalle> Detalle { get; set; } = new List<PrestamosDetalle>();

        [ForeignKey("AmigoId")]
        public virtual Amigos amigos { get; set; }
    }
}

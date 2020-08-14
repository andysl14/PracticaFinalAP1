using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticaFinalAP1.Entidades
{
    public class Juegos
    {
        [Key]
        public int JuegoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public float Precio { get; set; }
        public int Existencia { get; set; }
    }
}

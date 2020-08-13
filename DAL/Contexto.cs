using Microsoft.EntityFrameworkCore;
using PracticaFinalAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticaFinalAP1.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Amigos> Amigos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\Practica.db");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RegistroPedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPedidos.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Suplidores> Suplidores { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Data/GPedidos.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Suplidores>().HasData(
                    new Suplidores() { SuplidorId = 1, Nombres = "Nestle" },
                    new Suplidores() { SuplidorId = 2, Nombres = "Lays" },
                    new Suplidores() { SuplidorId = 3, Nombres = "CocaCola" }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos() { ProductoId = 1, Descripcion = "Leche Condesada", Costo = 35, Inventario = 70 },
                new Productos() { ProductoId = 2, Descripcion = "Papitas Clasicas", Costo = 25, Inventario = 40 },
                new Productos() { ProductoId = 3, Descripcion = "Papitas con Limon", Costo = 25, Inventario = 40 }
                );
        }
    }
}

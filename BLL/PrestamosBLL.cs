using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PracticaFinalAP1.DAL;
using PracticaFinalAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PracticaFinalAP1.BLL
{
    public class PrestamosBLL
    {
        public static bool Insertar(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                foreach (var item in prestamos.Detalle)
                {
                    item.juegos.Existencia -= item.Cantidad;
                    contexto.Entry(item.juegos).State = EntityState.Modified;
                }

                contexto.Prestamos.Add(prestamos);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Prestamos.Any(p => p.PrestamoId == id);
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool Modificar(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"DELETE FROM PrestamosDetalle WHERE PrestamoId={prestamos.PrestamoId}");

                foreach(var item in prestamos.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(prestamos).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Guardar(Prestamos prestamos)
        {
            bool paso;

            if (!Existe(prestamos.PrestamoId))
                paso = Insertar(prestamos);
            else
                paso = Modificar(prestamos);

            return paso;
        }

        public static Prestamos Buscar(int id)
        {
            Prestamos prestamos = new Prestamos();
            Contexto contexto = new Contexto();

            try
            {
                prestamos = contexto.Prestamos
                    .Where(p => p.PrestamoId == id)
                    .Include(p => p.Detalle).ThenInclude(pr => pr.juegos)
                    .SingleOrDefault();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamos;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var prestamos = PrestamosBLL.Buscar(id);
                if (prestamos != null)
                {
                    contexto.Prestamos.Remove(prestamos);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch(Exception)
            {
                throw;

            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}

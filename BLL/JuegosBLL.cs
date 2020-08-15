using Microsoft.EntityFrameworkCore;
using PracticaFinalAP1.DAL;
using PracticaFinalAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;

namespace PracticaFinalAP1.BLL
{
    public class JuegosBLL
    {
        private static bool Insertar(Juegos juegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Juegos.Add(juegos);
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

        public static bool Modificar(Juegos juegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(juegos).State = EntityState.Modified;
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
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Juegos.Any(e => e.JuegoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool Guardar(Juegos juegos)
        {
            if (!Existe(juegos.JuegoId))
                return Insertar(juegos);
            else
                return Modificar(juegos);
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var juegos = contexto.Juegos.Find(id);
                if (juegos != null)
                {
                    contexto.Juegos.Remove(juegos);
                    paso = contexto.SaveChanges() > 0;
                }
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

        public static Juegos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Juegos juegos;

            try
            {
                juegos = contexto.Juegos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return juegos;
        }

        public static List<Juegos> GetList(Expression<Func<Juegos, bool>> criterio)
        {
            List<Juegos> lista = new List<Juegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Juegos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<Juegos> GetJuegos()
        {
            List<Juegos> lista = new List<Juegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Juegos.ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;

        }

        public static void EntradaJuegos(int id, int cantidad)
        {
            Juegos juegos = Buscar(id);

            juegos.Existencia += cantidad;

            Modificar(juegos);
        }

        public static void DisminuirEntradaJuegos(int id, int cantidad)
        {
            Juegos juegos = Buscar(id);

            juegos.Existencia -= cantidad;

            if (juegos.Existencia >= 0)
            {
                Modificar(juegos);
            }
            else
            {
                
                
                return;
            }
        }
    }
}

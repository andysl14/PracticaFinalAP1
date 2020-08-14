using Microsoft.EntityFrameworkCore;
using PracticaFinalAP1.DAL;
using PracticaFinalAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PracticaFinalAP1.BLL
{
    public class EntradasJuegosBLL
    {
        private static bool Insertar(EntradaJuegos entradajuegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.EntradaJuegos.Add(entradajuegos);
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
                encontrado = contexto.EntradaJuegos.Any(e => e.EntradaId == id);
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

        public static bool Modificar(EntradaJuegos entradajuegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(entradajuegos).State = EntityState.Modified;
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

        public static bool Guardar(EntradaJuegos entradajuegos)
        {
            if (!Existe(entradajuegos.EntradaId))
                return Insertar(entradajuegos);
            else
                return Modificar(entradajuegos);
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var entradajuegos = contexto.EntradaJuegos.Find(id);
                if (entradajuegos != null)
                {
                    contexto.EntradaJuegos.Remove(entradajuegos);
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

        public static EntradaJuegos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            EntradaJuegos entradajuegos;

            try
            {
                entradajuegos = contexto.EntradaJuegos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entradajuegos;
        }

        public static List<EntradaJuegos> GetList(Expression<Func<EntradaJuegos, bool>> criterio)
        {
            List<EntradaJuegos> lista = new List<EntradaJuegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradaJuegos.Where(criterio).ToList();
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

        public static List<EntradaJuegos> GetEntradaJuegos()
        {
            List<EntradaJuegos> lista = new List<EntradaJuegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradaJuegos.ToList();
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

    }
}

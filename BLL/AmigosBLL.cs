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
    public class AmigosBLL
    {
        private static bool Insertar(Amigos amigos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Amigos.Add(amigos);
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

        public static bool Modificar(Amigos amigos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(amigos).State = EntityState.Modified;
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
                encontrado = contexto.Amigos.Any(e => e.AmigoId == id);
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

        public static bool Guardar(Amigos amigos)
        {
            if (!Existe(amigos.AmigoId))
                return Insertar(amigos);
            else
                return Modificar(amigos);
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var amigos = contexto.Amigos.Find(id);
                if (amigos != null)
                {
                    contexto.Amigos.Remove(amigos);
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

        public static Amigos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Amigos amigos;

            try
            {
                amigos = contexto.Amigos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return amigos;
        }

        public static List<Amigos> GetList(Expression<Func<Amigos, bool>> criterio)
        {
            List<Amigos> lista = new List<Amigos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Amigos.Where(criterio).ToList();
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

        public static List<Amigos> GetAmigos()
        {
            List<Amigos> lista = new List<Amigos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Amigos.ToList();
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
    }
}

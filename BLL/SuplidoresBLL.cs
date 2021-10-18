using RegistroPedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroPedidos.DAL;

namespace RegistroPedidos.BLL
{
    public class SuplidoresBLL
    {
        public static bool Existe(int id)
        {
            var contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Suplidores.Any(e => e.SuplidorId == id);
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

        public static bool Insertar(Suplidores suplidores)
        {
            var contexto = new Contexto();
            bool insertado = false;

            try
            {
                contexto.Suplidores.Add(suplidores);
                insertado = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return insertado;
        }

        public static bool Modificar(Suplidores suplidores)
        {
            var contexto = new Contexto();
            bool modificado = false;

            try
            {
                contexto.Entry(suplidores).State = EntityState.Modified;
                modificado = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return modificado;
        }

        public static bool Guardar(Suplidores suplidores)
        {
            if (!Existe(suplidores.SuplidorId))
                return Insertar(suplidores);
            else
                return Modificar(suplidores);
        }

        public static bool Eliminar(int id)
        {
            var contexto = new Contexto();
            bool eliminado = false;

            try
            {
                var suplidores = contexto.Suplidores.Find(id);

                if (suplidores != null)
                {
                    contexto.Suplidores.Remove(suplidores);
                    eliminado = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return eliminado;
        }

        public static Suplidores Buscar(int SuplidorId)
        {
            Contexto contexto = new Contexto();
            Suplidores persona;

            try
            {
                persona = contexto.Suplidores.Where(p => p.SuplidorId == SuplidorId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return persona;
        }

        public static List<Suplidores> GetList(Expression<Func<Suplidores, bool>> criterio)
        {
            List<Suplidores> lista = new List<Suplidores>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Suplidores.Where(criterio).AsNoTracking().ToList();
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

        public static List<Suplidores> GetPersonas()
        {
            List<Suplidores> lista = new List<Suplidores>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Suplidores.ToList();
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

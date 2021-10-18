using RegistroPedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroPedidos.DAL;
using System.Threading.Tasks;

namespace RegistroPedidos.BLL
{
    public class ProductosBLL
    {
        public static bool Existe(int id)
        {
            var contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Productos.Any(e => e.ProductoId == id);
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

        public static bool Insertar(Productos productos)
        {
            var contexto = new Contexto();
            bool insertado = false;

            try
            {
                contexto.Productos.Add(productos);
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

        public static bool Modificar(Productos productos)
        {
            var contexto = new Contexto();
            bool modificado = false;

            try
            {
                contexto.Entry(productos).State = EntityState.Modified;
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

        public static bool Guardar(Productos productos)
        {
            if (!Existe(productos.ProductoId))
                return Insertar(productos);
            else
                return Modificar(productos);
        }

        public static bool Eliminar(int id)
        {
            var contexto = new Contexto();
            bool eliminado = false;

            try
            {
                var productos = contexto.Productos.Find(id);

                if (productos != null)
                {
                    contexto.Productos.Remove(productos);
                    eliminado = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return eliminado;
        }

        public static Productos Buscar(int ProductoId)
        {
            Contexto contexto = new Contexto();
            Productos persona;

            try
            {
                persona = contexto.Productos.Where(p => p.ProductoId == ProductoId).FirstOrDefault();
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

        public static List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {
            List<Productos> lista = new List<Productos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Productos.Where(criterio).AsNoTracking().ToList();
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

        public static List<Productos> GetPersonas()
        {
            List<Productos> lista = new List<Productos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Productos.ToList();
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

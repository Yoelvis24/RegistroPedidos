using RegistroPedidos.Models;
using RegistroPedidos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroPedidos.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            if (!Existe(orden.OrdenId))
            {
                return Insertar(orden);
            }
            else
            {
                return Modificar(orden);
            }
        }

        public static bool Existe(int Id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ordenes.Any(e => e.OrdenId == Id);
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

        public static bool Insertar(Ordenes orden)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Ordenes.Add(orden);
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

        public static bool Modificar(Ordenes orden)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete from OrdenesDetalle where OrdenId = {orden.OrdenId}");

                foreach (var anterior in orden.OrdenesDetalle)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(orden).State = EntityState.Modified;

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

        public static bool Eliminar(int OrdenId)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var orden = contexto.Ordenes.Include(e => e.OrdenesDetalle).Where(m => m.OrdenId == OrdenId).FirstOrDefault();

                if (orden != null)
                {
                    contexto.Ordenes.Remove(orden);
                    paso = contexto.SaveChanges() > 0;
                    if (paso)
                    {
                        contexto.Database.ExecuteSqlRaw($"Delete from OrdenesDetalle where OrdenId = {OrdenId}");
                    }
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

        public static Ordenes Buscar(int OrdenId)
        {
            Contexto contexto = new Contexto();
            Ordenes orden;

            try
            {
                orden = contexto.Ordenes.Include(e => e.OrdenesDetalle).Where(m => m.OrdenId == OrdenId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return orden;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> lista = new List<Ordenes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Ordenes.Where(criterio).AsNoTracking().ToList();
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

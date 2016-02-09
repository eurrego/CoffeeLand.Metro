using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Modelo
{
    public class MCompra
    {
        #region Singleton

        private static MCompra instance;

        public static MCompra GetInstance()
        {
            if (instance == null)
            {
                instance = new MCompra();
            }

            return instance;
        }

        #endregion


        public List<Proveedor> ConsultarProveedor()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Proveedor> lista = new List<Proveedor>()
                {
                    new Proveedor
                    {
                        Nit = 0.ToString(),
                        NombreProveedor = "Proveedor"
                    }
                };

                var query = lista.Union(from c in entity.Proveedor
                                        where c.EstadoProveedor == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<TipoInsumo> ConsultarTipoInsumo()
        {
            using (var entity = new DBFincaEntities())
            {
                List<TipoInsumo> lista = new List<TipoInsumo>()
                {
                    new TipoInsumo
                    {
                        idTipoInsumo = 0,
                        NombreTipoInsumo = "Tipo insumo..."
                    }
                };

                var query = lista.Union(from c in entity.TipoInsumo
                                        where c.EstadoTipoInsumo == "A"
                                        select c);

                return query.ToList();
            }
        }

        public List<Insumo> ConsultarInsumo(byte idTipoInsumo)
        {
            using (var entity = new DBFincaEntities())
            {
                List<Insumo> lista = new List<Insumo>()
                {
                    new Insumo
                    {
                        idInsumo = 0,
                        NombreInsumo = "Tipo insumo..."
                    }
                };

                var query = lista.Union(from c in entity.Insumo
                                        where c.EstadoInsumo == "A" && c.idTipoInsumo == idTipoInsumo
                                        select c);

                return query.ToList();
            }
        }

        public string RegistroCompra(string nit, DateTime fecha, int numeroFactura)
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.RegistrarCompra(nit, fecha, numeroFactura).First();
                return rpta.Mensaje;
            }
        }

        public void RegistroDetalleCompra(DataTable DetalleCompra)
        {

            using (var entity = new DBFincaEntities())
            {
                entity.SP_InsertarDetalleCompra(DetalleCompra);

            }
        }

    }
}

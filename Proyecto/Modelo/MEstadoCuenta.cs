using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MEstadoCuenta
    {
        #region Singleton

        private static MEstadoCuenta instance;

        public static MEstadoCuenta GetInstance()
        {
            if (instance == null)
            {
                instance = new MEstadoCuenta();
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


        public object ConsultaCompraProveedor(string nit)
        {

            using (var entity = new DBFincaEntities())
            {

                var rpst = entity.ComprasProveedor(nit);

                return rpst.ToList();
            }
        }

        public object ConsultaDetalleCompra(int idCompra)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Compra_Insumo
                            join i in entity.Insumo on c.idInsumo equals i.idInsumo
                            where c.idCompra == idCompra
                            select new
                            {

                                nombre = i.NombreInsumo,
                                cantidad = c.Cantidad,
                                valor = c.Precio,
                                subtotal = c.Precio * c.Cantidad
                            };

                return query.ToList();

            }
        }


        public string RegistroAbono(int idCompra, decimal valor, DateTime fecha, decimal total)
        {

            using (var entity = new DBFincaEntities())
            {

                var rpst = entity.AbonoCompraProveedor(idCompra, valor, fecha, total).First();

                return rpst.Mensaje;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MProveedor
    {
        #region Singleton

        private static MProveedor instance;

        public static MProveedor GetInstance()
        {
            if (instance == null)
            {
                instance = new MProveedor();
            }

            return instance;
        }

        #endregion

        public List<Proveedor> ConsultarProveedor()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Proveedor
                            where c.EstadoProveedor == "A"
                            select c;

                return query.ToList();
            }
        }


        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Proveedor> ConsultarParametroProveedor(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Proveedor
                            where c.EstadoProveedor == "A" && c.NombreProveedor.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }


        public string GestionProveedor(string nit, string nombreProveedor, string telefono, string direccionProveedor, string tipoDocumento, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionProveedor(nit, nombreProveedor, telefono, direccionProveedor, tipoDocumento, opcion).First();
                return rpta.Mensaje;
            }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MProducto
    {
        #region Singleton

        private static MProducto instance;

        public static MProducto GetInstance()
        {
            if (instance == null)
            {
                instance = new MProducto();
            }

            return instance;
        }

        #endregion

        public List<Producto> ConsultarProducto()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "A"
                            select c;
                return query.ToList();
            }

        }

        public List<Producto> ConsultarParametroProducto(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Producto
                            where c.EstadoProducto == "A" && c.NombreProducto.Contains(parametro)
                            select c;

                return query.ToList();
            }

        }

        public string GestionProducto(string NombreProducto, string Descripcion, byte idProducto, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionProducto(NombreProducto, Descripcion, idProducto, opcion).First();
                return rpta.Mensaje;
            }
        }


    }
}

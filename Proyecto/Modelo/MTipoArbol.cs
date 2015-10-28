using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MTipoArbol
    {

        #region Singleton

        private static MTipoArbol instance;

        public static MTipoArbol GetInstance()
        {
            if (instance == null)
            {
                instance = new MTipoArbol();
            }

            return instance;
        }

        #endregion

        public object registrarTipoArbol(String NombreArbol, String Descripcion, int idTipoArbol, int opc)
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionTipoArboles(NombreArbol, Descripcion, idTipoArbol, opc);
                var mensaje = string.Empty;

                //foreach (var item in rpta)
                //{
                //    mensaje += item.Mensaje;
                //}

                return rpta;
            }

        }

        public List<TipoArbol> buscarTipoArbol(String NombreArbol)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoArbol
                            where c.EstadoTipoArbol == "A" && c.NombreTipoArbol.Contains(NombreArbol)
                            select c;

                return query.ToList();
            }
        }

        public List<TipoArbol> consultarTipoArbol()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoArbol
                            where c.EstadoTipoArbol == "A"
                            select c;
                return query.ToList();
            }
        }

    }
}

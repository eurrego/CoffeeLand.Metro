using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MTipoInsumo
    {
        #region Singleton

        private static MTipoInsumo instance;

        public static MTipoInsumo GetInstance()
        {
            if (instance == null)
            {
                instance = new MTipoInsumo();
            }

            return instance;
        }

        #endregion

        public List<TipoInsumo> ConsultarTipoInsumo()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoInsumo
                            where c.EstadoTipoInsumo == "A"
                            select c;
                return query.ToList();
            }

        }

        public List<TipoInsumo> ConsultarParametroTipoInsumo(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.TipoInsumo
                            where c.EstadoTipoInsumo == "A" && c.NombreTipoInsumo.Contains(parametro)
                            select c;

                return query.ToList();
            }

        }

        public string GestionTipoInsumo(string nombre, string descripcion, int id, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionTipoInsumo(nombre, descripcion, id, opcion);

                var mensaje = string.Empty;
                //foreach (var item in rpta)
                //{
                //    mensaje += item.Mensaje;
                //}

                return rpta.ToString();
            }
        }
    }
}

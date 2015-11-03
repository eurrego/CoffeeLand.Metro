using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MInsumo
    {
        #region Singleton

        private static MInsumo instance;

        public static MInsumo GetInstance()
        {
            if (instance == null)
            {
                instance = new MInsumo();
            }

            return instance;
        }

        #endregion

        public List<Insumo> ConsultarInsumo()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Insumo
                            where c.EstadoInsumo == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Insumo> ConsultarParametroInsumo(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Insumo
                            where c.EstadoInsumo == "A" && c.NombreInsumo.Contains(parametro)
                            select c;

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
                        NombreTipoInsumo = "Seleccione un Tipo de Insumo...",
                        Descripcion = "",
                        EstadoTipoInsumo= ""
                    } 
                };

                var query = lista.Union(from c in entity.TipoInsumo
                                        where c.EstadoTipoInsumo == "A"
                                        select c);
                return query.ToList();
            }
        }

        public string GestionInsumo(byte IdTipoInsumo, string nombre, string descripcion, string marca, string unidaMedida, int id, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionInsumo(IdTipoInsumo, nombre, descripcion, marca, unidaMedida, id, opcion).First();
                return rpta.Mensaje;
            }
        }


    }
}

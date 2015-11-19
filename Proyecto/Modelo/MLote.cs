using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MLote
    {
        #region Singleton

        private static MLote instance;

        public static MLote GetInstance()
        {
            if (instance == null)
            {
                instance = new MLote();
            }

            return instance;
        }

        #endregion

        public List<TipoArbol> ConsultarEmpleado()
        {
            using (var entity = new DBFincaEntities())
            {
                List<TipoArbol> lista = new List<TipoArbol>()
                {
                    new TipoArbol
                    {
                        idTipoArbol = 0,
                        NombreTipoArbol = "Seleccione un Tipo de Arbol..."
                    }
                };

                var query = lista.Union(from c in entity.TipoArbol
                                        where c.EstadoTipoArbol == "A"
                                        select c);

                return query.ToList();
            }
        }




    }
}

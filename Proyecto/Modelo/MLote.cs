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

        public List<TipoArbol> ConsultarTipoArbol()
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

        public List<Lote> ConsultarLotes()
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "A"
                            select c;

                return query.ToList();
            }
        }

        public List<Lote> ConsultarParametroLote(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Lote
                            where c.EstadoLote == "A" && c.NombreLote.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionLote(string NombreLote, string observaciones, string cuadras, int idLote, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionLotes(NombreLote, observaciones, cuadras, idLote, opcion).First();
                return rpta.Mensaje;
            }
        }


    }
}

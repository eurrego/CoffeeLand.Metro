using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MArbol
    {
        #region Singleton

        private static MArbol instance;

        public static MArbol GetInstance()
        {
            if (instance == null)
            {
                instance = new MArbol();
            }

            return instance;
        }

        #endregion

        public List<Lote> ConsultarLote()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Lote> lista = new List<Lote>()
                {
                    new Lote
                    {
                        idLote = 0,
                        NombreLote = "Seleccione un Lote..."
                    }
                };

                var query = lista.Union(from c in entity.Lote
                                        where c.EstadoLote == "A"
                                        select c);

                return query.ToList();
            }
        }

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
    }
}

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

        public object ConsultarArboles()
        {
            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.Arboles join t in entity.TipoArbol on c.idTIpoArbol equals t.idTipoArbol
                            select new {
                                idTipoArbol = c.idTIpoArbol,
                                NombreTipoArbol = t.NombreTipoArbol,
                                Cantidad = c.Cantidad
                            };

                return query.ToList();
            }
        }

        public string gestionArboles(short idLote, byte idTipoArbol, int cantidad, DateTime fecha)
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionArboles(idLote, idTipoArbol, cantidad, fecha).First();
                return rpta.Mensaje;
            }
        }

    }
}

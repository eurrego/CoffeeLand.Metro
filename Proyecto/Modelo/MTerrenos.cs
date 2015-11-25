using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MTerrenos
    {

        #region Singleton

        private static MTerrenos instance;

        public static MTerrenos GetInstance()
        {
            if (instance == null)
            {
                instance = new MTerrenos();
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
                        NombreLote = "Seleccione un Lote",
                    }
                };

                var query = lista.Union(from c in entity.Lote
                                        where c.EstadoLote == "A"
                                        select c);
                return query.ToList();
            }

        }

        public List<Labor> ConsultarLabor()
        {
            using (var entity = new DBFincaEntities())
            {

                List<Labor> lista = new List<Labor>()
                {
                    new Labor
                    {
                        idLabor = 0,
                        NombreLabor = "Seleccione una Labor",
                    }
                };

                var query = lista.Union(from c in entity.Labor
                                        where c.EstadoLabor == "A"
                                        select c);
                return query.ToList();
            }

        }

        public List<Insumo> ConsultarInsumo()
        {
            using (var entity = new DBFincaEntities())
            {

                List<Insumo> lista = new List<Insumo>()
                {
                    new Insumo
                    {
                        idInsumo = 0,
                        NombreInsumo = "Seleccione un Insumo",
                    }
                };

                var query = lista.Union(from c in entity.Insumo
                                        where c.EstadoInsumo == "A"
                                        select c);
                return query.ToList();
            }

        }



    }
}

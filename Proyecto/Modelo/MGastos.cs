using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MGastos
    {
        #region Singleton

        private static MGastos instance;

        public static MGastos GetInstance()
        {
            if (instance == null)
            {
                instance = new MGastos();
            }

            return instance;
        }

        #endregion

        public List<Concepto> ConsultarConcepto()
        {
            using (var entity = new DBFincaEntities())
            {

                List<Concepto> lista = new List<Concepto>()
                {
                    new Concepto
                    {
                        idConcepto = 0,
                        NombreConcepto = "Seleccione un concepto...",
                    }
                };

                var query = lista.Union(from c in entity.Concepto
                                        where c.EstadoConcepto == "A"
                                        select c);
                return query.ToList();
            }

        }


    }
}

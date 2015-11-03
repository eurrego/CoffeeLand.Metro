using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MConcepto
    {
        #region Singleton

        private static MConcepto instance;

        public static MConcepto GetInstance()
        {
            if (instance == null)
            {
                instance = new MConcepto();
            }

            return instance;
        }

        #endregion

        public List<Concepto> ConsultarConcepto()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Concepto
                            where c.EstadoConcepto == "A"
                            select c;
                return query.ToList();
            }
        }


        //este metodo consulta por medio de la cadena de texto ingresada 
        public List<Concepto> ConsultarParametroConcepto(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Concepto
                            where c.EstadoConcepto == "A" && c.NombreConcepto.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

        public string GestionConcepto(string NombreConcepto, string Descripcion, byte idConcepto, int opcion)
        {
            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionConcepto(NombreConcepto, Descripcion, idConcepto, opcion).First();
                return rpta.Mensaje;
            }
        }
    }
}

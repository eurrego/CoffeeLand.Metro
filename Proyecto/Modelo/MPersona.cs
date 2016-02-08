using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MPersona
    {
        #region Singleton

        private static MPersona instance;

        public static MPersona GetInstance()
        {
            if (instance == null)
            {
                instance = new MPersona();
            }

            return instance;
        }

        #endregion

        public List<Persona> ConsultarPersona()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A"
                            select c;

                return query.ToList();
            }

        }

        public List<Persona> ConsultarParametroPersona(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A" && c.NombrePersona.Contains(parametro)
                            select c;

                return query.ToList();
            }
        }

       




        public string GestionPersona(string nombre, string genero, string telefono, DateTime fechaNacimiento, int id, int opcion, string TipoDocumento, string TipoContrato)
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionPersona(nombre, genero, telefono, fechaNacimiento, id, opcion, TipoDocumento, TipoContrato).First();
                return rpta.Mensaje;
            }
        }
    }


}

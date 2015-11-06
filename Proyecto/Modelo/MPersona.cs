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

        public List<TipoDocumento> ConsultarTipoDocumento()
        {
            using (var entity = new DBFincaEntities())
            {

                List<TipoDocumento> lista = new List<TipoDocumento>()
                {
                    new TipoDocumento
                    {
                        idTipoDocumento = 0,
                        NombreTipoDocumento = "Seleccione un Tipo de Documento...",
                    }
                };

                var query = lista.Union(from c in entity.TipoDocumento
                                        select c);
                return query.ToList();
            }

        }

        public List<TipoContratoPersona> ConsultarTipoContrato()
        {
            using (var entity = new DBFincaEntities())
            {
                List<TipoContratoPersona> lista = new List<TipoContratoPersona>()
                {
                    new TipoContratoPersona
                    {
                        idTipoContratoPersona = 0,
                        NombreTipoContratoPersona = "Seleccione un Tipo de Contrato...",
                    }
                };

                var query = lista.Union(from c in entity.TipoContratoPersona
                                        select c);
                return query.ToList();
            }

        }

        public string GestionPersona(string nombre, string genero, string telefono, DateTime fechaNacimiento, int id, int opcion, byte idTipoDocumento, byte idTipoContrato)
        {
         
                using (var entity = new DBFincaEntities())
                {
                    var rpta = entity.gestionPersona(nombre, genero, telefono, fechaNacimiento, id, opcion, idTipoDocumento, idTipoContrato).First();
                    return rpta.Mensaje;
                }
          
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MPrestamosEmpleados
    {
        #region Singleton

        private static MPrestamosEmpleados instance;

        public static MPrestamosEmpleados GetInstance()
        {
            if (instance == null)
            {
                instance = new MPrestamosEmpleados();
            }

            return instance;
        }

        #endregion

        public List<Persona> ConsultarEmpleado()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Persona> lista = new List<Persona>()
                {
                    new Persona
                    {
                        DocumentoPersona = 0.ToString(),
                        NombrePersona = "Empleado..."
                    }
                };

                var query = lista.Union(from c in entity.Persona
                            where c.EstadoPersona == "A"
                            select c);

                return query.ToList();
            }
        }

        public List<Persona> ConsultarParametroPersona(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Persona
                            where c.EstadoPersona == "A" && c.DocumentoPersona.Equals(parametro)
                            select c;

                return query.ToList();
            }
        }

        public List<DeudaPersona> ConsultarDeudaEmpleado(string parametro)
        {
            using (var entity = new DBFincaEntities())
            {

                var query = from c in entity.DeudaPersona
                            where c.EstadoCuenta == "D" && c.DocumentoPersona.Equals(parametro)
                            select c;

                return query.ToList();
            }
        }

        public DateTime ConsultarFecha(string parametro)
        {

            using (var entity = new DBFincaEntities())
            {

                var cantidad = (from c in entity.DeudaPersona
                             where c.DocumentoPersona.Equals(parametro)
                             select c.Fecha).Count();

                if (cantidad == 0)
                {
                    return DateTime.Now;
                }
                else
                {
                    var query = (from c in entity.DeudaPersona
                                 where c.DocumentoPersona.Equals(parametro)
                                 select c.Fecha).Max();
                    return query;
                }                   
            }
        }

        public string insercionDeudaEmpleado(string documento, decimal valor , DateTime fecha, string descripcion )
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.insercionDeudaEmpleado(documento, valor, fecha, descripcion).First();
                return rpta.Mensaje;
            }

        }
    }
}

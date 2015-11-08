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

        //public object ConsultarDeudaEmpleado(string parametro)
        //{
        //    using (var entity = new DBFincaEntities())
        //    {
        //        var query = entity.DeudaPersona.Where(i => i.DocumentoPersona.Contains(parametro)).Select(t => new
        //        {

        //            t.Valor,
        //            t.Fecha,
        //            t.Descripcion,
        //            t.EstadoCuenta
        //        }).Where(i => i.EstadoCuenta.Equals("D"));

        //        return query.ToList();
        //    }
        //}
    }
}

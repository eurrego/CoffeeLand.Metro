using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MLabores
    {
        #region Singleton

        private static MLabores instance;

        public static MLabores GetInstance()
        {
            if (instance == null)
            {
                instance = new MLabores();
            }

            return instance;
        }

        #endregion


        public List<Labor> consultarLabor()
        {

            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor
                            where c.EstadoLabor == "A"
                            select c;

                return query.ToList();
            }

        }


        public List<Labor> buscarLabor(string NombreLabor)
        {
            using (var entity = new DBFincaEntities())
            {
                var query = from c in entity.Labor
                            where c.EstadoLabor == "A" && c.NombreLabor.Contains(NombreLabor)
                            select c;

                return query.ToList();
            }
        }


        public string GestionLabor( string NombreLabor, bool ModificaArbol, bool RequiereInsumo, string Descripcion, int idLabor, int opc)
        {

            using (var entity = new DBFincaEntities())
            {
                var rpta = entity.gestionLabor(NombreLabor, ModificaArbol, RequiereInsumo, Descripcion, idLabor, opc).First();
                return rpta.Mensaje;
            }

        }

        

       
    }
}

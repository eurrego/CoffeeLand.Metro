using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MPagos
    {
        #region Singleton

        private static MPagos instance;

        public static MPagos GetInstance()
        {
            if (instance == null)
            {
                instance = new MPagos();
            }

            return instance;
        }

        #endregion



        public object ConsultarSalario(int opc)
        {
            using (var entity = new DBFincaEntities())
            {



                var query = entity.PagosPersona(opc);




                return query.ToList();

            }

        }

        public object DetalleSalario(int cedula)
        {
            using (var entity = new DBFincaEntities())
            {



                var query = entity.DetalleSalario(cedula);




                return query.ToList();

            }

        }



        public void insertarMultiplesSalarios(DataTable dt, int opc)
        {
            using (var entity = new DBFincaEntities())
            {

                if (opc == 1)//permanente
                {



                    entity.SP_InsertMultiplesSalariosPersonaPermanente(dt);
                }
                else if (opc == 2)
                {
                    entity.SP_InsertMultiplesSalariosPersonaTemporal(dt);
                }





            }

        }



    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MreporteGastos
    {
        #region Singleton

        private static MreporteGastos instance;

        public static MreporteGastos GetInstance()
        {
            if (instance == null)
            {
                instance = new MreporteGastos();
            }

            return instance;
        }

        #endregion



        public object funcionreportegastos(DateTime a, DateTime b)
        {
            using (var entity = new DBFincaEntities())
            {


                var query = entity.SP_CONSULTA_EGRESO(a,b);
              
                
                return query.ToList();

            }

        }





    }
}

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



        public List<SalarioPersonaTemporal> ConsultarSalario()
        {
            using (var entity = new DBFincaEntities())
            {
                ////var deuda = from c in entity.SalarioPersonaTemporal
                ////            join Pe in entity.DeudaPersona on c.DocumentoPersona equals Pe.DocumentoPersona
                ////            group Pe by Pe.DocumentoPersona into gr
                ////            select new
                ////            {
                ////                docudeu = gr.Key,
                ////                deuda = gr.Sum(su => su.Valor)
                ////            };


                //var query = from SalTemp in entity.SalarioPersonaTemporal
                //            join Pe in entity.Persona on SalTemp.DocumentoPersona equals Pe.DocumentoPersona
                //            //join deu in deuda on Pe.DocumentoPersona equals deu.docudeu
                //            where SalTemp.EstadoCuenta == "D"

                //            //            group Pe by Pe.DocumentoPersona into grupo
                //            //            select new

                //            //            {
                //            //                doc = grupo.Key,
                //            //                nom = (from per in entity.Persona where per.DocumentoPersona == grupo.Key select per.NombrePersona),
                //            //                // pagar = g.Sum(s => s.Cantidad * s.Valor),


                //            //            };

                //            group SalTemp by new
                //{
                //    doc = SalTemp.DocumentoPersona,
                //    nom = Pe.NombrePersona,
                //    pagar = (SalTemp.Cantidad * SalTemp.Valor),
                //    deuda = (from c in entity.SalarioPersonaTemporal
                //             join Pe in entity.DeudaPersona on c.DocumentoPersona equals Pe.DocumentoPersona
                //             select (long)c.Valor).Sum()
                //} into g
                //select new { doc = g.Key.doc, nom = g.Key.nom, pagar = g.Key.pagar, deuda = g.Key.deuda };



                ////


                ////deuda = (from c in entity.SalarioPersonaTemporal
                //// join Pe in entity.DeudaPersona on c.DocumentoPersona equals Pe.DocumentoPersona
                //// select (long)c.Valor).Sum()


            }
        }  
        
    
        


        
    }
}

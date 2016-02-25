using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Modelo
{
    public class MFinca
    {

        #region Singleton

        private static MFinca instance;

        

      

        public static MFinca GetInstance()
        {
            if (instance == null)
            {
                instance = new MFinca();
            }

            return instance;
        }

        #endregion


        public void registrarFinca()
        {

        }


        public List<Departamento> ConsultarDepartamento()
        {
            using (var entity = new DBFincaEntities())
            {
                List<Departamento> lista = new List<Departamento>()
                {
                    new Departamento
                    {
                        idDepartamento = 0,
                        NombreDepartamento = "Seleccione un Departamento"
                    }
                };

                var query = lista.Union(from c in entity.Departamento
                                        select c);

                return query.ToList();
            }
        }


        public List<Municipio> ConsultarMunicipios(int departamento)
        {
            using (var entity = new DBFincaEntities())
            {
                List<Municipio> lista = new List<Municipio>()
                {
                    new Municipio
                    {
                        idMunicipio = 0,
                        NombreMunicipio = "Seleccione un Municipio"
                    }
                };

                var query = lista.Union(from c in entity.Municipio
                                        where c.idDepartamento == departamento
                                        select c);

                return query.ToList();
            }
        }


        public void modificarFinca(string nombreFinca, string propietario, int idMunicipio, string vereda, string telefono, string hectareas)

        {

            using (var entity = new DBFincaEntities())
            {

                entity.ModificarFinca(nombreFinca, propietario, idMunicipio, vereda, telefono, hectareas);


            }


        }


        //public List<Finca> ConsultarFinca()
        //{
        //    using (var entity = new DBFincaEntities())
        //    {


        //        var query = from c in entity.Finca
        //                    select c;

        //        return query.ToList();
        //    }
        //}

        //private string connectionString =
        //  "Data Source=DESKTOP-QI4E0BK;Initial Catalog=Usuarios;persist security info=True;"
        //  + "user id=sa;password=123";


        //private SqlCommand comman;

        //private SqlConnection conn;
        //public void Configuracion()

        //{


        //    using (conn = new SqlConnection(connectionString))

        //    {

        //        string query = "	Use Master" +
        //                            "EXEC master.dbo.sp_configure" + "'show advanced options', 1" +
        //                            "RECONFIGURE WITH OVERRIDE" +
        //                            "GO" +
        //                            "EXEC master.dbo.sp_configure" + "'xp_cmdshell', 1" +
        //                            "RECONFIGURE WITH OVERRIDE" +
        //                            "GO";


        //         comman = new SqlCommand(query, conn);


        //        conn.Open();
        //        try
        //        {

        //            comman.ExecuteNonQuery();

        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }

        //        conn.Close();

        //    }

        //}

        //public void CrearBaseDatos(string DataBase)

        //{

        //    using (conn = new SqlConnection(connectionString))


        //    {

        //        comman = new SqlCommand("CrearBaseDatos", conn);

        //    }


        //}

    }
}

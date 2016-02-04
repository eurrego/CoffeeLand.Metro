using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public partial class DBFincaEntities : DbContext
    {

        public virtual int SP_InsertMultiplesGastos(DataTable dtDatos)
        {
            var datos = new SqlParameter("DTgastos", SqlDbType.Structured);
            datos.Value = dtDatos;
            datos.TypeName = "dbo.RegistrarGasto";

            string command = "EXEC SP_InsertMultiplesGastos @DTgastos";

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreCommand(command, datos);
        }
   

    public virtual int SP_InsumoLaborLote(DataTable dtDatos)
    {
        var datos = new SqlParameter("DTInsumo", SqlDbType.Structured);
        datos.Value = dtDatos;
        datos.TypeName = "dbo.InsumoLaborLote";

        string command = "EXEC InsercionInsumoLaborLote @DTInsumo";

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreCommand(command, datos);
    }

        public virtual int SP_SalariosEmpleados(DataTable dtDatos)
        {
            var datos = new SqlParameter("DTsalario", SqlDbType.Structured);
            datos.Value = dtDatos;
            datos.TypeName = "dbo.SalarioPeronaTemporal";

            string command = "EXEC SalariosEmpleados @DTsalario";

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreCommand(command, datos);
        }

        public virtual int SP_InsertarDetalleCompra(DataTable dtDatos)
        {
            var datos = new SqlParameter("dtDetalleCompra", SqlDbType.Structured);
            datos.Value = dtDatos;
            datos.TypeName = "dbo.DetalleCompra";

            string command = "EXEC InsertarDetalleCompra @dtDetalleCompra";

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreCommand(command, datos);
        }

    }
}

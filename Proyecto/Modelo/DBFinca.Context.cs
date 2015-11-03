﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modelo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBFincaEntities : DbContext
    {
        public DBFincaEntities()
            : base("name=DBFincaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AbonoCompra> AbonoCompra { get; set; }
        public virtual DbSet<AbonoDeuda> AbonoDeuda { get; set; }
        public virtual DbSet<AbonoEgreso> AbonoEgreso { get; set; }
        public virtual DbSet<Arboles> Arboles { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Compra_Insumo> Compra_Insumo { get; set; }
        public virtual DbSet<Concepto> Concepto { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<DeudaPersona> DeudaPersona { get; set; }
        public virtual DbSet<Egreso> Egreso { get; set; }
        public virtual DbSet<Finca> Finca { get; set; }
        public virtual DbSet<Insumo> Insumo { get; set; }
        public virtual DbSet<Labor> Labor { get; set; }
        public virtual DbSet<Labor_Lote> Labor_Lote { get; set; }
        public virtual DbSet<LaborLote_Insumo> LaborLote_Insumo { get; set; }
        public virtual DbSet<Lote> Lote { get; set; }
        public virtual DbSet<MovimientoProduccion> MovimientoProduccion { get; set; }
        public virtual DbSet<MovimientosArboles> MovimientosArboles { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Produccion> Produccion { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<RegistroPago> RegistroPago { get; set; }
        public virtual DbSet<RegistroPago_DeudaPersona> RegistroPago_DeudaPersona { get; set; }
        public virtual DbSet<RegistroPagoSalario> RegistroPagoSalario { get; set; }
        public virtual DbSet<SalarioPersonaPermanente> SalarioPersonaPermanente { get; set; }
        public virtual DbSet<SalarioPersonaTemporal> SalarioPersonaTemporal { get; set; }
        public virtual DbSet<TipoArbol> TipoArbol { get; set; }
        public virtual DbSet<TipoContratoPersona> TipoContratoPersona { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoInsumo> TipoInsumo { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
    
        public virtual ObjectResult<gestionTipoArboles_Result> gestionTipoArboles(string nombreArbol, string descripcion, Nullable<int> idTipoArbol, Nullable<int> opc)
        {
            var nombreArbolParameter = nombreArbol != null ?
                new ObjectParameter("NombreArbol", nombreArbol) :
                new ObjectParameter("NombreArbol", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var idTipoArbolParameter = idTipoArbol.HasValue ?
                new ObjectParameter("idTipoArbol", idTipoArbol) :
                new ObjectParameter("idTipoArbol", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<gestionTipoArboles_Result>("gestionTipoArboles", nombreArbolParameter, descripcionParameter, idTipoArbolParameter, opcParameter);
        }
    
        public virtual ObjectResult<gestionConcepto_Result> gestionConcepto(string nombreConcepto, string descripcion, Nullable<int> idConcepto, Nullable<int> opc)
        {
            var nombreConceptoParameter = nombreConcepto != null ?
                new ObjectParameter("NombreConcepto", nombreConcepto) :
                new ObjectParameter("NombreConcepto", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var idConceptoParameter = idConcepto.HasValue ?
                new ObjectParameter("idConcepto", idConcepto) :
                new ObjectParameter("idConcepto", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<gestionConcepto_Result>("gestionConcepto", nombreConceptoParameter, descripcionParameter, idConceptoParameter, opcParameter);
        }
    
        public virtual ObjectResult<gestionTipoInsumo_Result> gestionTipoInsumo(string nombreTipoInsumo, string descripcion, Nullable<int> idTipoInsumo, Nullable<int> opc)
        {
            var nombreTipoInsumoParameter = nombreTipoInsumo != null ?
                new ObjectParameter("NombreTipoInsumo", nombreTipoInsumo) :
                new ObjectParameter("NombreTipoInsumo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var idTipoInsumoParameter = idTipoInsumo.HasValue ?
                new ObjectParameter("idTipoInsumo", idTipoInsumo) :
                new ObjectParameter("idTipoInsumo", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<gestionTipoInsumo_Result>("gestionTipoInsumo", nombreTipoInsumoParameter, descripcionParameter, idTipoInsumoParameter, opcParameter);
        }
    
        public virtual ObjectResult<gestionProducto_Result> gestionProducto(string nombreProducto, string descripcion, Nullable<int> idProducto, Nullable<int> opc)
        {
            var nombreProductoParameter = nombreProducto != null ?
                new ObjectParameter("NombreProducto", nombreProducto) :
                new ObjectParameter("NombreProducto", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var idProductoParameter = idProducto.HasValue ?
                new ObjectParameter("idProducto", idProducto) :
                new ObjectParameter("idProducto", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<gestionProducto_Result>("gestionProducto", nombreProductoParameter, descripcionParameter, idProductoParameter, opcParameter);
        }
    
        public virtual ObjectResult<gestionInsumo_Result> gestionInsumo(Nullable<byte> idTipoInsumo, string nombreInsumo, string descripcion, string marca, string unidadMedida, Nullable<int> idInsumo, Nullable<int> opc)
        {
            var idTipoInsumoParameter = idTipoInsumo.HasValue ?
                new ObjectParameter("idTipoInsumo", idTipoInsumo) :
                new ObjectParameter("idTipoInsumo", typeof(byte));
    
            var nombreInsumoParameter = nombreInsumo != null ?
                new ObjectParameter("nombreInsumo", nombreInsumo) :
                new ObjectParameter("nombreInsumo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("descripcion", descripcion) :
                new ObjectParameter("descripcion", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("marca", marca) :
                new ObjectParameter("marca", typeof(string));
    
            var unidadMedidaParameter = unidadMedida != null ?
                new ObjectParameter("unidadMedida", unidadMedida) :
                new ObjectParameter("unidadMedida", typeof(string));
    
            var idInsumoParameter = idInsumo.HasValue ?
                new ObjectParameter("idInsumo", idInsumo) :
                new ObjectParameter("idInsumo", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<gestionInsumo_Result>("gestionInsumo", idTipoInsumoParameter, nombreInsumoParameter, descripcionParameter, marcaParameter, unidadMedidaParameter, idInsumoParameter, opcParameter);
        }
    
        public virtual ObjectResult<string> gestionLabor(string tipoPagoLabor, string nombreLabor, Nullable<bool> modificaArbol, Nullable<bool> requiereInsumo, string descripcion, Nullable<int> idLabor, Nullable<int> opc)
        {
            var tipoPagoLaborParameter = tipoPagoLabor != null ?
                new ObjectParameter("TipoPagoLabor", tipoPagoLabor) :
                new ObjectParameter("TipoPagoLabor", typeof(string));
    
            var nombreLaborParameter = nombreLabor != null ?
                new ObjectParameter("nombreLabor", nombreLabor) :
                new ObjectParameter("nombreLabor", typeof(string));
    
            var modificaArbolParameter = modificaArbol.HasValue ?
                new ObjectParameter("modificaArbol", modificaArbol) :
                new ObjectParameter("modificaArbol", typeof(bool));
    
            var requiereInsumoParameter = requiereInsumo.HasValue ?
                new ObjectParameter("requiereInsumo", requiereInsumo) :
                new ObjectParameter("requiereInsumo", typeof(bool));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("descripcion", descripcion) :
                new ObjectParameter("descripcion", typeof(string));
    
            var idLaborParameter = idLabor.HasValue ?
                new ObjectParameter("idLabor", idLabor) :
                new ObjectParameter("idLabor", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("gestionLabor", tipoPagoLaborParameter, nombreLaborParameter, modificaArbolParameter, requiereInsumoParameter, descripcionParameter, idLaborParameter, opcParameter);
        }
    
        public virtual ObjectResult<string> gestionPersona(string nombrePersona, string genero, string telefono, Nullable<System.DateTime> fechaNacimiento, Nullable<int> documentoPerosna, Nullable<int> opc, Nullable<byte> idTipoDocumento, Nullable<byte> idTipoContrato)
        {
            var nombrePersonaParameter = nombrePersona != null ?
                new ObjectParameter("nombrePersona", nombrePersona) :
                new ObjectParameter("nombrePersona", typeof(string));
    
            var generoParameter = genero != null ?
                new ObjectParameter("genero", genero) :
                new ObjectParameter("genero", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var fechaNacimientoParameter = fechaNacimiento.HasValue ?
                new ObjectParameter("fechaNacimiento", fechaNacimiento) :
                new ObjectParameter("fechaNacimiento", typeof(System.DateTime));
    
            var documentoPerosnaParameter = documentoPerosna.HasValue ?
                new ObjectParameter("documentoPerosna", documentoPerosna) :
                new ObjectParameter("documentoPerosna", typeof(int));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            var idTipoDocumentoParameter = idTipoDocumento.HasValue ?
                new ObjectParameter("idTipoDocumento", idTipoDocumento) :
                new ObjectParameter("idTipoDocumento", typeof(byte));
    
            var idTipoContratoParameter = idTipoContrato.HasValue ?
                new ObjectParameter("idTipoContrato", idTipoContrato) :
                new ObjectParameter("idTipoContrato", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("gestionPersona", nombrePersonaParameter, generoParameter, telefonoParameter, fechaNacimientoParameter, documentoPerosnaParameter, opcParameter, idTipoDocumentoParameter, idTipoContratoParameter);
        }
    
        public virtual ObjectResult<string> gestionProveedor(string nit, string nombreProveedor, string telefono, string direccion, string tipoDocumento, Nullable<int> opc)
        {
            var nitParameter = nit != null ?
                new ObjectParameter("nit", nit) :
                new ObjectParameter("nit", typeof(string));
    
            var nombreProveedorParameter = nombreProveedor != null ?
                new ObjectParameter("nombreProveedor", nombreProveedor) :
                new ObjectParameter("nombreProveedor", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var direccionParameter = direccion != null ?
                new ObjectParameter("direccion", direccion) :
                new ObjectParameter("direccion", typeof(string));
    
            var tipoDocumentoParameter = tipoDocumento != null ?
                new ObjectParameter("TipoDocumento", tipoDocumento) :
                new ObjectParameter("TipoDocumento", typeof(string));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("gestionProveedor", nitParameter, nombreProveedorParameter, telefonoParameter, direccionParameter, tipoDocumentoParameter, opcParameter);
        }
    }
}

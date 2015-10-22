create database DBFinca

go 

use DBFinca

go


create table Departamento(
idDepartamento tinyint constraint PK_Departamento primary key not null,
NombreDepartamento varchar(25) not null
)

create table Municipio(
idMunicipio smallint constraint PK_Municipio primary key not null,
idDepartamento tinyint constraint FK_Municipio_Departamento foreign key references Departamento(idDepartamento) not null,
NombreMunicipio varchar(50) not null
)

create table Finca(
idFinca tinyint constraint PK_Finca primary key identity not null,
NombreFinca varchar(25) not null,
Propietario varchar (45) not null,
idDepartamento tinyint constraint FK_Finca_Departamento foreign key references Departamento(idDepartamento) not null,
idMunicipio smallint constraint FK_Finca_Municipio foreign key references Municipio(idMunicipio) not null,
Vereda varchar(50) not null,
Telefono varchar(10) not null,
Hectareas Varchar(5) not null
)

create table Lote(
idLote smallint constraint PK_Lote primary key identity not null,
idFinca tinyint constraint FK_Lote_Finca foreign key  references Finca(idFinca) not null,
NombreLote Varchar(25) not null,
Observaciones varchar(100),
EstadoLote char(1) not null constraint DF_Lote default ('A')
)

create table TipoArbol(
idTipoArbol tinyint primary key identity not null,
NombreTipoArbol varchar(45) not null,
Descripcion varchar(45) not null,
EstadoTipoArbol char(1) not null constraint DF_TipoArbol default ('A')
)

create table Arboles(
idArboles int constraint PK_Arboles primary  key identity not null,
idLote smallint constraint FK_Arboles_Lote foreign key references Lote(idLote) not null,
idTIpoArbol tinyint constraint FK_Arboles_TipoArbol foreign key references TipoArbol(idTipoArbol) not null,
Cantidad int not null
)

create table MovimientosArboles(
idMovimientosArboles int identity primary  key,
idArboles int constraint FK_MovimientosArboles_Arboles foreign key references Arboles(idArboles) not null,
Fecha date not null,
Cantidad int not null
)

create table Produccion(
idProduccion int constraint PK_Produccion primary key identity not null,
idLote smallint constraint FK_Produccion_Lote foreign key references Lote(idLote) not null,
Fecha date not null,
Cantidad decimal,
EstadoProduccion char(2) not null constraint DF_Produccion default ('NV')
)

create table Producto(
idProducto tinyint constraint PK_Producto primary key identity not null,
NombreProducto varchar(20) not null,
Descripcion varchar (70), 
EstadoProducto char(1) not null constraint DF_Producto default ('A')
)

create table Venta(
idVenta int constraint PK_Venta primary key identity not null,
idProducto tinyint constraint FK_Venta_Producto foreign key references Producto (idProducto) not null,
Fecha date not null,
PrecioCarga money not null,
CantidadCargas decimal not null
)

create table MovimientoProduccion(
id int identity primary  key,
idProduccion int constraint FK_MovimientoProduccion_Produccion foreign key references Produccion(idProduccion) not null,
idVenta int constraint FK_MovimientoProduccion_Venta foreign key references Venta(idVenta) not null,
Cantidad decimal not null
)


create table Labor(
idLabor smallint constraint PK_Labor primary key identity not null,
NombreLabor varchar (25) not null,
ModificaArboles bit not null,
TipoPagoLabor varchar (20) not null,
RequiereInsumo bit not null,
Descripcion varchar(60),
EstadoLabor char(1) not null constraint DF_Labor default ('A')
)

create table Labor_Lote(
idLabor_Lote int constraint PK_LaborLote primary key identity not null,
idLabor smallint constraint FK_LaborLote_Labor foreign key references Labor (idLabor) not null,
idLote smallint constraint FK_LaborLote_Lote foreign key references Lote (idLote) not null,
Fecha date not null
)

create table TipoInsumo(
idTipoInsumo tinyint constraint PK_TipoInsumo primary key identity not null,
NombreTipoInsumo varchar(45) not null,
Descripcion varchar(45) not null,
EstadoTipoInsumo char(1) not null constraint DF_TipoInsumo default ('A')
)

create table Insumo(
idInsumo smallint constraint PK_Insumo primary key identity not null,
idTipoInsumo tinyint constraint FK_Insumo_TipoInsumo foreign key references TipoInsumo(idTipoInsumo),
NombreInsumo varchar(45) not null,
Descripcion varchar(150) not null,
CantidadExistente decimal not null constraint DF_Insumo default 0,
Marca varchar(45) not null, 
UnidadMedida varchar(45) not null,
EstadoInsumo char(1) not null constraint DF_InsumoDesac default ('A')
)

create table LaborLote_Insumo(
id int identity primary  key,
idLabor_Lote int constraint FK_LaborLoteInsumo_LaborLote foreign key references Labor_Lote (idLabor_Lote) not null,
idInsumo smallint constraint FK_LaborLoteInsumo_Insumo foreign key references Insumo(idInsumo) not null,
Cantidad decimal not null,
Precio money not null
)

create table Proveedor(
Nit varchar (20) constraint PK_Proveedor primary key not null,
TipoDocumento varchar (20) not null,
NombreProveedor varchar (45) not null,
Telefono varchar (10) not null,
Direccion varchar (45) not null,
EstadoProveedor char(1) not null constraint DF_Proveedor default ('A')
)

create table Compra(
idCompra int constraint PK_Compra primary key identity not null,
NitProveedor varchar(20) constraint FK_Compra_Proveedor foreign key references Proveedor(Nit) not null,
Fecha date not null,
NumeroFactura int ,
EstadoCuenta char(1) not null constraint DF_Compra default ('D')
)

create table AbonoCompra(
idAbonoCompra int identity primary  key,
idCompra int constraint FK_AbonoCompra_Compra foreign key references Compra (idCompra) not null,
Valor money not null,
Fecha date not null
)

create table Compra_Insumo(
idCompraInsumo int identity primary  key,
idCompra int constraint FK_CompraInsumo_Compra foreign key references Compra (idCompra) not null,
idInsumo smallint constraint FK_AbonoCompra_Insumo foreign key references Insumo (idInsumo) not null,
Cantidad int not null,
Precio money not null
)

create table Concepto(
idConcepto smallint constraint PK_Concepto primary key identity not null,
NombreConcepto varchar (45) not null,
Descripcion varchar (45) not null,
EstadoConcepto char(1) not null constraint DF_Concepto default ('A')
)

create table Egreso (
idEgreso int constraint PK_Egreso primary key identity not null,
idConcepto smallint constraint FK_Egreso_Concepto foreign key references Concepto(idConcepto) not null,
Descripcion varchar (150),
Fecha date not null,
Valor money  not null,
EstadoCuenta char(1) not null constraint DF_Egreso default ('D')
)

create table AbonoEgreso(
idAbonoEgreso int identity primary  key,
idEgreso int constraint FK_AbonoEgreso_Egreso foreign key references Egreso (idEgreso) not null,
Fecha date not null,
Valor money not null
)


create table TipoContratoPersona(
idTipoContratoPersona tinyint constraint PK_TipoContratoPersona primary key identity not null,
NombreTipoContratoPersona varchar(60) not null 
)

create table TipoDocumento(
idTipoDocumento tinyint constraint PK_TipoDocumento primary key identity not null,
NombreTipoDocumento varchar(60) not null
)

create table Persona (
DocumentoPersona varchar (15) not null constraint PK_Persona primary key,
idTipoDocumento tinyint constraint FK_Persona_TipoDocumento foreign key references TipoDocumento (idTipoDocumento) not null,
idTipoContratoPersona tinyint constraint FK_Persona_TipoContratoPersona foreign key references TipoContratoPersona (idTipoContratoPersona) not null,
NombrePersona varchar(50) not null,
Genero char (1) not null,
Telefono varchar (10) not null,
FechaNacimineto date not null,
EstadoPersona char(1) not null constraint DF_Persona default ('A')
)

create table SalarioPersonaTemporal(
idSalarioPersonaTemporal int constraint PK_SalarioPersonaTemporal primary key identity not null,
DocumentoPersona varchar (15) constraint FK_SalarioPersonaTemporal_Persona foreign key references Persona(DocumentoPersona) not null,
idLabor_Lote int constraint FK_SalarioPersonaTemporal_LaborLote foreign key references Labor_Lote(idLabor_Lote) not null,
Cantidad int not null,
Valor money not null,
EstadoCuenta char(1) constraint DF_SalarioPersonaTemporal default ('D') not null
)

create table RegistroPago(
idRegistroPago int constraint PK_RegistroPago primary key not null,
Fecha date not null,
valor money not null
)

create table SalarioPersonaPermanente(
idSalarioPersonaPermanente int constraint PK_SalarioPersonaPermanente primary key identity not null,
DocumentoPersona varchar (15) constraint FK_SalarioPersonaPermanente_Persona foreign key references Persona(DocumentoPersona) not null,
Valor money not null
)

create table RegistroPagoSalario(
idRegistroPagoSalario int identity primary  key,
idRegistroPago int constraint FK_RegistroPagoSalario_RegistroPago foreign key references RegistroPago(idRegistroPago) not null,
idSalarioPersonaTemporal int constraint FK_RegistroPagoSalario_idSalarioPersonaTemporal foreign key references SalarioPersonaTemporal (idSalarioPersonaTemporal),
idSalarioPersonaPermanente int constraint FK_RegistroPagoSalario_SalarioPersonaPermanente foreign key references SalarioPersonaPermanente (idSalarioPersonaPermanente)
)

create table DeudaPersona(
idDeudaPersona int constraint PK_DeudaPersona primary key identity not null,
DocumentoPersona varchar (15) constraint FK_DeudaPersona_Persona foreign key references Persona (DocumentoPersona) not null,
Valor money not null,
Fecha date not null,
EstadoCuenta char (1) not null constraint DF_DeudaPersona default ('D')
)

create table AbonoDeuda(
idAbonoDeuda int identity primary  key,
idDeudaPersona int constraint FK_AbonoDeuda_DeudaPersona foreign key references DeudaPersona(idDeudaPersona) not null,
Valor money not null,
Fecha date not null
)

create table RegistroPago_DeudaPersona(
idRegistroPago_DeudaPersona int identity primary  key,
idRegistroPago int constraint FK_RegistroPagoDeudaPersona_RegistroPago foreign key references RegistroPago(idRegistroPago) not null,
idDeudaPersona int constraint FK_RegistroPagoDeudaPersona_DeudaPersona foreign key references DeudaPersona(idDeudaPersona) not null
)


create table Usuario(
idUsuario smallint constraint PK_Usuario primary key identity not null,
Nickname varchar(15) not null,
Rol varchar(15) not null,
Imagen image,
Contraseña varchar(12) not null,
UltimoAcceso datetime not null,
PreguntaSeguridad varchar(70) not null,
Respuesta varchar (30) not null,
EstadoUsuario char(1) not null constraint DF_Usuario default ('A')
)



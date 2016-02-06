
alter table DeudaPersona
add Descripcion varchar(150)

alter table Lote
add Cuadras varchar (15)

alter table Insumo
add PrecioPromedio int

alter table MovimientosArboles
add TipoMovimiento varchar(7)

alter table Labor
drop column TipoPagoLabor


create table CostoBeneficio(
idCostoBeneficio int identity constraint PK_CostoBeneficio primary key,
idVenta int constraint FK_CostoBeneficio_Venta foreign key references Venta(idVenta),
idCompra int constraint FK_CostoBeneficio_Compra foreign key references Compra(idCompra),
Precio money,
EstadoCuenta char constraint DF_CostoBeneeficio default 'D' )



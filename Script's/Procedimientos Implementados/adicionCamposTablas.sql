
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

 create trigger CantidadArboles
  on MovimientosArboles
  for insert
  as
   declare @idArboles int
   declare @cantidad int
   declare @idMovimiento int
   
   set @idArboles = (select top 1 idArboles from MovimientosArboles ORDER BY idMovimientosArboles DESC) 
   set @idMovimiento = (select top 1 idMovimientosArboles from MovimientosArboles ORDER BY idMovimientosArboles DESC) 
   set @cantidad = (select Cantidad from MovimientosArboles where idMovimientosArboles =  @idMovimiento)

   update Arboles set Cantidad = (Cantidad + @cantidad) where idArboles = @idArboles
    
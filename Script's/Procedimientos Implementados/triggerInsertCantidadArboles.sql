 create trigger InsertCantidadArboles
  on MovimientosArboles
  for insert
  as
   declare @idArboles int

   set @idArboles = (select top 1 idArboles from MovimientosArboles ORDER BY idMovimientosArboles DESC) 
   update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles
    
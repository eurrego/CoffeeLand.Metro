create proc gestionArboles
(@idLote smallint ,@idTipoArbol tinyint, @cantidad int, @fecha datetime, @idMovimiento int, @opcion int)

as
begin

	set nocount on
	declare @mensaje varchar(50)

	declare @idArboles int
	

	if( @opcion = 1 ) 
	begin
	       
		if ((select count(idLote) from Arboles where idLote =  @idLote ) = 0)
		 begin
		       insert into Arboles(idLote,idTIpoArbol,Cantidad)
				      values (@idLote,@idTipoArbol,'0')
		 end
		else
		 begin
		    if ((select count(idTIpoArbol) from Arboles where idTIpoArbol =  @idTipoArbol and idLote = @idLote) = 0)
				begin
					insert into Arboles(idLote,idTIpoArbol,Cantidad)
					values (@idLote,@idTipoArbol,'0')	
			end
		 end

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)

			insert into MovimientosArboles (idArboles, Fecha, Cantidad)
			values (@idArboles, @fecha,  @cantidad)

			update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles

		 set @mensaje = 'Registro exitoso!'
	end
	else if (@opcion = 2)
	begin 
			update MovimientosArboles
			set
			Fecha = @fecha,
			Cantidad = @cantidad
			where idMovimientosArboles = @idMovimiento

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)
			update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles

			set @mensaje = 'Actualización exitosa!'
	end
	else if (@opcion = 3)
	begin
	      delete MovimientosArboles 
		    where idMovimientosArboles = @idMovimiento

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)
			update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles

			set @mensaje = 'Eliminación exitosa!'
	end


		select @mensaje as Mensaje
end


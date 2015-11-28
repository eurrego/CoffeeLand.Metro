create proc gestionArboles
(@idLote smallint ,@idTipoArbol tinyint, @cantidad int, @fecha datetime)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	
	
			if ((select count(idTIpoArbol) from Arboles where idTIpoArbol = @idTipoArbol ) = 0)
				begin
					insert into Arboles(idLote,idTIpoArbol,Cantidad)
					values (@idLote,@idTipoArbol,'0')
			end
			declare @idArboles int
			
			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol)
					insert into MovimientosArboles (idArboles, Fecha, Cantidad)
					values (@idArboles, @fecha,  @cantidad)

			set @mensaje = 'Registro exitoso!'
	

		select @mensaje as Mensaje
end

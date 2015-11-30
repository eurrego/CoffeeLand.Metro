create proc gestionArboles
(@idLote smallint ,@idTipoArbol tinyint, @cantidad int, @fecha datetime)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	       
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
		
	     declare @idArboles int
		 set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)
					insert into MovimientosArboles (idArboles, Fecha, Cantidad)
					values (@idArboles, @fecha,  @cantidad)

	    set @mensaje = 'Registro exitoso!'
		select @mensaje as Mensaje
end


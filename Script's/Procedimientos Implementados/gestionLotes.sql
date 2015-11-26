create proc gestionLotes
(@nombreLote varchar(25), @observaciones varchar(100),@cuadras varchar(15) , @idLote int, @opc int)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	
	if (@opc = 1)
		begin
			if ((select count(NombreLote) from Lote where NombreLote = @nombreLote) = 0)
				begin
					declare @idFinca tinyint = (select idFinca from Finca )
					insert into Lote (idFinca,NombreLote,Observaciones,Cuadras)
					values (@idFinca,@nombreLote,@observaciones,@cuadras)
					set @mensaje = 'Registro exitoso'
				end
				else
					begin
						set @mensaje = 'Existe un lote con este nombre'
					end
		end

    else if (@opc = 2)
		begin
		
			update Lote
			set
			NombreLote = @nombreLote,
			Observaciones = @observaciones,
			Cuadras = @cuadras
			where idLote= @idLote

			---- hace falta el idLOte desde otro procedimiento

			set @mensaje = 'Actualización exitosa'
		end

    else if (@opc = 3)
		begin
		
			update Lote
			set
			EstadoLote = 'i'
			where idLote= @idLote

			set @mensaje = 'Inhabilitación exitosa'
		end

		select @mensaje as Mensaje
end
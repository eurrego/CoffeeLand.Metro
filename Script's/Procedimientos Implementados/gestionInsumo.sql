--------------------------------------------------------------INSUMOS------------

 create proc gestionInsumo 
 (@idTipoInsumo tinyint, @nombreInsumo varchar(45), 
 @descripcion varchar(150), @marca varchar(45),
 @unidadMedida varchar(45),@idInsumo int,@opc int)

 as
 begin
	 set nocount on
	 declare @mensaje varchar(50)
	 if(@opc = 1)
		begin
			if ((select count(nombreInsumo) from Insumo where NombreInsumo = @nombreInsumo) = 0)
				 begin
					 insert into Insumo (idTipoInsumo, NombreInsumo,Descripcion,Marca,UnidadMedida)
					 values (@idTipoInsumo,@nombreInsumo,@descripcion,@marca,@unidadMedida)
					 set @mensaje = 'Registro exitoso!'
				 end

			else
				begin
					set @mensaje = 'Existe un Insumo con este nombre'
				end
		end

	  else if(@opc = 2)

			 begin
				 update Insumo
				 set
				 idTipoInsumo = @idTipoInsumo,
				 NombreInsumo = @nombreInsumo,
				 Descripcion = @descripcion,
				 Marca = @marca,
				 UnidadMedida = UnidadMedida
				 where idInsumo = @idInsumo
				 set @mensaje = 'Actualización exitosa!'
			end

	 else if (@opc = 3)
			begin
				update Insumo
				set
				EstadoInsumo = 'I'
				where idinsumo= @idInsumo

				set @mensaje = 'Inhabilitación exitosa!'
		end

			select @mensaje  as Mensaje
 end
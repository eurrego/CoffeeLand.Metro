-------------------------------------------LABOR----------------------------------

create proc gestionLabor
 ( @nombreLabor varchar(25),@modificaArbol bit,@requiereInsumo bit,  
 @descripcion varchar(150),@idLabor int,@opc int)

 as
 begin
	 set nocount on
	 declare @mensaje varchar(50)
	 if(@opc = 1)
		begin
			if ((select count(NombreLabor) from Labor where NombreLabor = @nombreLabor) = 0)
				 begin
					 insert into Labor(NombreLabor,ModificaArboles,RequiereInsumo,Descripcion)
					 values (@nombreLabor,@modificaArbol,@requiereInsumo,@descripcion)
					 set @mensaje = 'Registro exitoso!'
				 end

			else
				begin
					set @mensaje = 'Existe una labor con este nombre'
				end
		end

	  else if(@opc = 2)

			 begin
				 update Labor
				 set
				 NombreLabor = @nombreLabor,
				 ModificaArboles = @modificaArbol,
				 RequiereInsumo=@requiereInsumo,
				 Descripcion = @descripcion
				 where idLabor = @idLabor
				 set @mensaje = 'Actualización exitosa!'
			end

	 else if (@opc = 3)
			begin
				update Labor
				set
				EstadoLabor = 'I'
				where idLabor= @idLabor

				set @mensaje = 'Inhabilitación exitosa'
		end

			select @mensaje  as Mensaje
 end
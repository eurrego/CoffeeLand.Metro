create procedure GestionUsuario(
@idUsuario int,
@nickName varchar(15),
@rol varchar(15),
@contrasena varchar(40),
@preguntaSeguridad varchar(70),
@respuesta varchar(30),
@opc int)

as

begin

declare @Mensaje varchar(50) 

	if(@opc = 1)
		begin	
			insert into Usuario (Nickname,Rol,Contrasena,PreguntaSeguridad,Respuesta,EstadoUsuario)
			values (@nickName,@rol,@contrasena,@preguntaSeguridad,@respuesta,'A')

			set @Mensaje = 'Registro exitoso'
		end

	else if(@opc = 2)

		begin
			update Usuario
			set
			Nickname = @nickName,
			Rol = @rol,
			Contrasena = @contrasena,
			PreguntaSeguridad = @preguntaSeguridad,
			Respuesta = @respuesta
			where idUsuario =@idUsuario

			set @Mensaje = 'Actualización exitosa'
		end

	else if (@opc = 3)
		begin
			
			update Usuario
			set
			EstadoUsuario = 'I'
			where idUsuario = @idUsuario
			set @Mensaje = 'Inhabilitación exitosa'
		end
	else if (@opc = 4)
		begin
			
			update Usuario
			set
			EstadoUsuario = 'A'
			where idUsuario = @idUsuario

			set @Mensaje = 'Habilitación exitosa'
		end


	else if(@opc = 5)
		begin

		update Usuario 
		set 
		Contrasena = @contrasena
		where idUsuario = @idUsuario

		set @Mensaje = 'La contraseña se ah cambiado correctamente'


		end

		select @Mensaje as Mensaje

end
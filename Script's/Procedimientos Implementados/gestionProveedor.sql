create proc gestionProveedor
 (@nit varchar(20),@nombreProveedor varchar(45), @telefono varchar(10),@direccion varchar(45),@TipoDocumento varchar (20),@opc int)

 as
 begin
     set nocount on
	 declare @mensaje varchar(50)

 if(@opc=1)
	begin	
		if ((select COUNT(Nit) from Proveedor where Nit = @nit) = 0)
			begin
				if ((select count(NombreProveedor) from Proveedor where NombreProveedor = @nombreProveedor) = 0)
					 begin
						 insert into Proveedor (Nit,NombreProveedor,Telefono,Direccion,TipoDocumento)
						 values (@nit,@nombreProveedor,@telefono,@direccion,@TipoDocumento)
						 set @mensaje = 'Registro exitoso!'
					 end

				else
					begin
						set @mensaje = 'Existe un proveedor con este nombre'
					end
			end
			else
				begin
					set @mensaje = 'Existe un proveedor con este nit'
				end

	end

 else if(@opc=2)

		begin
			update Proveedor
			set

			NombreProveedor=@nombreProveedor,
			Telefono=@telefono,
			Direccion=@direccion,
			TipoDocumento = @TipoDocumento
			where Nit = @nit 
			set @mensaje = 'Actualización exitosa!'
		end

  else if (@opc = 3)
		
		begin
			update Proveedor
			set
			EstadoProveedor = 'I'
			where Nit= @nit

			set @mensaje = 'Inhabilitación exitosa!'
		end
		select @mensaje  as Mensaje
 end

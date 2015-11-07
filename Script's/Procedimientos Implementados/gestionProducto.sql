--------------------------------PRODUCTO---------------------
create proc gestionProducto
(@NombreProducto varchar (45),@Descripcion varchar(45),@idProducto int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreProducto) from Producto where NombreProducto = @NombreProducto) = 0)
				begin
					insert into Producto(NombreProducto,Descripcion)
					values (@NombreProducto,@Descripcion)
					set @mensaje = 'Registro exitoso'
				end

			 else
				begin
					set @mensaje = 'Existe un producto con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			update Producto
			set
			NombreProducto = @NombreProducto,
			Descripcion = @Descripcion
			where idProducto = @idProducto
			set @mensaje = 'Actualización exitosa!'
		end

	else if (@opc = 3)
		
		begin
			update Producto
			set
			EstadoProducto = 'I'
			where idProducto= @idProducto

			set @mensaje = 'Inhabilitación exitosa!'
		end
	select @mensaje  as Mensaje

end
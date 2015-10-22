use DBFinca;
go
--------------------------------GESTION TIPO ARBOLES---------------------
create proc gestionTipoArboles
(@NombreArbol varchar (45),@Descripcion varchar(45),@idTipoArbol int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreTipoArbol) from TipoArbol where NombreTipoArbol = @NombreArbol) = 0)
				begin
					insert into TipoArbol(NombreTipoArbol,Descripcion)
					values (@NombreArbol,@Descripcion)
					set @mensaje = 'Registro exitoso'
				end

			 else
				begin
					set @mensaje = 'Existe un tipo de arbol con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			update TipoArbol
			set
			NombreTipoArbol = @NombreArbol,
			Descripcion = @Descripcion
			where idTipoArbol = @idTipoArbol
			set @mensaje = 'Actualizacion exitosa'
		end

	else if (@opc = 3)
		
		begin
			update TipoArbol
			set
			EstadoTipoArbol = 'i'
			where idTipoArbol= @idTipoArbol

			set @mensaje = 'inhabilitacion exitosa'
		end
	select @mensaje  as Mensaje

end

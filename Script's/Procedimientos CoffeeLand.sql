
create proc gestionFinca
(@nom varchar(25) ,@propie varchar(45),@depart int,@municipio int,
@vereda varchar(15),@telefono varchar(10),@hect varchar(15))

as
begin

	set nocount on
	declare @mensaje varchar(50)
	if((select count(idFinca) from Finca) = 1)
		begin
			update Finca
			set
			NombreFinca=@nom,
			Propietario=@propie,
			idDepartamento=@depart,
			idMunicipio=@municipio,
			Vereda=@vereda,
			Telefono=@telefono,
			Hectareas=@hect

			set @mensaje = 'Actualizacion exitosa'

		 end
	else
		begin 
			insert into Finca (NombreFinca,Propietario,idDepartamento,idMunicipio,Vereda,Telefono,Hectareas)
						values(@nom,@propie,@depart,@municipio,@vereda,@telefono,@hect)
			set @mensaje = 'Registro exitoso'
		end

		select @mensaje 
end



---------------------------------- LOTES----------------------------

create proc gestionLotes
(@nombreLote varchar(25), @observacones varchar(100),@idLote int, @opc int)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	
	if (@opc = 1)
		begin
			if ((select count(NombreLote) from Lote where NombreLote = @nombreLote) = 0)
				begin
					declare @idFinca tinyint = (select idFinca from Finca )
					insert into Lote (idFinca,NombreLote,Observaciones)
					values (@idFinca,@nombreLote,@observacones)
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
			Observaciones = @observacones
			where idLote= @idLote

			---- hace falta el idLOte desde otro procedimiento

			set @mensaje = 'Actualizacion exitosa'
		end

    else if (@opc = 3)
		begin
		
			update Lote
			set
			DesactivarLote = 'i'
			where idLote= @idLote

			set @mensaje = 'inhabilitacion exitosa'
		end

		select @mensaje 
end


---------------------------------------------Concepto---------------------------------------------------

create proc gestionConcepto
(@NombreConcepto varchar (45),@Descripcion varchar(45),@idConcepto int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreConcepto) from Concepto where NombreConcepto = @NombreConcepto) = 0)
				begin
					insert into Concepto (NombreConcepto,Descripcion)
					values (@NombreConcepto,@Descripcion)
					set @mensaje = 'Registro exitoso'
				end

			 else
				begin
					set @mensaje = 'Existe un concepto con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			update Concepto
			set
			NombreConcepto = @NombreConcepto,
			Descripcion = @Descripcion
			where idConcepto = @idConcepto
			set @mensaje = 'Actualizacion exitosa'
		end

	else if (@opc=3)   
		
		begin
			update Concepto
			set
			EstadoConcepto = 'I'
			where idConcepto= @idConcepto

			set @mensaje = 'inhabilitacion exitosa'
		end
	select @mensaje

end


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
			set @mensaje = 'Actualizacion exitosa'
		end

	else if (@opc = 3)
		
		begin
			update Producto
			set
			EstadoProducto = 'I'
			where idProducto= @idProducto

			set @mensaje = 'inhabilitacion exitosa'
		end
	select @mensaje

end


------------------------USUARIO-------------------------------------

create proc gestionUsuario
(@nickname varchar(15),@rol varchar(15),@imagen image, @contraseña varchar(12),
 @pregunta varchar(70),@respuesta varchar(30),@opc int)

 as 
 begin
set nocount on
declare @mensaje varchar(50)

 if(@opc = 1)
		 begin
			 insert into Usuario (Nickname,Rol,Imagen,Contraseña,PreguntaSeguridad,Respuesta)
			 values(@nickname,@rol,@imagen,@contraseña,@pregunta,@respuesta)
			 set @mensaje = 'Registro exitoso'
		 end

 if(@opc = 2)

		 begin
			 update Usuario
			 set
			 Nickname = @nickname,
			 Rol = @rol,
			 Imagen = @imagen,
			 Contraseña = @contraseña,
			 PreguntaSeguridad = @pregunta,
			 Respuesta = @respuesta

			 set @mensaje = 'Actualizacion exitosa'
		 end

		 select @mensaje
 end


----------------------PRODUCCION-----------------------------------

create proc registrar_Produccion
(@nombreLote varchar(25),@fecha date, @cantiad decimal)

as
begin
	set nocount on
	declare @mensaje varchar(50)

	declare @idLote int = (select idLote from Lote where NombreLote = @nombreLote)

	insert into Produccion (idLote,Fecha,Cantidad)
	values (@idLote,@fecha,@cantiad)
	set @mensaje = 'Registro exitoso'

end




-------------------------------------------------------EGRESOS--------------------------------------


create proc registrarEgreso
(@nombreConcepto varchar(45), 
 @descripcion varchar(150), @fecha date, @valor money,@estadoCuenta char(1), @opc int)

as
begin

set nocount on
declare @mensaje varchar(50)
declare @idConcepto int = (select idConcepto from Concepto where NombreConcepto = @nombreConcepto) 



insert into Egreso(idConcepto, Descripcion, Fecha, Valor, EstadoCuenta)
values (@idConcepto,@descripcion,@fecha,@valor,@estadoCuenta)
	set @mensaje = 'Registro exitoso'


end

----------------------------------------COMPRA---------------------------------------

create proc registrarCompra
(@NombreProveedor varchar(45), 
 @fecha date, @numeroFactura int,@estadoCuenta char(1))

as
begin

	set nocount on
	declare @mensaje varchar(50)
	declare @nitProveedor int = (select Nit from Proveedor where NombreProveedor = @NombreProveedor) 


	insert into Compra ( NitProveedor,Fecha,NumeroFactura, EstadoCuenta)
	values (@nitProveedor,@fecha,@numeroFactura,@estadoCuenta)
	set @mensaje = 'Registro exitoso'

	select @mensaje
end

------------------------------COMPRA_INSUMO----------------------------

create proc registroDetalleCompra
(@idcompra int, @nombreInsumo varchar(45),@cantidad int, @precio money)

as
begin
	set nocount on

	declare @idInsumo int = (select idInsumo from Insumo where NombreInsumo=@nombreInsumo)

	insert into Compra_Insumo (idCompra,idInsumo,Cantidad,Precio)
	values (@idcompra,@idInsumo,@cantidad,@precio)

end


-------------------------------------------LABOR----------------------------------

create proc gestionLabor
 (@TipoPagoLabor varchar(20), @nombreLabor varchar(25), 
 @descripcion varchar(150),@idLabor int,@opc int)

 as
 begin
	 set nocount on
	 declare @mensaje varchar(50)
	 if(@opc = 1)
		begin
			if ((select count(NombreLabor) from Labor where NombreLabor = @nombreLabor) = 0)
				 begin
					 insert into Labor(TipoPagoLa	bor,NombreLabor,Descripcion)
					 values (@TipoPagoLabor,@nombreLabor,@descripcion)
					 set @mensaje = 'Registro exitoso'
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
				 TipoPagoLabor = @TipoPagoLabor,
				 NombreLabor = @nombreLabor,
				 Descripcion = @descripcion,
				 where idLabor = @idLabor
				 set @mensaje = 'Actualizacion exitosa'
			end

	 else if (@opc = 3)
			begin
				update Labor
				set
				EstadoLabor = 'i'
				where idLabor= @idLabor

				set @mensaje = 'inhabilitacion exitosa'
		end

			select @mensaje
 end

 ------------------------------LABOR_LOTE--------------------------
create proc asignarLaborLote
(@nombreLabor varchar (25),@nombreLote varchar(25),@fecha date)

as 
begin

set nocount on
declare @mensaje varchar(50)
declare @idlabor int = (select idLabor from Labor where NombreLabor = @nombreLabor)
declare @idlote int = (select idLote from Lote where NombreLote = @nombreLote)

insert into Labor_Lote (idLabor,idLote,Fecha)
values (@idlabor,@idlote,@fecha)


set @mensaje = 'Asignacion exitosa'

select @mensaje  

end


create proc asignarInsumoLabor
(@idLabor_Lote int,@nombreInsumo varchar(45),@Cantidad decimal,@precio money)

as
begin

declare @idInsumo int = (select idInsumo from Insumo where NombreInsumo = @nombreInsumo) 

insert into LaborLote_Insumo (idLabor_Lote,idInsumo,Cantidad,Precio)
			values (@idLabor_Lote,@idInsumo,@Cantidad,@precio)

end









create proc GestionVenta
(@nit int,@fecha date,@numeroFactura 
int,@idProducto int,@PrecioCarga 
money,@CantidadCargas decimal(10,2),@PrecioBeneficio money) 

as
	begin

		insert into Compra (NitProveedor,Fecha,NumeroFactura,EstadoCuenta)
				values(@nit,@fecha,@numeroFactura,'D')


	   declare @IdCompra int = (select top 1 idCompra from Compra order by idCompra desc)


	   insert into Venta (idProducto,Fecha,PrecioCarga,CantidadCargas)
				values (@idProducto,@fecha,@PrecioCarga,@CantidadCargas)

	   declare @IdVenta int = (select top 1 idVenta from Venta order by idVenta desc)

	   insert into CostoBeneficio (idCompra,idVenta,Precio,EstadoCuenta)
				values (@IdCompra,@IdVenta,@PrecioBeneficio,'D')

	end

create procedure RegistrarCompra
(@nit varchar(20),@fecha date,@numeroFactura int)
as
begin

	declare @Compra varchar(50)

	if ((select COUNT(idCompra) from Compra where NitProveedor = @nit and NumeroFactura = @numeroFactura) = 0)

		begin

			insert into Compra values (@nit,@fecha,@numeroFactura,'D')

			set @Compra = (select top 1 idCompra from Compra order by idCompra desc)

		end

	else 

		begin 
	
			set @compra = 0
	
		end

	select @Compra  as Mensaje

	

end
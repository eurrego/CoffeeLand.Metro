create procedure AbonoCompraProveedor(@idCompra int,@valor money,@fecha datetime,@Total money)
as
begin

	if(@valor = @Total)
		begin
			insert into AbonoCompra values(@idCompra,@valor,@fecha)

			update Compra 
			set
			EstadoCuenta = 'P'
			where idCompra = @idCompra


		end
	else		
		begin		
			insert into AbonoCompra values(@idCompra,@valor,@fecha)	
		end

	select  'Registro exitoso!' as Mensaje
end
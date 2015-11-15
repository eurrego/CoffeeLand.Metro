create proc insercionAbonoDeuda
(
 @valor money, 
 @fecha date,
 @idDeuda int,
 @newValor money,
 @opc int
)
as
begin

set nocount on
declare @mensaje varchar(50)      

	if(@opc = 1) /* ingreso cuando el prestamo es igual al abono*/
	 begin
			insert into AbonoDeuda(idDeudaPersona ,Valor,Fecha)
			values (@idDeuda,@valor,@fecha)

			update DeudaPersona 
				   set
				   EstadoCuenta = 'P',
				   Valor = 0
				   WHERE idDeudaPersona = @idDeuda 

			set @mensaje = 'Prestamo de la fecha: '+ CONVERT(VARCHAR, (select fecha from DeudaPersona where idDeudaPersona = @idDeuda ), 101) +' Cancelado!';
	 end

	else if (@opc = 2) /* ingreso cuando el valor del prestamo es mayor a el abono*/
	 begin
			insert into AbonoDeuda(idDeudaPersona ,Valor,Fecha)
			values (@idDeuda,@valor,@fecha)

			update DeudaPersona 
				   set
				   Valor =  @newValor
				   WHERE idDeudaPersona = @idDeuda 

	   set @mensaje = 'Abono Exitoso'
	 end

	 else if (@opc = 3) /* Ingreso cuando el valor del abono es mayor a el prestamo*/
		begin
			
			insert into AbonoDeuda(idDeudaPersona ,Valor,Fecha)
			values (@idDeuda,@valor,@fecha)

			update DeudaPersona 
				   set
				   EstadoCuenta = 'P',
		           Valor = 0
				   WHERE idDeudaPersona = @idDeuda 

			set @mensaje = 'Abono Exitoso'
		end

select @mensaje as Mensaje

end



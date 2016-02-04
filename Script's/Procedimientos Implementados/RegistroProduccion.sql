create proc registroProduccion
(@idLote int,@fecha date, @cantidad int)
as
begin

	set nocount on
	
	
		begin

			insert into Produccion(idLote,Fecha,Cantidad)
			values (@idLote,@fecha,@cantidad)
			
		end
		
	
end
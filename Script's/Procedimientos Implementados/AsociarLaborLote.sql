create proc asociarLaborLote
(@idLabor int, @idLote int, @fecha date)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	
	
					insert into Labor_Lote (idLabor,idLote,Fecha)
					values (@idLabor,@idLote,@fecha)
					set @mensaje = (select top 1 idLabor_Lote from Labor_Lote order by idLabor_Lote desc)
			
			
		select @mensaje as mensaje
end



USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[PagosPersona]    Script Date: 08/02/2016 14:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[PagosPersona](@opcionPago int)
as
begin
	if(@opcionPago = 1)--PERSONA TEMPORAL
		begin
			
			
			select tab.DocumentoPersona,tab.Nombre,tab.Valor_a_pagar,tab.Valor_Deuda from
				(select SPT.DocumentoPersona, PER.NombrePersona as "Nombre" ,sum(SPT.Cantidad * SPT.Valor) as Valor_a_pagar, 
					(select sum(t.abonos) from (
						(select isnull(DP.Valor - SUM(AB.Valor),DP.Valor) as "abonos" from DeudaPersona DP 
							left join AbonoDeuda AB on DP.idDeudaPersona = AB.idDeudaPersona
							where DP.DocumentoPersona = SPT.DocumentoPersona and DP.EstadoCuenta='D'
							group by DP.Valor)) t) as "Valor_Deuda"
				from SalarioPersonaTemporal SPT
				join Persona PER on SPT.DocumentoPersona = PER.DocumentoPersona
				where SPT.EstadoCuenta='D'
				group by SPT.DocumentoPersona,  PER.NombrePersona ) tab
			group by tab.DocumentoPersona,tab.Nombre,tab.Valor_a_pagar,tab.Valor_Deuda

		end
	
	if(@opcionPago = 2)--PERSONA PERMANENTE
		begin		
			(select SPP.DocumentoPersona, PER.NombrePersona as "Nombre", SPP.Valor as Valor_a_pagar, sum(DP.Valor) as Valor_Deuda  from SalarioPersonaPermanente SPP
			join DeudaPersona DP on SPP.DocumentoPersona = DP.DocumentoPersona
			join Persona PER on SPP.DocumentoPersona = PER.DocumentoPersona
			where DP.EstadoCuenta='D'
			group by SPP.DocumentoPersona, PER.NombrePersona,SPP.Valor) 
		end
	
end




	
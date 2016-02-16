

create procedure DetalleSalario(@cedula int)
as
begin
	
		select LAB.NombreLabor , LBL.Fecha,SPT.Cantidad, SPT.Valor , (SPT.Cantidad * SPT.Valor) as subtotal  from SalarioPersonaTemporal SPT
		join Labor_Lote LBL on  SPT.idLabor_Lote= LBL.idLabor_Lote
		join Labor LAB on LBL.idLabor= LAB.idLabor
		where SPT.EstadoCuenta='D' and  SPT.DocumentoPersona=@cedula

end




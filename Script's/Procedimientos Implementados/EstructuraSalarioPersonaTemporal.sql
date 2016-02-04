
create TYPE SalarioPeronaTemporal AS TABLE 
(
	DocumentoPersona varchar(15), 
	idLabor_Lote int, 
	Cantidad int, 
    Valor money
   
)
GO

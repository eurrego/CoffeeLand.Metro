
create TYPE RegistrarGasto AS TABLE 
(
	idConcepto int, 
	Descripcion varchar(150), 
	Fecha date, 
    Valor money,
    EstadoCuenta char(1)
)
GO


create PROCEDURE [dbo].[SP_InsertMultiplesGastos] 
	@DTgastos RegistrarGasto READONLY
AS
    
	INSERT INTO Egreso
           (idConcepto,Descripcion,Fecha,Valor,EstadoCuenta)
        SELECT idConcepto, Descripcion,Fecha,Valor,EstadoCuenta FROM  @DTgastos;
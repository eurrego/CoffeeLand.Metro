
create PROCEDURE [dbo].[SalariosEmpleados] 
	@DTsalario SalarioPeronaTemporal READONLY
AS
    
	INSERT INTO SalarioPersonaTemporal
           (DocumentoPersona,idLabor_Lote,Cantidad,Valor)
        SELECT DocumentoPersona, idLabor_Lote,Cantidad,Valor FROM  @DTsalario;
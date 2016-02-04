USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[SalariosEmpleados]    Script Date: 4/02/2016 3:40:05 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SalariosEmpleados] 
	@DTsalario SalarioPeronaTemporal READONLY
AS
    
	INSERT INTO SalarioPersonaTemporal
           (DocumentoPersona,idLabor_Lote,Cantidad,Valor)
        SELECT DocumentoPersona, idLabor_Lote,Cantidad,Valor FROM  @DTsalario;
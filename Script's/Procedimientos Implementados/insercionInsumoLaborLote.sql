USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[InsercionInsumoLaborLote]    Script Date: 2/12/2015 4:52:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsercionInsumoLaborLote] 
	@DTInsumo InsumoLaborLote READONLY
AS
    
	INSERT INTO LaborLote_Insumo
           (idLabor_Lote,idInsumo,Cantidad,Precio)
        SELECT idLabor_Lote,idInsumo,Cantidad,Precio FROM  @DTInsumo;

		update I 
		set CantidadExistente=CantidadExistente - DTI.Cantidad FROM  Insumo as I
		inner join @DTInsumo as DTI  on i.idInsumo = DTI.idInsumo
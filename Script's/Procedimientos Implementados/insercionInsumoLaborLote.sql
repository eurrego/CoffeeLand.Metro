

create PROCEDURE [dbo].[InsercionInsumoLaborLote] 
	@DTInsumo InsumoLaborLote READONLY
AS
    
	INSERT INTO LaborLote_Insumo
           (idLabor_Lote,idInsumo,Cantidad,Precio)
        SELECT idLabor_Lote,idInsumo,Cantidad,Precio FROM  @DTInsumo;

		update I 
		set CantidadExistente=CantidadExistente - DTI.Cantidad FROM  Insumo as I
		inner join @DTInsumo as DTI  on i.idInsumo = DTI.idInsumo
		where i.idInsumo = DTI.idInsumo